namespace StoreApp.Web.Data;

using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
                        Category = "Electronics",
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Smartphone 2",
                        Description = "A sleek smartphone with a powerful camera.",
                        Price = 699.99m,
                        Category = "Electronics",
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Headphones 3",
                        Description = "Noise-cancelling headphones for immersive sound.",
                        Price = 199.99m,
                        Category = "Audio",
                    },
                    new()
                    {
                        Id = 4,
                        Name = "Coffee Maker 4",
                        Description = "Brew the perfect cup of coffee every morning.",
                        Price = 49.99m,
                        Category = "Home Appliances",
                    },
                    new()
                    {
                        Id = 5,
                        Name = "Running Shoes 5",
                        Description = "Comfortable running shoes for all terrains.",
                        Price = 89.99m,
                        Category = "Footwear",
                    },
                    new()
                    {
                        Id = 6,
                        Name = "Backpack 6",
                        Description = "A durable backpack for travel and daily use.",
                        Price = 59.99m,
                        Category = "Accessories",
                    },
                }
            );
    }
}
