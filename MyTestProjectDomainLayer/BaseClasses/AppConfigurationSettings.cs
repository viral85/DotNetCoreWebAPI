using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProjectDomainLayer.BaseClasses
{
    public partial class AppConfigurationSettings
    {
        public String DatabaseConnection { get; set; }
        public String JWTSecretKey { get; set; }
        public String AllowedHosts { get; set; }
        public String ApiKey { get; set; }
        public String AppURL { get; set; }
        public String ApiURL { get; set; }
        public String AppName { get; set; }
        public Boolean EnableDebuggingAndDocumentation { get; set; }
        public String DevCORSURL { get; set; }
        public String StagingCORSURL { get; set; }
        public String ProdCORSURL { get; set; }
        public String ProjectFriendlyName { get; set; }
        public String ProjectXMLFile { get; set; }
        public String SystemLogoAssetPath { get; set; }
        public String SystemName { get; set; }
        public String SystemFromEmail { get; set; }
        public String SystemInquiryEmail { get; set; }
        public String UniqueUserIdIsEmail { get; set; }
        public Boolean TurnOffFluentValidation { get; set; }
        public String ResetPasswordURL { get; set; }
        public String Environment { get; set; }
        public String SendGridAPIUrl { get; set; }
        public String SendGridAPIKey { get; set; }
      
      

        public const String const_Field_DatabaseConnection = "DatabaseConnection";
        public const String const_Field_JWTSecretKey = "JWTSecretKey";
        public const String const_Field_AllowedHosts = "AllowedHosts";
        public const String const_Field_ApiKey = "ApiKey";
        public const String const_Field_EncryptedTokenValue = "EncryptedTokenValue";
        public const String const_Field_LoggedInUserId = "LoggedInUserId";
        public const String const_Field_AppURL = "AppURL";
        public const String const_Field_ApiURL = "ApiURL";
        public const String const_Field_AppName = "AppName";
        public const String const_Field_EnableDebuggingAndDocumentation = "EnableDebuggingAndDocumentation";
        public const String const_Field_DevCORSURL = "DevCORSURL";
        public const String const_Field_StagingCORSURL = "StagingCORSURL";
        public const String const_Field_ProdCORSURL = "ProdCORSURL";
        public const String const_Field_ProjectFriendlyName = "ProjectFriendlyName";
        public const String const_Field_ProjectXMLFile = "ProjectXMLFile";
        public const String const_Field_SystemLogoAssetPath = "SystemLogoAssetPath";
        public const String const_Field_SystemName = "SystemName";
        public const String const_Field_SystemFromEmail = "SystemFromEmail";
        public const String const_Field_SystemInquiryEmail = "SystemInquiryEmail";
        public const String const_Field_UniqueUserIdIsEmail = "UniqueUserIdIsEmail";
        public const String const_Field_TurnOffFluentValidation = "TurnOffFluentValidation";
        public const String const_Field_ResetPasswordURL = "ResetPasswordURL";
        public const String const_Field_Environment = "Environment";
        public const String const_Field_SendGridAPIUrl = "SendGridAPIUrl";
        public const String const_Field_SendGridAPIKey = "SendGridAPIKey";
       
        public AppConfigurationSettings()
        {
            DatabaseConnection = String.Empty;
            JWTSecretKey = String.Empty;
            AllowedHosts = String.Empty;
            ApiKey = String.Empty;
            EnableDebuggingAndDocumentation = true;
            DevCORSURL = String.Empty;
            StagingCORSURL = String.Empty;
            ProdCORSURL = String.Empty;
            ProjectFriendlyName = String.Empty;
            ProjectXMLFile = String.Empty;
            SystemLogoAssetPath = String.Empty;
            SystemName = String.Empty;
            SystemFromEmail = String.Empty;
            SystemInquiryEmail = String.Empty;
            UniqueUserIdIsEmail = String.Empty;
            TurnOffFluentValidation = false;
            ResetPasswordURL = String.Empty;
            Environment = String.Empty;           
        
        }
    }
}
