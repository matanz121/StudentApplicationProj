namespace StudentsApplicationProj.Server.Models
{
    public class FileUrl
    {
        public int Id { get; set; }
        public CourseApplication CourseApplication { get; set; }
        public int CourseApplicationId { get; set; }
        public string GradeSheetPath { get; set; }
        public string SyllabusPath { get; set; }
        public string CertificatePath { get; set; }
    }
}
