using Shared.Domain.Merchants.Dtos;
using Application.Merchants.Repositories;
using Api.Commons;
using Shared.Domain.Merchants.Models;

namespace Application.Merchants.Services;

public class MerchantService : IMerchantService
{
    private readonly IMerchantRepository _merchantRepo;

    public MerchantService(IMerchantRepository merchantRepo)
    {
        _merchantRepo = merchantRepo;
    }

    public async Task<ApiResponse<List<Merchant>>> GetAllAsync()
    {
        var merchants = await _merchantRepo.GetAllAsync();
        return ApiResponse<List<Merchant>>.Ok(merchants);
    }

    public async Task<ApiResponse<Merchant>> GetByIdAsync(Guid id)
    {
        var merchant = await _merchantRepo.FindByIdAsync(id);
        if (merchant == null)
            return ApiResponse<Merchant>.Fail("找不到商家");

        return ApiResponse<Merchant>.Ok(merchant);
    }

    public async Task<ApiResponse<Merchant>> CreateAsync(MerchantDto dto)
    {
        var merchant = new Merchant
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            BusinessName = dto.BusinessName,
            Description = dto.Description,
            Feature = dto.Feature,
            MerchantLogoId = dto.MerchantLogoId,
            Location = dto.Location,
            BusinessHours = dto.BusinessHours,
            IsActive = dto.IsActive,
        };

        await _merchantRepo.AddAsync(merchant);

        return ApiResponse<Merchant>.Ok(merchant);
    }

    public async Task<ApiResponse<Merchant>> UpdateAsync(Guid id, MerchantDto dto)
    {
        var merchant = await _merchantRepo.FindByIdAsync(id);
        if (merchant == null)
            return ApiResponse<Merchant>.Fail("商家不存在");

        merchant.UserId = dto.UserId;
        merchant.BusinessName = dto.BusinessName;
        merchant.Description = dto.Description;
        merchant.Feature = dto.Feature;
        merchant.MerchantLogoId = dto.MerchantLogoId;
        merchant.Location = dto.Location;
        merchant.BusinessHours = dto.BusinessHours;
        merchant.IsActive = dto.IsActive;

        await _merchantRepo.UpdateAsync(merchant);

        return ApiResponse<Merchant>.Ok(merchant);
    }

    public async Task<ApiResponse<string>> DeleteAsync(Guid id)
    {
        var merchant = await _merchantRepo.FindByIdAsync(id);
        if (merchant == null)
            return ApiResponse<string>.Fail("商家不存在");

        await _merchantRepo.DeleteAsync(merchant);
        return ApiResponse<string>.Ok("刪除成功");
    }
}
