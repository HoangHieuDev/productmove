using ProductMove_Model;

namespace ExecutiveBoard_API.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        void DeleteProduct(int id);
        void UpdateProduct(int id, Product product);
        void AddProduct(Product product);
    }
}
