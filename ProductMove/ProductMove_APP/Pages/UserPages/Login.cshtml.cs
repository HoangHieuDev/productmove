using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProductMove_APP.Services;
using ProductMove_Model;
using Microsoft.AspNetCore.Http;

namespace ProductMove_APP.Pages.User
{
    public class LoginModel : PageModel
    {
        public const string UserADMIN = "admin";
        public const string PassADMIN = "admin";
        [BindProperty]
        public ProductMove_Model.User? user { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            string username = user!.userName;
            string password = user.passWord;
            if (username == UserADMIN && password == PassADMIN)
            {
                List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim("Admin","Admin")
            };
                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,

                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("phanquyen", "ADMIN");

                return RedirectToPage("/AccountManager/index");

            }
            var listUser = await UserServices.GetUsers();
            var checkUsername = (from data in listUser
                                 where data.userName == username
                                 && data.passWord == password
                                 select data).ToList();
            if (checkUsername.Count() == 0)
            {
                ViewData["Invalid"] = "user name and password invalid!";
                return Page();
            }
            else
            {
                List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim("Admin","Admin")
            };
                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,

                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetInt32("idUser", checkUsername[0].idUser);
                HttpContext.Session.SetString("phanquyen", checkUsername[0].decentralization);
                HttpContext.Session.SetString("diachi", checkUsername[0].address);
                var listWarehouse = await WarehouseServices.GetWarehouses();
                if (listWarehouse == null)
                {
                    return BadRequest();
                }
                var idUser = checkUsername[0].idUser;
                var warehouse_idUser = (from data in listWarehouse where data.idUser == idUser select data).ToList();
                if (warehouse_idUser.Count == 1)
                {
                    var idwarehouseWithUser = warehouse_idUser[0].idWarehouse;
                    HttpContext.Session.SetInt32("idKho", (int)idwarehouseWithUser!);
                }
                if (checkUsername[0].decentralization == "Cơ sở sản xuất")
                {
                    return RedirectToPage("/CSSXManager/index");
                }
                if (checkUsername[0].decentralization == "Nhà phân phối")
                {
                    return RedirectToPage("/DLPPManager/index");
                }
                if (checkUsername[0].decentralization == "Trung tâm bảo hành")
                {
                    return RedirectToPage("/TTBHManager/index");
                }
                return Page();
            }
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return Page();
        }
    }
}

