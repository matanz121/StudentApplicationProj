using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;

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
        }
    }
}
