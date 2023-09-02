using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;
using System.ComponentModel.DataAnnotations;

namespace ProductMove_APP.Pages.ProductManager
{
    public class NhapKho_DLPP_BH_XongModel : PageModel
    {
        DateTime now = DateTime.Now;
        public class Input
        {
            [Required(ErrorMessage = "Thông tin vừa nhập không hợp lệ!")]
            public string Seriname { get; set; } = default!;
        }
        [BindProperty]
        public Input input { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "Nhà phân phối")
            {
                var seriName = input.Seriname;
                var listSeri = await SeriServices.GetSeris();
                var data_seriInput = (from data in listSeri where data.seriName == seriName select data).ToList();
                if (data_seriInput.Count == 1)
                {
                    var idwarehouseWithUser = HttpContext.Session.GetInt32("idKho")!;
                    var import_warehouse = new Import
                    {
                        idProduct = data_seriInput[0].idProduct,
                        idWarehouse = idwarehouseWithUser,
                        importDate = now.ToString("dd/MM/yyyy"),
                        total = 1,
                    };
                    await ImportServices.AddImport(import_warehouse);
                    var seri_update_information = new Seri
                    {
                        seriName = data_seriInput[0].seriName,
                        productionTime = data_seriInput[0].seriName,
                        idProduct = data_seriInput[0].idProduct,
                        idWarehouse = idwarehouseWithUser,
                        productStatus = "Máy sửa chữa hoàn tất",
                    };
                    await SeriServices.UpdateSeri(data_seriInput[0].idSeri, seri_update_information);
                    //kiểm tra xem trong kho đã tồn tại loại sản phẩm đó với trạng thái là máy mới hay chưa
                    //nếu có thì tiến hành update số lượng mà khồng cần thêm chi tiết sản phẩm đó vào kho
                    var listDetailWarehouse = await DetailWarehouseServices.GetDetailWarehouses();
                    var detailwarehouse_check = (from data in listDetailWarehouse
                                                 where data.idWarehouse == idwarehouseWithUser &&
                                                 data.productStatus == "Máy sửa chữa hoàn tất" &&
                                                 data.idProduct == data_seriInput[0].idProduct
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
                            totalProduct = detailwarehouse_check[0].totalProduct + 1,
                        };
                        await DetailWarehouseServices.UpdateDetailWarehouse(detaiWarehouse.idDetailWarehouse, detaiWarehouse);
                    }
                    else
                    {
                        //thêm thông tin sản phẩm vào kho 
                        var detaiWarehouse = new DetailWarehouse
                        {
                            idWarehouse = idwarehouseWithUser,
                            productStatus = "Máy sửa chữa hoàn tất",
                            totalProduct = 1,
                            idProduct = data_seriInput[0].idProduct
                        };
                        await DetailWarehouseServices.AddDetailWarehouse(detaiWarehouse);
                    }

                    //tăng số lượng trong sản phẩm trong kho
                    var listWarehouse = await WarehouseServices.GetWarehouses();
                    var idUser = HttpContext.Session.GetInt32("idUser");
                    var warehouse_iduser = (from data in listWarehouse where data.idUser == idUser select data).ToList();
                    if (warehouse_iduser.Count == 0)
                    {
                        return BadRequest();
                    }
                    var total_product = warehouse_iduser[0].totalProduct;
                    var warehouse = new Warehouse
                    {
                        idWarehouse = idwarehouseWithUser,
                        totalProduct = total_product + 1,
                        idUser = (int)idUser!
                    };
                    await WarehouseServices.UpdateWarehouse(idwarehouseWithUser, warehouse);

                    return RedirectToPage("./Index");
                }
                else
                {
                    ViewData["invalid"] = "Seri is not invalid!";
                    return Page();
                }
            }
            return BadRequest();
        }
    }
}
