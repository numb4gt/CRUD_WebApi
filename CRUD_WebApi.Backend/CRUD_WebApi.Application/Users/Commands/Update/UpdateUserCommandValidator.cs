using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() {
            RuleFor(updateUserCommand =>
                 updateUserCommand.Id).GreaterThan(0).NotEmpty();
            RuleFor(updateUserCommand =>
                 updateUserCommand.Email).NotEmpty().EmailAddress().MaximumLength(250);
            RuleFor(updateUserCommand =>
                updateUserCommand.Name).NotEmpty().MaximumLength(250);
            RuleFor(updateUserCommand =>
                updateUserCommand.Age).GreaterThan(0).NotEmpty();
            RuleFor(updateUserCommand => 
                updateUserCommand.Roles).NotEmpty().Must(HaveValidRoles)
                    .Must(HaveMaxFourRoles).Must(HaveNoDuplicateRoles);
        }

        private static bool HaveValidRoles(List<string> roles)
        {
            var allowedRoles = new List<string> { "Admin", "SuperAdmin", "Support", "User" };
            return roles.All(role => allowedRoles.Contains(role));
        }

        private static bool HaveMaxFourRoles(List<string> roles)
        {
            return roles.Count <= 4;
        }

        private static bool HaveNoDuplicateRoles(List<string> roles)
        {
            return roles.Distinct().Count() == roles.Count;
        }

    }
}
