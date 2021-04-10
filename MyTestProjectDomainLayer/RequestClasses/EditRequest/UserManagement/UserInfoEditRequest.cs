using System;

namespace MyTestProjectDomainLayer.RequestClasses.EditRequest.UserManagement
{
    public class UserInfoEditRequest
    {
        public Int64 Id { get; set; }
        public Boolean IsActive { get; set; }
        public Int32? Age { get; set; }
        public String? Phone { get; set; }
        public String Email { get; set; }
        public String FullName { get; set; }
        public String ValidationMode { get; set; }
   
        private void InitializeObject()
        {
            FullName = string.Empty;
            Age = null;
            Phone = null;
            Email = string.Empty;
            IsActive = true;
            Id = 0;
        }
        public UserInfoEditRequest()
        {
            InitializeObject();
        }
    }
}
