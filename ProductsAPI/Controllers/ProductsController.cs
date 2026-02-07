using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    // This controller will handle requests to the /api/products endpoint
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        // This action will handle GET requests to /api/products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products ?? new List<Product>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product entity)
        {
            if (entity == null || entity.ProductId != id)
            {
                return BadRequest();
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the existing product's properties with the new values from the request body
            existingProduct.ProductName = entity.ProductName;
            existingProduct.Price = entity.Price;
            existingProduct.IsActive = entity.IsActive;

            // Mark the existing product as modified and save changes
            try
            {
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
            // Handle concurrency issues
            // If the product was deleted by another user, return NotFound
            // If the product was updated by another user, rethrow the exception to be handled by the global error handler
            catch (DbUpdateConcurrencyException)
            {
                // Check if the product still exists in the database
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                // If the product still exists, it means it was updated by another user, so we rethrow the exception
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // This action will handle DELETE requests to /api/products/{id}
        //
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
