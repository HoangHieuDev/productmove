using ProductMove_Model;

namespace ExecutiveBoard_API.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategorys();
        Category GetCategory(int id);
        void DeleteCategory(int id);
        void UpdateCategory(int id, Category category);
        void AddCategory(Category category);
    }
}
