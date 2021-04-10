using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyTestProjectAPIBaseLayer.Infrastructure;
using MyTestProjectDomainLayer.BaseClasses;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProjectAPIBaseLayer.Filters
{
    /// <summary>
    /// Exception Filter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// This method will be called upon occurrence of Exception on any of method of any controller.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            StringBuilder sbErrorInfo = new StringBuilder();
            ApiResponseWrapper apiError = new ApiResponseWrapper();
            string errorInControllerValue = string.Empty;
            string actionMethodValue = string.Empty;
            
            foreach (KeyValuePair<string, string> kva in context.ActionDescriptor.RouteValues)
            {
                if (kva.Key.ToLower() == "controller")
                {
                    errorInControllerValue = kva.Value;
                }
                if (kva.Key.ToLower() == "action")
                {
                    actionMethodValue = kva.Value;
                }
            }
            sbErrorInfo.AppendLine("Error Route: [Controller] : " + errorInControllerValue + " | [Action] : " + actionMethodValue);
            sbErrorInfo.AppendLine("Error Message :  " + context.Exception.Message);
            sbErrorInfo.AppendLine("Stack Trace:  " + context.Exception.StackTrace);
            string errorDetails = sbErrorInfo.ToString();

            if (context.Exception is UnauthorizedAccessException)
            {
                apiError = GenerateExceptionObject(errorDetails, SystemEnums.ResponseCodes.UnAuthorized.ToString(), true);
                context.HttpContext.Response.StatusCode = Convert.ToInt32(SystemEnums.ResponseCodes.UnAuthorized);
            }
            else
            {
                apiError = GenerateExceptionObject(errorDetails, SystemEnums.ResponseCodes.InternalServerError.ToString(), false);
                context.HttpContext.Response.StatusCode = Convert.ToInt32(SystemEnums.ResponseCodes.InternalServerError);
            }
            
            context.ExceptionHandled = true;
            if (context.HttpContext.Request.Headers.TryGetValue("LoggedInUserId", out var vUID))
            {
                errorDetails = "Logged In User (UID) : " + vUID + Environment.NewLine + errorDetails;
            }

           
            // always return a JSON result
            context.Result = new JsonResult(apiError);
            
            base.OnException(context);
        }

        /// <summary>
        /// This method will generate Exception object to be thrown to user.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private ApiResponseWrapper GenerateExceptionObject(string errorMessage, string statusCode, bool foreceLogout)
        {

            List<string> objEmptyResponse = new List<string>();
            ApiResponseWrapper apiError = new ApiResponseWrapper();
            apiError.apiResponseData = objEmptyResponse;
            apiError.apiResponseMessage = errorMessage;
            apiError.forceLogout = foreceLogout;
            apiError.apiResponseCode = statusCode;
            return apiError;
        }

       

    }


}
