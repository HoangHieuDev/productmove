using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class DetailWarehouseServices
    {
        public static async Task<IList<DetailWarehouse>> GetDetailWarehouses()
        {
            var req = new RestRequest($"/DetailWarehouses/GetDetailWarehouses", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<DetailWarehouse>>(data)!;
            return result;
        }
        public static async Task<DetailWarehouse> GetDetailWarehouse(int id)
        {
            var req = new RestRequest($"/DetailWarehouses/GetDetailWarehouse/{id}", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<DetailWarehouse>(data)!;
            return result;
        }
        public static async Task AddDetailWarehouse(DetailWarehouse DetailWarehouse)
        {
            var req = new RestRequest($"/DetailWarehouses/AddDetailWarehouse", Method.Post);
            req.AddJsonBody(DetailWarehouse);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task DeleteDetailWarehouse(int id)
        {
            var req = new RestRequest($"/DetailWarehouses/DeleteDetailWarehouse/{id}", Method.Delete);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task UpdateDetailWarehouse(int? id, DetailWarehouse DetailWarehouse)
        {
            var req = new RestRequest($"/DetailWarehouses/UpdateDetailWarehouse/{id}", Method.Put);
            req.AddJsonBody(DetailWarehouse);
            await Program.Client_2.ExecuteAsync(req);
        }
    }
}
