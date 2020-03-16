using FluentValidation;
using PrimeTech.Infrastructure.Resources.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeTech.Api.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterResource>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x=>x.Password)
                .WithMessage("'Confirm Password' must be equal to 'Password'.");
        }
    }
}
