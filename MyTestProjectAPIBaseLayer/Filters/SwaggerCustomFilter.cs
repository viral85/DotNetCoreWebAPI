using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MyTestProjectDomainLayer.BaseClasses;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyTestProjectAPIBaseLayer.Filters
{
    /// <summary>
    /// Class for Swagger Custom Filter.
    /// </summary>
    public class SwaggerCustomFilter : IOperationFilter
    {
        /// <summary>
        /// Implementation of Input Header.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
   

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            
        }
    }
}
