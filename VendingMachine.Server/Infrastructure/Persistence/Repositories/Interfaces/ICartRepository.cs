using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartAsync();
    Task<Cart> CreateCartAsync();
    Task<CartItem?> GetCartItemAsync(int productId);
    Task<CartItem?> GetCartItemByIdAsync(int cartItemId);
    Task<List<CartItem>> GetCartItemsByProductIdAsync(int productId);
    Task<CartItem> AddCartItemAsync(CartItem cartItem);
    Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
    Task RemoveCartItemAsync(int productId);
    Task RemoveCartItemByIdAsync(int cartItemId);
    Task ClearCartAsync();
    Task SaveChangesAsync();
} 