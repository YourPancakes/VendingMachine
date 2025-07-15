using FluentResults;
using VendingMachine.Application.DTOs.Cart;
using VendingMachine.Application.DTOs.Coin;

namespace VendingMachine.Application.Services;

public interface ICoinService
{
    Task<Result<IEnumerable<CoinDto>>> GetAllCoinsAsync();
    Task<Result<CoinDto>> UpdateInsertedQuantityAsync(int coinId, int insertedQuantity);
    Task<Result<ChangeCalculationDto>> CalculateDetailedChangeAsync(decimal cartTotal);
    Task<Result<bool>> HasSufficientChangeAsync(decimal amount);
} 