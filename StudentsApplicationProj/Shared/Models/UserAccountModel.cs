using StudentsApplicationProj.Shared.Enum;

namespace StudentsApplicationProj.Shared.Models
{
    public class UserAccountModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
        public bool AccountStatus { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
