using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUD_WebApi.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD_WebApi.Persistence.TypeConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Name).HasMaxLength(50);
            builder.HasIndex(user => user.Email).IsUnique();
        }
    }
}
