using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Models;

namespace StudentsApplicationProj.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,DepartmentHead")]
    public class DepartmentHeadController : ControllerBase
    {
        private readonly IDepartmentHeadService _departmentHeadService;
        public DepartmentHeadController(IDepartmentHeadService departmentHeadService)
        {
            _departmentHeadService = departmentHeadService;
        }

        [HttpPost, Route("acceptOrDecline")]
        public IActionResult AcceptOrDeclineApplication(CourseApplicationViewModel application)
        {
            bool status = _departmentHeadService.AcceptOrDeclineApplication(application.Id, application.Status);
            if (status)
            {
                return Ok(application);
            }
            return BadRequest();
        }
    }
}
