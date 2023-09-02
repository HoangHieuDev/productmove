using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.ProductType
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Category category { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {

            await CategoryServices.AddCategory(category);
            return RedirectToPage("./Index");
        }
    }
}
