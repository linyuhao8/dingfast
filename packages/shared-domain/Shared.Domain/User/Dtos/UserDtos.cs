using System.ComponentModel.DataAnnotations;
using Shared.Domain.User.Enums;

namespace Shared.Domain.User.Dtos
{
    // 一般展示使用者資料（回傳用）
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public UserRole Role { get; set; } = UserRole.Customer;
    }

    // 創建使用者（註冊用）
    public class CreateUserDto
    {
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email 為必填")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "密碼為必填")]
        [MinLength(6, ErrorMessage = "密碼長度至少為 6 個字元")]
        public string Password { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public UserRole Role { get; set; } = UserRole.Customer;
    }

    // 更新使用者（不含密碼）
    public class UpdateUserDto
    {
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public UserRole? Role { get; set; }
    }

    // 更新密碼
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "請輸入目前密碼")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "請輸入新密碼")]
        [MinLength(6, ErrorMessage = "新密碼至少要 6 個字元")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
