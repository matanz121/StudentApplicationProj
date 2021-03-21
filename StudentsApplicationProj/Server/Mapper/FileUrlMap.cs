using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsApplicationProj.Server.Models;


namespace StudentsApplicationProj.Server.Mapper
{
    public class FileUrlMap : IEntityTypeConfiguration<FileUrl>
    {
        public void Configure(EntityTypeBuilder<FileUrl> builder)
        {
            builder.ToTable("FileUrl");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FileName).HasColumnType("nvarchar(128)");
            builder.Property(x => x.Url).IsRequired().HasColumnType("nvarchar(258)");
            builder.Property(x => x.CourseApplicationId).IsRequired().HasColumnType("int");

            builder.HasOne(file => file.CourseApplication)
                .WithMany(application => application.FileUrls)
                .HasForeignKey(file => file.CourseApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
