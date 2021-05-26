using StudentsApplicationProj.Shared.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsApplicationProj.Shared.Models
{
    public class RegisterRequest
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "First name could not be longer than 25 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Last name could not be longer than 25 characters")]
        public string LastName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Password must be at least 8 characters", MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public UserRole UserRole { get; set; }
        public int? DepartmentId { get; set; }
    }
}
