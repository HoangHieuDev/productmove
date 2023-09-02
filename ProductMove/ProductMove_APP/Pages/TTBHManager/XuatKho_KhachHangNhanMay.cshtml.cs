using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_APP.Services;
using ProductMove_Model;
using System.ComponentModel.DataAnnotations;

namespace ProductMove_APP.Pages.TTBHManager
{
    public class XuatKho_KhachHangNhanMayModel : PageModel
    {
        DateTime now = DateTime.Now;

        [BindProperty]
        public Input input { get; set; } = default!;
        public IActionResult OnGet()
        {
            var loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return BadRequest();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var phanquyen = HttpContext.Session.GetString("phanquyen");
            if (phanquyen != null && phanquyen == "Trung tâm bảo hành")
            {
                var seriName = input.Seriname;
                var listSeri = await SeriServices.GetSeris();
                var data_seriInput = (from data in listSeri where data.seriName == seriName select data).ToList();
                if (data_seriInput.Count == 1)
                {
                    var idwarehouseWithUser = HttpContext.Session.GetInt32("idKho")!;
                    var seri_update_information = new Seri
                    {
                        seriName = data_seriInput[0].seriName,
                        productionTime = data_seriInput[0].seriName,
                        idProduct = data_seriInput[0].idProduct,
                        idWarehouse = null,
                        productStatus = "Khách đã nhận sau khi sửa chữa",
                    };
                    var export_warehouse = new Export
                    {
                        idProduct = data_seriInput[0].idProduct,
                        idWarehouse = idwarehouseWithUser,
                        exportDate = now.ToString("dd/MM/yyyy"),
                        total = 1,
                    };
                    await ExportServices.AddExport(export_warehouse);
                    await SeriServices.UpdateSeri(data_seriInput[0].idSeri, seri_update_information);
                    //kiểm tra xem trong kho đã tồn tại loại sản phẩm đó với trạng thái là máy mới hay chưa
                    //nếu có thì tiến hành update số lượng mà khồng cần thêm chi tiết sản phẩm đó vào kho
                    var listDetailWarehouse = await DetailWarehouseServices.GetDetailWarehouses();
                    var detailwarehouse_check = (from data in listDetailWarehouse
                                                 where data.idWarehouse == idwarehouseWithUser &&
                                                 data.productStatus == "Khách đã nhận sau khi sửa chữa" &&
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
                            productStatus = "Máy sửa chữa hoàn tất",
                            totalProduct = detailwarehouse_check[0].totalProduct - 1,
                        };
                        if (detaiWarehouse.totalProduct == 0)
                        {
                            await DetailWarehouseServices.DeleteDetailWarehouse(detaiWarehouse.idDetailWarehouse);
                        }
                        else
                        {
                            await DetailWarehouseServices.UpdateDetailWarehouse(detaiWarehouse.idDetailWarehouse, detaiWarehouse);
                        }
                    }
                    else
                    {
                        //thêm thông tin sản phẩm vào kho 
                        var detaiWarehouse = new DetailWarehouse
                        {
                            idWarehouse = idwarehouseWithUser,
                            productStatus = "Khách đã nhận sau khi sửa chữa",
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
                        totalProduct = total_product - 1,
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
        public class Input
        {
            [Required(ErrorMessage = "Thông tin vừa nhập không hợp lệ!")]
            public string Seriname { get; set; } = default!;
        }
    }
}
