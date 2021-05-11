using Microsoft.EntityFrameworkCore;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Services
{
    public interface IAuthService
    {
        SystemUser Login(LoginRequest loginModel);
        Task<bool> Register(RegisterRequest registerModel);
        Task<bool> UpdateProfile (UpdateProfileRequest updateProfileModel, int UserId);
        
        //Task<bool> ChangePassword(ChangePasswordRequest passwordModel);
    }

    public class AuthService : IAuthService
    {
        private readonly StudentDbContext _context;
        private readonly IEmailSenderService _emailSenderService;

        public AuthService(StudentDbContext context, IEmailSenderService emailSenderService)
        {
            _context = context;
            _emailSenderService = emailSenderService;
        }

        public SystemUser Login(LoginRequest loginModel)
        {
            var hashedPassword = HashPassword(loginModel.Password);
            return _context.SystemUser
                .Where(x => x.Email == loginModel.Email && x.Password == hashedPassword && x.AccountStatus == true)
                .FirstOrDefault();
        }

        public async Task<bool> Register(RegisterRequest registerModel)
        {
            var user = new SystemUser
            {
                DepartmentId = 1,
                Email = registerModel.Email,
                Password = HashPassword(registerModel.Password),
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserRole = registerModel.UserRole,
                AccountStatus = false
            };
            try
            {
                _context.SystemUser.Add(user);
                await _context.SaveChangesAsync();
                var emailModel = new SendGridModel
                {
                    Subject = "Account created",
                    To = user.Email,
                    PlainText = "",
                    HtmlContent = $"<p> Your account for {user.UserRole} has been created successfully, you can login after an admin approval</p>"
                };
                await _emailSenderService.SendEmail(emailModel);
                return true;
            }
            catch
            {
                return false;
            }
        }

                public async Task<bool> UpdateProfile(UpdateProfileRequest updateProfileModel, int UserId)
        {
            var systemUser = _context.SystemUser
                .Where(x => x.Id == UserId)
                .FirstOrDefault();
            if (systemUser == null)
            {
                return false;
            }
            try
            {
                systemUser.FirstName = updateProfileModel.FirstName;
                systemUser.LastName = updateProfileModel.LastName;

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public async Task<bool> ChangePassword(ChangePasswordRequest passwordModel)
        //{
        //    var systemUser = _context.SystemUser
        //        .Where(x => x.Email == passwordModel.Email)
        //        .FirstOrDefault();
        //    if (systemUser == null)
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        systemUser.FirstName = ChangePasswordRequest.FirstName;
        //        systemUser.LastName = ChangePasswordRequest.LastName;

        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        private string HashPassword(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
