using AutoMapper;
using FluentResults;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.DTOs.Brand;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Application.Services.Implementations;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<BrandDto>>> GetAllAsync()
    {
        var brands = await _brandRepository.GetAllAsync();
        var brandDtos = _mapper.Map<IEnumerable<BrandDto>>(brands);
        return Result.Ok(brandDtos);
    }

    public async Task<Result<BrandDto?>> GetByIdAsync(int id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);
        if (brand == null)
        {
            return Result.Ok<BrandDto?>(null);
        }
        var brandDto = _mapper.Map<BrandDto>(brand);
        return Result.Ok<BrandDto?>(brandDto);
    }

    public async Task<Result<BrandDto>> CreateAsync(CreateBrandDto createBrandDto)
    {
        var brand = _mapper.Map<Brand>(createBrandDto);
        var createdBrand = await _brandRepository.AddAsync(brand);
        var createdBrandDto = _mapper.Map<BrandDto>(createdBrand);
        return Result.Ok(createdBrandDto);
    }

    public async Task<Result<BrandDto>> UpdateAsync(int id, UpdateBrandDto updateBrandDto)
    {
        var existingBrand = await _brandRepository.GetByIdAsync(id);
        if (existingBrand == null)
        {
            return Result.Fail<BrandDto>("Brand not found");
        }

        _mapper.Map(updateBrandDto, existingBrand);
        var updatedBrand = await _brandRepository.UpdateAsync(existingBrand);
        var updatedBrandDto = _mapper.Map<BrandDto>(updatedBrand);
        return Result.Ok(updatedBrandDto);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);
        if (brand == null)
        {
            return Result.Fail("Brand not found");
        }

        await _brandRepository.DeleteAsync(id);
        return Result.Ok();
    }
} 