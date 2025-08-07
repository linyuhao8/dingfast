using Microsoft.EntityFrameworkCore;
using Shared.Domain.MerchantCategories.Models;
using Shared.Domain.Merchants.Models;

public class MerchantCategoryRepository : IMerchantCategoryRepository
{
    private readonly AppDbContext _db;

    public MerchantCategoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<MerchantCategory>> GetAllAsync()
    {
        return await _db.MerchantCategories
            .Include(c => c.Image)
            .ToListAsync();
    }

    public async Task<MerchantCategory?> GetByIdAsync(int id)
    {
        return await _db.MerchantCategories
            .Include(c => c.Image)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<MerchantCategory> CreateAsync(MerchantCategory category)
    {
        _db.MerchantCategories.Add(category);
        await _db.SaveChangesAsync();
        return category;
    }

    public async Task UpdateAsync(MerchantCategory category)
    {
        _db.MerchantCategories.Update(category);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await GetByIdAsync(id);
        if (category != null)
        {
            _db.MerchantCategories.Remove(category);
            await _db.SaveChangesAsync();
        }
    }

    public async Task AssignMerchantAsync(Guid merchantId, int categoryId)
    {
        var mapping = new MerchantCategoryMapping
        {
            MerchantId = merchantId,
            CategoryId = categoryId
        };

        _db.MerchantCategoryMappings.Add(mapping);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Merchant>> GetMerchantsByCategoryAsync(int categoryId)
    {
        return await _db.MerchantCategoryMappings
            .Where(m => m.CategoryId == categoryId)
            .Include(m => m.Merchant)
            .Select(m => m.Merchant)
            .ToListAsync();
    }
}
