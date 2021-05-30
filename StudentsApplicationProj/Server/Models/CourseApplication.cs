using StudentsApplicationProj.Shared.Enum;
using System;

namespace StudentsApplicationProj.Server.Models
{
    public class CourseApplication
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationBody { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime ApplicationDateTime { get; set; }
        public FileUrl FileUrls { get; set; }
        public StudentCourse StudentCourse { get; set; }
        public int StudentCourseId { get; set; }
        public string NoteMessage { get; set; }
        public string NoteFrom { get; set; }
    }
}
