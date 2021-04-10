using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using MyTestProjectAPIBaseLayer.BaseClasses;

namespace MyTestProjectAPI.ServiceInstallers
{  /// <summary>
   /// 
   /// </summary>
    public static class InstallerExtentions
    {
        /// <summary>
        /// 
        /// </summary>
        public static void InstallServicesInAssemblies(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IInstallers).IsAssignableFrom(x)
           && !x.IsAbstract && !x.IsInterface).Select(Activator.CreateInstance).Cast<IInstallers>().ToList();

            installers.ForEach(installer => installer.InstallerService(services, configuration));
        }
    }
}