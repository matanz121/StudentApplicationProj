using System.Collections.Generic;


namespace StudentsApplicationProj.Server.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public float Grade { get; set; }
        public SystemUser CourseInstructor { get; set; }
        public int? CourseInstructorId { get; set; }
        public IList<StudentCourse> StudentCourse { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
