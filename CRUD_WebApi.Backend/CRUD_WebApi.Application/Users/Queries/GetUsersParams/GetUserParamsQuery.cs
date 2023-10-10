using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Queries.GetUsersParams
{
    public class GetUserParamsQuery : IRequest<UserParamsViewModel>
    {
        public int Id { get; set; }
    }
}
