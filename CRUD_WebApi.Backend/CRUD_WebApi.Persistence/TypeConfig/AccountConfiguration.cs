using CRUD_WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Persistence.TypeConfig
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
            builder.Property(user => user.Login).HasMaxLength(20).IsRequired();
            builder.Property(user => user.Password).HasMaxLength(30).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(50);
        }
    }
}
