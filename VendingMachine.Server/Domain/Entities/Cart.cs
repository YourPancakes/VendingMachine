namespace VendingMachine.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    
    public decimal Total => CartItems.Sum(item => item.Subtotal);
    public int TotalItems => CartItems.Sum(item => item.Quantity);
    
    public bool IsEmpty => !CartItems.Any();
} 