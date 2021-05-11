using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Services
{
    public interface IStudentService
    {
        List<StudentCourse> GetApplicationList(int studentId);
        Task<int> AddNewApplication(int studentId, int courseId, CourseApplication studentCourse);
        Task<bool> AppealForDeclinedApplication(int applicationId, int studentId, string applicationName, string applicationBody);
        List<Course> GetCourses(int studentId);
        Task<bool> UpdateFilePath(int applicationId, string gradesheetPath, string syllabusPath, string certificatePath);
        Task<CourseApplication> GetApplication(int applicationId);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _context;
        private readonly IEmailSenderService _emailSenderService;

        public StudentService(StudentDbContext context, IEmailSenderService emailSenderService)
        {
            _context = context;
            _emailSenderService = emailSenderService;
        }

        public async Task<int> AddNewApplication(int studentId, int courseId, CourseApplication courseApplication)
        {
            try
            {
                courseApplication.Status = ApplicationStatus.Created;
                courseApplication.ApplicationDateTime = DateTime.Now;
                courseApplication.NoteMessage = "No comment yet";
                _context.StudentCourse.Add(new StudentCourse {
                    CourseId = courseId,
                    StudentId = studentId,
                    CourseApplication = courseApplication
                });
                await _context.SaveChangesAsync();
                var course = _context.Course
                    .Where(x => x.Id == courseId)
                    .Include(x => x.CourseInstructor)
                    .FirstOrDefault();
                var student = _context.SystemUser
                    .Where(x => x.Id == studentId)
                    .FirstOrDefault();
                if(course != null && student != null)
                {
                    var emailModel = new SendGridModel
                    {
                        Subject = "Exemption Request",
                        To = course.CourseInstructor.Email,
                        PlainText = "",
                        HtmlContent = $"<p> {student.FirstName} placed a new request for the course {course.CourseName} that is instructed by you.</p>"
                    };
                    await _emailSenderService.SendEmail(emailModel);
                }
                return courseApplication.Id;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<bool> AppealForDeclinedApplication(int applicationId, int studentId, string applicationName, string applicationBody)
        {
            try
            {
                var application = _context.CourseApplication
                    .Where(x => x.Id == applicationId && x.StudentCourse.StudentId == studentId)
                    .Include(x => x.StudentCourse)
                    .ThenInclude(x => x.Student)
                    .FirstOrDefault();
                if(application != null && application.StudentCourse != null)
                {
                    application.Status = ApplicationStatus.Created;
                    application.ApplicationName = applicationName;
                    application.ApplicationBody = applicationBody;
                    application.NoteMessage = "No new comments on this appeal yet";
                    await _context.SaveChangesAsync();
                    var course = _context.Course
                        .Where(x => x.Id == application.StudentCourse.CourseId)
                        .Include(x => x.CourseInstructor)
                        .FirstOrDefault();
                    if(course != null & course.CourseInstructor != null)
                    {
                        var emailModel = new SendGridModel
                        {
                            Subject = "Exemption Appeal",
                            To = course.CourseInstructor.Email,
                            PlainText = "",
                            HtmlContent = $"<p> {application.StudentCourse.Student.FirstName} appealed the request for the course {course.CourseName} that is instructed by you.</p>"
                        };
                        await _emailSenderService.SendEmail(emailModel);
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<StudentCourse> GetApplicationList(int studentId)
        {
            return _context.StudentCourse
                .Where(x => x.StudentId == studentId)
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Include(x => x.CourseApplication)
                .ThenInclude(x => x.FileUrls)
                .ToList();
        }

        public List<Course> GetCourses(int studentId)
        {
            int departmentId = _context.SystemUser
                .Where(x => x.Id == studentId)
                .Select(x => x.DepartmentId)
                .FirstOrDefault();
            if(departmentId > 0)
            {
                var alreadyAppliedCourses = _context.StudentCourse
                    .Where(x => x.StudentId == studentId)
                    .Select(x => x.CourseId)
                    .ToList();
                var courses = _context.Course
                    .Where(x => x.DepartmentId == departmentId)
                    .ToList();
                if(alreadyAppliedCourses != null && alreadyAppliedCourses.Count > 0)
                {
                    return courses.Where(item =>
                        !alreadyAppliedCourses.Any(id => id == item.Id))
                        .ToList();
                }
                return courses;
            }
            return new List<Course>();
        }

        public async Task<bool> UpdateFilePath(int applicationId, string gradesheetPath, string syllabusPath, string certificatePath)
        {
            bool status = false;
            var application = _context.CourseApplication
                .Where(x => x.Id == applicationId)
                .Include(x => x.FileUrls)
                .FirstOrDefault();
            if(application != null)
            {
                if(application.FileUrls == null)
                {
                    application.FileUrls = new FileUrl
                    {
                        GradeSheetPath = gradesheetPath,
                        SyllabusPath = syllabusPath,
                        CertificatePath = certificatePath
                    };
                }
                else
                {
                    application.FileUrls.GradeSheetPath = gradesheetPath;
                    application.FileUrls.SyllabusPath = syllabusPath;
                    application.FileUrls.CertificatePath = certificatePath;
                }
                try
                {
                    await _context.SaveChangesAsync();
                    status = true;
                }
                catch
                {
                    status = false;
                }
            }
            return status;
        }

        public async Task<CourseApplication> GetApplication(int applicationId)
        {
            return await _context.CourseApplication
                .Where(x => x.Id == applicationId)
                .Include(x => x.StudentCourse)
                .FirstOrDefaultAsync();
        }
    }
}
