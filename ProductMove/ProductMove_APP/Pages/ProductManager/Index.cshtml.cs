using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.ProductManager
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Product> products { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "ADMIN")
            {
                var result = await ProductServices.GetProducts()!;
                products = result!;
                return Page();
            }
            return BadRequest();

        }
    }
}
