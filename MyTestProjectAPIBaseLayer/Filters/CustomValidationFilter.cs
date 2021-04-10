using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTestProjectDomainLayer.BaseClasses;

namespace MyTestProjectAPIBaseLayer.Filters
{
    public class CustomValidationFilter : IAsyncActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid == false)
            {
                var errorInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                ApiResponseWrapper objApiResponseWrapper = new ApiResponseWrapper();

                foreach (var error in errorInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        objApiResponseWrapper.apiResponseMessage = objApiResponseWrapper.apiResponseMessage + error.Key + " : " + subError + Environment.NewLine;

                    }
                }

                objApiResponseWrapper.apiResponseStatus = false;
                objApiResponseWrapper.apiResponseCode = "400";
               
                objApiResponseWrapper.forceLogout = true;
                List<String> objEmptyResponse = new List<string>();
                objApiResponseWrapper.apiResponseData = objEmptyResponse;
                //context.Result = new BadRequestObjectResult(objApiResponseWrapper.apiResponseMessage);

                //var responseObj = new
                //{
                //    Message = "Bad Request",
                //    Errors = objApiResponseWrapper.apiResponseMessage
                //};

                context.Result = new JsonResult(objApiResponseWrapper)
                {
                    StatusCode = 400
                };
                return;
            }


            await next();
        }
    }
}
