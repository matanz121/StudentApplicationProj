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
        List<UserAccount> GetInstructors(int departmentId);
        bool AddCourse(Course course);
        bool ApproveAccount(int accountId);
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
                .ThenInclude(x => x.UserAccount)
                .Include(x => x.CourseApplication)
                .ThenInclude(x => x.FileUrls)
                .ToList();
        }

        public List<Department> GetDepartments()
        {
            return _context.Department
                .ToList();
        }

        public List<UserAccount> GetInstructors(int departmentId)
        {
            return _context.SystemUser
                .Where(x => x.DepartmentId == departmentId && x.UserAccount.UserRole == UserRole.Instructor)
                .Select(x => x.UserAccount)
                .ToList();
        }

        public bool AddCourse(Course course)
        {
            try
            {
                _context.Course.Add(course);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ApproveAccount(int accountId)
        {
            var systemUser = _context.SystemUser
                .Where(x => x.UserAccountId == accountId)
                .Include(x => x.UserAccount)
                .FirstOrDefault();
            if(systemUser == null || systemUser.UserAccount == null)
            {
                return false;
            }
            try
            {
                systemUser.UserAccount.AccountStatus = true;
                if(systemUser.UserAccount.UserRole == UserRole.DepartmentHead)
                {
                    var dept = _context.Department
                        .Where(x => x.Id == systemUser.DepartmentId)
                        .FirstOrDefault();
                    dept.DepartmentHeadId = systemUser.Id;
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
