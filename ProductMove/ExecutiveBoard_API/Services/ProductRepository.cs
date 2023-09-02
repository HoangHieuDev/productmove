using Dapper;
using ExecutiveBoard_API.Interfaces;
using ProductMove_Model;

namespace ExecutiveBoard_API.Services
{
    public class ProductRepository : IProductRepository
    {
        public void AddProduct(Product product)
        {
            try
            {
                Program.Sql.Execute("INSERT INTO Product(idCategory, productName,price,description, warrantyPeriod) " +
                                                              "VALUES (@idcategory, @productName, @price, @description, @warrantyPeriod);", new
                                                              {
                                                                  idcategory = product.idCategory,
                                                                  productName = product.productName,
                                                                  price = product.price,
                                                                  description = product.description,
                                                                  warrantyPeriod = product.warrantyPeriod
                                                              });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var data = Program.Sql.Execute("DELETE FROM Product WHERE idProduct = @idproduct", new Product
                {
                    idProduct = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetProduct(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Product>("Select * from product p where p.idProduct = @idProduct", new Product
                {
                    idProduct = id,
                });
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Product> GetProducts()
        {
            try
            {
                var data = Program.Sql.Query<Product>(" Select idProduct, p.idCategory , productName , price, description, warrantyPeriod , c.categoryName from Product p LEFT JOIN Category c ON p.idCategory = c.idCategory ").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(int id, Product product)
        {
            try
            {
                var data = Program.Sql.Execute("Update Product Set " +
                "idCategory = @idCategory, " +
                "productName = @productName, " +
                "price = @price, " +
                "description = @description," +
                "warrantyPeriod = @warrantyPeriod " +
                "where idProduct = @idProduct;",
                new Product
                {
                    idCategory = product.idCategory,
                    productName = product.productName,
                    price = product.price,
                    description = product.description,
                    warrantyPeriod = product.warrantyPeriod,
                    idProduct = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
