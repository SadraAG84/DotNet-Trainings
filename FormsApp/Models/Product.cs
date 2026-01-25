using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

// using Microsoft.AspNetCore.Mvc; --> for Bind attribute

namespace FormsApp.Models
{
    // [Bind("Name,Price,Image")] --> we can use Bind attribute here as well to prevent overposting attack and again just get the informatoin we want
    public class Product
    {
        // [BindNever] --> to not bind this property from user input and do not get this value from the request
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters")]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }
        public string? Image { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
