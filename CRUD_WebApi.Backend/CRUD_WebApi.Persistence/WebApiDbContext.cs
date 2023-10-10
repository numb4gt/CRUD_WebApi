using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_WebApi.Persistence.TypeConfig;
using CRUD_WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using CRUD_WebApi.Application.Interface;

namespace CRUD_WebApi.Persistence
{
    public class WebApiDbContext : DbContext, IWebApiDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
