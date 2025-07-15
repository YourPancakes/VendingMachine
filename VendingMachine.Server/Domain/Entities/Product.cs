namespace VendingMachine.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BrandId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public virtual Brand Brand { get; set; } = null!;
} 