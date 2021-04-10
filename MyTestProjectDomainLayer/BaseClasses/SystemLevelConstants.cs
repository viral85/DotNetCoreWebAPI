
using System.Collections.Generic;


namespace MyTestProjectDomainLayer.BaseClasses
{
    public class SystemLevelConstants
    {
        public static List<string> emptyResponse = new List<string>();
        public const string const_AppConfigurationSettings = "AppConfigurationSettings";
        public const string const_UserId = "User Id";
        public const string const_Token = "Token *";
        public const string const_ApiKey = "Api Key *";
        
        public const string const_DropDownSelect = "-- Select--";
        public const string const_EncryptedId = "Record.Id";
        public const string const_IsActive = "Active ? ";

      
       
        
        public const string const_Field_EntityMessage = "EntityMessage";
 
        public const string const_Result_Success = "successfully.";
        public const string const_Record_Not_Found = "Record Not Found";
        public const string const_Result_Failure = "Failure: ";
        public const string const_InvalidToken = "Invalid Token";
     
       
       
        public SystemLevelConstants()
        {
            emptyResponse = new List<string>();
        }
        
    }
}
