using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Services
{
    public interface IDepartmentHeadService
    {
        List<StudentCourse> GetApplicationList(int departmentHeadId);
        Task<bool> AcceptOrDeclineApplication(int departmentHeadId, int applicationId, ApplicationStatus status, string message);
    }

    public class DepartmentHeadService : IDepartmentHeadService
    {
        private readonly StudentDbContext _context;
        private readonly IEmailSenderService _emailSenderService;

        public DepartmentHeadService(StudentDbContext context, IEmailSenderService emailSenderService)
        {
            _context = context;
            _emailSenderService = emailSenderService;
        }

        public async Task<bool> AcceptOrDeclineApplication(int departmentHeadId, int applicationId, ApplicationStatus status, string message)
        {
            if(status == ApplicationStatus.ApprovedByAll || status == ApplicationStatus.Declined)
            {
                var deptHead = _context.SystemUser.Where(x => x.Id == departmentHeadId).FirstOrDefault();
                var application = _context.CourseApplication
                .Where(x => x.Id == applicationId)
                .Include(x => x.StudentCourse)
                .ThenInclude(x => x.Student)
                .Include(x => x.StudentCourse)
                .ThenInclude(x => x.Course)
                .FirstOrDefault();
                if (application != null)
                {
                    try
                    {
                        application.Status = status;
                        application.NoteMessage = message;
                        application.NoteFrom = deptHead.FirstName;
                        await _context.SaveChangesAsync();
                        if(application.StudentCourse.Course != null && application.StudentCourse.Student != null)
                        {
                            string emailStatus = status == ApplicationStatus.Declined ? "declined by department head" : "approved by department head";
                            var emailModel = new SendGridModel
                            {
                                Subject = status != ApplicationStatus.Declined ? "Request Accepted by Department Head" : " Request declined by Department Head",
                                To = application.StudentCourse.Student.Email,
                                PlainText = "",
                                HtmlContent = $"<p> Your application for course {application.StudentCourse.Course.CourseName} has been {emailStatus}</p>"
                            };
                            await _emailSenderService.SendEmail(emailModel);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public List<StudentCourse> GetApplicationList(int departmentHeadId)
        {
            int departmentId = _context.Department
                .Where(x => x.DepartmentHeadId == departmentHeadId)
                .Select(x => x.Id)
                .FirstOrDefault();
            if(departmentId > 0)
            {
                var courseAsDepartmentHead =  _context.StudentCourse
                    .Where(x => x.Course.DepartmentId == departmentId)
                    .Include(x => x.Course)
                    .Include(x => x.Student)
                    .Include(x => x.CourseApplication)
                    .ThenInclude(x => x.FileUrls)
                    .Where(x => x.CourseApplication.Status == ApplicationStatus.ApprovedByInstructor)
                    .ToList();
                var courseAsInstructor = _context.StudentCourse
                    .Where(x => x.Course.CourseInstructorId == departmentHeadId)
                    .Include(x => x.Course)
                    .Include(x => x.Student)
                    .Include(x => x.CourseApplication)
                    .ThenInclude(x => x.FileUrls)
                    .Where(x => x.CourseApplication.Status == ApplicationStatus.Created)
                    .ToList();
                return courseAsDepartmentHead.Union(courseAsInstructor).ToList();
            }
            return new List<StudentCourse>();
        }
    }
}
