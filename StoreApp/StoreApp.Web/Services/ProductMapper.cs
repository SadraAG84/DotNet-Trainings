namespace StoreApp.Web.Services;

using System.Collections.Generic;
using System.Linq;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

public class ProductMapper : IProductMapper
{
    public IEnumerable<ProductViewModel> Map(IEnumerable<Product> products)
    {
        return products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Category =
                p.Categories != null && p.Categories.Any()
                    ? string.Join(", ", p.Categories.Select(c => c.Name))
                    : string.Empty,
        });
    }
}
