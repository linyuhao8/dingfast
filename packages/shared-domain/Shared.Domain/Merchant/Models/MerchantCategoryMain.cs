using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Images.Models;
using Shared.Domain.Merchants.Models;

namespace Shared.Domain.MerchantCategories.Models;

[Table("merchant_categories")]
public class MerchantCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid? ImageId { get; set; }

    [ForeignKey(nameof(ImageId))]
    public Image? Image { get; set; }

    public ICollection<MerchantCategoryMapping> MerchantCategoryMappings { get; set; } = new List<MerchantCategoryMapping>();
}
