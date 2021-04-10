using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTestProjectAPIBaseLayer.BaseClasses;
using MyTestProjectAPIBaseLayer.Filters;
using MyTestProjectDomainLayer.BaseClasses;
using MyTestProjectDomainLayer.CommonEntities.UserManagement;
using MyTestProjectDomainLayer.RequestClasses.AddRequest.UserManagement;
using MyTestProjectDomainLayer.RequestClasses.EditRequest.UserManagement;
using MyTestProjectDomainLayer.Validators.AddRequestValidators.UserManagement;
using MyTestProjectDomainLayer.Validators.EditRequestValidators.UserManagement;
using MyTestProjectRepositoryLayer.CommonRepository.UserManagement;
using MyTestProjectServiceLayer.BaseClasses;

namespace MyTestProjectAPI.ServiceInstallers
{
    /// <summary>
    /// Domain service registration
    /// </summary>
    public class DomainServicesRegistration : IInstallers
    {
        private IConfiguration configurationObject;
        /// <summary>
        /// Installer Service 
        /// </summary>
        /// <param name="configuration">configuration parameter</param>
        /// <param name="services">service parameter</param>
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            configurationObject = configuration;
            string dbConnection = configurationObject.GetValue<string>("AppConfigurationSettings:DatabaseConnection");
            services.AddSingleton(typeof(IBaseService<>), typeof(BaseService<>))
            .AddMvc(setupAction: options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<CustomValidationFilter>();
            })

               .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
               .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            if (configurationObject.GetValue<string>("AppConfigurationSettings:" + AppConfigurationSettings.const_Field_TurnOffFluentValidation) == "false")
            {
                #region User Management Validation
                services.AddSingleton<IValidator<UserInfoAddRequest>, ARVUserInfo>();
                services.AddSingleton<IValidator<UserInfoEditRequest>, ERVUserInfo>();
                #endregion
            }

            #region User Management
            services.AddTransient<IBaseRepository<UserInfo>>(s => new UserInfoRepository(dbConnection));
            #endregion


      

        }      
    }
}