using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class SeriServices
    {
        public static async Task<IList<Seri>> GetSeris()
        {
            var req = new RestRequest($"/Seris/GetSeris", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Seri>>(data)!;
            return result;
        }
        public static async Task<Seri?> GetSeri(int id)
        {
            var req = new RestRequest($"/Seris/GetSeri/{id}", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Seri>(data)!;
            return result;
        }
        public static async Task AddSeri(Seri Seri)
        {
            var request = new RestRequest($"/Seris/AddSeri", Method.Post);
            request.AddJsonBody(Seri);
            await Program.Client_2.ExecuteAsync(request);
        }
        public static async Task DeleteSeri(int? id)
        {
            var req = new RestRequest($"/Seris/DeleteSeri/{id}", Method.Delete);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task UpdateSeri(int? id, Seri Seri)
        {
            var req = new RestRequest($"/Seris/UpdateSeri/{id}", Method.Put);
            req.AddJsonBody(Seri);
            await Program.Client_2.ExecuteAsync(req);
        }
    }
}
