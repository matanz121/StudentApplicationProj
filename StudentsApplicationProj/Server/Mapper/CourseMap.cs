using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;

namespace StudentsApplicationProj.Server.Mapper
{
    public class CourseMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.DepartmentId);
            builder.Property(x => x.CourseName).IsRequired().HasColumnType("nvarchar(128)");
            builder.Property(x => x.DepartmentId).IsRequired().HasColumnType("int");
            builder.Property(x => x.CourseInstructorId).HasColumnType("int");
            
            builder.HasMany(course => course.StudentCourse)
                .WithOne(studentCourse => studentCourse.Course)
                .HasForeignKey(course => course.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
