using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.MerchantCategories.Models;
using Shared.Domain.Merchants.Dtos;
using Api.Commons;
using Application.Merchants.Services;

[Route("api/[controller]")]
[ApiController]
public class MerchantController : ApiBaseController
{
    private readonly IMerchantService _service;

    public MerchantController(IMerchantService service)
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
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return FromApiResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MerchantDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return FromApiResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MerchantDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return FromApiResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        return FromApiResponse(result);
    }
}
