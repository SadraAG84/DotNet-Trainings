namespace StoreApp.Data.Concrete;

using System.Collections.Generic;
using StoreApp.Data.Abstract;
using StoreApp.Web.Data;

public class EFStoreRepository : IStoreRepository
{
    private readonly StoreDbContext _context;

    public EFStoreRepository(StoreDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> Products => _context.Products;
    public IQueryable<Category> Categories => _context.Categories;

    public void CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public int GetProductCount(string category)
    {
        return _context
            .Products.Where(p =>
                string.IsNullOrEmpty(category) || p.Categories.Any(c => c.Url == category)
            )
            .Count();
    }

    public IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize)
    {
        return _context
            .Products.Where(p =>
                string.IsNullOrEmpty(category) || p.Categories.Any(c => c.Url == category)
            )
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}
