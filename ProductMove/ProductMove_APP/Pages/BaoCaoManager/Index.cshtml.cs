using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_Model;
using ProductMove_APP.Services;

namespace ProductMove_APP.Pages.BaoCaoManager
{
    public class IndexModel : PageModel
    {
        

        public IList<Report> reports = new List<Report>();
        IList<Report> ReportList { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            ReportList = await ReportService.GetReports();
            if (ReportList != null)
            {
                var idKhoCheck = HttpContext.Session.GetInt32("idKho");
                var result = ReportList;
                foreach (Report report in result)
                {
                    if (report.idWarehouse == idKhoCheck)
                    {
                        reports.Add(report);
                    }
                }
                
            }
            return Page();
        }
    }
}
