namespace VendingMachine.Application.DTOs;

public record CreateProductDto(string Name, int BrandId, decimal Price, int Quantity); 