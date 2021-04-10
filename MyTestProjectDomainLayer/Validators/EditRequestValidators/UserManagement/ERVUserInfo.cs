using FluentValidation;
using MyTestProjectDomainLayer.RequestClasses.EditRequest.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProjectDomainLayer.Validators.EditRequestValidators.UserManagement
{
    public class ERVUserInfo : AbstractValidator<UserInfoEditRequest>
    {
        public ERVUserInfo()
        {
            When(model => model.ValidationMode != "ByPass", () =>
            {
                RuleFor(r => r.FullName)
                   .NotEmpty()
                   .NotNull()
                   .Length(5, 50);

                RuleFor(r => r.Email)
                    .NotEmpty()
                    .NotNull()
                    .EmailAddress();
            });
        }
    }
}
