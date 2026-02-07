using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ProductsContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Product>()
                .HasData(
                    new Product
                    {
                        ProductId = 1,
                        ProductName = "Laptop",
                        Price = 100,
                        IsActive = true,
                    },
                    new Product
                    {
                        ProductId = 2,
                        ProductName = "Smartphone",
                        Price = 200.40m,
                        IsActive = true,
                    },
                    new Product
                    {
                        ProductId = 3,
                        ProductName = "Headphones",
                        Price = 350.50m,
                        IsActive = false,
                    },
                    new Product
                    {
                        ProductId = 4,
                        ProductName = "Monitor",
                        Price = 470.99m,
                        IsActive = true,
                    }
                );
        }

        public DbSet<Product> Products { get; set; } = null!;
    }
}
