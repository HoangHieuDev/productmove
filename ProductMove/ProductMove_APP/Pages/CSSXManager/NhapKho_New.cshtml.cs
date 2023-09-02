using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;

namespace ProductMove_APP.Pages.CSSXManager
{
    public class NhapKho_NewModel : PageModel
    {
        DateTime now = DateTime.Now;
        string value = "";
        Random random = new Random();
        char[] list = new char[] { 'p', 'r', 'o', 'd', 'u', 'c', 't', 'm', 'o', 'v', 'e', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        [BindProperty]
        public Product product { get; set; } = default!;
        [BindProperty]
        public Import import { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "Cơ sở sản xuất")
            {
                try
                {
                    var products = await ProductServices.GetProducts();
                    ViewData["product"] = products;
                    return Page();
                }
                catch
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "Cơ sở sản xuất")
            {
                var idUser = HttpContext.Session.GetInt32("idUser");
                var listWarehouse = await WarehouseServices.GetWarehouses();
                if (listWarehouse == null)
                {
                    return BadRequest();
                }
                var idwarehouseWithUser = HttpContext.Session.GetInt32("idKho")!;

                var import_warehouse = new Import
                {
                    idProduct = product.idProduct,
                    idWarehouse = idwarehouseWithUser,
                    importDate = now.ToString("dd/MM/yyyy"),
                    total = import.total,
                };
                await ImportServices.AddImport(import_warehouse);

                for (int i = 1; i <= import.total; i++)
                {
                    var seriName = ranDomSeri();
                    var seri = new Seri
                    {
                        idSeri = 0,
                        seriName = seriName,
                        productionTime = now.ToString("dd/MM/yyyy"),
                        idProduct = product.idProduct,
                        idWarehouse = idwarehouseWithUser,
                        productStatus = "Máy mới"
                    };
                    await SeriServices.AddSeri(seri);
                    value = "";
                }
                //kiểm tra xem trong kho đã tồn tại loại sản phẩm đó với trạng thái là máy mới hay chưa
                //nếu có thì tiến hành update số lượng mà khồng cần thêm chi tiết sản phẩm đó vào kho
                var listDetailWarehouse = await DetailWarehouseServices.GetDetailWarehouses();
                var detailwarehouse_check = (from data in listDetailWarehouse
                                             where data.idWarehouse == idwarehouseWithUser &&
                                             data.productStatus == "Máy mới" &&
                                             data.idProduct == product.idProduct
                                             select data).ToList();

                if (detailwarehouse_check.Count > 0)
                {
                    //update số lượng
                    var detaiWarehouse = new DetailWarehouse
                    {
                        idWarehouse = idwarehouseWithUser,
                        idDetailWarehouse = detailwarehouse_check[0].idDetailWarehouse,
                        idProduct = detailwarehouse_check[0].idProduct,
                        productStatus = detailwarehouse_check[0].productStatus,
                        totalProduct = detailwarehouse_check[0].totalProduct + import.total,
                    };
                    await DetailWarehouseServices.UpdateDetailWarehouse(detaiWarehouse.idDetailWarehouse, detaiWarehouse);
                }
                else
                {
                    //thêm thông tin sản phẩm vào kho 
                    var detaiWarehouse = new DetailWarehouse
                    {
                        idWarehouse = idwarehouseWithUser,
                        productStatus = "Máy mới",
                        totalProduct = import.total,
                        idProduct = product.idProduct
                    };
                    await DetailWarehouseServices.AddDetailWarehouse(detaiWarehouse);
                }
                //tăng số lượng trong sản phẩm trong kho
                var warehouse_iduser = (from data in listWarehouse where data.idUser == idUser select data).ToList();
                if (warehouse_iduser.Count == 0)
                {
                    return BadRequest();
                }
                var total_product = warehouse_iduser[0].totalProduct;
                var warehouse = new Warehouse
                {
                    idWarehouse = idwarehouseWithUser,
                    totalProduct = total_product + import.total,
                    idUser = (int)idUser!
                };
                await WarehouseServices.UpdateWarehouse(idwarehouseWithUser, warehouse);

                return RedirectToPage("./Index");
            }
            return BadRequest();
        }
        public string ranDomSeri()
        {
            //random ra 9 ký tự trong mã số seri của sản phẩm
            while (value.Length < 9)
            {
                value += list[random.Next(0, list.Count())];
            }
            return value;
        }
    }
}
