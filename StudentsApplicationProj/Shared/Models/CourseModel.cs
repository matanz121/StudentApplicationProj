using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsApplicationProj.Shared.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public float Grade { get; set; }
        public UserModel CourseInstructor { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
