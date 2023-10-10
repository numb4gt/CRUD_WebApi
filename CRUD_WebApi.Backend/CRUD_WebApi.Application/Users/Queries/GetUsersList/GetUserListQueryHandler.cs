using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRUD_WebApi.Application.Interface;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CRUD_WebApi.Domain;

namespace CRUD_WebApi.Application.Users.Queries.GetUsersList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListViewModel>
    {
        private readonly IWebApiDbContext _webApiDbContext;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IWebApiDbContext context, IMapper mapper) => (_webApiDbContext, _mapper) = (context, mapper);

        public async Task<UserListViewModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<User> usersQuery = _webApiDbContext.Users;

            //filtration
            usersQuery = usersQuery.Where(GetFilterProperty(request));

            //sorting
            if(request.Direction?.ToLower() == "desc")
            {
                usersQuery = usersQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                usersQuery = usersQuery.OrderBy(GetSortProperty(request));
            }

            //pagination
            var userList = await usersQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<UserLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new UserListViewModel { Users = userList };
        }

        public static Expression<Func<User, object>> GetSortProperty(GetUserListQuery request)
        {
            return request.SortBy?.ToLower() switch
            {
                "age" => user => user.Age,
                "email" => user => user.Email,
                "name" => user => user.Name,
                _ => user => user.Id,
            };
        }

        public static Expression<Func<User, bool>> GetFilterProperty(GetUserListQuery request)
        {
            return request.FilterBy?.ToLower() switch
            {
                "age" => user => user.Age == Convert.ToInt32(request.ParamToFilter),
                "email" => user => user.Email == request.ParamToFilter,
                "name" => user => user.Name == request.ParamToFilter,
                "role" => user => user.Roles.Any(userRole => userRole.Role == 
                    (Role)Enum.Parse(typeof(Role), request.ParamToFilter)),
                _ => user => true,
            };
        }
    }
}
