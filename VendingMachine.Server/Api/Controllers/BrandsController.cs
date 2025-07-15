using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.DTOs.Brand;
using VendingMachine.Application.Services;

namespace Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
[Produces("application/json")]
public class BrandsController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _brandService.GetAllAsync();
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _brandService.GetByIdAsync(id);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBrandDto createBrandDto)
    {
        var result = await _brandService.CreateAsync(createBrandDto);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandDto updateBrandDto)
    {
        var result = await _brandService.UpdateAsync(id, updateBrandDto);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _brandService.DeleteAsync(id);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
} 