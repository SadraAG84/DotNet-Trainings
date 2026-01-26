using System.Diagnostics;
using System.Threading.Tasks;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    public HomeController() { }

    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Products;
        if (!string.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString;
            products = products.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
        }

        if (!String.IsNullOrEmpty(category) && category != "0")
        {
            ViewBag.Category = category;
            products = products.Where(p => p.CategoryId.ToString() == category).ToList();
        }

        // ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name", category);

        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category,
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    // public IActionResult Create([Bind("Name,Price,Image")] Product model) --> Using Bind attribute to prevent overposting attack and just get the information we want
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        var extension = "";

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        if (imageFile != null && !allowedExtensions.Contains(extension.ToLower()))
        {
            extension = Path.GetExtension(imageFile.FileName);
            ModelState.AddModelError("ImageFile", "Only .jpg, .jpeg, .png files are allowed.");
        }

        if (ModelState.IsValid)
        {
            if (imageFile != null)
            {
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/img",
                    randomFileName
                );
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
                model.ProductId = Repository.Products.Max(p => p.ProductId) + 1;
                Repository.CreateProduct(model);
                return RedirectToAction("Index");
            }
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(entity);
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();
        }
        return View("DeleteConfirm", entity);
    }

    [HttpPost]
    public IActionResult Delete(int id, int ProductId)
    {
        if (id != ProductId)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == ProductId);
        if (entity == null)
        {
            return NotFound();
        }
        Repository.DeleteProduct(entity);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/img",
                    randomFileName
                );

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
            }
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }
}

// var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
// string ramdomFileName = model.Image;
// if (imageFile != null)
// {
//     var extension = Path.GetExtension(imageFile.FileName);
//     ramdomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
//     var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ramdomFileName);

//     if (!allowedExtensions.Contains(extension.ToLower()))
//     {
//         ModelState.AddModelError("ImageFile", "Only .jpg, .jpeg, .png files are allowed.");
//     }

//     if (ModelState.IsValid)
//     {
//         using (var stream = new FileStream(path, FileMode.Create))
//         {
//             await imageFile.CopyToAsync(stream);
//         }
//     }
// }

// if (ModelState.IsValid)
// {
//     model.Image = ramdomFileName;
//     Repository.EditProduct(model);
//     return RedirectToAction("Index");
// }
// ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
// return View(model);
