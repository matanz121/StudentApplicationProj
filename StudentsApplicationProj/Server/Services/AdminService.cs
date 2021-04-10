using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System.Collections.Generic;
using System.Linq;

namespace StudentsApplicationProj.Server.Services
{
    public interface IAdminService
    {
        List<StudentCourse> GetApplications();
        List<Department> GetDepartments();
        List<SystemUser> GetInstructors(int departmentId);
        List<SystemUser> GetAccountsToApprove();
        bool AddCourse(Course course);
        bool ApproveOrDeleteAccount(int accountId, bool approveOrDelete);
        bool AssignCourse(int courseId, int accountId);
    }

    public class AdminService: IAdminService
    {
        private readonly StudentDbContext _context;
        public AdminService(StudentDbContext context)
        {
            _context = context;
        }

        public List<StudentCourse> GetApplications()
        {
            return _context.StudentCourse
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Include(x => x.CourseApplication)
                .ThenInclude(x => x.FileUrls)
                .Where(x => x.CourseApplication.Status != ApplicationStatus.ApprovedByAll)
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

        public bool AddCourse(Course course)
        {
            try
            {
                course.DepartmentId = 1;
                _context.Course.Add(course);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ApproveOrDeleteAccount(int accountId, bool approveOrDelete)
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
                }
                else
                {
                    _context.SystemUser.Remove(systemUser);
                }
                _context.SaveChanges();
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
