using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.Services;

namespace Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
[Produces("application/json")]
public class CoinsController : ControllerBase
{
    private readonly ICoinService _coinService;
    private readonly ICartService _cartService;

    public CoinsController(ICoinService vendingService, ICartService cartService)
    {
        _coinService = vendingService;
        _cartService = cartService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _coinService.GetAllCoinsAsync();
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPut("{id}/quantity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateQuantity(int id, [FromBody] int quantity)
    {
        var result = await _coinService.UpdateInsertedQuantityAsync(id, quantity);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpGet("change")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetChange()
    {
        
        var cartResult = await _cartService.GetCartAsync();
        if (cartResult.IsFailed)
        {
            return BadRequest(cartResult.Errors);
        }
        
        var result = await _coinService.CalculateDetailedChangeAsync(cartResult.Value.Total);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }
} 