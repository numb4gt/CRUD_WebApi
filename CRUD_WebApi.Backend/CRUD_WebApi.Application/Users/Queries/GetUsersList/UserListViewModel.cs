using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Users.Queries.GetUsersList
{
    public class UserListViewModel
    {
        public IList<UserLookUpDto> Users { get; set; }
    }
}
