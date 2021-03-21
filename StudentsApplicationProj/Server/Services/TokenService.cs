using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentsApplicationProj.Server.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserAccount user);
    }

    public class TokenService: ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(UserAccount user)
        {
            string secretKey = _config.GetValue<string>("key:secretKey");
            var byteKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(byteKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim("userid", user.Id.ToString()),
                new Claim("role", user.UserRole.ToString()),
                new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())
            };
            var jwtToken = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
