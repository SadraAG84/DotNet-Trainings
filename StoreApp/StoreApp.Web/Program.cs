using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Data;
using StoreApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register ProductMapper for dependency injection
builder.Services.AddScoped<IProductMapper, ProductMapper>();

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("StoreDbConnection"),
        b => b.MigrationsAssembly("StoreApp.Web")
    )
);

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

app.UseStaticFiles();

// Custom route for products in a category
// localhost:5000/products/electronics
app.MapControllerRoute(
    name: "product_in_category",
    pattern: "products/{category}",
    new { controller = "Home", action = "Index" }
);

// Custom route for product details using product name
// loclalhost:5000/laptop-1
app.MapControllerRoute(
    name: "product_details",
    pattern: "{name}",
    new { controller = "Home", action = "Details" }
);

app.MapDefaultControllerRoute();
app.Run();
