using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI.Controllers
{
    // This controller will handle requests to the /api/products endpoint
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Products = new[]
        {
            "Laptop 1",
            "Smartphone 2",
            "Tablet 3",
            "Headphones 4",
            "Smartwatch 5",
        };

        // This action will handle GET requests to /api/products
        [HttpGet]
        public string[] GetProducts()
        {
            // In a real application, you would retrieve this data from a database
            return Products;
        }

        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            // In a real application, you would retrieve this data from a database
            if (id < 1 || id > Products.Length)
            {
                return null;
            }
            return Products[id - 1];
        }
    }
}
