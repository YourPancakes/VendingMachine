namespace VendingMachine.Application.DTOs.Coin;

public record ChangeCalculationDto
{
    public decimal ChangeAmount { get; init; }
    public List<ChangeCoinDto> ChangeCoins { get; init; } = new();
    public string Message { get; init; } = string.Empty;
    public bool CanPurchase { get; init; }
}