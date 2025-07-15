using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.DTOs.Product;
using VendingMachine.Application.Services;

namespace Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();
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
        var result = await _productService.GetByIdAsync(id);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }



    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Filter([FromQuery] int? brandId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] string? name, [FromQuery] bool? inStock)
    {
        var filter = new ProductFilterDto
        {
            BrandId = brandId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Name = name,
            InStock = inStock
        };

        var result = await _productService.FilterAsync(filter);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
    {
        var result = await _productService.CreateAsync(createProductDto);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateProductDto)
    {
        var result = await _productService.UpdateAsync(id, updateProductDto);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
} 