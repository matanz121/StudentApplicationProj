using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Services
{
    public interface IInstructorService
    {
        List<StudentCourse> GetApplicationList(int instructorId);
        Task<bool> AcceptOrDeclineApplication(int instructorId, int applicationId, ApplicationStatus status, string message);
    }

    public class InstructorService : IInstructorService
    {
        private readonly StudentDbContext _context;
        private readonly IEmailSenderService _emailSenderService;
        public InstructorService(StudentDbContext context, IEmailSenderService emailSenderService)
        {
            _context = context;
            _emailSenderService = emailSenderService;
        }

        public async Task<bool> AcceptOrDeclineApplication(int instructorId, int applicationId, ApplicationStatus status, string message)
        {
            if (status == ApplicationStatus.ApprovedByInstructor || status == ApplicationStatus.Declined)
            {
                var instructor = _context.SystemUser.Where(x => x.Id == instructorId).FirstOrDefault();
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
                        application.NoteFrom = instructor.FirstName;
                        await _context.SaveChangesAsync();
                        if(application.StudentCourse.Course != null && application.StudentCourse.Student != null)
                        {
                            string emailStatus = status == ApplicationStatus.Declined ? "declined by instructor" : "approved by instructor";
                            var emailModel = new SendGridModel
                            {
                                Subject = status != ApplicationStatus.Declined ? "Request Accepted by Instructor" : " Request declined by Instructor",
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

        public List<StudentCourse> GetApplicationList(int instructorId)
        {
            return _context.StudentCourse
                .Where(x => x.Course.CourseInstructorId == instructorId)
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Include(x => x.CourseApplication)
                .ThenInclude(x => x.FileUrls)
                .Where(x => x.CourseApplication.Status == ApplicationStatus.Created)
                .ToList();
        }
    }
}
