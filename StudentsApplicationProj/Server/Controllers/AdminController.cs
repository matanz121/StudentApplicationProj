using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                _mapper.Map<List<UserAccountModel>>(_adminService.GetInstructors(departmentId))
            );

        [HttpGet, Route("accountsToApprove")]
        public IActionResult GetAccountsToApprove() => Ok(
                _mapper.Map<List<UserAccountModel>>(_adminService.GetAccountsToApprove())
            );

        [HttpPost, Route("course")]
        public async Task<IActionResult> AddCourse(CourseModel courseModel)
        {
            var course = _mapper.Map<Course>(courseModel);
            var status = await _adminService.AddCourse(course);
            if (status)
            {
                return Ok(courseModel);
            }
            return BadRequest();
        }

        [HttpPut, Route("accounts/{approveOrDelete}")]
        public async Task<IActionResult> ApproveAccount(UserModel user, bool approveOrDelete)
        {
            var status = await _adminService.ApproveOrDeleteAccount(user.Id, approveOrDelete);
            if (status)
            {
                return Ok(user);
            }
            return BadRequest();
        }
    }
}
