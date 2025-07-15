using FluentResults;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.DTOs.Brand;

namespace VendingMachine.Application.Services;

public interface IBrandService
{
    Task<Result<IEnumerable<BrandDto>>> GetAllAsync();
    Task<Result<BrandDto?>> GetByIdAsync(int id);
    Task<Result<BrandDto>> CreateAsync(CreateBrandDto createBrandDto);
    Task<Result<BrandDto>> UpdateAsync(int id, UpdateBrandDto updateBrandDto);
    Task<Result> DeleteAsync(int id);
} 