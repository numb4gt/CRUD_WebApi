using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Commands.Grant
{
    public class GrantUserCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
