using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

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
        // Get the selected category from the route data and pass it to the view using ViewBag (we use ViewBag because we want to pass the selected category to the view and we don't want to create a view model for this because it is a simple data and we can use ViewBag for this)
        ViewBag.SelectedCategory = RouteData?.Values["category"];

        // Get all unique category names from all products
        var categoryNames = _storeRepository
            .Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Url = c.Url,
            })
            .ToList();
        return View(categoryNames);
    }
}
