using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class WarehouseServices
    {
        public static async Task<IList<Warehouse>> GetWarehouses()
        {
            var req = new RestRequest($"/Warehouses/GetWarehouses", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Warehouse>>(data)!;
            return result;
        }
        public static async Task<Warehouse?> GetWarehouse(int? id)
        {
            var req = new RestRequest($"/Warehouses/GetWarehouse/{id}", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Warehouse>(data)!;
            return result;
        }
        public static async Task AddWarehouse(Warehouse Warehouse)
        {
            var request = new RestRequest($"/Warehouses/AddWarehouse", Method.Post);
            request.AddJsonBody(Warehouse);
            await Program.Client_2.ExecuteAsync(request);
        }
        public static async Task DeleteWarehouse(int? id)
        {
            var req = new RestRequest($"/Warehouses/DeleteWarehouse/{id}", Method.Delete);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task UpdateWarehouse(int? id, Warehouse Warehouse)
        {
            var req = new RestRequest($"/Warehouses/UpdateWarehouse/{id}", Method.Put);
            req.AddJsonBody(Warehouse);
            await Program.Client_2.ExecuteAsync(req);
        }
    }
}
