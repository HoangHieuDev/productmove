using Dapper;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;
namespace SureSellOrganizers_API.Services
{
    public class WarehouseRepository : IWarehouseRepository
    {
        public void AddWarehouse(Warehouse warehouse)
        {
            try
            {
                Program.Sql.Execute("insert into Warehouse(totalProduct, idUser) values (@totalProduct, @idUser)",
                    new Warehouse
                    {
                        totalProduct = warehouse.totalProduct,
                        idUser = warehouse.idUser,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteWarehouse(int id)
        {
            try
            {
                var data = Program.Sql.Execute("Delete From Warehouse where idWarehouse = @idWarehouse", new Warehouse
                {
                    idWarehouse = id
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Warehouse GetWarehouse(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Warehouse>("Select * from Warehouse where idWarehouse = @idWarehouse", new Warehouse
                {
                    idWarehouse = id
                });

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Warehouse> GetWarehouses()
        {
            try
            {
                var data = Program.Sql.Query<Warehouse>("Select * from Warehouse").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateWarehouse(int id, Warehouse warehouse)
        {
            try
            {
                var data = Program.Sql.Execute("UPDATE Warehouse SET totalProduct = @totalProduct, idUser = @idUser " +
                    "where idWarehouse = @idWarehouse",
                new Warehouse
                {
                    idWarehouse = id,
                    totalProduct = warehouse.totalProduct,
                    idUser = warehouse.idUser,
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
