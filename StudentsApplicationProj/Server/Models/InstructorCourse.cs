namespace StudentsApplicationProj.Server.Models
{
    public class InstructorCourse
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public SystemUser Instructor { get; set; }
        public int InstructorId { get; set; }
    }
}
