using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    // This controller will handle requests to the /api/products endpoint
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product>? _products;

        public ProductsController()
        {
            _products = new List<Product>
            {
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
                },
            };
        }

        // This action will handle GET requests to /api/products
        [HttpGet]
        public List<Product> GetProducts()
        {
            // In a real application, you would retrieve this data from a database
            return _products ?? new List<Product>();
        }

        [HttpGet("{id}")]
        public Product? GetProduct(int id)
        {
            // In a real application, you would retrieve this data from a database
            if (id < 1 || id > (_products?.Count ?? 0))
            {
                return null;
            }
            return _products?.FirstOrDefault(p => p.ProductId == id);
        }
    }
}
