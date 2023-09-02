using Newtonsoft.Json;
using ProductMove_Model;
using RestSharp;

namespace ProductMove_APP.Services
{
    public class CategoryServices
    {
        public static async Task<IList<Category>> GetCategorys()
        {
            var req = new RestRequest($"/Categorys/GetCategorys", Method.Get);
            var res = await Program.Client.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<IList<Category>>(data)!;
            return result;
        }
        public static async Task<Category?> GetCategory(int id)
        {
            var req = new RestRequest($"/Categorys/GetCategory/{id}", Method.Get);
            var res = await Program.Client.ExecuteAsync(req);
            var data = res.Content!;
            var result = JsonConvert.DeserializeObject<Category>(data)!;
            return result;
        }
        public static async Task AddCategory(Category category)
        {
            var request = new RestRequest($"/Categorys/AddCategory", Method.Post);
            request.AddJsonBody(category);
            await Program.Client.ExecuteAsync(request);
        }
        public static async Task DeleteCategory(int? id)
        {
            var req = new RestRequest($"/Categorys/DeleteCategory/{id}", Method.Delete);
            await Program.Client.ExecuteAsync(req);
        }
        public static async Task UpdateCategory(int? id, Category category)
        {
            var req = new RestRequest($"/Categorys/UpdateCategory/{id}", Method.Put);
            req.AddJsonBody(category);
            await Program.Client.ExecuteAsync(req);
        }
    }
}
