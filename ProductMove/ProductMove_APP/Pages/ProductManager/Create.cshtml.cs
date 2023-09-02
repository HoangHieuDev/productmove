using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.ProductManager
{
    public class CreateModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "ADMIN")
            {
                try
                {
                    var category = await CategoryServices.GetCategorys();
                    ViewData["category"] = category;
                    return Page();
                }
                catch
                {
                    return NotFound();
                }
            }
            return BadRequest();

        }
        [BindProperty]
        public Product product { get; set; } = default!;
        public Category category { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "ADMIN")
            {
                await ProductServices.AddProduct(product);
                return RedirectToPage("./Index");
            }
            return BadRequest();

        }
    }
}
