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
using CRUD_WebApi.Domain;
using System.Data;

namespace CRUD_WebApi.Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IWebApiDbContext _webApiDbContext;

        public UpdateUserCommandHandler(IWebApiDbContext context) => _webApiDbContext = context;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity =
                    await _webApiDbContext.Users.Include(user => user.Roles).FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Domain.User), request.Id);
                }

                entity.Name = request.Name;
                entity.Email = request.Email;
                entity.Age = request.Age;
                entity.Roles = request.Roles.Select(roleName => new UserRole { Role = Enum.Parse<Role>(roleName) }).ToList();


                await _webApiDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (DbUpdateException ex)
            {
                throw new RepetitiveEmailException();
            }
        }
    }
}
