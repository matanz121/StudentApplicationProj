﻿using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Services
{
    public interface IAuthService
    {
        UserAccount Login(LoginRequest loginModel);
        Task<UserAccount> Register(RegisterRequest registerModel);
    }

    public class AuthService : IAuthService
    {
        private readonly StudentDbContext _context;
        public AuthService(StudentDbContext context)
        {
            _context = context;
        }

        public UserAccount Login(LoginRequest loginModel)
        {
            var hashedPassword = HashPassword(loginModel.Password);
            return _context.UserAccount
                .Where(x => x.Email == loginModel.Email && x.Password == hashedPassword)
                .FirstOrDefault();
        }

        public async Task<UserAccount> Register(RegisterRequest registerModel)
        {
            var user = new SystemUser
            {
                DepartmentId = 1,
                UserAccount = new UserAccount
                {
                    Email = registerModel.Email,
                    Password = HashPassword(registerModel.Password),
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserRole = UserRole.Student,
                    AccountStatus = false
                }
            };
            try
            {
                _context.SystemUser.Add(user);
                await _context.SaveChangesAsync();
                return user.UserAccount;
            }
            catch
            {
                return null;
            }
        }

        private string HashPassword(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
