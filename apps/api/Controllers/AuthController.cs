
using Api.Controllers;
using auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Domain.Users.Dtos;
using Api.Commons;
using Shared.Domain.Users.Models;
using System.Security.Claims;
using Shared.Domain.Users.Enums;
using user.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/auth")]
public class AuthController : ApiBaseController
{
    private readonly AuthService _authService;
    private readonly UserService _userService;

    public AuthController(AuthService authService, UserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto.Email, dto.Password);

        if (!result.Success)
        {
            // 失敗時回傳 ApiResponse 失敗格式
            return BadRequest(ApiResponse<string>.Fail(result.Message ?? "未知錯誤"));

        }

        var token = result.Data!.Token;
        var user = result.Data!.User;

        Response.Cookies.Append("dingfast-jwt-token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        // 成功時回傳 ApiResponse 成功格式（可自訂訊息）
        return Ok(ApiResponse<UserDto>.Ok(user, "登入成功"));
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // 清除 cookie 裡的 JWT Token
        Response.Cookies.Delete("dingfast-jwt-token", new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // 或 true，依你環境而定
            SameSite = SameSiteMode.Strict,
            Path = "/"
        });

        return Ok(ApiResponse<string>.Ok("登出成功"));
    }
    // 範例檢查 token 是否有效的 CheckAuth API
    [HttpGet("check-auth")]
    public async Task<IActionResult> CheckAuth()
    {
        var hasToken = Request.Cookies.TryGetValue("dingfast-jwt-token", out var tokenValue);
        if (!hasToken)
            return Unauthorized(ApiResponse<string>.Fail("Token not found"));

        var principal = _authService.ValidateTokenAndGetClaims(tokenValue);
        if (principal == null)
            return Unauthorized(ApiResponse<string>.Fail("Invalid or expired token"));

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(ApiResponse<string>.Fail("Invalid token data"));

        var userResponse = await _userService.GetUserByIdAsync(Guid.Parse(userId));
        if (!userResponse.Success || userResponse.Data == null)
            return Unauthorized(ApiResponse<string>.Fail("User not found"));

        var user = userResponse.Data;

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            Name = user.Name!,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address
        };

        return Ok(ApiResponse<UserDto>.Ok(userDto, "Token is valid"));
    }




}