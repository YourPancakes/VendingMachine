using Microsoft.EntityFrameworkCore;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Infrastructure.Persistence.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetCartAsync()
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .ThenInclude(p => p.Brand)
            .FirstOrDefaultAsync();
        
        return cart;
    }

    public async Task<Cart> CreateCartAsync()
    {
        var cart = new Cart();
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<CartItem?> GetCartItemAsync(int productId)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .ThenInclude(p => p.Brand)
            .FirstOrDefaultAsync(ci => ci.ProductId == productId);
    }

    public async Task<CartItem?> GetCartItemByIdAsync(int cartItemId)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .ThenInclude(p => p.Brand)
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
    }

    public async Task<List<CartItem>> GetCartItemsByProductIdAsync(int productId)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .ThenInclude(p => p.Brand)
            .Where(ci => ci.ProductId == productId)
            .ToListAsync();
    }

    public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
    {
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
        
        return cartItem;
    }

    public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();
        return cartItem;
    }

    public async Task RemoveCartItemAsync(int productId)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == productId);
        
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveCartItemByIdAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ClearCartAsync()
    {
        var cart = await GetCartAsync();
        if (cart != null)
        {
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
} 