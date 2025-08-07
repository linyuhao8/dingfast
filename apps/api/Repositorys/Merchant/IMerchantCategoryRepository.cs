using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Domain.MerchantCategories.Models;
using Shared.Domain.Merchants.Models;

public interface IMerchantCategoryRepository
{
    Task<List<MerchantCategory>> GetAllAsync();
    Task<MerchantCategory?> GetByIdAsync(int id);
    Task<MerchantCategory> CreateAsync(MerchantCategory category);
    Task UpdateAsync(MerchantCategory category);
    Task DeleteAsync(int id);

    Task AssignMerchantAsync(Guid merchantId, int categoryId);
    Task<List<Merchant>> GetMerchantsByCategoryAsync(int categoryId);
}
