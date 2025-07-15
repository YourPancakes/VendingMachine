using Microsoft.EntityFrameworkCore;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<MachineLock> MachineLocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.HasOne(e => e.Brand)
                  .WithMany(e => e.Products)
                  .HasForeignKey(e => e.BrandId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Coin>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Denomination).HasConversion<int>();
            entity.HasIndex(e => e.Denomination).IsUnique();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.BrandName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
            entity.HasOne(e => e.Cart)
                  .WithMany(e => e.CartItems)
                  .HasForeignKey(e => e.CartId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Product)
                  .WithMany()
                  .HasForeignKey(e => e.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MachineLock>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LockedBy).HasMaxLength(100);
        });
    }
} 