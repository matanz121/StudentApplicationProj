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
            
            builder.HasMany(dept => dept.Courses)
                .WithOne(course => course.Department)
                .HasForeignKey(dept => dept.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(dept => dept.DepartmentUsers)
                .WithOne(student => student.Department)
                .HasForeignKey(dept => dept.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
