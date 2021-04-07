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
    [Authorize(Roles = "Administrator,Student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public StudentController(IStudentService studentService, ITokenService tokenService, IMapper mapper)
        {
            _studentService = studentService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost, Route("appeal")]
        public IActionResult AppealApplication(CourseApplicationViewModel application)
        {
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if (userInfo.UserRole == UserRole.Student && userInfo.UserId > 0)
            {
                bool status = _studentService.AppealForDeclinedApplication(application.Id, userInfo.UserId);
                if (status)
                {
                    return Ok(application);
                }
            }
            return BadRequest();
        }

        [HttpGet, Route("courses")]
        public IActionResult GetCourses()
        {
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if (userInfo.UserRole == UserRole.Student && userInfo.UserId > 0)
            {
                var courses = _studentService.GetCourses(userInfo.UserId);
                var result = _mapper.Map<IList<CourseModel>>(courses);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost, Route("application")]
        public IActionResult AddApplication(ApplicationRequestFormModel application)
        {
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if (userInfo.UserRole == UserRole.Student && userInfo.UserId > 0 && application.CourseId > 0)
            {
                CourseApplication courseApplication = _mapper.Map<CourseApplication>(application);
                bool status = _studentService.AddNewApplication(userInfo.UserId, application.CourseId, courseApplication);
                if (status)
                {
                    return Ok(application);
                }
            }
            return BadRequest();
        }
    }
}
