using Dapper;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Services
{
    public class DetailWarehouseRepository : IDetailWarehouseRepository
    {
        public void AddDetailWarehouse(DetailWarehouse detailWarehouse)
        {
            try
            {
                Program.Sql.Execute("Insert into DetailWarehouse(idProduct, idWarehouse, totalProduct, productStatus) values " +
                    "(@idProduct, @idWarehouse, @totalProduct, @productStatus)", new DetailWarehouse
                    {
                        idProduct = detailWarehouse.idProduct,
                        idWarehouse = detailWarehouse.idWarehouse,
                        totalProduct = detailWarehouse.totalProduct,
                        productStatus = detailWarehouse.productStatus,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteDetailWarehouse(int id)
        {
            try
            {
                var data = Program.Sql.Execute("Delete From DetailWarehouse where idDetailWarehouse = @idDetailWarehouse", new DetailWarehouse
                {
                    idDetailWarehouse = id
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DetailWarehouse GetDetailWarehouse(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<DetailWarehouse>("Select * from DetailWarehouse where idDetailWarehouse = @idDetailWarehouse", new DetailWarehouse
                {
                    idDetailWarehouse = id
                });

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DetailWarehouse> GetDetailWarehouses()
        {
            try
            {
                var data = Program.Sql.Query<DetailWarehouse>("SELECT d.idDetailWarehouse, d.idWarehouse, d.productStatus, d.totalProduct, d.idProduct, u.decentralization, p.productName, u.address FROM DetailWarehouse d left join Warehouse w ON d.idWarehouse = w.idWarehouse left join [User] u ON w.idUser = u.idUser left join Product p  ON d.idProduct = p.idProduct").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateDetailWarehouse(int id, DetailWarehouse detailWarehouse)
        {
            try
            {
                var data = Program.Sql.Execute("UPDATE DetailWarehouse SET idProduct = @idProduct, idWarehouse = @idWarehouse, " +
                    "totalProduct = @totalProduct,productStatus = @productStatus " +
                    "Where idDetailWarehouse = @idDetailWarehouse", new DetailWarehouse
                    {
                        idDetailWarehouse = id,
                        idProduct = detailWarehouse.idProduct,
                        idWarehouse = detailWarehouse.idWarehouse,
                        totalProduct = detailWarehouse.totalProduct,
                        productStatus = detailWarehouse.productStatus,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
