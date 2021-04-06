using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System.Collections.Generic;
using System.Linq;

namespace StudentsApplicationProj.Server.Services
{
    public interface IDepartmentHeadService
    {
        List<StudentCourse> GetApplicationList(int departmentHeadId);
        bool AcceptOrDeclineApplication(int applicationId, ApplicationStatus status);
    }

    public class DepartmentHeadService : IDepartmentHeadService
    {
        private readonly StudentDbContext _context;
        public DepartmentHeadService(StudentDbContext context)
        {
            _context = context;
        }

        public bool AcceptOrDeclineApplication(int applicationId, ApplicationStatus status)
        {
            if(status == ApplicationStatus.ApprovedByAll || status == ApplicationStatus.Declined)
            {
                var application = _context.CourseApplication
                .Where(x => x.Id == applicationId)
                .FirstOrDefault();
                if (application != null)
                {
                    try
                    {
                        application.Status = status;
                        _context.SaveChanges();
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
                return _context.StudentCourse
                    .Where(x => x.Course.DepartmentId == departmentId)
                    .Include(x => x.Course)
                    .Include(x => x.Student)
                    .ThenInclude(x => x.UserAccount)
                    .Include(x => x.CourseApplication)
                    .ThenInclude(x => x.FileUrls)
                    .Where(x => x.CourseApplication.Status == ApplicationStatus.ApprovedByInstructor)
                    .ToList();
            }
            return new List<StudentCourse>();
        }
    }
}
