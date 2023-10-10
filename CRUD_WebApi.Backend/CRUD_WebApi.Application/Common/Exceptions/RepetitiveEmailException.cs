using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Common.Exceptions
{
    public class RepetitiveEmailException : Exception
    {
        public RepetitiveEmailException()
            : base($"Please try other adress") { }
    }
}
