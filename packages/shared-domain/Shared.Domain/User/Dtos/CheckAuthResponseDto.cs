namespace Shared.Domain.Users.Dtos;

public class CheckAuthResponseDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
    public string Name { get; set; } = "";
    // 需要也可以加更多欄位，例如 PhoneNumber、Address 等
}
