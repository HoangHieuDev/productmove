using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.AccountManager
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public ProductMove_Model.User user { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "ADMIN")
            {
                var result = await UserServices.GetUser(id);
                user = result!;
                return Page();
            }
            return BadRequest();

        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            await UserServices.DeleteUser(id);
            return RedirectToPage("./Index");
        }
    }
}
