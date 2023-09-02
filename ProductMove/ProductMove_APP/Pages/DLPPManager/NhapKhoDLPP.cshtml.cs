using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_Model;
using ProductMove_APP.Services;

namespace ProductMove_APP.Pages.ProductManager
{
    public class NhapKhoDLPPModel : PageModel
    {
        public IList<Product> SanPhams { get; set; } = default!;
        public IList<DetailWarehouse> ChiTietKho { get; set; } = default!;
        public IList<Inputdata> Inputdatas = new List<Inputdata>();

        [BindProperty]
        public Warehouse Kho { get; set; } = default!;
        [BindProperty]
        public DetailWarehouse chiTietKho { get; set; } = default!;
        [BindProperty]
        public DetailWarehouse chiTietKho2 { get; set; } = default!;
        [BindProperty]
        public DetailWarehouse chiTietKho3 { get; set; } = default!;
        [BindProperty]
        public Warehouse Kho_up { get; set; } = default!;
        [BindProperty]
        public Seri Seri { get; set; } = default!;

        [BindProperty]
        public Import NhapKho { get; set; } = default!;
        [BindProperty]
        public Product SanPham { get; set; } = default!;
        public List<Seri> SeriList { get; set; } = default!;
        public string ttsp = "Máy mới";
        public class Inputdata
        {
            public int? idDetailWarehouse { get; set; }
            public int? idWarehouse { get; set; }
            public int? totalProduct { get; set; }
            public int? idProduct { get; set; }
            public string? productStatus { get; set; } = default!;

            public string? productName { get; set; } = default!;
            public string? address { get; set; } = default!;
            public string? decentralization { get; set; } = default!;

        }

        public const string MAYMOI = "Máy mới";

        public async Task<IActionResult> OnGet()
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen")!;
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            var datawarehouse = await DetailWarehouseServices.GetDetailWarehouses();
            IList<DetailWarehouse> data = new List<DetailWarehouse>();
            foreach (var item in datawarehouse)
            {
                if (item.productStatus.Equals(ttsp))
                {
                    data.Add(item);
                }
            }

