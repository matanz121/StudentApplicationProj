using System.ComponentModel.DataAnnotations;

namespace StudentsApplicationProj.Shared.Models
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmNewPassword { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
