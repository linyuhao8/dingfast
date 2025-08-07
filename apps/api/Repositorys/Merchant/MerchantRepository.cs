using Application.Merchants.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Merchants.Models;

namespace Application.Merchants.Repositories;

public class MerchantRepository : IMerchantRepository
{
    private readonly AppDbContext _context;

    public MerchantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Merchant>> GetAllAsync()
    {
        return await _context.Set<Merchant>().ToListAsync();
    }

    public async Task<Merchant?> FindByIdAsync(Guid id)
    {
        return await _context.Set<Merchant>().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Merchant merchant)
    {
        await _context.Set<Merchant>().AddAsync(merchant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Merchant merchant)
    {
        _context.Set<Merchant>().Update(merchant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Merchant merchant)
    {
        _context.Set<Merchant>().Remove(merchant);
        await _context.SaveChangesAsync();
    }
}
