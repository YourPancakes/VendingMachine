using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(int id);
    Task<Brand> AddAsync(Brand brand);
    Task<Brand> UpdateAsync(Brand brand);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
} 