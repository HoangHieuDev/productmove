using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.ProductType
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Category category { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var result = await CategoryServices.GetCategory(id);

            ViewData["category"] = category;
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                category = result;
            }
            return Page();
        }
    }
}
