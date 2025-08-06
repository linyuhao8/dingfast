
using Api.Controllers;
using auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Domain.Users.Dtos;
using Api.Commons;

[ApiController]
[Route("api/auth")]
public class AuthController : ApiBaseController
{
    public readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
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

        var token = result.Data!;
        Response.Cookies.Append("dingfast-jwt-token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        // 成功時回傳 ApiResponse 成功格式（可自訂訊息）
        return Ok(ApiResponse<string>.Ok("登入成功"));
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

}