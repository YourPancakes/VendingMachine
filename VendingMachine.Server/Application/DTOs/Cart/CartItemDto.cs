namespace VendingMachine.Application.DTOs.Cart
{
    public record CartItemDto
    {
        public int Id { get; init; }
        public int ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string BrandName { get; init; } = string.Empty;
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public decimal Subtotal => UnitPrice * Quantity;
    }
}
