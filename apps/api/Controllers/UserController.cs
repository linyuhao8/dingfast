using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Users.Models;
using Shared.Domain.Users.Dtos;
using user.Services;
using System;
using System.Threading.Tasks;
using Api.Controllers;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/users")]
public class UserController : ApiBaseController
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await _userService.GetAllUsersAsync();
        return FromApiResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var response = await _userService.GetUserByIdAsync(id);
        return FromApiResponse(response);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var response = await _userService.CreateUserAsync(dto);
        return FromApiResponse(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
    {
        var response = await _userService.UpdateUserAsync(id, dto);
        if (!response.Success)
            return Fail(response.Message ?? "更新失敗");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var response = await _userService.DeleteUserAsync(id);
        if (!response.Success)
            return Fail(response.Message ?? "刪除失敗");
        return NoContent();
    }

    [HttpPut("password")]
    [Authorize]
    public async Task<IActionResult> UpdatePassword(Guid userId, [FromBody] UpdatePasswordDto dto)
    {
        var response = await _userService.UpdatePasswordAsync(userId, dto);
        return FromApiResponse(response);
    }

}