using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Models;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,Instructor")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpPost, Route("acceptOrDecline")]
        public async Task<IActionResult> AcceptOrDeclineApplication(CourseApplicationViewModel application)
        {
            bool status = await _instructorService.AcceptOrDeclineApplication(application.Id, application.Status, application.NoteMessage);
            if (status)
            {
                return Ok(application);
            }
            return BadRequest();
        }
    }
}
