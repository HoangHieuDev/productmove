using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductMove_APP.Services;
using System.ComponentModel.DataAnnotations;

namespace ProductMove_APP.Pages.AccountManager
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Input input { get; set; } = default!;

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
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "ADMIN")
            {
                var user = new ProductMove_Model.User
                {
                    userName = input.userName,
                    passWord = input.passWord,
                    decentralization = input.decentralization,
                    address = input.address,
                    email = input.email

                };

                try
                {
                    var listUser = await UserServices.GetUsers();
                    var checkUsername = (from data in listUser where data.userName == user.userName select data).ToList();
                    if (checkUsername.Count > 0)
                    {
                        ViewData["checkUsername"] = "failed";
                        return Page();
                    }
                    await UserServices.UpdateUser(id, user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToPage("./Index");
            }
            return BadRequest();
        }
        public class Input
        {
            [Required]
            public string userName { get; set; } = null!;
            [Required]
            public string passWord { get; set; } = null!;
            [Required]
            public string decentralization { get; set; } = null!;
            [Required]
            public string address { get; set; } = null!;
            [Required]
            public string email { get; set; } = null!;
        }
    }
}
