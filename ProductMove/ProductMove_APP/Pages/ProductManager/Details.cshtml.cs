using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.ProductManager
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "ADMIN")
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var result = await ProductServices.GetProduct(id);
                var category = await CategoryServices.GetCategorys();
                ViewData["category"] = category;
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    product = result;
                }
                return Page();
            }
            return BadRequest();

        }


    }
}
