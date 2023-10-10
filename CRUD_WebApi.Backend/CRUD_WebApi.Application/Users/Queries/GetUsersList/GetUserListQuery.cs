using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Queries.GetUsersList
{
    public class GetUserListQuery : IRequest<UserListViewModel>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? Direction { get; set; }
        public string FilterBy { get; set; }
        public string ParamToFilter { get; set; }
    }

}
