using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.AccountManager
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<ProductMove_Model.User> users { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "ADMIN")
            {
                var result = await UserServices.GetUsers();
                users = result;
                return Page();
            }
            return BadRequest();
        }
    }
}
