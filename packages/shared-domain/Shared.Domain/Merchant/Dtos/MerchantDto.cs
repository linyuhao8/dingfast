namespace Shared.Domain.Merchants.Dtos;

public class MerchantDto
{
    public Guid? UserId { get; set; }
    public string BusinessName { get; set; } = null!;
    public string? Description { get; set; }
    public string? Feature { get; set; }
    public Guid? MerchantLogoId { get; set; }
    public string? Location { get; set; }
    public string? BusinessHours { get; set; }
    public bool IsActive { get; set; } = true;
}
