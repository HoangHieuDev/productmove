using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;
namespace ProductMove_APP.Pages.BaoCaoManager
{
    public class DeleteModel : PageModel
    {
        public Report report { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id_)
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            if (id_ == null || ReportService.GetReports() == null)
            {
                return NotFound();
            }
            var report_ = await ReportService.GetReport((int)id_);

            if (report_ == null)
            {
                return NotFound();
            }
            else
            {
                report = report_;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id_)
        {
            if (id_ == null || ReportService.GetReports() == null)
            {
                return NotFound();
            }
            var report = await ReportService.GetReport((int)id_);

            if (report != null)
            {
              await  ReportService.DeleteReport((int)id_); 
            }

            return RedirectToPage("./Index");
        }
    }
}
