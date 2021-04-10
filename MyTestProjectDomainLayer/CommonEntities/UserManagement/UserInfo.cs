using System;
using MyTestProjectDomainLayer.RequestClasses.AddRequest.UserManagement;
using MyTestProjectDomainLayer.CommonAbstraction.UserManagement;
using MyTestProjectDomainLayer.RequestClasses.EditRequest.UserManagement;

namespace MyTestProjectDomainLayer.CommonEntities.UserManagement
{
    public class UserInfo : IUserInfo
    {
        public Int64 Id { get; set; }
        public Boolean IsActive { get; set; }
        public Int32? Age { get; set; }
        public String? Phone { get; set; }
        public String Email { get; set; }
        public String FullName { get; set; }

        public const String const_Table_UserInfo = "UserInfo";
		public const String const_Proc_UserInfo_SelectSearch = "UserInfo_SelectSearch";
		public const String const_Proc_UserInfo_Insert = "UserInfo_Insert";
        public const String const_Proc_UserInfo_LogOut = "UserInfo_LogOut";
        public const String const_Proc_UserInfo_Update = "UserInfo_Update";
		public const String const_Proc_UserInfo_ChangeRecordStatus = "UserInfo_ChangeRecordStatus";



        public const String const_Field_Age = "Age";
        public const String const_Field_Phone = "Phone";
        public const String const_Field_Email = "Email";
        public const String const_Field_IsActive = "IsActive";
        public const String const_Field_FullName = "FullName";
        public const String const_Field_Id = "Id";
        private void InitializeObject()
        {
            FullName = string.Empty;
            Age = null;
            Phone = null;
            Email = string.Empty;
            IsActive = true;
            Id = 0;
        }

        public UserInfo()
        {
            this.InitializeObject();
            
        }
        public UserInfo(UserInfoEditRequest objEditRequestObject)
        {
            Id = objEditRequestObject.Id;
            FullName = objEditRequestObject.FullName;
            IsActive = objEditRequestObject.IsActive;
            Age = objEditRequestObject.Age;
            Email = objEditRequestObject.Email;
            Phone = objEditRequestObject.Phone;
        }

        public UserInfo(UserInfoAddRequest objAddRequestObject)
        {
            FullName = objAddRequestObject.FullName;
            IsActive = objAddRequestObject.IsActive;
            Age = objAddRequestObject.Age;
            Email = objAddRequestObject.Email;
            Phone = objAddRequestObject.Phone;
        }
    }
}