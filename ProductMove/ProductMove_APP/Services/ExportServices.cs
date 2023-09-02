using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class ExportServices
    {
        public static async Task<IList<Export>> GetExports()
        {
            var req = new RestRequest($"/Exports/GetExports", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Export>>(data)!;
            return result;
        }
        public static async Task<Export> GetExport(int id)
        {
            var req = new RestRequest($"/Exports/GetExport/{id}", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Export>(data)!;
            return result;
        }
        public static async Task AddExport(Export Export)
        {
            var req = new RestRequest($"/Exports/AddExport", Method.Post);
            req.AddJsonBody(Export);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task DeleteExport(int id)
        {
            var req = new RestRequest($"/Exports/DeleteExport/{id}", Method.Delete);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task UpdateExport(int? id, Export Export)
        {
            var req = new RestRequest($"/Exports/UpdateExport/{id}", Method.Put);
            req.AddJsonBody(Export);
            await Program.Client_2.ExecuteAsync(req);
        }
    }
}
