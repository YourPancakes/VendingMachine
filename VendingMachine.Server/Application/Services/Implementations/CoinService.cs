using AutoMapper;
using FluentResults;
using VendingMachine.Application.DTOs.Cart;
using VendingMachine.Application.DTOs.Coin;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Application.Services;

public class CoinService : ICoinService
{
    private readonly IProductRepository _productRepository;
    private readonly ICoinRepository _coinRepository;
    private readonly IMapper _mapper;

    public CoinService(IProductRepository productRepository, 
        ICoinRepository coinRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _coinRepository = coinRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CoinDto>>> GetAllCoinsAsync()
    {
        var coins = await _coinRepository.GetAllAsync();
        var coinDtos = _mapper.Map<IEnumerable<CoinDto>>(coins);
        return Result.Ok(coinDtos);
    }

    public async Task<Result<CoinDto>> UpdateInsertedQuantityAsync(int coinId, int insertedQuantity)
    {
        var coins = await _coinRepository.GetAllAsync();
        var coin = coins.FirstOrDefault(c => c.Id == coinId);
        
        if (coin == null)
        {
            return Result.Fail<CoinDto>("Coin not found");
        }

        if (insertedQuantity < 0)
        {
            return Result.Fail<CoinDto>("Quantity cannot be negative");
        }

        coin.Quantity = insertedQuantity;
        var updatedCoin = await _coinRepository.UpdateAsync(coin);
        var coinDto = _mapper.Map<CoinDto>(updatedCoin);

        return Result.Ok(coinDto);
    }

    public async Task<Result<ChangeCalculationDto>> CalculateDetailedChangeAsync(decimal cartTotal)
    {
        var coins = await _coinRepository.GetAllAsync();
        
        var insertedAmount = coins.Sum(c => (decimal)c.Denomination * c.Quantity);

        if (insertedAmount < cartTotal)
        {
            return Result.Ok(new ChangeCalculationDto
            {
                CanPurchase = false,
                Message = "Sorry, we cannot sell you the product at the moment because the machine cannot provide you with the necessary change."
            });
        }

        var changeAmount = insertedAmount - cartTotal;
        if (changeAmount <= 0)
        {
            return Result.Ok(new ChangeCalculationDto
            {
                CanPurchase = true,
                ChangeAmount = 0,
                Message = "Thank you for your purchase, please take your change."
            });
        }

        var insertedCoins = coins.Where(c => c.Quantity > 0).OrderByDescending(c => c.Denomination).ToList();
        var changeCoins = new List<ChangeCoinDto>();
        var remainingChange = changeAmount;

        foreach (var coin in insertedCoins)
        {
            var coinValue = (decimal)coin.Denomination;
            var maxCoinsToUse = Math.Min(coin.Quantity, (int)(remainingChange / coinValue));
            
            if (maxCoinsToUse > 0)
            {
                changeCoins.Add(new ChangeCoinDto
                {
                    Denomination = coinValue,
                    Quantity = maxCoinsToUse,
                    TotalValue = coinValue * maxCoinsToUse
                });
                
                remainingChange -= coinValue * maxCoinsToUse;
            }

            if (remainingChange <= 0)
            {
                break;
            }
        }

        if (remainingChange > 0)
        {
            var availableDenominations = new[] { 10m, 5m, 2m, 1m };
            
            foreach (var denomination in availableDenominations)
            {
                var maxCoinsToUse = (int)(remainingChange / denomination);
                
                if (maxCoinsToUse > 0)
                {
                    changeCoins.Add(new ChangeCoinDto
                    {
                        Denomination = denomination,
                        Quantity = maxCoinsToUse,
                        TotalValue = denomination * maxCoinsToUse
                    });
                    
                    remainingChange -= denomination * maxCoinsToUse;
                }

                if (remainingChange <= 0)
                {
                    break;
                }
            }
        }

        if (remainingChange > 0)
        {
            return Result.Ok(new ChangeCalculationDto
            {
                CanPurchase = false,
                Message = "Sorry, we cannot sell you the product at the moment because the machine cannot provide you with the necessary change."
            });
        }

        return Result.Ok(new ChangeCalculationDto
        {
            CanPurchase = true,
            ChangeAmount = changeAmount,
            ChangeCoins = changeCoins,
            Message = "Thank you for your purchase, please take your change."
        });
    }

    public async Task<Result<bool>> HasSufficientChangeAsync(decimal amount)
    {
        if (amount <= 0)
        {
            return Result.Ok(true);
        }

        var coins = await _coinRepository.GetAllAsync();
        var availableChange = coins.Sum(c => (decimal)c.Denomination * c.Quantity);

        return Result.Ok(availableChange >= amount);
    }

} 