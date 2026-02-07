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
        public IActionResult GetProducts()
        {
            if (_products == null || !_products.Any())
            {
                return NotFound();
            }
            return Ok(_products ?? new List<Product>());
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _products?.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
