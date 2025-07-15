namespace VendingMachine.Application.DTOs;

public record ProductDto(int Id, string Name, int BrandId, string BrandName, decimal Price, int Quantity); 