            for (int i = 0; i < data.Count(); i++)
            {
                var pq = data[i].decentralization;
                if (pq == "Cơ sở sản xuất")
                {
                    Inputdata data_ = new Inputdata();
                    data_.idDetailWarehouse = data[i].idDetailWarehouse;
                    data_.idWarehouse = data[i].idWarehouse;
                    data_.address = data[i].address;
                    data_.totalProduct = data[i].totalProduct;
                    data_.idProduct = data[i].idProduct;
                    data_.productName = data[i].productName;
                    Inputdatas.Add(data_);
                }
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string idsp, string idkhos, string idctk)
        {
            //lấy thông tin kho từ tài khoản vừa đăng nhập
            var _id = HttpContext.Session.GetInt32("idUser");
            var reswarehouse = await WarehouseServices.GetWarehouses();
            Warehouse result = new Warehouse();
            foreach (var item in reswarehouse)
            {
                if (item.idUser == _id)
                {
                    result = item;
                }
            }
            // var result = await KhoService.getKhoByIdTaiKhoanAsync(_id);
            Kho = result!;
            //Lấy id kho để lưu lại thông tin sản phẩm vừa nhập đã vào kho đó
            int? idkho_tk = result!.idWarehouse;
            int soluong_res = result.totalProduct;



            var soluong = Request.Form["soluongnhap"];
            string soluongnhap = soluong;
            var resSeri = await SeriServices.GetSeris();
            IList<Seri> gettop = new List<Seri>();
            int slCheck = 0;
            foreach(var res_ in resSeri)
            {
                if (slCheck == int.Parse(soluongnhap)) break;
                if(res_.idProduct == int.Parse(idsp) && res_.idWarehouse == int.Parse(idkhos))
                {
                    slCheck++;
                    gettop.Add(res_);
                }
            }
            //var gettop = await SeriService.getSpByTopAsync(soluongnhap, idsp, idkhos);
            for (var i = 0; i < gettop.Count(); i++)
            {
                var idseri = gettop[i].idSeri;
                var tenseri = gettop[i].seriName;
                var thoigiansanxuat = gettop[i].productionTime;
                var idsanpham = gettop[i].idProduct;
                var idkho = gettop[i].idWarehouse;
                var ttsp = gettop[i].productStatus;

                //chuyển sản phẩm đến kho của đại lý phân phối
                Seri.idSeri = idseri;
                Seri.idWarehouse = idkho_tk;
                Seri.productionTime = thoigiansanxuat;
                Seri.idProduct = idsanpham;
                Seri.seriName = tenseri;
                Seri.productStatus = ttsp;
                await SeriServices.UpdateSeri(idseri, Seri);
            }
            //giảm số sản phẩm của kho cơ sở sản xuất đã lấy sản phẩm
            int soluongnhap_cv = Int32.Parse(soluongnhap);
            var data = await WarehouseServices.GetWarehouse(int.Parse(idkhos));
            //Warehouse data = new Warehouse();
            //foreach(var warehouse in dtWarehouse)
            //{
            //    if(warehouse.idWarehouse == int.Parse(idkhos))
            //    {
            //        data = warehouse;
            //    }
            //}
            //var data = await KhoService.getKhoByIdKhoAsync(idkhos);
            Kho.idWarehouse = data!.idWarehouse;
            Kho.totalProduct = data.totalProduct - soluongnhap_cv;
            Kho.idUser = data.idUser;
            int idkho_cv = Int32.Parse(idkhos);
            int idsp_cv = Int32.Parse(idsp);
            int idctk_cv = Int32.Parse(idctk);
            await WarehouseServices.UpdateWarehouse(idkho_cv, Kho);

            //Tăng số lượng của kho đang đăng nhập
            Kho_up.idWarehouse = idkho_tk;
            Kho_up.totalProduct = soluong_res + soluongnhap_cv;
            Kho_up.idUser = (int)_id!;
            await WarehouseServices.UpdateWarehouse(idkho_tk, Kho_up);

            //update lại chi tiết kho của kho cơ sở sản xuất đã lấy sản phẩm
            var res_dtwarehoure = await DetailWarehouseServices.GetDetailWarehouses();
            DetailWarehouse chitietkho = new DetailWarehouse();
            foreach(var item in res_dtwarehoure)
            {
                if(item.idProduct == idsp_cv && item.idWarehouse == idkho_cv && item.productStatus.Equals(MAYMOI))
                {
                    chitietkho = item;
                }
            }
            //var chitietkho = await ChitietkhoService.getChiTietKhoByObject(idkho_cv, MAYMOI, idsp_cv);
            //nếu số lượng nhập vào bằng với số lượng có trong kho cơ sở sản xuất thì tiến hành chuyển toàn bộ sang kho của tài khoản đăng nhập
            if (soluongnhap_cv == chitietkho.totalProduct)
            {
               
                DetailWarehouse thongtinsanpham = new DetailWarehouse();
                foreach (var item in res_dtwarehoure)
                {
                    if (item.idProduct == idsp_cv && item.idWarehouse == idkho_tk && item.productStatus.Equals(MAYMOI))
                    {
                        thongtinsanpham = item;
                    }
                }
                //var thongtinsanpham = await ChitietkhoService.getChiTietKhoByObject(idkho_tk, MAYMOI, idsp_cv);
                if (thongtinsanpham.idWarehouse == idkho_tk && thongtinsanpham.productStatus == MAYMOI)
                {
                    await DetailWarehouseServices.DeleteDetailWarehouse(idctk_cv);
                    chiTietKho.idDetailWarehouse = thongtinsanpham.idDetailWarehouse;
                    chiTietKho.idWarehouse = thongtinsanpham.idWarehouse;
                    chiTietKho.productStatus = thongtinsanpham.productStatus;
                    chiTietKho.totalProduct = thongtinsanpham.totalProduct + soluongnhap_cv;
                    chiTietKho.idProduct = thongtinsanpham.idProduct;
                    await DetailWarehouseServices.UpdateDetailWarehouse(thongtinsanpham.idDetailWarehouse, chiTietKho);
                }
                else
                {
                    chiTietKho.idDetailWarehouse = chitietkho.idDetailWarehouse;
                    chiTietKho.idWarehouse = idkho_tk;
                    chiTietKho.productStatus = MAYMOI;
                    chiTietKho.totalProduct = chitietkho.totalProduct;
                    chiTietKho.idProduct = chitietkho.idProduct;
                    await DetailWarehouseServices.UpdateDetailWarehouse(chitietkho.idDetailWarehouse, chiTietKho);
                }
                Export export = new Export();
                export.idWarehouse = int.Parse(idkhos);
                export.idProduct = chitietkho.idProduct;
                export.exportDate = DateTime.Today.ToShortDateString();
                export.total = soluongnhap_cv;
                var export_ = await ExportServices.GetExports();
                bool checkEp = true;
                foreach (var export1 in export_)
                {
                    if (export.idWarehouse == export1.idWarehouse && export.exportDate == export1.exportDate && export.idProduct == export1.idProduct)
                    {
                        export.total += soluongnhap_cv;
                        await ExportServices.UpdateExport(export1.idExport, export);
                        checkEp = false;
                        break;
                    }
                }
                if (checkEp)
                {
                    await ExportServices.AddExport(export);
                }
                Import import = new Import();
                import.idWarehouse = idkho_tk;
                import.idProduct = chitietkho.idProduct;
                import.importDate = DateTime.Today.ToShortDateString();
                import.total = soluongnhap_cv;
                var import_ = await ImportServices.GetImports();
                bool checkIp = true;
                foreach (var import1 in import_)
                {
                    if (import.idWarehouse == import1.idWarehouse && import.importDate == import1.importDate && import.idProduct == import1.idProduct)
                    {
                        import.total += soluongnhap_cv;
                        await ImportServices.UpdateImport(import1.idImport, import);
                        checkIp = false;
                        break;
                    }
                }
                if (checkIp)
                {
                    await ImportServices.AddImport(import);
                }

            }
            //nếu số lượng nhập vượt quá số lượng sản phẩm trong kho đang có thì tiến hành thông báo lỗi
            if (soluongnhap_cv > chitietkho.totalProduct)
            {
                foreach(var res__ in res_dtwarehoure)
                {
                    if (res__.productStatus.Equals(ttsp))
                    {
                        ChiTietKho.Add(res__);
                    }
                }
                //ChiTietKho = await ChitietkhoService.getAllSpByTTSPAsync(ttsp);
                ViewData["err"] = "Err";
                return Page();
            }
            //nếu số lượng nhập nhỏ hơn số lượng sản phẩm trong kho thì tiến hành thêm chi tiết kho mới và giảm số lượng đang có của kho được lấy sản phẩm
            if (soluongnhap_cv < chitietkho.totalProduct)
            {
                chiTietKho2.idDetailWarehouse = chitietkho.idDetailWarehouse;
                chiTietKho2.idWarehouse = chitietkho.idWarehouse;
                chiTietKho2.productStatus = MAYMOI;
                chiTietKho2.totalProduct = chitietkho.totalProduct - soluongnhap_cv;
                chiTietKho2.idProduct = chitietkho.idProduct;
                await DetailWarehouseServices.UpdateDetailWarehouse(chitietkho.idDetailWarehouse, chiTietKho2);
                var res_dtwarehoure1 = await DetailWarehouseServices.GetDetailWarehouses();
                DetailWarehouse thongtinsanpham = new DetailWarehouse();
                foreach (var item in res_dtwarehoure)
                {
                    if (item.idProduct == idsp_cv && item.idWarehouse == idkho_tk && item.productStatus.Equals(MAYMOI))
                    {
                        thongtinsanpham = item;
                    }
                }
                //var thongtinsanpham = await ChitietkhoService.getChiTietKhoByObject(idkho_tk, MAYMOI, idsp_cv);

                if (thongtinsanpham.idWarehouse == idkho_tk && thongtinsanpham.productStatus == MAYMOI)
                {
                    chiTietKho2.idDetailWarehouse = thongtinsanpham.idDetailWarehouse;
                    chiTietKho2.idWarehouse = thongtinsanpham.idWarehouse;
                    chiTietKho2.productStatus = thongtinsanpham.productStatus;
                    chiTietKho2.totalProduct = thongtinsanpham.totalProduct + soluongnhap_cv;
                    chiTietKho2.idProduct = thongtinsanpham.idProduct;
                    await DetailWarehouseServices.UpdateDetailWarehouse(thongtinsanpham.idDetailWarehouse, chiTietKho2);
                }
                else
                {
                    DetailWarehouse chiTiets = new DetailWarehouse();
                    chiTiets.idWarehouse = idkho_tk;
                    chiTiets.productStatus = MAYMOI;
                    chiTiets.totalProduct = soluongnhap_cv;
                    chiTiets.idProduct = idsp_cv;
                    await DetailWarehouseServices.AddDetailWarehouse(chiTiets);
                }
                Export export = new Export();
                export.idWarehouse = int.Parse(idkhos);
                export.idProduct = chiTietKho2.idProduct;
                export.exportDate = DateTime.Today.ToShortDateString();
                export.total = soluongnhap_cv;
                var export_ = await ExportServices.GetExports();
                bool checkEp = true;
                foreach (var export1 in export_)
                {
                    if (export.idWarehouse == export1.idWarehouse && export.exportDate == export1.exportDate && export.idProduct == export1.idProduct)
                    {
                        export.total += soluongnhap_cv;
                        await ExportServices.UpdateExport(export1.idExport, export);
                        checkEp = false;
                        break;
                    }
                }
                if (checkEp)
                {
                    await ExportServices.AddExport(export);
                }
                Import import = new Import();
                import.idWarehouse = idkho_tk;
                import.idProduct = chiTietKho2.idProduct;
                import.importDate = DateTime.Today.ToShortDateString();
                import.total = soluongnhap_cv;
                var import_ = await ImportServices.GetImports();
                bool checkIp = true;
                foreach(var import1 in import_)
                {
                    if(import.idWarehouse == import1.idWarehouse && import.importDate == import1.importDate && import.idProduct == import1.idProduct)
                    {
                        import.total += soluongnhap_cv;
                        await ImportServices.UpdateImport(import1.idImport,import);
                        checkIp = false;
                        break;
                    }
                }
                if (checkIp)
                {
                    await ImportServices.AddImport(import);
                }
                
            }
            var res_dtwarehoure2 = await DetailWarehouseServices.GetDetailWarehouses();
            IList <DetailWarehouse> data_ = new List<DetailWarehouse>();
            foreach (var item in res_dtwarehoure)
            {
                if (item.productStatus.Equals(ttsp))
                {
                    data_.Add(item);
                }
            }
            //var data_ = await ChitietkhoService.getAllSpByTTSPAsync(ttsp);
            for (int i = 0; i < data_.Count(); i++)
            {
                var pq = data_[i].decentralization;
                if (pq == "Cơ sở sản xuất")
                {
                    Inputdata dataS = new Inputdata();
                    dataS.idDetailWarehouse = data_[i].idDetailWarehouse;
                    dataS.idWarehouse = data_[i].idWarehouse;
                    dataS.address = data_[i].address;
                    dataS.totalProduct = data_[i].totalProduct;
                    dataS.idProduct = data_[i].idProduct;
                    dataS.productName = data_[i].productName;
                    Inputdatas.Add(dataS);
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
