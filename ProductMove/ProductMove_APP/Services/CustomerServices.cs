using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class CustomerServices
    {
        public static async Task<IList<Customer>> GetCustomers()
        {
            var req = new RestRequest($"/Customers/GetCustomers", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Customer>>(data)!;
            return result;
        }
        public static async Task<Customer?> GetCustomer(int id)
        {
            var req = new RestRequest($"/Customers/GetCustomer/{id}", Method.Get);
            var res = await Program.Client_2.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Customer>(data)!;
            return result;
        }
        public static async Task AddCustomer(Customer Customer)
        {
            var request = new RestRequest($"/Customers/AddCustomer", Method.Post);
            request.AddJsonBody(Customer);
            await Program.Client_2.ExecuteAsync(request);
        }
        public static async Task DeleteCustomer(int? id)
        {
            var req = new RestRequest($"/Customers/DeleteCustomer/{id}", Method.Delete);
            await Program.Client_2.ExecuteAsync(req);
        }
        public static async Task UpdateCustomer(int? id, Customer Customer)
        {
            var req = new RestRequest($"/Customers/UpdateCustomer/{id}", Method.Put);
            req.AddJsonBody(Customer);
            await Program.Client_2.ExecuteAsync(req);
        }
    }
}
