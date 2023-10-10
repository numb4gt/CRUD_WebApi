using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Accounts.Queries.Authentication
{
    public class AuthenticationQuery : IRequest<AuthenticationDto>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
