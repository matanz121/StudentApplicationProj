namespace StudentsApplicationProj.Server.Models
{
    public class SystemUser
    {
        public int Id { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public UserAccount UserAccount { get; set; }
        public int UserAccountId { get; set; }
    }
}
