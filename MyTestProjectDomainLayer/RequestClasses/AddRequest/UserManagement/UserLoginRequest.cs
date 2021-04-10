using MyTestProjectDomainLayer.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTestProjectDomainLayer.RequestClasses.AddRequest.UserManagement
{
    public class UserLoginRequest
    {
        public string UniqueUserId { get; set; }
        public string UserPassword { get; set; }
    }
}
