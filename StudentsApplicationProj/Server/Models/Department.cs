using System.Collections.Generic;

namespace StudentsApplicationProj.Server.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentHeadId { get; set; }
        public IList<Course> Courses { get; set; }
        public ICollection<SystemUser> DepartmentUsers { get; set; }
    }
}
