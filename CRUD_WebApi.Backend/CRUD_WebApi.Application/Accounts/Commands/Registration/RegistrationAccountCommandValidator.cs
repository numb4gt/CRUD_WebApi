using CRUD_WebApi.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Accounts.Commands.Registration
{
    public class RegistrationAccountCommandValidator : AbstractValidator<RegistrationAccountCommand>
    {
        public RegistrationAccountCommandValidator()
        {
            RuleFor(registerUserCommand =>
                registerUserCommand.Login).NotEmpty().MaximumLength(20);
            RuleFor(registerUserCommand =>
                registerUserCommand.Password).NotEmpty().MaximumLength(30);
            RuleFor(registerUserCommand =>
                registerUserCommand.Email).NotEmpty().MaximumLength(50);
        }
    }
}
