using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Queries.GetUsersList
{
    public class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
    {
        public GetUserListQueryValidator() {
            RuleFor(getUserCommand =>
                   getUserCommand.Page).GreaterThan(0);
            RuleFor(getUserCommand =>
                    getUserCommand.PageSize).GreaterThan(0);

            RuleFor(getUserCommand => getUserCommand.FilterBy)
                .Must((getUserCommand, filterBy) =>
                {
                    if (filterBy == "age")
                    {
                        if (int.TryParse(getUserCommand.ParamToFilter, out int paramValue))
                        {
                            return paramValue > 0;
                        }
                        return false;
                    }
                    return true;
                });

            RuleFor(getUserCommand => getUserCommand.FilterBy)
                .Must((getUserCommand, filterBy) =>
                {
                    if (filterBy == "role")
                    {
                        var allowedValues = new string[] { "Admin", "SuperAdmin", "Support", "User" };
                        return allowedValues.Contains(getUserCommand.ParamToFilter);
                    }
                    return true;
                });

        }

    }
}
