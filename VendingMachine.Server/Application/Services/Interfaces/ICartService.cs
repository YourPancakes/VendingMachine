using FluentResults;
using VendingMachine.Application.DTOs.Cart;

namespace VendingMachine.Application.Services;

public interface ICartService
{
    Task<Result<CartDto>> GetCartAsync();
    Task<Result<CartDto>> AddToCartAsync(AddToCartDto addToCartDto);
    Task<Result<CartDto>> UpdateCartItemAsync(UpdateCartItemDto updateCartItemDto);
    Task<Result<CartDto>> RemoveCartItemByIdAsync(int cartItemId);
    Task<Result> ClearCartAsync();
    Task<Result<decimal>> GetCartTotalAsync();
    Task<Result<PurchaseResultDto>> ProcessPurchaseAsync();
} 