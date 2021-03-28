using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System;
using System.Text;

namespace StudentsApplicationProj.Server.Mapper
{
    public class UserAccountMap: IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("UserAccount");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired().HasColumnType("nvarchar(64)");
            builder.Property(x => x.Password).IsRequired().HasColumnType("nvarchar(256)");
            builder.Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar(64)");
            builder.Property(x => x.LastName).IsRequired().HasColumnType("nvarchar(64)");
            builder.Property(x => x.UserRole).IsRequired().HasColumnType("int");
            builder.Property(x => x.AccountStatus).HasColumnType("bit").HasDefaultValue(false);

            builder.HasData(new UserAccount
            {
                Id = 1,
                Email = "admin@gmail.com",
                Password = HashPassword("AdminPassword123"),
                FirstName = "Admin First",
                LastName = "Admin Last",
                UserRole = UserRole.Administrator,
                AccountStatus = true
            });
        }

        private string HashPassword(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
