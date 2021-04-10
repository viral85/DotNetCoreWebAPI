using Microsoft.Data.SqlClient;
using MyTestProjectDomainLayer.BaseClasses;
using MyTestProjectRepositoryLayer.BaseClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestProjectRepositoryLayer.BaseClasses
{
    public class DatabaseEngine
    {

        private SqlConnection dbConnection { get; set; }
        private SqlParameterCollection commandParameters { get; set; }
        private String commandName { get; set; }
        private String connectionString { get; set; }
        private CommandType commandType { get; set; }
        private Int32 timeout { get; set; }
        private Int32 affectedRecords { get; set; }
        private ApiResponseWrapper apiDataResponse { get; set; }

        public DatabaseEngine(string _connectionString)
        {
            connectionString = _connectionString;
        }
        
        private List<SqlParameter> AddApiLogInfoParameterList()
        {
            SqlParameter[] objApiLogInformationParameter = new SqlParameter[6];
            objApiLogInformationParameter[0] = new SqlParameter();
           
            objApiLogInformationParameter[0].SqlDbType = SqlDbType.VarChar;
            objApiLogInformationParameter[0].Size = 1000;
            objApiLogInformationParameter[0].Direction = System.Data.ParameterDirection.InputOutput;
            objApiLogInformationParameter[0].ParameterName = SystemLevelConstants.const_Field_EntityMessage;
            objApiLogInformationParameter[0].Value = String.Empty;

            List<SqlParameter> listOfParams = new List<SqlParameter>();
            listOfParams.Add(objApiLogInformationParameter[0]);
            return listOfParams;
        }

        public void returnAPIResponse<T>(ref ApiResponseWrapper objApiResponseWrapper)
        {
            DataSet dsOutput = objApiResponseWrapper.apiResponseData;
            List<T> lstEntityToReturn = new List<T>();
            if (dsOutput.Tables.Count > 0)
            {
                lstEntityToReturn = AutoMapper.ConvertDataTable<T>(dsOutput.Tables[0]);
            }
            objApiResponseWrapper.apiResponseStatus = objApiResponseWrapper.apiResponseMessage.ToLower().Contains(SystemLevelConstants.const_Result_Success);
            objApiResponseWrapper.apiResponseData = lstEntityToReturn;

        }
        public async Task<ApiResponseWrapper> ExecuteObjectAsync(SqlCommand customSqlCommand, bool isQuery)
        {
            apiDataResponse = new ApiResponseWrapper();
            using (dbConnection = new SqlConnection(connectionString))
            {
                customSqlCommand.CommandTimeout = 1200;
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }
                customSqlCommand.Connection = dbConnection;

                List<SqlParameter> sqlpList = new List<SqlParameter>();
                if (sqlpList.Count == 0)
                {
                    sqlpList = AddApiLogInfoParameterList();
                    foreach (SqlParameter sqlp in sqlpList.ToList())
                    {
                        if (customSqlCommand.Parameters.IndexOf(sqlp.ParameterName) == -1)
                        {
                            customSqlCommand.Parameters.Add(sqlp);
                        }
                    }
                }
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(customSqlCommand))
                {
                    da.Fill(ds);
                    apiDataResponse.apiResponseData = ds;
                }

                if (isQuery)
                {
                    apiDataResponse.apiResponseMessage = SystemLevelConstants.const_Result_Success;
                    apiDataResponse.apiResponseStatus = true;
                }
                else
                {
                    string strMessage = customSqlCommand.Parameters[SystemLevelConstants.const_Field_EntityMessage].Value.ToString();
                    apiDataResponse.apiResponseStatus = strMessage.Contains(SystemLevelConstants.const_Result_Success);
                    apiDataResponse.apiResponseMessage = strMessage;
                }



            }
            if (apiDataResponse.apiResponseData == null)
            {
                List<string> lsDefaultReturn = new List<string>();
                apiDataResponse.apiResponseData = lsDefaultReturn;
            }
            return apiDataResponse;
        }
    }
}
