namespace StoreApp.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

public class HomeController : Controller
{
    public int pageSize = 2;

    // Dependency injection of the repository(we use it because we want to get the products from the database and show them in the view)(if we don't use it, we have to create an instance of the repository in the controller and that is not good because it will make the controller tightly coupled with the repository and that is not good for testing and maintenance)
    private readonly IStoreRepository _storeRepository;

    public HomeController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    // localhost:5000/home/index?page=1
    public IActionResult Index(int page = 1)
    {
        var products = _storeRepository
            .Products.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category,
            })
            .ToList();
        return View(
            new ProductListViewModel
            {
                Products = products,
                PageInfo = new PageInfo
                {
                    ItemsPerPage = pageSize,
                    TotalItems = _storeRepository.Products.Count(),
                },
            }
        );
    }
}
