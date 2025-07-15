namespace VendingMachine.Application.DTOs.Product;

public record ProductFilterDto
{
    public int? BrandId { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public string? Name { get; init; }
    public bool? InStock { get; init; }
} 