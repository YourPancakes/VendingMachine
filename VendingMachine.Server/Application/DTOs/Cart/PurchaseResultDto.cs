using VendingMachine.Application.DTOs.Coin;

namespace VendingMachine.Application.DTOs.Cart;

public record PurchaseResultDto
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public decimal TotalPaid { get; init; }
    public decimal ChangeAmount { get; init; }
    public List<ChangeCoinDto> ChangeCoins { get; init; } = new();
    public List<CartItemDto> PurchasedItems { get; init; } = new();
    public DateTime PurchaseTime { get; init; } = DateTime.UtcNow;
} 