namespace VendingMachine.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    
    public virtual Cart Cart { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
    
    public decimal Subtotal => UnitPrice * Quantity;
} 