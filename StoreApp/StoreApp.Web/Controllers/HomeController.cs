namespace StoreApp.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;

public class HomeController : Controller
{
    // Dependency injection of the repository(we use it because we want to get the products from the database and show them in the view)(if we don't use it, we have to create an instance of the repository in the controller and that is not good because it will make the controller tightly coupled with the repository and that is not good for testing and maintenance)
    private readonly IStoreRepository _storeRepository;

    public HomeController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public IActionResult Index()
    {
        var products = _storeRepository.Products.ToList();
        return View(products);
    }
}
