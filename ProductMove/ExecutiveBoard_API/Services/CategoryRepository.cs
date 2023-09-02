using Dapper;
using ExecutiveBoard_API.Interfaces;
using ProductMove_Model;

namespace ExecutiveBoard_API.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        public void AddCategory(Category category)
        {
            try
            {
                Program.Sql.Execute("Insert into Category(categoryName) " +
                                                              "values (@categoryName);", new
                                                              {
                                                                  category.categoryName
                                                              });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                var data = Program.Sql.Execute("DELETE FROM Category WHERE idCategory = @idCategory", new Category
                {
                    idCategory = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category GetCategory(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Category>("Select * from Category p where p.idCategory = @idCategory", new Category
                {
                    idCategory = id,
                });
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetCategorys()
        {
            try
            {
                var data = Program.Sql.Query<Category>("Select * from Category").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCategory(int id, Category category)
        {
            try
            {
                var data = Program.Sql.Execute("Update Category Set " +
                "categoryName = @categoryName " +
                "where idCategory = @idCategory;",
                new Category
                {
                    categoryName = category.categoryName,
                    idCategory = id
                }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
