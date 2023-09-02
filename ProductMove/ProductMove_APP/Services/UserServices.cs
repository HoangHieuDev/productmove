using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class UserServices
    {
        public static async Task<IList<User>> GetUsers()
        {
            var req = new RestRequest($"/Users/GetUsers", Method.Get);
            var res = await Program.Client.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<User>>(data)!;
            return result;
        }
        public static async Task<User> GetUser(int id)
        {
            var req = new RestRequest($"/Users/GetUser/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<User>(data)!;
            return result;
        }
        public static async Task AddUser(User user)
        {
            var req = new RestRequest($"/Users/AddUser", Method.Post);
            req.AddJsonBody(user);
            await Program.Client.ExecuteAsync(req);
        }
        public static async Task DeleteUser(int id)
        {
            var req = new RestRequest($"/Users/DeleteUser/{id}", Method.Delete);
            await Program.Client.ExecuteAsync(req);
        }
        public static async Task UpdateUser(int? id, User user)
        {
            var req = new RestRequest($"/Users/UpdateUser/{id}", Method.Put);
            req.AddJsonBody(user);
            await Program.Client.ExecuteAsync(req);
        }
    }
}
