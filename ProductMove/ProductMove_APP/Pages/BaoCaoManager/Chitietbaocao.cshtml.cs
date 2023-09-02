using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductMove_Model;
using System.Globalization;
using ProductMove_APP.Services;

namespace ProductMove_APP.Pages.BaoCaoManager
{
    public class ChitietbaocaoModel : PageModel
    {
        
        public IList<Import> imports { get; set; } = default!;
        public IList<Export> exports { get; set; } = default!;
        public IList<DetailWarehouse> detailWarehouses = new List<DetailWarehouse>();
        public Report report { get; set; } = default!;

        public DateTime Date { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id_)
        {
            string loginCheck = HttpContext.Session.GetString("phanquyen");
            if (loginCheck == null)
            {
                return RedirectToPage("/UsersManager/Login");
            }
            var report1 = await ReportService.GetReports();
            foreach(var report_ in report1)
            {
                if(report_.idReport == id_)
                {
                    report = report_;
                }
            }
            
            if (report.typeOfReport != 2)
            {
                ViewData["quy"] = DateTime.ParseExact(report.time!, "MM/yyyy", CultureInfo.InvariantCulture).Month;
                ViewData["nam"] = DateTime.ParseExact(report.time!, "MM/yyyy", CultureInfo.InvariantCulture).Year;
            }

            var idKho = report.idWarehouse;

            

            int id = (int)idKho!;
            var rs = await DetailWarehouseServices.GetDetailWarehouses();
            foreach(var item in rs)
            {
                if(item.idWarehouse == id)
                {
                    detailWarehouses.Add(item);
                }
            }
            var result = await ImportServices.GetImports();
            var result_ = await ExportServices.GetExports();
            IList<Import> nhap = new List<Import>();
            IList<Export> xuat = new List<Export>();
            if (report != null)
            {
                if (report.typeOfReport == 1)
                {
                    Date = DateTime.ParseExact(report.time!, "MM/yyyy", CultureInfo.InvariantCulture);
                    int month = Date.Month;
                    int year = Date.Year;
                    foreach (Import listNhapKho in result)
                    {
                        int monthcheck = DateTime.ParseExact(listNhapKho.importDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                        int yearcheck = DateTime.ParseExact(listNhapKho.importDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                        if (monthcheck == month)
                        {
                            if (yearcheck == year)
                            {
                                if (idKho == listNhapKho.idWarehouse)
                                {
                                    nhap.Add(listNhapKho);
                                }
                            }

                        }
                    }
                    imports = nhap;
                    foreach (Export listxuatKho in result_)
                    {
                        int monthcheck_ = DateTime.ParseExact(listxuatKho.exportDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                        int yearcheck_ = DateTime.ParseExact(listxuatKho.exportDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                        if (monthcheck_ == month)
                        {
                            if (yearcheck_ == year)
                            {
                                if (idKho == listxuatKho.idWarehouse)
                                {
                                    xuat.Add(listxuatKho);
                                }
                            }

                        }
                    }
                    exports = xuat;
                }
                else if (report.typeOfReport == 2)
                {
                    int year = Convert.ToInt32(report.time);
                    foreach (Import listNhapKho in result)
                    {
                        int yearcheck = DateTime.ParseExact(listNhapKho.importDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;

                        if (yearcheck == year)
                        {
                            if (idKho == listNhapKho.idWarehouse)
                            {
                                nhap.Add(listNhapKho);
                            }
                        }
                    }
                    imports = nhap;
                    foreach (Export listxuatKho in result_)
                    {
                        int yearcheck_ = DateTime.ParseExact(listxuatKho.exportDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;

                        if (yearcheck_ == year)
                        {
                            if (idKho == listxuatKho.idWarehouse)
                            {
                                xuat.Add(listxuatKho);
                            }
                        }
                    }
                    exports = xuat;
                }
                else
                {
                    Date = DateTime.ParseExact(report.time!, "MM/yyyy", CultureInfo.InvariantCulture);
                    int quy = Date.Month;
                    int month = 0;
                    int year = Date.Year;
                    switch (quy)
                    {
                        case 1: { month = 1; break; }
                        case 2: { month = 4; break; }
                        case 3: { month = 7; break; }
                        case 4: { month = 10; break; }
                        default: break;
                    }
                    for (int i = month; i < (month + 3); i++)
                    {
                        foreach (Import listNhapKho in result)
                        {
                            int monthcheck = DateTime.ParseExact(listNhapKho.importDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                            int yearcheck = DateTime.ParseExact(listNhapKho.importDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                            if (monthcheck == i)
                            {
                                if (yearcheck == year)
                                {
                                    if (idKho == listNhapKho.idWarehouse)
                                    {
                                        nhap.Add(listNhapKho);
                                    }
                                }

                            }
                        }

                        foreach (Export listxuatKho in result_)
                        {
                            int monthcheck_ = DateTime.ParseExact(listxuatKho.exportDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                            int yearcheck_ = DateTime.ParseExact(listxuatKho.exportDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                            if (monthcheck_ == i)
                            {
                                if (yearcheck_ == year)
                                {
                                    if (idKho == listxuatKho.idWarehouse)
                                    {
                                        xuat.Add(listxuatKho);
                                    }
                                }

                            }
                        }

                    }
                    imports = nhap;
                    exports = xuat;
                }
            }
            return Page();

        }
    }
}
