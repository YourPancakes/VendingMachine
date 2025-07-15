using AutoMapper;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.DTOs.Brand;
using VendingMachine.Application.DTOs.Cart;
using VendingMachine.Application.DTOs.Coin;
using VendingMachine.Application.DTOs.MachineLock;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));
        
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        
        CreateMap<Brand, BrandDto>();
        CreateMap<BrandDto, Brand>();
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();
        
        CreateMap<Coin, CoinDto>();
        CreateMap<CoinDto, Coin>();
        
        CreateMap<MachineLock, MachineLockDto>();
        CreateMap<MachineLockDto, MachineLock>();
        
        CreateMap<Cart, CartDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));
        
        CreateMap<CartItem, CartItemDto>();
        CreateMap<CartItemDto, CartItem>();
    }
} 