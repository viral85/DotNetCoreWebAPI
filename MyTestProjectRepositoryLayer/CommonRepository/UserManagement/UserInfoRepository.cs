using Microsoft.Data.SqlClient;
using MyTestProjectRepositoryLayer.BaseClasses;
using MyTestProjectDomainLayer.BaseClasses;
using MyTestProjectDomainLayer.CommonEntities.UserManagement;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyTestProjectRepositoryLayer.CommonRepository.UserManagement
{
    public class UserInfoRepository : BaseDBCommand, IBaseRepository<UserInfo>
    {
        public UserInfoRepository(string _connectionstring)
        {
            connectionString = _connectionstring;
            customCommand = new SqlCommand();
            customCommand.Parameters.Clear();
        }
        private void InjectCommonParameters(bool blnIncludeId, UserInfo entityObject)
        {
            customCommand = new SqlCommand();
            customCommand.Parameters.Clear();
            customCommand.Parameters.AddWithValue(UserInfo.const_Field_Id,entityObject.Id);
            customCommand.Parameters.AddWithValue(UserInfo.const_Field_IsActive,entityObject.IsActive);
            customCommand.Parameters.AddWithValue(UserInfo.const_Field_Email, entityObject.Email);
            customCommand.Parameters.AddWithValue(UserInfo.const_Field_FullName, entityObject.FullName.Trim());
            customCommand.Parameters.AddWithValue(UserInfo.const_Field_Age, entityObject.Age == null ? null : entityObject.Age);
            customCommand.Parameters.AddWithValue(UserInfo.const_Field_Phone, entityObject.Phone == null ? null : entityObject.Phone);
        }
        public async Task<ApiResponseWrapper> AddEntity(UserInfo entityObject)
        {
            InjectCommonParameters(false, entityObject);
            customCommand.CommandText = UserInfo.const_Proc_UserInfo_Insert;
            customCommand.CommandType = CommandType.StoredProcedure;
            DatabaseEngine objDatabaseEngine = new DatabaseEngine(connectionString);
            ApiResponseWrapper objApiResponseWrapper = await objDatabaseEngine.ExecuteObjectAsync(customCommand, false);
            objDatabaseEngine.returnAPIResponse<UserInfo>(ref objApiResponseWrapper);
            return objApiResponseWrapper;

        }

        public async Task<ApiResponseWrapper> UpdateEntity(UserInfo entityObject)
        {
            InjectCommonParameters(true, entityObject);
            customCommand.CommandText = UserInfo.const_Proc_UserInfo_Update;
            customCommand.CommandType = CommandType.StoredProcedure;
            DatabaseEngine objDatabaseEngine = new DatabaseEngine(connectionString);
            ApiResponseWrapper objApiResponseWrapper = await objDatabaseEngine.ExecuteObjectAsync(customCommand, false);
            objDatabaseEngine.returnAPIResponse<UserInfo>(ref objApiResponseWrapper);
            return objApiResponseWrapper;

        }
        public async Task<ApiResponseWrapper> DeleteEntity(UserInfo entityObject)
        {
            customCommand.Parameters.Clear();
            customCommand.Parameters.AddWithValue(UserInfo.const_Field_Id, entityObject.Id);
            customCommand.CommandText = UserInfo.const_Proc_UserInfo_ChangeRecordStatus;
            customCommand.CommandType = CommandType.StoredProcedure;
            DatabaseEngine objDatabaseEngine = new DatabaseEngine(connectionString);
            ApiResponseWrapper objApiResponseWrapper = await objDatabaseEngine.ExecuteObjectAsync(customCommand, false);
            objDatabaseEngine.returnAPIResponse<UserInfo>(ref objApiResponseWrapper);
            return objApiResponseWrapper;
        }
        public async Task<ApiResponseWrapper> GetEntityList(UserInfo entityObject)
        {
          
            InjectCommonParameters(true, entityObject);
            customCommand.CommandText = UserInfo.const_Proc_UserInfo_SelectSearch;
            customCommand.CommandType = CommandType.StoredProcedure;
            DatabaseEngine objDatabaseEngine = new DatabaseEngine(connectionString);
            ApiResponseWrapper objApiResponseWrapper = await objDatabaseEngine.ExecuteObjectAsync(customCommand, true);
            DataSet ds = objApiResponseWrapper.apiResponseData;
            List<UserInfo> lstEntityToReturn = new List<UserInfo>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstEntityToReturn = AutoMapper.ConvertDataTable<UserInfo>(ds.Tables[0]);
                    objApiResponseWrapper.apiResponseStatus = true;
                    objApiResponseWrapper.apiResponseData = lstEntityToReturn;
                }
            }
            
            objApiResponseWrapper.apiResponseData = lstEntityToReturn;
            return objApiResponseWrapper;
        }
      
    }
}