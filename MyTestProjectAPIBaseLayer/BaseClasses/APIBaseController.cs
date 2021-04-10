using Microsoft.AspNetCore.Mvc;
using MyTestProjectDomainLayer.BaseClasses;
using MyTestProjectAPIBaseLayer.Filters;
using System;
using Microsoft.AspNetCore.StaticFiles;

namespace MyTestProjectAPIBaseLayer.BaseClasses
{
    /// <summary>
    /// Base Controller for All Controller.
    /// </summary>
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/[controller]")]
    public class APIBaseController : ControllerBase
    {
       
        /// <summary>
        /// 
        /// </summary>
        public ApiResponseWrapper APIResponse { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public APIBaseController()
        {
            APIResponse = new ApiResponseWrapper();
            APIResponse.apiResponseData = null;
            APIResponse.apiResponseMessage = "Invalid API Call from Client!!!";
            APIResponse.apiResponseStatus = false;
        }

    }
}
