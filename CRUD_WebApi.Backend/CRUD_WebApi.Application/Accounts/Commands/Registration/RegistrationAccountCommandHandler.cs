using CRUD_WebApi.Application.Interface;
using CRUD_WebApi.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Accounts.Commands.Registration
{
    public class RegistrationAccountCommandHandler : IRequestHandler<RegistrationAccountCommand, int>
    {
        private readonly IWebApiDbContext _dbContext;

        public RegistrationAccountCommandHandler(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(RegistrationAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Login = request.Login,
                Password = request.Password,
                Email = request.Email
            };

            await _dbContext.Accounts.AddAsync(account, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return account.Id;
        }
    }
}
