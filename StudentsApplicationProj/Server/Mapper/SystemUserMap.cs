using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;

namespace StudentsApplicationProj.Server.Mapper
{
    public class SystemUserMap : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.ToTable("SystemUser");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.DepartmentId);
            builder.Property(x => x.DepartmentId).IsRequired().HasColumnType("int");
            builder.Property(x => x.UserAccountId).IsRequired().HasColumnType("int");
        }
    }
}
