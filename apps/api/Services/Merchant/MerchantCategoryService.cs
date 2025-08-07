using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Domain.MerchantCategories.Models;
using Shared.Domain.Merchants.Models;
using Api.Commons;
using Shared.Domain.Merchants.Dtos;

public class MerchantCategoryService
{
    private readonly IMerchantCategoryRepository _repo;

    public MerchantCategoryService(IMerchantCategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<List<MerchantCategory>>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();
        return ApiResponse<List<MerchantCategory>>.Ok(data);
    }

    public async Task<ApiResponse<MerchantCategory?>> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null)
            return ApiResponse<MerchantCategory?>.Fail("Category not found");
        
        return ApiResponse<MerchantCategory?>.Ok(category);
    }

    public async Task<ApiResponse<MerchantCategory>> CreateAsync(MerchantCategoryCreateDto dto)
    {       
         var entity = new MerchantCategory
        { 
            Name = dto.Name,
            Description = dto.Description,
            ImageId = dto.ImageId
        };
        var created = await _repo.CreateAsync(entity);
        return ApiResponse<MerchantCategory>.Ok(created, "Category created successfully");
    }

public async Task<ApiResponse<MerchantCategory>> UpdateAsync(int id, MerchantCategoryUpdateDto dto)
{
    var entity = await _repo.GetByIdAsync(id);
    if (entity == null)
        return ApiResponse<MerchantCategory>.Fail("資料不存在");

    entity.Name = dto.Name;
    entity.Description = dto.Description;
    entity.ImageId = dto.ImageId;

    await _repo.UpdateAsync(entity);

    return ApiResponse<MerchantCategory>.Ok(entity);
}

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
        return ApiResponse<string>.Ok("OK", "Category deleted");
    }

    public async Task<ApiResponse<string>> AssignMerchantToCategory(Guid merchantId, int categoryId)
    {
        await _repo.AssignMerchantAsync(merchantId, categoryId);
        return ApiResponse<string>.Ok("OK", "Merchant assigned to category");
    }

    public async Task<ApiResponse<List<Merchant>>> GetMerchantsByCategory(int categoryId)
    {
        var merchants = await _repo.GetMerchantsByCategoryAsync(categoryId);
        return ApiResponse<List<Merchant>>.Ok(merchants);
    }
}
