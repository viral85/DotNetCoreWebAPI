using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyTestProjectDomainLayer.BaseClasses;

namespace MyTestProjectAPIBaseLayer.BaseClasses
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInstallers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="services"></param>
        void InstallerService(IServiceCollection services, IConfiguration configuration);
    }
}
