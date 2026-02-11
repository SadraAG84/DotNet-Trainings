namespace StoreApp.Web.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;
using StoreApp.Web.Services;

public class HomeController : Controller
{
    private const int pageSize = 2;
    private readonly IProductMapper _productMapper;

    // Dependency injection of the repository(we use it because we want to get the products from the database and show them in the view)(if we don't use it, we have to create an instance of the repository in the controller and that is not good because it will make the controller tightly coupled with the repository and that is not good for testing and maintenance)
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;

    public HomeController(
        IStoreRepository storeRepository,
        IProductMapper productMapper,
        IMapper mapper
    )
    {
        _storeRepository = storeRepository;
        _productMapper = productMapper;
        _mapper = mapper;
    }

    // localhost:5000/home/index?page=1
    public IActionResult Index(string category, int page = 1)
    {
        var products = _storeRepository.GetProductsByCategory(category, page, pageSize);
        var productViewModels = _productMapper.Map(products);

        var model = new ProductListViewModel
        {
            Products = productViewModels,
            PageInfo = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = _storeRepository.GetProductCount(category),
            },
        };
        return View(model);
    }
}
