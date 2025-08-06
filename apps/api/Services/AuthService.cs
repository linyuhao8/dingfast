
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using user.Repositories;
using Shared.Domain.Users.Dtos;
using Api.Commons;
using Shared.Domain.Users.Models;

namespace auth.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthService(UserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<ApiResponse<string>> LoginAsync(string email, string password)
        {
            var user = await _userRepo.FindByEmailAsync(email);

            if (user == null)
                return ApiResponse<string>.Fail("找不到此 Email");
            //檢查hash password
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isValid)
                return ApiResponse<string>.Fail("密碼錯誤");
            var token = GenerateToken(user);

            return ApiResponse<string>.Ok(token);

        }
        public string GenerateToken(User user)
        {
            // 產生 JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
