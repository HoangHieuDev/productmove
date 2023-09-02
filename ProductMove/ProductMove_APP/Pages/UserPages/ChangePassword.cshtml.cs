using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using System.ComponentModel.DataAnnotations;

namespace ProductMove_APP.Pages.UserPages
{
    public class ChangePasswordModel : PageModel
    {
        [BindProperty]
        public Input input { get; set; } = default!;
        public class Input
        {
            [Required(ErrorMessage = "Vui lòng nhập tài khoản!")]
            public string UserName { get; set; } = default!;
            [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
            public string OldPassword { get; set; } = default!;
            [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới!")]
            public string NewPassword { get; set; } = default!;
            [Compare(otherProperty: "NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu xác nhận không trùng khớp")]
            public string re_NewPassword { get; set; } = default!;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var username = input.UserName;
            var newpassword = input.NewPassword;

            var listUsername = await UserServices.GetUsers();
            var checkUsername = (from data in listUsername where data.userName == username select data).ToList();
            if (checkUsername.Count == 1)
            {
                var user = new ProductMove_Model.User
                {
                    userName = username,
                    passWord = newpassword,
                    decentralization = checkUsername[0].decentralization,
                    address = checkUsername[0].address,
                    email = checkUsername[0].email,
                };
                await UserServices.UpdateUser(checkUsername[0].idUser, user);
                return RedirectToPage("./Login");
            }
            else
            {
                ViewData["bad"] = "User not exits";
                return Page();
            }
        }
    }
}
