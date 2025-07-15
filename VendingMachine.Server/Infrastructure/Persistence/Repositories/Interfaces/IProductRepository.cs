using VendingMachine.Application.DTOs.Product;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> FilterAsync(ProductFilterDto filter);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
} 