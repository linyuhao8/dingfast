namespace Shared.Domain.User.Dtos
{
    using Shared.Domain.User.Enums;
    using System.ComponentModel.DataAnnotations;
    //送出API資料用
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public UserRole Role { get; set; } = UserRole.Guest;
    }
    //創建User
    public class CreateUserDto
    {
        // 訪客可能沒名字，所以不一定要必填
        public string? Name { get; set; }

        // Email 有需要可以加 [EmailAddress] 驗證
        [EmailAddress]
        public string? Email { get; set; }

        // 電話可選
        public string? PhoneNumber { get; set; }

        // 地址可選
        public string? Address { get; set; }

        // 預設角色為 Guest，建立時可選擇
        public UserRole Role { get; set; } = UserRole.Guest;

        // 建立時若是註冊用戶，密碼必填，訪客可不用
        [Required(ErrorMessage = "密碼為必填")]
        [MinLength(6, ErrorMessage = "密碼長度至少為 6 個字元")]
        public string Password { get; set; } = string.Empty;
    }
    //更新User資料不含Password
    public class UpdateUserDto
    {
        // 更新時通常 Id 不會在 DTO 裡帶，透過 URL 傳遞
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public UserRole? Role { get; set; }
    }

    public class UpdatePasswordDto
    {
        [Required]
        public required string CurrentPassword { get; set; }

        [Required]
        [MinLength(6)]
        public required string NewPassword { get; set; }
    }

}
