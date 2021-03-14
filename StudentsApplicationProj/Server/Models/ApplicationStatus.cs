namespace StudentsApplicationProj.Server.Models
{
    public enum ApplicationStatus
    {
        Created = 1,
        ApprovedByInstructor = 2,
        DeclinedByInstructor = 3,
        ApprovedByDeptHead = 4,
        DeclinedByDeptHead = 5,
        AppealedByStudent = 6,
        ApprovedByAll = 7
    }
}
