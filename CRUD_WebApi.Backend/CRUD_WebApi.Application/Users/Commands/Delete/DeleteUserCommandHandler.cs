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

namespace CRUD_WebApi.Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IWebApiDbContext _webApiDbContext;

        public DeleteUserCommandHandler(IWebApiDbContext context) => _webApiDbContext = context;

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity =
               await _webApiDbContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.User), request.Id);
            }

            _webApiDbContext.Users.Remove(entity);
            await _webApiDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
