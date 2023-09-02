using Dapper;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;
namespace SureSellOrganizers_API.Services
{
    public class SeriRepository : ISeriRepository
    {
        public void AddSeri(Seri seri)
        {
            try
            {
                Program.Sql.Execute("INSERT INTO Seri (seriName, productionTime, idProduct, idWarehouse, productStatus) " +
                                    "VALUES (@seriName, @productionTime, @idProduct,@idWarehouse, @productStatus)",
                    new Seri
                    {
                        seriName = seri.seriName,
                        productionTime = seri.productionTime,
                        idProduct = seri.idProduct,
                        idWarehouse = seri.idWarehouse,
                        productStatus = seri.productStatus,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSeri(int id)
        {
            try
            {
                var data = Program.Sql.Execute("Delete From Seri where idSeri = @idSeri", new Seri
                {
                    idSeri = id
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Seri GetSeri(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Seri>("Select * from Seri where idSeri = @idSeri", new Seri
                {
                    idSeri = id
                });

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Seri> GetSeries()
        {
            try
            {
                var data = Program.Sql.Query<Seri>("Select * from Seri").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateSeri(int id, Seri seri)
        {
            try
            {
                var data = Program.Sql.Execute("UPDATE Seri SET seriName = @seriName, " +
                                                "productionTime = @productionTime, " +
                                                "idProduct = @idProduct, " +
                                                "idWarehouse = @idWarehouse, " +
                                                "productStatus = @productStatus " +
                                                "WHERE idSeri = @idSeri",
                new Seri
                {
                    idSeri = id,
                    seriName = seri.seriName,
                    productionTime = seri.productionTime,
                    idProduct = seri.idProduct,
                    idWarehouse = seri.idWarehouse,
                    productStatus = seri.productStatus,
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
