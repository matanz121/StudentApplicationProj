using System.Collections.Generic;

namespace StudentsApplicationProj.Server.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public SystemUser DepartmentHead { get; set; }
        public int? DepartmentHeadId { get; set; }
        public IList<Course> Courses { get; set; }
        public IList<SystemUser> DepartmentUsers { get; set; }
    }
}
