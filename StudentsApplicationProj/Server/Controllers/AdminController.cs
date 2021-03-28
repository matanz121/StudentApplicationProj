using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Models;


namespace StudentsApplicationProj.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpGet, Route("departments")]
        public IActionResult GetDepartments() => Ok(
                _mapper.Map<DepartmentModel>(_adminService.GetDepartments())
            );

        [HttpGet, Route("instructors/{departmentId}")]
        public IActionResult GetInstructors(int departmentId) => Ok(
                _mapper.Map<UserAccountModel>(_adminService.GetInstructors(departmentId))
            );

        [HttpPost, Route("course")]
        public IActionResult AddCourse(CourseModel courseModel)
        {
            var course = _mapper.Map<Course>(courseModel);
            var status = _adminService.AddCourse(course);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost, Route("accounts")]
        public IActionResult ApproveAccount(UserModel user)
        {
            var status = _adminService.ApproveAccount(user.Id);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet, Route("course/assign/{courseId}/{instructorId}")]
        public IActionResult ApproveAccount(int courseId, int instructorId)
        {
            var status = _adminService.AssignCourse(courseId, instructorId);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
