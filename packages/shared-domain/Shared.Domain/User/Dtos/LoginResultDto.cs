using Shared.Domain.Users.Dtos;
public class LoginResultDto
{
    public UserDto User { get; set; } = default!;
    public string Token { get; set; } = default!;
}
