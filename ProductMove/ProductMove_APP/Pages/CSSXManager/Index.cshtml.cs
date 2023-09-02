using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.CSSXManager
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<DetailWarehouse> detailWarehouse { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "Cơ sở sản xuất")
            {
                var idwarehouseWithUser = HttpContext.Session.GetInt32("idKho");
                var listDetailWarehouse = await DetailWarehouseServices.GetDetailWarehouses();
                var listDetailWarehouseByIDWarehouse = (from data in listDetailWarehouse
                                                        where data.idWarehouse == idwarehouseWithUser
                                                        select data).ToList();
                detailWarehouse = listDetailWarehouseByIDWarehouse;
                return Page();
            }
            return BadRequest();

        }
    }
}
