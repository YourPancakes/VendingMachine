using Microsoft.EntityFrameworkCore;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Enums;
using VendingMachine.Infrastructure.Persistence;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Infrastructure.Persistence.Repositories;

public class CoinRepository : ICoinRepository
{
    private readonly AppDbContext _context;

    public CoinRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Coin>> GetAllAsync()
    {
        return await _context.Coins.OrderBy(c => c.Denomination).ToListAsync();
    }

    public async Task<Coin?> GetByDenominationAsync(CoinDenomination denomination)
    {
        return await _context.Coins.FirstOrDefaultAsync(c => c.Denomination == denomination);
    }

    public async Task<Coin> AddAsync(Coin coin)
    {
        _context.Coins.Add(coin);
        await _context.SaveChangesAsync();
        return coin;
    }

    public async Task<Coin> UpdateAsync(Coin coin)
    {
        _context.Coins.Update(coin);
        await _context.SaveChangesAsync();
        return coin;
    }

    public async Task DeleteAsync(int id)
    {
        var coin = await _context.Coins.FindAsync(id);
        if (coin != null)
        {
            _context.Coins.Remove(coin);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Coins.AnyAsync(c => c.Id == id);
    }
} 