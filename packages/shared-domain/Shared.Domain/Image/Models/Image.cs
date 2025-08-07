using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Users.Models;
using Shared.Domain.Merchants.Models;
using Shared.Domain.Commons;

namespace Shared.Domain.Images.Models;

[Table("images")]
public class Image : BaseEntity
{

    [Required]
    [MaxLength(255)]
    public string Filename { get; set; } = null!;

    [Required]
    [MaxLength(2048)]
    public string Url { get; set; } = null!;

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public User? User { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public int? Size { get; set; }

    [MaxLength(100)]
    public string? MimeType { get; set; }


    // Navigation Properties
    public ICollection<Merchant> MerchantsAsLogo { get; set; } = new List<Merchant>();
}
