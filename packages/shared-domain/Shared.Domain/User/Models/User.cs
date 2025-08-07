using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.Users.Enums;
using Shared.Domain.Commons;
using Shared.Domain.Images.Models;

namespace Shared.Domain.Users.Models;

[Table("users")]
public class User : BaseEntity
{

    public string? Name { get; set; }

    [Required(ErrorMessage = "Email 為必填")]
    [EmailAddress(ErrorMessage = "Email 格式錯誤")]
    public required string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "密碼為必填")]
    [MinLength(6, ErrorMessage = "密碼長度至少為 6 個字元")]
    public required string Password { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public UserRole Role { get; set; } = UserRole.Customer;
    
    public ICollection<Image> Images { get; set; } = new List<Image>();

}
