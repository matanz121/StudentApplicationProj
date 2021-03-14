namespace StudentsApplicationProj.Server.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
        public bool AccountStatus { get; set; }
    }
}
