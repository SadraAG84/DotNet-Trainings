namespace StoreApp.Data.Concrete;

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

    public void CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
}
