using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Merchants.Models;
using Shared.Domain.MerchantCategories.Models;

namespace Shared.Domain.MerchantCategories.Models;

[Table("merchant_category_mappings")]
public class MerchantCategoryMapping
{
    public Guid MerchantId { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey(nameof(MerchantId))]
    public Merchant Merchant { get; set; } = null!;

    [ForeignKey(nameof(CategoryId))]
    public MerchantCategoryMain Category { get; set; } = null!;
}
