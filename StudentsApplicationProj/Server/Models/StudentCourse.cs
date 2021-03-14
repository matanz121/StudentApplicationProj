using System;

namespace StudentsApplicationProj.Server.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public SystemUser Student { get; set; }
        public int StudentId { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime ApplicationDateTime { get; set; }
    }
}
