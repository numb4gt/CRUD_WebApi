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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(userRole => userRole.Id);
            builder.Property(userRole => userRole.Role).IsRequired();
            builder.Property(userRole => userRole.UserId).IsRequired();
            builder.HasIndex(userRole => new { userRole.UserId, userRole.Role }).IsUnique();

            builder.Property(userRole => userRole.Role)
                        .IsRequired()
                        .HasConversion(
                            roleValue => (int)roleValue, // Преобразование в целое число
                            roleInt => (Role)Enum.ToObject(typeof(Role), roleInt) // Обратное преобразование в перечисление Role
                        );

            builder.HasOne(userRole => userRole.User) // Отношение один-ко-многим
            .WithMany(user => user.Roles) // Навигационное свойство в классе User
            .HasForeignKey(userRole => userRole.UserId)// Внешний ключ
            .IsRequired();
        }
    }
}
