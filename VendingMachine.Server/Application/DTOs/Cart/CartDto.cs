namespace VendingMachine.Application.DTOs.Cart;

public record CartDto
{
    public int Id { get; init; }
    public List<CartItemDto> Items { get; init; } = new();
    public decimal Total => Items.Sum(item => item.Subtotal);
    public int TotalItems => Items.Sum(item => item.Quantity);
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
    public bool IsEmpty => !Items.Any();
}