using AutoMapper;
using FluentResults;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.DTOs.Product;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ProductDto>>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Result.Ok(productDtos);
    }

    public async Task<Result<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return Result.Ok<ProductDto?>(null);
        }
        
        var productDto = _mapper.Map<ProductDto>(product);
        return Result.Ok<ProductDto?>(productDto);
    }

    public async Task<Result<IEnumerable<ProductDto>>> FilterAsync(ProductFilterDto filter)
    {
        if (filter.BrandId == null && filter.MinPrice == null && filter.MaxPrice == null && 
            string.IsNullOrWhiteSpace(filter.Name) && filter.InStock == null)
        {
            return await GetAllAsync();
        }

        if (filter.MinPrice.HasValue && filter.MaxPrice.HasValue)
        {
            var validationResult = ValidatePriceRange(filter.MinPrice.Value, filter.MaxPrice.Value);
            if (validationResult.IsFailed)
            {
                return Result.Fail<IEnumerable<ProductDto>>(validationResult.Errors.First().Message);
            }
        }

        if (filter.BrandId.HasValue && !await _brandRepository.ExistsAsync(filter.BrandId.Value))
        {
            return Result.Fail<IEnumerable<ProductDto>>("Brand not found");
        }

        var products = await _productRepository.FilterAsync(filter);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Result.Ok(productDtos);
    }

    public async Task<Result<ProductDto>> CreateAsync(CreateProductDto createProductDto)
    {
        if (!await _brandRepository.ExistsAsync(createProductDto.BrandId))
        {
            return Result.Fail<ProductDto>("Brand not found");
        }
        
        var product = _mapper.Map<Product>(createProductDto);
        var createdProduct = await _productRepository.AddAsync(product);
        
        var productWithBrand = await _productRepository.GetByIdAsync(createdProduct.Id);
        var productDto = _mapper.Map<ProductDto>(productWithBrand);
        
        return Result.Ok(productDto);
    }

    public async Task<Result<ProductDto>> UpdateAsync(int id, UpdateProductDto updateProductDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return Result.Fail<ProductDto>("Product not found");
        }
        
        if (!await _brandRepository.ExistsAsync(updateProductDto.BrandId))
        {
            return Result.Fail<ProductDto>("Brand not found");
        }
        
        _mapper.Map(updateProductDto, existingProduct);
        var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
        
        var productWithBrand = await _productRepository.GetByIdAsync(id);
        var productDto = _mapper.Map<ProductDto>(productWithBrand);
        
        return Result.Ok(productDto);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        if (!await _productRepository.ExistsAsync(id))
        {
            return Result.Fail("Product not found");
        }
        
        await _productRepository.DeleteAsync(id);
        return Result.Ok();
    }

    private static Result ValidatePriceRange(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
        {
            return Result.Fail("Invalid price range");
        }
        return Result.Ok();
    }
} 