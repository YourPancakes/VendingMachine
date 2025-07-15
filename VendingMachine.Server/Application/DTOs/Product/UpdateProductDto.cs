namespace VendingMachine.Application.DTOs;

public record UpdateProductDto(int Id, string Name, int BrandId, decimal Price, int Quantity); 