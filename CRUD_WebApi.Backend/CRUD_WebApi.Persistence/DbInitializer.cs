using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(WebApiDbContext context) {
            context.Database.Migrate();
        }
    }
}
