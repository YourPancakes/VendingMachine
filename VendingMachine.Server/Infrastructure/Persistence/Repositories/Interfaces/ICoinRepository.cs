using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Enums;

namespace VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

public interface ICoinRepository
{
    Task<IEnumerable<Coin>> GetAllAsync();
    Task<Coin?> GetByDenominationAsync(CoinDenomination denomination);
    Task<Coin> AddAsync(Coin coin);
    Task<Coin> UpdateAsync(Coin coin);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
} 