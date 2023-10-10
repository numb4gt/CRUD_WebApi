using CRUD_WebApi.Application.Common.Exceptions;
using CRUD_WebApi.Application.Interface;
using CRUD_WebApi.Application.Users.Commands.Create;
using CRUD_WebApi.Domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Commands.Grant
{
    public class GrantUserCommandHandler : IRequestHandler<GrantUserCommand, int>
    {
        private readonly IWebApiDbContext _webApiDbContext;
        public GrantUserCommandHandler(IWebApiDbContext context) => _webApiDbContext = context;

        public async Task<int> Handle(GrantUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _webApiDbContext.Users.Include(user => user.Roles)
                    .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(Domain.User), request.UserId);
                }

                var grant = new UserRole
                {
                    UserId = request.UserId,
                    Role = (Role)Enum.Parse(typeof(Role), request.Role)
                };

                await _webApiDbContext.UserRoles.AddAsync(grant);
                await _webApiDbContext.SaveChangesAsync(cancellationToken);

                return grant.Id;
            } catch (DbUpdateException ex)
            {
                throw new RoleAlreadyAssignedException();
            }
        }
    }
}
