using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;

namespace StudentsApplicationProj.Server.Mapper
{
    public class DepartmentMap: IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DepartmentName).IsRequired().HasColumnType("nvarchar(128)");
            builder.Property(x => x.DepartmentHeadId).HasColumnType("int");

            builder.HasData(
            new Department
            {
                Id = 1,
                DepartmentName = "CSE"
            },
            new Department
            {
                Id = 2,
                DepartmentName = "EEE"
            },

            new Department
            {
                Id = 3,
                DepartmentName = "ME"
            }
            );
        }
    }
}
