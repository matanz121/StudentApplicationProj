using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Mapper;

namespace StudentsApplicationProj.Server.Models
{
    public class StudentDbContext: DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CourseMap());
            builder.ApplyConfiguration(new DepartmentMap());
            builder.ApplyConfiguration(new StudentCourseMap());
            builder.ApplyConfiguration(new SystemUserMap());
            builder.ApplyConfiguration(new CourseApplicationMap());
            builder.ApplyConfiguration(new FileUrlMap());
        }

        public DbSet<Course> Course { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<CourseApplication> CourseApplication { get; set; }
        public DbSet<FileUrl> FileUrl { get; set; }
    }
}
