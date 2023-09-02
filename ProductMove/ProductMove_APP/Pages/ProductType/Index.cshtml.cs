using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.ProductType
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Category> categorys { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var result = await CategoryServices.GetCategorys()!;
            categorys = result!;

            return Page();
        }
    }
}
