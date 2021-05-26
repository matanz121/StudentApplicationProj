using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Services
{
    public interface IAdminService
    {
        List<StudentCourse> GetApplications();
        List<Department> GetDepartments();
        List<SystemUser> GetInstructors(int departmentId);
        List<SystemUser> GetAccountsToApprove();
        Task<bool> AddCourse(Course course);
        Task<bool> ApproveOrDeleteAccount(int accountId, bool approveOrDelete);
        bool AssignCourse(int courseId, int accountId);
    }

    public class AdminService: IAdminService
    {
        private readonly StudentDbContext _context;
        private readonly IEmailSenderService _emailSenderService;

        public AdminService(StudentDbContext context, IEmailSenderService emailSenderService)
        {
            _context = context;
            _emailSenderService = emailSenderService;
        }

        public List<StudentCourse> GetApplications()
        {
            return _context.StudentCourse
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Include(x => x.CourseApplication)
                .ThenInclude(x => x.FileUrls)
                .Where(x => x.CourseApplication.Status != ApplicationStatus.ApprovedByAll && x.CourseApplication.Status != ApplicationStatus.Declined)
                .ToList();
        }

        public List<Department> GetDepartments()
        {
            return _context.Department
                .ToList();
        }

        public List<SystemUser> GetInstructors(int departmentId)
        {
            return _context.SystemUser
                .Include(x => x.Department)
                .Where(x => x.DepartmentId == departmentId && (x.UserRole == UserRole.Instructor || x.UserRole == UserRole.DepartmentHead))
                .ToList();
        }

        public List<SystemUser> GetAccountsToApprove()
        {
            return _context.SystemUser
                .Include(x => x.Department)
                .Where(x => x.AccountStatus == false)
                .ToList();
        }

        public async Task<bool> AddCourse(Course course)
        {
            try
            {
                _context.Course.Add(course);
                _context.SaveChanges();
                var instructor = _context.SystemUser
                    .Where(x => x.Id == course.CourseInstructorId)
                    .FirstOrDefault();
                if(instructor != null)
                {
                    var emailModel = new SendGridModel
                    {
                        Subject ="New Course Added",
                        To = instructor.Email,
                        PlainText = "",
                        HtmlContent = $"<p> Admin has assigned you a new course {course.CourseName}</p>"
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

        public async Task<bool> ApproveOrDeleteAccount(int accountId, bool approveOrDelete)
        {
            var systemUser = _context.SystemUser
                .Where(x => x.Id == accountId)
                .FirstOrDefault();
            if(systemUser == null)
            {
                return false;
            }
            try
            {
                if (approveOrDelete)
                {
                    systemUser.AccountStatus = true;
                    if (systemUser.UserRole == UserRole.DepartmentHead)
                    {
                        var dept = _context.Department
                            .Where(x => x.Id == systemUser.DepartmentId)
                            .FirstOrDefault();
                        dept.DepartmentHeadId = accountId;
                    }
                    var emailModel = new SendGridModel
                    {
                        Subject = "Account approval",
                        To = systemUser.Email,
                        PlainText = "",
                        HtmlContent = $"<p> Admin has approved your acount, you can sign in now.</p>"
                    };
                    await _emailSenderService.SendEmail(emailModel);
                }
                else
                {
                    _context.SystemUser.Remove(systemUser);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AssignCourse(int courseId, int userId)
        {
            var course = _context.Course
                .Where(x => x.Id == courseId)
                .FirstOrDefault();
            if(course == null)
            {
                return false;
            }
            try
            {
                course.CourseInstructorId = userId;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
