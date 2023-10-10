using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Common.Exceptions
{
    public class RoleAlreadyAssignedException : Exception
    {
        public RoleAlreadyAssignedException()
                    : base($"Please try other role") { }
    }
}
