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
        private readonly ITokenService _tokenService;
        public InstructorController(IInstructorService instructorService, ITokenService tokenService)
        {
            _instructorService = instructorService;
            _tokenService = tokenService;
        }

        [HttpPost, Route("acceptOrDecline")]
        public async Task<IActionResult> AcceptOrDeclineApplication(CourseApplicationViewModel application)
        {
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if (userInfo == null || userInfo.UserId <= 0)
            {
                return Unauthorized();
            }
            bool status = await _instructorService.AcceptOrDeclineApplication(userInfo.UserId, application.Id, application.Status, application.NoteMessage);
            if (status)
            {
                return Ok(application);
            }
            return BadRequest();
        }
    }
}
