using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_WebApi.Domain;
using MediatR;
using CRUD_WebApi.Application.Interface;
using Microsoft.EntityFrameworkCore;
using CRUD_WebApi.Application.Common.Exceptions;

namespace CRUD_WebApi.Application.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IWebApiDbContext _webApiDbContext;

        public CreateUserCommandHandler(IWebApiDbContext context) => _webApiDbContext = context;

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new Domain.User
                {
                    Age = request.Age,
                    Name = request.Name,
                    Email = request.Email,
                    Roles = request.Roles.Select(roleName => new UserRole { Role = Enum.Parse<Role>(roleName) }).ToList()
                };

                await _webApiDbContext.Users.AddAsync(user);
                await _webApiDbContext.SaveChangesAsync(cancellationToken);

                return user.Id;
            }catch(DbUpdateException ex) {
                throw new RepetitiveEmailException();
            }
        }
    }
}
