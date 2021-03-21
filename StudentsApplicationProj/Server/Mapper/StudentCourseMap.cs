using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;

namespace StudentsApplicationProj.Server.Mapper
{
    public class StudentCourseMap : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.ToTable("StudentCourse");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CourseId);
            builder.Property(x => x.CourseId).IsRequired().HasColumnType("int");
            builder.Property(x => x.StudentId).IsRequired().HasColumnType("int");

            builder.HasOne(studentCourse => studentCourse.Course)
                .WithMany(course => course.StudentCourse)
                .HasForeignKey(studentCourse => studentCourse.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
