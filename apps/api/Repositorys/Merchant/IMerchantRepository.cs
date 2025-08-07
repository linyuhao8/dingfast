using Shared.Domain.Merchants.Models;

namespace Application.Merchants.Repositories;

public interface IMerchantRepository
{
    Task<Merchant?> FindByIdAsync(Guid id);
    Task<List<Merchant>> GetAllAsync();
    Task AddAsync(Merchant merchant);
    Task UpdateAsync(Merchant merchant);
    Task DeleteAsync(Merchant merchant);
}
