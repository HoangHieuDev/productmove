using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class ImportServices
    {
        public static async Task<IList<Import>> GetImports()
        {
            var req = new RestRequest($"/Imports/GetImports", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Import>>(data)!;
            return result;
        }
        public static async Task<Import> GetImport(int id)
        {
            var req = new RestRequest($"/Imports/GetImport/{id}", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Import>(data)!;
            return result;
        }
        public static async Task AddImport(Import Import)
        {
            var req = new RestRequest($"/Imports/AddImport", Method.Post);
            req.AddJsonBody(Import);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task DeleteImport(int id)
        {
            var req = new RestRequest($"/Imports/DeleteImport/{id}", Method.Delete);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task UpdateImport(int? id, Import Import)
        {
            var req = new RestRequest($"/Imports/UpdateImport/{id}", Method.Put);
            req.AddJsonBody(Import);
            await Program.Client_2.ExecuteAsync(req);
        }
    }
}
