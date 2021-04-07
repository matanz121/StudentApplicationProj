﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Enum;
using StudentsApplicationProj.Shared.Models;
using System.Collections.Generic;

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
        public IActionResult AcceptOrDeclineApplication(CourseApplicationViewModel application)
        {
            bool status = _instructorService.AcceptOrDeclineApplication(application.Id, application.Status);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
