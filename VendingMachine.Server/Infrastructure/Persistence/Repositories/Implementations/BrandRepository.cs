using Microsoft.EntityFrameworkCore;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Persistence;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Infrastructure.Persistence.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly AppDbContext _context;

    public BrandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(int id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public async Task<Brand> AddAsync(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> UpdateAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task DeleteAsync(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand != null)
        {
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Brands.AnyAsync(b => b.Id == id);
    }
} 