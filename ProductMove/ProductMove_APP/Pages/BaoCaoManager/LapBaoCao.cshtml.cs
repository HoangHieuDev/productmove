using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.BaoCaoManager
{
    public class LapBaoCaoModel : PageModel
    {
        public  IActionResult OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen")!;
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            return Page();
        }
        [BindProperty]
        public Report report { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            var idKho = HttpContext.Session.GetInt32("idKho");
            report.typeOfReport = Convert.ToInt32(Request.Form["LoaiBaoCao"]);
            if (report.typeOfReport == 2)
            {
                report.time = Request.Form["ThoiGian"].ToString();
            }
            else
            {
                report.time = Request.Form["thang"].ToString() + "/" + Request.Form["ThoiGian"].ToString();
            }
            report.idWarehouse = Convert.ToInt32(idKho);
            //report.idReport = 0;
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            await ReportService.AddReport(report);
            return RedirectToPage("./Index");
        }
    }
}
