using VendingMachine.Domain.Enums;

namespace VendingMachine.Application.DTOs.Coin;

public record CoinDto(int Id, CoinDenomination Denomination, int Quantity); 