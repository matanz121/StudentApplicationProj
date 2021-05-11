using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;

namespace StudentsApplicationProj.Server.Mapper
{
    public class CourseApplicationMap : IEntityTypeConfiguration<CourseApplication>
    {
        public void Configure(EntityTypeBuilder<CourseApplication> builder)
        {
            builder.ToTable("CourseApplication");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired().HasColumnType("int");
            builder.Property(x => x.ApplicationDateTime).HasColumnType("date");
            builder.Property(x => x.ApplicationName).IsRequired().HasColumnType("nvarchar(128)");
            builder.Property(x => x.ApplicationBody).IsRequired().HasColumnType("nvarchar(1024)");
            builder.Property(x => x.StudentCourseId).IsRequired().HasColumnType("int");
            builder.Property(x => x.NoteMessage).HasColumnType("nvarchar(MAX)");
        }
    }
}
