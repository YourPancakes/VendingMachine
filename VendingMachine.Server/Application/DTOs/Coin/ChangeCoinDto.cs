namespace VendingMachine.Application.DTOs.Coin
{
    public record ChangeCoinDto
    {
        public decimal Denomination { get; init; }
        public int Quantity { get; init; }
        public decimal TotalValue { get; init; }
    }
}
