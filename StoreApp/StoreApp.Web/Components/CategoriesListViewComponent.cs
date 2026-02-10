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
        var categories = _storeRepository
            .Products.Select(p => p.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
        return View(categories);
    }
}
