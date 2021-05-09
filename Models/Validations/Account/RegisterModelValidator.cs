using FluentValidation;
using Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Validations.Account
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First Name Empty")
                .Matches(@"^[a-zA-Z-']*$").WithMessage("First Name Only Letter")
                .MaximumLength(50).WithMessage("First Name Wrong Length");
            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last Name Empty")
                .Matches(@"^[a-zA-Z-']*$").WithMessage("Last Name Only Letter")
                .MaximumLength(50).WithMessage("Last Name Wrong Length");
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email Empty")
                .EmailAddress();
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password Empty")
                .MinimumLength(6).WithMessage("Password Wrong Length")
                .Must(m => m != null && m.Any(c => char.IsDigit(c))).WithMessage("Password Contain Digit")
                .Must(m => m != null && m.Any(c => char.IsLetter(c))).WithMessage("Password Contain Letter");
        }
    }
}
