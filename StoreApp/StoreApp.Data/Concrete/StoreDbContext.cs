namespace StoreApp.Web.Data;

using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Product>()
            .HasMany(e => e.Categories)
            .WithMany(e => e.Products)
            .UsingEntity<ProductCategory>();

        modelBuilder.Entity<Category>().HasIndex(c => c.Url).IsUnique();

        modelBuilder
            .Entity<Product>()
            .HasData(
                new List<Product>
                {
                    new()
                    {
                        Id = 1,
                        Name = "Laptop 1",
                        Description = "A high-performance laptop for work and gaming.",
                        Price = 999.99m,
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Smartphone 2",
                        Description = "A sleek smartphone with a powerful camera.",
                        Price = 699.99m,
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Headphones 3",
                        Description = "Noise-cancelling headphones for immersive sound.",
                        Price = 199.99m,
                    },
                    new()
                    {
                        Id = 4,
                        Name = "Coffee Maker 4",
                        Description = "Brew the perfect cup of coffee every morning.",
                        Price = 49.99m,
                    },
                    new()
                    {
                        Id = 5,
                        Name = "Running Shoes 5",
                        Description = "Comfortable running shoes for all terrains.",
                        Price = 89.99m,
                    },
                    new()
                    {
                        Id = 6,
                        Name = "Backpack 6",
                        Description = "A durable backpack for travel and daily use.",
                        Price = 59.99m,
                    },
                    new()
                    {
                        Id = 7,
                        Name = "Smartwatch 7",
                        Description = "A stylish smartwatch with fitness tracking features.",
                        Price = 149.99m,
                    },
                }
            );
        modelBuilder
            .Entity<Category>()
            .HasData(
                new List<Category>
                {
                    new()
                    {
                        Id = 1,
                        Name = "Electronics",
                        Url = "electronics",
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Home Appliances",
                        Url = "home-appliances",
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Sportswear",
                        Url = "sportswear",
                    },
                }
            );
        modelBuilder
            .Entity<ProductCategory>()
            .HasData(
                new List<ProductCategory>
                {
                    new() { ProductId = 1, CategoryId = 1 },
                    new() { ProductId = 2, CategoryId = 1 },
                    new() { ProductId = 3, CategoryId = 1 },
                    new() { ProductId = 4, CategoryId = 2 },
                    new() { ProductId = 5, CategoryId = 3 },
                    new() { ProductId = 6, CategoryId = 3 },
                    new() { ProductId = 7, CategoryId = 1 },
                }
            );
    }
}
