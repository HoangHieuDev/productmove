using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_Model;
using ProductMove_APP.Services;

namespace ProductMove_APP.Pages.ProductManager
{
    public class LapHoaDonModel : PageModel
    {
        DateTime now = DateTime.Now;
        [BindProperty]
        public Customer khachHang { get; set; } = default!;
        [BindProperty]
        public Bill hoaDon { get; set; } = default!;
        [BindProperty]
        public Seri seri { get; set; } = default!;
        public async Task<IActionResult> OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen")!;
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            return Page();
        }
        [BindProperty]
        public Seri Seri { get; set; } = default!;
        [BindProperty]
        public Warehouse Kho { get; set; } = default!;
        [BindProperty]
        public DetailWarehouse ChiTietKho { get; set; } = default!;
        [BindProperty]
        public Export XuatKho { get; set; } = default!;

        public const string MAYMOI = "Máy mới";
        public async Task<IActionResult> OnPostAsync()
        {
            var req_sdt = khachHang.customerPhone;
            IList<Customer> kh = new List<Customer>();
            kh = await CustomerServices.GetCustomers();
            Customer khachhang = new Customer();
            foreach(Customer customer in kh)
            {
                if(customer.customerPhone == req_sdt)
                {
                    khachhang = customer;
                }
            }
            //var khachhang = await KhachHangService.getKhachHangBySDT(req_sdt);
            IList<Seri> seris = new List<Seri>();
            seris = await SeriServices.GetSeris();
            Seri req_seri = new Seri();
            foreach(Seri seri1 in seris)
            {
                if(seri1.seriName.Equals(seri.seriName))
                {
                    req_seri = seri1;
                }
            }
            //var req_seri = await SeriService.getSeriByNameAsync(seri.tenSeri);
            if (req_seri?.seriName == null || req_seri?.productStatus == "Máy đã bán")
            {
                ViewData["nodata"] = "msg_không tồn tại số seri";
                return Page();
            }
            if (khachhang.customerName == null)
            {
                //kiểm tra khách hàng có từng mua hàng hay chưa, nếu chưa có thì tiến hành tạo thông tin khách hàng mới.
               // await KhachHangService.addKhachHangAsync(khachHang);
                await CustomerServices.AddCustomer(khachhang);
                //sau khi tạo mới thông tin khách hàng thì tiến hành tạo hóa đơn cho khách
                var cus_req = await CustomerServices.GetCustomers();
                Customer req_khachhang = new Customer();
                foreach (var cus_ in cus_req)
                {
                    if (cus_.customerPhone.Equals(req_sdt))
                    {
                        req_khachhang = cus_;
                    }
                }
                //var req_khachhang = await KhachHangService.getKhachHangBySDT(req_sdt);
                var id_khachhang = req_khachhang.idCustomer;

                hoaDon.idCustomer = id_khachhang;
                hoaDon.dateOfBill = now.ToString("dd/mm/yyyy");
                var diachi_accountdangdangnhap = HttpContext.Session.GetString("diachi");
                hoaDon.address = diachi_accountdangdangnhap!;
                
                hoaDon.idSeri = req_seri!.idSeri;
                await BillServices.AddBill(hoaDon);
                //await HoaDonService.newHoaDon(hoaDon);
                await updateKho();

            }
            else
            {
                //nếu khách hàng đã từng mua sản phẩm thì tiến hành lập hóa đơn
                var id_khachhang = khachhang.idCustomer;

                hoaDon.idCustomer = id_khachhang;
                hoaDon.dateOfBill = now.ToString("dd/mm/yyyy");
                var diachi_accountdangdangnhap = HttpContext.Session.GetString("diachi");
                hoaDon.address = diachi_accountdangdangnhap!;
                
                hoaDon.idSeri = req_seri!.idSeri;
                await BillServices.AddBill(hoaDon);
                await updateKho();
            }
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> updateKho()
        {
            //lấy thông tin kho từ tài khoản vừa đăng nhập
            int? _id = HttpContext.Session.GetInt32("idKho");
            var result = await WarehouseServices.GetWarehouse(_id);
            Kho = result!;
            //Lấy id kho để lưu lại thông tin sản phẩm vừa nhập đã vào kho đó
            int? idkho = result!.idWarehouse;

            var name = Seri.seriName;
            //Kiểm tra xem có thông tin máy nào có tên seri trùng hay không
            var rsSeri = await SeriServices.GetSeris();
            Seri result_check = new Seri();
            foreach (var seri_1 in rsSeri)
            {
                if(seri_1.seriName == name)
                {
                    result_check = seri_1;
                }
            }
            //var result_check = await SeriService.getSeriByNameAsync(name);
            int idseri = result_check.idSeri;
            var idsp = result_check.idProduct;

            var rsDetailWare = await DetailWarehouseServices.GetDetailWarehouses();
            DetailWarehouse chitietkho = new DetailWarehouse();
            foreach (var rsDetailWare_ in rsDetailWare)
            {
                if(rsDetailWare_.idWarehouse == idkho && rsDetailWare_.productStatus.Equals(MAYMOI) && rsDetailWare_.idProduct == idsp)
                {
                    chitietkho = rsDetailWare_;
                }
            }
            //var chitietkho = await ChitietkhoService.getChiTietKhoByObject(idkho, MAYMOI, idsp);

            //nếu kiểm tra thành công có sản phẩm có số seri đó thì tiến hành lưu vào kho để bảo hành
            if (result_check.seriName != null && result_check.idWarehouse == idkho)
            {
                Seri.idSeri = idseri;
                Seri.idWarehouse = null;
                Seri.productionTime = result_check.productionTime;
                Seri.idProduct = result_check.idProduct;
                Seri.seriName = result_check.seriName;
                Seri.productStatus = "Máy đã bán";
                await SeriServices.UpdateSeri(idseri,Seri);
                //await SeriService.updateSeriAsync(idseri, Seri);

                //sau khi lưu vào kho thì update lại số lượng sản phẩm trong kho
                //tăng số lượng trong sản phẩm trong kho
                Kho.idWarehouse = idkho;
                Kho.totalProduct--;
                Kho.idUser = (int)_id!;
                await WarehouseServices.UpdateWarehouse(idkho,Kho);
                //await KhoService.updateKhoAsync(idkho, Kho);

                //thêm thông tin thời gian nhập sản phẩm bảo hành 
                XuatKho.idWarehouse = idkho;
                XuatKho.idProduct = result_check.idProduct;
                XuatKho.productName = now.ToString("dd/MM/yyyy");
                XuatKho.total = 1;
                //XuatKho.SeriSanpham = Seri.tenSeri;
                await ExportServices.AddExport(XuatKho);
                //await XuatKhoService.addSpXuatKhoAsync(XuatKho);

                //giảm số lượng trong bảng chi tiết kho
                ChiTietKho.idDetailWarehouse = chitietkho.idDetailWarehouse;
                ChiTietKho.idWarehouse = chitietkho.idWarehouse;
                ChiTietKho.productStatus = MAYMOI;
                ChiTietKho.totalProduct = chitietkho.totalProduct - 1;
                ChiTietKho.idProduct = chitietkho.idProduct;
                await DetailWarehouseServices.UpdateDetailWarehouse(chitietkho.idDetailWarehouse, ChiTietKho);
                //await ChitietkhoService.updateChiTietKhoAsync(chitietkho.idChiTietKho, ChiTietKho);

            }
            else
            {
                ViewData["nodata"] = "Sản phẩm không còn bảo hành hoặc không tồn tại số seri này";
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
