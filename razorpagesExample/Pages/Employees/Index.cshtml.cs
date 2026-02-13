using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razorpagesExample.Pages.Employees;

public class IndexModel : PageModel
{
    public string Message { get; set; } = string.Empty;

    public void OnGet()
    {
        Message = "Welcome to the Employees page!";
    }
}
