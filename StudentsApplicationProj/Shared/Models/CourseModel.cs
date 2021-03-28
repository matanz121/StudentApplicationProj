using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Shared.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public float Grade { get; set; }
        public UserModel CourseInstructor { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
