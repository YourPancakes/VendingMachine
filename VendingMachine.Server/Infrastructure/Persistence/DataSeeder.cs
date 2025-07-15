using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Enums;

namespace VendingMachine.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Brands.Any())
        {
            var brands = new List<Brand>
            {
                new Brand { Name = "Coca-Cola" },
                new Brand { Name = "Pepsi" },
                new Brand { Name = "Sprite" },
                new Brand { Name = "Fanta" }
            };

            context.Brands.AddRange(brands);
            await context.SaveChangesAsync();
        }

        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Coca-Cola Classic", BrandId = 1, Price = 90.00m, Quantity = 20 },
                new Product { Name = "Coca-Cola Zero", BrandId = 1, Price = 90.00m, Quantity = 15 },
                new Product { Name = "Coca-Cola Cherry", BrandId = 1, Price = 95.00m, Quantity = 10 },
                new Product { Name = "Coca-Cola Vanilla", BrandId = 1, Price = 95.00m, Quantity = 10 },
                new Product { Name = "Pepsi Cola", BrandId = 2, Price = 90.00m, Quantity = 18 },
                new Product { Name = "Pepsi Max", BrandId = 2, Price = 90.00m, Quantity = 12 },
                new Product { Name = "Pepsi Lime", BrandId = 2, Price = 95.00m, Quantity = 10 },
                new Product { Name = "Sprite", BrandId = 3, Price = 90.00m, Quantity = 16 },
                new Product { Name = "Sprite Lemon Lime", BrandId = 3, Price = 95.00m, Quantity = 12 },
                new Product { Name = "Fanta Orange", BrandId = 4, Price = 90.00m, Quantity = 14 },
                new Product { Name = "Fanta Lemon", BrandId = 4, Price = 95.00m, Quantity = 14 },
                new Product { Name = "Fanta Grape", BrandId = 4, Price = 95.00m, Quantity = 10 },
                new Product { Name = "Fanta Pineapple", BrandId = 4, Price = 95.00m, Quantity = 10 }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }

        if (!context.Coins.Any())
        {
            var coins = new List<Coin>
            {
                new Coin { Denomination = CoinDenomination.One, Quantity = 0 },
                new Coin { Denomination = CoinDenomination.Two, Quantity = 0 },
                new Coin { Denomination = CoinDenomination.Five, Quantity = 0 },
                new Coin { Denomination = CoinDenomination.Ten, Quantity = 0 }
            };

            context.Coins.AddRange(coins);
            await context.SaveChangesAsync();
        }

        if (!context.Carts.Any())
        {
            var cart = new Cart();
            context.Carts.Add(cart);
            await context.SaveChangesAsync();
        }

        if (!context.MachineLocks.Any())
        {
            var machineLock = new MachineLock
            {
                IsLocked = false,
                LockedBy = null,
                LockTime = null
            };
            context.MachineLocks.Add(machineLock);
            await context.SaveChangesAsync();
        }
    }
} 