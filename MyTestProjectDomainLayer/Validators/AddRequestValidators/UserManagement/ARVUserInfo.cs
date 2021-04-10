using FluentValidation;
using MyTestProjectDomainLayer.RequestClasses.AddRequest.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProjectDomainLayer.Validators.AddRequestValidators.UserManagement
{
    public class ARVUserInfo: AbstractValidator<UserInfoAddRequest>
    {
        public ARVUserInfo()
        {
         
                RuleFor(r => r.FullName)
                    .NotEmpty()
                    .NotNull()
                    .Length(5,50);

                RuleFor(r => r.Email)
                    .NotEmpty()
                    .NotNull()
                    .EmailAddress();

        }
    }
}
