using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc; --> for Bind attribute

namespace FormsApp.Models
{

    // [Bind("Name,Price,Image")] --> we can use Bind attribute here as well to prevent overposting attack and again just get the informatoin we want
    public class Product
    {

        // [BindNever] --> to not bind this property from user input and do not get this value from the request
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
