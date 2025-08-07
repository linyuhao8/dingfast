namespace Shared.Domain.Merchants.Dtos
{
    public class MerchantCategoryCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public Guid? ImageId { get; set; }
}

public class MerchantCategoryUpdateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public Guid? ImageId { get; set; }
}

}