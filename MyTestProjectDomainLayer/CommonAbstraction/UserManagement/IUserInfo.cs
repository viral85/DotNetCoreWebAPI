using System;
namespace MyTestProjectDomainLayer.CommonAbstraction.UserManagement
{
	public interface IUserInfo
	{
       Int64 Id { get; set; }
       Boolean IsActive { get; set; }
       Int32? Age { get; set; }
       String? Phone { get; set; }
       String Email { get; set; }
       String FullName { get; set; }
    }
}
