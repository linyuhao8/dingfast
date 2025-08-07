using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.MerchantCategories.Models;
using Shared.Domain.Merchants.Dtos;
using Api.Commons;

[ApiController]
[Route("api/[controller]")]
public class MerchantCategoryController : ApiBaseController
{
    private readonly MerchantCategoryService _service;

    public MerchantCategoryController(MerchantCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return FromApiResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return FromApiResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]  MerchantCategoryCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return FromApiResponse(result);
    }

[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, [FromBody] MerchantCategoryUpdateDto dto)
{
    var result = await _service.UpdateAsync(id, dto);
    return FromApiResponse(result);
}

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return FromApiResponse(result);
    }

    [HttpPost("{categoryId}/assign/{merchantId}")]
    public async Task<IActionResult> AssignMerchant(Guid merchantId, int categoryId)
    {
        var result = await _service.AssignMerchantToCategory(merchantId, categoryId);
        return FromApiResponse(result);
    }

    [HttpGet("{categoryId}/merchants")]
    public async Task<IActionResult> GetMerchantsByCategory(int categoryId)
    {
        var result = await _service.GetMerchantsByCategory(categoryId);
        return FromApiResponse(result);
    }
}
