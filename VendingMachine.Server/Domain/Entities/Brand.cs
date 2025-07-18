namespace VendingMachine.Domain.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
} 