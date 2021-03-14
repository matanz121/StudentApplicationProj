using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;

namespace StudentsApplicationProj.Server.Mapper
{
    public class InstructorCourseMap: IEntityTypeConfiguration<InstructorCourse>
    {
        public void Configure(EntityTypeBuilder<InstructorCourse> builder)
        {
            builder.ToTable("InstructorCourse");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CourseId);
            builder.Property(x => x.CourseId).IsRequired().HasColumnType("int");
            builder.Property(x => x.InstructorId).IsRequired().HasColumnType("int");
        }
    }
}
