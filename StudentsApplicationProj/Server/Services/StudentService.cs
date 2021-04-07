using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsApplicationProj.Server.Services
{
    public interface IStudentService
    {
        List<StudentCourse> GetApplicationList(int studentId);
        bool AddNewApplication(int studentId, int courseId, CourseApplication studentCourse);
        bool AppealForDeclinedApplication(int applicationId, int studentId);
        List<Course> GetCourses(int studentId);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _context;
        public StudentService(StudentDbContext context)
        {
            _context = context;
        }

        public bool AddNewApplication(int studentId, int courseId, CourseApplication courseApplication)
        {
            try
            {
                courseApplication.Status = ApplicationStatus.Created;
                courseApplication.ApplicationDateTime = DateTime.Now;
                _context.StudentCourse.Add(new StudentCourse {
                    CourseId = courseId,
                    StudentId = studentId,
                    CourseApplication = courseApplication
                });
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AppealForDeclinedApplication(int applicationId, int studentId)
        {
            try
            {
                var application = _context.CourseApplication
                    .Where(x => x.Id == applicationId && x.StudentCourse.StudentId == studentId)
                    .FirstOrDefault();
                _context.SaveChanges();
                if(application != null)
                {
                    application.Status = ApplicationStatus.AppealedByStudent;
                    _context.SaveChanges();
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
                .ThenInclude(x => x.UserAccount)
                .Include(x => x.CourseApplication)
                .ThenInclude(x => x.FileUrls)
                .ToList();
        }

        public List<Course> GetCourses(int studentId)
        {
            int departmentId = _context.SystemUser
                .Where(x => x.UserAccountId == studentId)
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
                        alreadyAppliedCourses.Any(id => id != item.Id))
                        .ToList();
                }
                return courses;
            }
            return new List<Course>();
        }
    }
}
