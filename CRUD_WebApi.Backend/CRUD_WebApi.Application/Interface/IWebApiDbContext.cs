using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUD_WebApi.Domain;

namespace CRUD_WebApi.Application.Interface
{
    public interface IWebApiDbContext
    {
        DbSet<Domain.User> Users { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<Account> Accounts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken); 
    }
}
