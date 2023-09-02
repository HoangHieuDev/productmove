using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class ReportService
    {
        public static async Task<IList<Report>> GetReports()
        {
            var req = new RestRequest($"/Reports/GetReports", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Report>>(data)!;
            return result;
        }
        public static async Task<Report> GetReport(int id)
        {
            var req = new RestRequest($"/Reports/GetReport/{id}", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Report>(data)!;
            return result;
        }
        public static async Task AddReport(Report Report)
        {
            var req = new RestRequest($"/Reports/AddReport", Method.Post);
            req.AddJsonBody(Report);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task DeleteReport(int id)
        {
            var req = new RestRequest($"/Reports/DeleteReport/{id}", Method.Delete);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task UpdateReport(int? id, Report Report)
        {
            var req = new RestRequest($"/Reports/UpdateReport/{id}", Method.Put);
            req.AddJsonBody(Report);
            await Program.Client_2.ExecuteAsync(req);
        }
    }
}
