using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsApplicationProj.Shared.Models
{
    public class ApplicationRequestFormModel
    {
        [Required]
        public int? CourseId { get; set; }
        [Required]
        [StringLength(50)]
        public string ApplicationName { get; set; }
        [Required]
        [StringLength(1000)]
        public string ApplicationBody { get; set; }
        public IList<IBrowserFile> Files { get; set; }
    }
}
