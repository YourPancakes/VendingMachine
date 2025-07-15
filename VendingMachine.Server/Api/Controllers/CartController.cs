using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.DTOs.Cart;
using VendingMachine.Application.Services;

namespace Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
[Produces("application/json")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCart()
    {
        var result = await _cartService.GetCartAsync();
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
    {
        var result = await _cartService.AddToCartAsync(addToCartDto);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemDto updateCartItemDto)
    {
        var result = await _cartService.UpdateCartItemAsync(updateCartItemDto);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpDelete("remove/{cartItemId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveFromCart(int cartItemId)
    {
        var result = await _cartService.RemoveCartItemByIdAsync(cartItemId);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpDelete("clear")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ClearCart()
    {
        var result = await _cartService.ClearCartAsync();
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }

    [HttpPost("purchase")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProcessPurchase()
    {
        var result = await _cartService.ProcessPurchaseAsync();
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpGet("total")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCartTotal()
    {
        var result = await _cartService.GetCartTotalAsync();
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }
} 