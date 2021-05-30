using StudentsApplicationProj.Shared.Enum;
using System;

namespace StudentsApplicationProj.Shared.Models
{
    public class CourseApplicationViewModel
    {
        public int Id { get; set; }
        public UserModel Student { get; set; }
        public CourseModel Course { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationBody { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime? ApplicationDateTime { get; set; }
        public FileUrlModel FileUrls { get; set; }
        public string NoteMessage { get; set; }
        public string NoteFrom { get; set; }
    }
}
