
namespace Shared.Domain.User.Models;

using Shared.Domain.User.Enums;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }          // 訪客可能沒名字
    public string? Email { get; set; }         // 訪客可能沒 Email
    public string? Password { get; set; }      // 訪客無密碼
    public string? PhoneNumber { get; set; }   // 可能必填（視需求）
    public string? Address { get; set; }       // 可能必填（視需求）
    public UserRole Role { get; set; } = UserRole.Guest;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
