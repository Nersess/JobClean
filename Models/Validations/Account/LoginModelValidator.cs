using FluentValidation;
using Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Validations.Account
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("Username Empty");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password Empty");
        }
    }
}
