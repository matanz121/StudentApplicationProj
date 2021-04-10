using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Enum;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StudentsApplicationProj.Server.Services
{
    public interface ITokenService
    {
        string GenerateToken(SystemUser user);
        UserInfoFromToken GetUserInfoFromToken(HttpRequest Request);
    }

    public class TokenService: ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(SystemUser user)
        {
            string secretKey = _config.GetValue<string>("SecretKey");
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

        public UserInfoFromToken GetUserInfoFromToken(HttpRequest Request)
        {
            var userInfo = new UserInfoFromToken();
            try
            {
                string header = Request.Headers["Authorization"];
                string[] tokenArray = header.Split(' ');
                string accessToken = tokenArray[1];
                var jwtToken = new JwtSecurityToken(accessToken);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type.Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                userInfo.UserId = Int32.Parse(userIdClaim.Value);
                var userRoleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type.Equals("role", StringComparison.InvariantCultureIgnoreCase)).Value;
                userInfo.UserRole = (UserRole)Enum.Parse(typeof(UserRole), userRoleClaim);
                return userInfo;

            }
            catch
            {
                return userInfo;
            }
        }
    }

    public class UserInfoFromToken
    {
        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
