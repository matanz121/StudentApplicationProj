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
        private readonly ITokenService _tokenService;
        public DepartmentHeadController(IDepartmentHeadService departmentHeadService, ITokenService tokenService)
        {
            _departmentHeadService = departmentHeadService;
            _tokenService = tokenService;
        }

        [HttpPost, Route("acceptOrDecline")]
        public async Task<IActionResult> AcceptOrDeclineApplication(CourseApplicationViewModel application)
        {
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if(userInfo == null || userInfo.UserId <= 0)
            {
                return Unauthorized();
            }
            bool status = await _departmentHeadService.AcceptOrDeclineApplication(userInfo.UserId, application.Id, application.Status, application.NoteMessage);
            if (status)
            {
                return Ok(application);
            }
            return BadRequest();
        }
    }
}
