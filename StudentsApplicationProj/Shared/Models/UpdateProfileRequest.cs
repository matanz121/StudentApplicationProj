using System.ComponentModel.DataAnnotations;

namespace StudentsApplicationProj.Shared.Models
{
    public class UpdateProfileRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}