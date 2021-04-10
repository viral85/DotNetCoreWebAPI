using Microsoft.OpenApi.Models;
using MyTestProjectDomainLayer.BaseClasses;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace MyTestProjectAPIBaseLayer.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

       
            
        }
    }
}