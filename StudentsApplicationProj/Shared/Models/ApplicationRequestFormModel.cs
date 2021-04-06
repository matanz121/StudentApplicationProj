using System.ComponentModel.DataAnnotations;

namespace StudentsApplicationProj.Shared.Models
{
    public class ApplicationRequestFormModel
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ApplicationName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string ApplicationBody { get; set; }
    }
}
