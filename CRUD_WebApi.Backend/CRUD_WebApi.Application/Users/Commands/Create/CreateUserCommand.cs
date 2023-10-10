using CRUD_WebApi.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<string> Roles { get; set; }
    }
}
