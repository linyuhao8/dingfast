using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shared.Domain.Users.Models;
[Table("guests")]
public class Guest
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // 用來綁定當前訪客行為，例如 Session Id 或其他識別碼
    public string? SessionId { get; set; }

    // 暫時姓名，非必填
    public string? TempName { get; set; }

    // 暫時電話，非必填
    public string? TempPhone { get; set; }

    // 暫時 Email，非必填
    [EmailAddress]
    public string? TempEmail { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

