using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class BillServices
    {
        public static async Task<IList<Bill>> GetBills()
        {
            var req = new RestRequest($"/Bills/GetBills", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Bill>>(data)!;
            return result;
        }
        public static async Task<Bill?> GetBill(int id)
        {
            var req = new RestRequest($"/Bills/GetBill/{id}", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Bill>(data)!;
            return result;
        }
        public static async Task AddBill(Bill Bill)
        {
            var request = new RestRequest($"/Bills/AddBill", Method.Post);
            request.AddJsonBody(Bill);
            await Program.Client_2.ExecuteAsync(request);
        }
        public static async Task DeleteBill(int? id)
        {
            var req = new RestRequest($"/Bills/DeleteBill/{id}", Method.Delete);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task UpdateBill(int? id, Bill Bill)
        {
            var req = new RestRequest($"/Bills/UpdateBill/{id}", Method.Put);
            req.AddJsonBody(Bill);
            await Program.Client_2.ExecuteAsync(req);
        }
    }
}
