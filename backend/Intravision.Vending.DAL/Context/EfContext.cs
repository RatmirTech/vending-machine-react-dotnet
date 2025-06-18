using Intravision.Vending.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Intravision.Vending.DAL.Context;

public class EfContext : DbContext
{
    public EfContext(DbContextOptions<EfContext> options)
        : base(options)
    {
    }

    public DbSet<Brand> Brands => Set<Brand>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductImage> ProductImages => Set<ProductImage>();

    public DbSet<Coin> Coins => Set<Coin>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brands");
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id)
                  .ValueGeneratedNever();

            entity.Property(b => b.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasMany(b => b.Products)
                  .WithOne(p => p.Brand)
                  .HasForeignKey(p => p.BrandId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                  .ValueGeneratedNever();

            entity.Property(p => p.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(p => p.PriceInCents)
                  .IsRequired();

            entity.Property(p => p.QuantityInStock)
                  .IsRequired();

            entity.HasMany(p => p.Images)
                  .WithOne(img => img.Product)
                  .HasForeignKey(img => img.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("ProductImages");
            entity.HasKey(img => img.Id);
            entity.Property(img => img.Id)
                  .ValueGeneratedNever();

            entity.Property(img => img.ImageUrl)
                  .IsRequired()
                  .HasMaxLength(500);
        });

        modelBuilder.Entity<Coin>(entity =>
        {
            entity.ToTable("Coins");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
                  .ValueGeneratedNever();

            entity.Property(c => c.Denomination)
                  .IsRequired();

            entity.Property(c => c.Quantity)
                  .IsRequired();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id)
                  .ValueGeneratedNever();

            entity.Property(o => o.CreatedAt)
                  .IsRequired();

            entity.Property(o => o.TotalPrice)
                  .IsRequired();

            entity.HasMany(o => o.Items)
                  .WithOne(i => i.Order)
                  .HasForeignKey(i => i.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItems");
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Id)
                  .ValueGeneratedOnAdd();

            entity.Property(i => i.ProductName)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(i => i.BrandName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(i => i.UnitPrice)
                  .IsRequired();

            entity.Property(i => i.Quantity)
                  .IsRequired();
        });
    }
}