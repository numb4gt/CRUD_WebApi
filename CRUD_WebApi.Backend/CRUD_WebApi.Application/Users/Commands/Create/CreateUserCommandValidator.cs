using Azure.Core;
using CRUD_WebApi.Application.Common.Exceptions;
using CRUD_WebApi.Application.Interface;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() {
            RuleFor(createUserCommand => 
                createUserCommand.Email).NotEmpty().EmailAddress().MaximumLength(250);
            RuleFor(createUserCommand =>
                createUserCommand.Name).NotEmpty().MaximumLength(250);
            RuleFor(createUserCommand =>
                createUserCommand.Age).GreaterThan(0).NotEmpty();
            RuleFor(createUserCommand =>
                createUserCommand.Roles).NotEmpty().Must(HaveValidRoles)
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
