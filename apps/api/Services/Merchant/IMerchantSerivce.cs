using Shared.Domain.Merchants.Dtos;
using Shared.Domain.Commons;
using Shared.Domain.Merchants.Models;
using Api.Commons;

namespace Application.Merchants.Services;

public interface IMerchantService
{
    Task<ApiResponse<List<Merchant>>> GetAllAsync();
    Task<ApiResponse<Merchant>> GetByIdAsync(Guid id);
    Task<ApiResponse<Merchant>> CreateAsync(MerchantDto dto);
    Task<ApiResponse<Merchant>> UpdateAsync(Guid id, MerchantDto dto);
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
