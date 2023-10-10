using AutoMapper;
using CRUD_WebApi.Application.Common.Exceptions;
using CRUD_WebApi.Application.Interface;
using CRUD_WebApi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRUD_WebApi.Application.Accounts.Queries.Authentication
{
    public class AuthenticationQueryHandler : IRequestHandler<AuthenticationQuery, AuthenticationDto>
    {
        private readonly IWebApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthenticationQueryHandler(IWebApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AuthenticationDto> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(user =>
                            user.Login == request.Login
                            && user.Password == request.Password,
                            cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(Account), request.Login);
            }

            var userValidateDto = _mapper.Map<AuthenticationDto>(user);
            return userValidateDto;
        }
    }
}
