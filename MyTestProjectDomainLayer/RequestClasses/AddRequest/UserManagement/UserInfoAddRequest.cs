using System;


namespace MyTestProjectDomainLayer.RequestClasses.AddRequest.UserManagement
{
    public class UserInfoAddRequest 
    {
        public Boolean IsActive { get; set; }
        public Int32? Age { get; set; }
        public String? Phone { get; set; }
        public String Email { get; set; }
        public String FullName { get; set; }

        private void InitializeObject()
        {
            FullName = string.Empty;
            Age = null;
            Phone = null;
            Email = string.Empty;
            IsActive = true;
        }
        public UserInfoAddRequest()
        {
            InitializeObject();
        }
    }
}
