using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyTestProjectAPIBaseLayer.BaseClasses;
using MyTestProjectDomainLayer.BaseClasses;
using MyTestProjectDomainLayer.CommonEntities.UserManagement;
using MyTestProjectDomainLayer.RequestClasses.AddRequest.UserManagement;
using MyTestProjectDomainLayer.RequestClasses.EditRequest.UserManagement;

namespace MyTestProjectAPI.Controllers.UserManagement
{
    /// <summary>
    /// Controller for User Info.
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "UserManagement")]
    public class UserInfoAPIController : APIBaseController
    {

        /// <summary>
        /// Define Configuration settings objects
        /// </summary>
        private readonly AppConfigurationSettings objAppConfigurationSettings = new AppConfigurationSettings();

        /// <summary>
        /// private read only service injection.
        /// </summary>
        private readonly IBaseService<UserInfo> baseService;

        /// <summary>
        /// Constructure for User Info.
        /// </summary>
        /// <param name="_appSettingsSection"></param>
        /// <param name="_baseService"></param>

        public UserInfoAPIController(IOptions<AppConfigurationSettings> _appSettingsSection, IBaseService<UserInfo> _baseService)
        {
            objAppConfigurationSettings = _appSettingsSection.Value;
            baseService = _baseService;
        }
        /// <summary>
        /// Add method for User Info.
        /// </summary>
        /// <param name="entityObject" > Class object for Add method.</param>
        /// <returns>Uniform Response with Success or Error.</returns>
        [HttpPost("AddEntity")]
        public async Task<IActionResult> AddEntity([FromBody] UserInfoAddRequest entityObject)
        {
            UserInfo dataObject = new UserInfo(entityObject);         
            APIResponse = await baseService.AddEntity(dataObject);
            return Ok(APIResponse);
        }
        /// <summary>
        /// Update method for User Info.
        /// </summary>
        /// <param name="entityObject">Class object for Update method.</param>
        /// <returns>Uniform Response with Success or Error.</returns>
        [HttpPut("UpdateEntity")]
        public async Task<IActionResult> UpdateEntity([FromBody] UserInfoEditRequest entityObject)
        {
            UserInfo dataObject = new UserInfo(entityObject);
            APIResponse = await baseService.UpdateEntity(dataObject);
            return Ok(APIResponse);
        }
        /// <summary>
        /// Delete method for User Info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Uniform Response with Success or Error.</returns>
        [HttpDelete("DeleteEntity")]
        public async Task<IActionResult> DeleteEntity([FromQuery] Int64 userId)
        {
            UserInfo entityObject = new UserInfo();
            entityObject.Id = userId;
            APIResponse = await baseService.DeleteEntity(entityObject);   
            return Ok(APIResponse);
        }
        /// <summary>
        /// Get list of User Info based on filter parameter.
        /// </summary>
        /// <param name="entityObject"> Object with filter information.</param>
        /// <returns>Uniform Response with Success or Error.</returns>
        [HttpGet("GetEntityList")]
        public async Task<IActionResult> GetEntityList([FromQuery] UserInfoEditRequest entityObject = null)
        {
            UserInfo dataObject = new UserInfo(entityObject);
            APIResponse = await baseService.GetEntityList(dataObject);
            return Ok(APIResponse);
        }

    }
}
