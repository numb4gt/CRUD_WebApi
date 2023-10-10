using CRUD_WebApi.Application.Users.Commands.Delete;
using CRUD_WebApi.Application.Users.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Commands.Grant
{
    public class GrantUserCommandValidator : AbstractValidator<GrantUserCommand>
    {
        public GrantUserCommandValidator()
        {
            RuleFor(updateUserCommand =>
                updateUserCommand.UserId).GreaterThan(0).NotEmpty();
            RuleFor(updateUserCommand =>
                updateUserCommand.Role).NotEmpty().Must(BeOneOfAllowedValues);
        }

        private static bool BeOneOfAllowedValues(string value)
        {
            var allowedValues = new string[] { "Admin", "SuperAdmin", "Support", "User" };
            return allowedValues.Contains(value);
        }
    }
}
