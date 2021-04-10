using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System.Collections.Generic;
using System.Linq;

namespace StudentsApplicationProj.Server.Services
{
    public interface IInstructorService
    {
        List<StudentCourse> GetApplicationList(int instructorId);
        bool AcceptOrDeclineApplication(int applicationId, ApplicationStatus status);
    }

    public class InstructorService : IInstructorService
    {
        private readonly StudentDbContext _context;
        public InstructorService(StudentDbContext context)
        {
            _context = context;
        }

        public bool AcceptOrDeclineApplication(int applicationId, ApplicationStatus status)
        {
            if (status == ApplicationStatus.ApprovedByInstructor || status == ApplicationStatus.Declined)
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
