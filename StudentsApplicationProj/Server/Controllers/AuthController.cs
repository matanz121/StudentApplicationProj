﻿using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Models;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login(LoginRequest model)
        {
            var result = _authService.Login(model);
            if(result != null)
            {
                return Ok(new UserToken
                {
                    Token = _tokenService.GenerateToken(result),
                    Name = result.FirstName,
                    UserRole = result.UserRole
                });
            }
            return BadRequest();
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var result = await _authService.Register(model);
            if (result)
            {
                return Ok(new { });
            }
            return BadRequest();
        }
    }
}
