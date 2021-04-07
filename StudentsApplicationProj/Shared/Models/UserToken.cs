using StudentsApplicationProj.Shared.Enum;

namespace StudentsApplicationProj.Shared.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public UserRole UserRole { get; set; }
    }
}
