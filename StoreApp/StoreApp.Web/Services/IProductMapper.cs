namespace StoreApp.Web.Services;

using System.Collections.Generic;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

public interface IProductMapper
{
    IEnumerable<ProductViewModel> Map(IEnumerable<Product> products);
}
