using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ProductMove_APP.Pages.AccountManager
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Input input { get; set; } = default!;
        //public User user { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
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
                var listUser = await UserServices.GetUsers();
                var checkUsername = (from data in listUser where data.userName == user.userName select data).ToList();
                if (checkUsername.Count > 0)
                {
                    ViewData["checkUsername"] = "failed";
                    return Page();
                }
                await UserServices.AddUser(user);
                //gọi lần 2 để kiểm tra tài khoản vừa được tạo
                var listUser_2 = await UserServices.GetUsers();
                var checkUsername_2 = (from data in listUser_2 where data.userName == user.userName select data).ToList();
                if (checkUsername_2.Count > 0)
                {
                    //nếu tài khoản đã được tạo thành công thì tiến hành tạo kho cho tài khoản
                    var warehouse = new Warehouse
                    {
                        idWarehouse = 0,
                        idUser = checkUsername_2[0].idUser,
                        totalProduct = 0,
                    };
                    await WarehouseServices.AddWarehouse(warehouse);
                }
                return RedirectToPage("./Index");
            }
            return BadRequest();
        }
        public class Input
        {
            [Required(ErrorMessage = "Tài khoản nhập vào không hợp lệ.")]
            public string userName { get; set; } = null!;
            [Required(ErrorMessage = "Mật khẩu nhập vào không hợp lệ.")]
            public string passWord { get; set; } = null!;
            [Required]
            public string decentralization { get; set; } = null!;
            [Required(ErrorMessage = "Địa chỉ nhập vào không hợp lệ.")]
            public string address { get; set; } = null!;
            [Required(ErrorMessage = "Email nhập vào không hợp lệ.")]
            public string email { get; set; } = null!;
        }
    }
}
