using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.ProductType
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Category category { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await CategoryServices.GetCategory(id);


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
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            await CategoryServices.DeleteCategory(id);
            return RedirectToPage("./Index");
        }
    }
}
