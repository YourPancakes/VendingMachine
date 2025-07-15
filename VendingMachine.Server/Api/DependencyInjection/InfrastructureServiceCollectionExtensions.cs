using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Services;
using VendingMachine.Application.Services.Implementations;
using VendingMachine.Infrastructure.Persistence;
using VendingMachine.Infrastructure.Persistence.Repositories;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Api.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICoinRepository, CoinRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IMachineLockRepository, MachineLockRepository>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICoinService, CoinService>();
        services.AddScoped<IMachineLockService, MachineLockService>();

        return services;
    }
} 