using Microsoft.AspNetCore.Http;

namespace StudentsApplicationProj.Server.Models
{
    public class FileUploadModel
    {
        public IFormFile GradeSheet { get; set; }
        public IFormFile Syllabus { get; set; }
        public IFormFile Certificate { get; set; }
    }
}
