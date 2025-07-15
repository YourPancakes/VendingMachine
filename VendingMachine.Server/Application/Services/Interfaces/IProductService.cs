using FluentResults;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.DTOs.Product;

namespace VendingMachine.Application.Services;

public interface IProductService
{
    Task<Result<IEnumerable<ProductDto>>> GetAllAsync();
    Task<Result<ProductDto?>> GetByIdAsync(int id);
    Task<Result<IEnumerable<ProductDto>>> FilterAsync(ProductFilterDto filter);
    Task<Result<ProductDto>> CreateAsync(CreateProductDto createProductDto);
    Task<Result<ProductDto>> UpdateAsync(int id, UpdateProductDto updateProductDto);
    Task<Result> DeleteAsync(int id);
} 