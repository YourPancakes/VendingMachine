using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.DTOs.Product;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Persistence;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Brand)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> FilterAsync(ProductFilterDto filter)
    {
        var query = _context.Products.Include(p => p.Brand).AsQueryable();

        if (filter.BrandId.HasValue)
        {
            query = query.Where(p => p.BrandId == filter.BrandId.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
        }

        if (filter.InStock.HasValue)
        {
            if (filter.InStock.Value)
            {
                query = query.Where(p => p.Quantity > 0);
            }
            else
            {
                query = query.Where(p => p.Quantity == 0);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }
} 