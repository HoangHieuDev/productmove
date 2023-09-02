using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;
using System.Formats.Asn1;

namespace ProductMove_APP.Services
{
    public class ProductServices
    {
        public static async Task<IList<Product>> GetProducts()
        {
            var req = new RestRequest($"/Products/GetProducts", Method.Get);
            var res = await Program.Client.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Product>>(data)!;
            return result;
        }
        public static async Task<Product?> GetProduct(int id)
        {
            var req = new RestRequest($"/Products/GetProduct/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Product>(data)!;
            return result;
        }
        public static async Task AddProduct (Product product)
        {
            var request = new RestRequest($"/Products/AddProduct", Method.Post);
            request.AddJsonBody(product);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task DeleteProduct(int? id)
        {
            var req = new RestRequest($"/Products/DeleteProduct/{id}", Method.Delete);
            await Program.Client.ExecuteAsync(req);
        }
        public static async Task UpdateProduct(int? id, Product product)
        {
            var req = new RestRequest($"/Products/UpdateProduct/{id}", Method.Put);
            req.AddJsonBody(product);
            await Program.Client.ExecuteAsync(req);
        }
    }
}
