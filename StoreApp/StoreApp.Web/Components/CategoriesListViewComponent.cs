using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;

namespace StoreApp.Web.Components;

public class CategoriesListViewComponent : ViewComponent
{
    private readonly IStoreRepository _storeRepository;

    public CategoriesListViewComponent(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public IViewComponentResult Invoke()
    {
        // Get all unique category names from all products
        var categoryNames = _storeRepository.Products
            .SelectMany(p => p.Categories)
            .Select(c => c.Name)
            .Distinct()
            .OrderBy(name => name)
            .ToList();
        return View(categoryNames);
    }
}
