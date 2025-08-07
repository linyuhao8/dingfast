using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Users.Models;
using Shared.Domain.Commons;
using Shared.Domain.Images.Models;
// using Shared.Domain.Image.Models;
// using Shared.Domain.Menu.Models;
// using Shared.Domain.MerchantCategory.Models;

namespace Shared.Domain.Merchants.Models;

[Table("merchants")]
public class Merchant : BaseEntity
{

    [ForeignKey("User")]
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    [Required]
    [MaxLength(255)]
    public string BusinessName { get; set; } = null!;

    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Feature { get; set; }

    [ForeignKey("MerchantLogo")]
    public Guid? MerchantLogoId { get; set; }
    public Image? MerchantLogo { get; set; }

    // Location 可以轉型成 Address/GeoLocation class（之後可改）
    public string? Location { get; set; }
    //之後改json
    public string? BusinessHours { get; set; }

    public bool IsActive { get; set; } = true;
    
     // 其他導航屬性（例如 Menu、MerchantCategory）也改成只單向保留
    // 例如：
    // public ICollection<Menu> Menus { get; set; } = new List<Menu>();
    // public ICollection<MerchantCategory> MerchantCategories { get; set; } = new List<MerchantCategory>();
}
