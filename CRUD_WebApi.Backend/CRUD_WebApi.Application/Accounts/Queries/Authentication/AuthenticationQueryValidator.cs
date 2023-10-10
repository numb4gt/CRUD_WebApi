using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Accounts.Queries.Authentication
{
    public class AuthenticationQueryValidator : AbstractValidator<AuthenticationQuery>
    {
        public AuthenticationQueryValidator()
        {
            RuleFor(validateUserQuery =>
               validateUserQuery.Login).NotEmpty().MaximumLength(20);
            RuleFor(validateUserQuery =>
                validateUserQuery.Password).NotEmpty().MaximumLength(30);
        }
    }
}
