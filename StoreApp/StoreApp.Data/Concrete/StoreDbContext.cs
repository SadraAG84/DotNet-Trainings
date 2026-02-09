namespace StoreApp.Web.Data;

using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
