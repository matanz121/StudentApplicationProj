using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Models;
using System.Threading.Tasks;

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
        public async Task<IActionResult> AcceptOrDeclineApplication(CourseApplicationViewModel application)
        {
            bool status = await _departmentHeadService.AcceptOrDeclineApplication(application.Id, application.Status, application.NoteMessage);
            if (status)
            {
                return Ok(application);
            }
            return BadRequest();
        }
    }
}
