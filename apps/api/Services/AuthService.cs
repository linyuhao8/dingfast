using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using user.Repositories;
using Shared.Domain.Users.Dtos;
using Api.Commons;
using Shared.Domain.Users.Models;
using Microsoft.AspNetCore.Http.HttpResults;

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
        public async Task<ApiResponse<LoginResultDto>> LoginAsync(string email, string password)
        {
            var user = await _userRepo.FindByEmailAsync(email);

            if (user == null)
                return ApiResponse<LoginResultDto>.Fail("找不到此 Email");

            // 檢查 hash password
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isValid)
                return ApiResponse<LoginResultDto>.Fail("密碼錯誤");

            var token = GenerateToken(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                Name = user.Name!,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };
            var result = new LoginResultDto
            {
                User = userDto,
                Token = token
            };

            return ApiResponse<LoginResultDto>.Ok(result, "登入成功");
        }


        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.Name, user.Name ?? "")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? ValidateTokenAndGetClaims(string? token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = true,
                    ValidIssuer = _config["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _config["Jwt:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal; // 驗證成功回傳 ClaimsPrincipal (包含所有 claims)
            }
            catch
            {
                return null;
            }
        }

    }
}
