using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Enum;
using StudentsApplicationProj.Shared.Models;
using System.Collections.Generic;

namespace StudentsApplicationProj.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IDepartmentHeadService _departmentHeadService;
        private readonly IInstructorService _instructorService;
        private readonly IStudentService _studentService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public ApplicationController(
            IAdminService adminService,
            IDepartmentHeadService departmentHeadService,
            IInstructorService instructorService,
            IStudentService studentService,
            ITokenService tokenService,
            IMapper mapper)
        {
            _adminService = adminService;
            _departmentHeadService = departmentHeadService;
            _instructorService = instructorService;
            _studentService = studentService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var applications = new List<StudentCourse>();
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if (userInfo.UserRole == UserRole.Administrator)
            {
                applications = _adminService.GetApplications();
            }
            else if (userInfo.UserRole == UserRole.DepartmentHead)
            {
                applications = _departmentHeadService.GetApplicationList(userInfo.UserId);
            }
            else if (userInfo.UserRole == UserRole.Instructor)
            {
                applications = _instructorService.GetApplicationList(userInfo.UserId);
            }
            else if (userInfo.UserRole == UserRole.Student)
            {
                applications = _studentService.GetApplicationList(userInfo.UserId);
            }
            var result = _mapper.Map<IList<CourseApplicationViewModel>>(applications);
            return Ok(result);
        }
    }
}
