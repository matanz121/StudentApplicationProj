using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Server.Services;
using StudentsApplicationProj.Shared.Enum;
using StudentsApplicationProj.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

        [HttpPost, Route("appeal/{applicationId}")]
        public async Task<IActionResult> AppealApplication(int applicationId, ApplicationRequestFormModel application)
        {
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if (userInfo.UserRole == UserRole.Student && userInfo.UserId > 0)
            {
                bool status = await _studentService.AppealForDeclinedApplication(applicationId, userInfo.UserId, application.ApplicationName, application.ApplicationBody);
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
        public async Task<IActionResult> AddApplication(ApplicationRequestFormModel application)
        {
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if (userInfo.UserRole == UserRole.Student && userInfo.UserId > 0 && application.CourseId > 0)
            {
                CourseApplication courseApplication = _mapper.Map<CourseApplication>(application);
                int id = await _studentService.AddNewApplication(userInfo.UserId, application.CourseId, courseApplication);
                if (id > 0)
                {
                    return Ok(new { Id = id});
                }
            }
            return BadRequest();
        }

        [HttpGet, Route("application/{applicationId}")]
        public async Task<IActionResult> GetApplication(int applicationId)
        {
            var userInfo = _tokenService.GetUserInfoFromToken(Request);
            if (userInfo.UserRole == UserRole.Student && userInfo.UserId > 0)
            {
                CourseApplication courseApplication = await _studentService.GetApplication(applicationId);
                if (courseApplication != null)
                {
                    var response = _mapper.Map<ApplicationRequestFormModel>(courseApplication);
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        [HttpPost, Route("upload-file/{applicationId}")]
        public async Task<IActionResult> UploadFile([FromForm]FileUploadModel model, int applicationId)
        
        {
            string gradesheetPath = null;
            string syllabusPath = null;
            string certificatePath = null;
            if(model.GradeSheet != null)
            {
                gradesheetPath = await SaveFile(model.GradeSheet);
            }
            if(model.Syllabus != null)
            {
                syllabusPath = await SaveFile(model.Syllabus);
            }
            if(model.Certificate != null)
            {
                certificatePath = await SaveFile(model.Certificate);
            }
            bool status = await _studentService.UpdateFilePath(applicationId, gradesheetPath, syllabusPath, certificatePath);
            if (status)
            {
                return Ok(new { });
            }
            return BadRequest();
        }

        private static async Task<string> SaveFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + '_' + file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\\Client\\wwwroot\\files", fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
                return fileName;
            }
            return null;
        }
    }
}
