using StudentsApplicationProj.Shared.Enum;

namespace StudentsApplicationProj.Server.Models
{
    public class SystemUser
    {
        public int Id { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
        public bool AccountStatus { get; set; }
    }
}
