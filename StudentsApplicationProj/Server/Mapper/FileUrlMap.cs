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
            builder.Property(x => x.GradeSheetPath).HasColumnType("nvarchar(256)");
            builder.Property(x => x.SyllabusPath).HasColumnType("nvarchar(256)");
            builder.Property(x => x.CertificatePath).HasColumnType("nvarchar(256)");
        }
    }
}
