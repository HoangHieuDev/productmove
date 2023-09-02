using Dapper;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Services
{
    public class ImportRepository : IImportRepository
    {
        public void AddImport(Import import)
        {
            try
            {
                Program.Sql.Execute("INSERT INTO Import (idProduct, idWarehouse, ImportDate, total) " +
                                    "VALUES(@idProduct, @idWarehouse, @ImportDate, @total)",
                    new Import
                    {
                        idProduct = import.idProduct,
                        idWarehouse = import.idWarehouse,
                        importDate = import.importDate,
                        total = import.total,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteImport(int id)
        {
            try
            {
                var data = Program.Sql.Execute("Delete From Import where idImport = @idImport", new Import
                {
                    idImport = id
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Import GetImport(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Import>("Select * from Import where idImport = @idImport", new Import
                {
                    idImport = id
                });

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Import> GetImports()
        {
            try
            {
                var data = Program.Sql.Query<Import>("select " +
                    "i.idImport, i.idProduct, " +
                    "i.idWarehouse, i.importDate, " +
                    "i.total, " +
                    "p.idCategory, p.productName, " +
                    "p.price, p.description, " +
                    "p.warrantyPeriod " +
                    "from Import i " +
                    "Left Join Product p " +
                    "ON i.idProduct = p.idProduct").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateImport(int id, Import import)
        {
            try
            {
                var data = Program.Sql.Execute("UPDATE dbo.Import " +
                    "SET idProduct = @idProduct" +
                    ", idWarehouse = @idWarehouse" +
                    ", ImportDate = @ImportDate" +
                    ", total = @total" +
                    " WHERE idImport = @idImport", new Import
                    {
                        idImport = id,
                        idProduct = import.idProduct,
                        idWarehouse = import.idWarehouse,
                        importDate = import.importDate,
                        total = import.total,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
