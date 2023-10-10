using CRUD_WebApi.Application.Interface;
using MediatR;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUD_WebApi.Application.Common.Exceptions;
using CRUD_WebApi.Domain;

namespace CRUD_WebApi.Application.Users.Queries.GetUsersParams
{
    public class GetUserParamsQueryHandler : IRequestHandler<GetUserParamsQuery, UserParamsViewModel>
    {
        private readonly IWebApiDbContext _webApiDbContext;
        private readonly IMapper _mapper;

        public GetUserParamsQueryHandler(IWebApiDbContext context, IMapper mapper) => (_webApiDbContext, _mapper) = (context, mapper);

        public async Task<UserParamsViewModel> Handle(GetUserParamsQuery request, CancellationToken cancellationToken)
        {
            var entity =
                await _webApiDbContext.Users.Include(user => user.Roles).FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.User), request.Id);
            }

            return _mapper.Map<UserParamsViewModel>(entity);
        }
    }
}
