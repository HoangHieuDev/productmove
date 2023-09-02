using Dapper;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;
using System.Collections.Generic;

namespace SureSellOrganizers_API.Services
{
    public class ExportRepository : IExportRepository
    {
        public void AddExport(Export export)
        {
            try
            {
                Program.Sql.Execute("INSERT INTO Export (idProduct, idWarehouse, exportDate, total) " +
                                    "VALUES(@idProduct, @idWarehouse, @exportDate, @total)",
                    new Export
                    {
                        idProduct = export.idProduct,
                        idWarehouse = export.idWarehouse,
                        exportDate = export.exportDate,
                        total = export.total,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteExport(int id)
        {
            try
            {
                var data = Program.Sql.Execute("Delete From Export where idExport = @idExport", new Export
                {
                    idExport = id
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Export GetExport(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Export>("Select * from Export where idExport = @idExport", new Export
                {
                    idExport = id
                });

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Export> GetExports()
        {
            try
            {
                var data = Program.Sql.Query<Export>("select " +
                    "i.idexport, i.idProduct, " +
                    "i.idWarehouse, i.exportDate, " +
                    "i.total, " +
                    "p.idCategory, p.productName, " +
                    "p.price, p.description, " +
                    "p.warrantyPeriod " +
                    "from export i " +
                    "Left Join Product p " +
                    "ON i.idProduct = p.idProduct").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateExport(int id, Export export)
        {
            try
            {
                var data = Program.Sql.Execute("UPDATE dbo.Export " +
                    "SET idProduct = @idProduct" +
                    ", idWarehouse = @idWarehouse" +
                    ", exportDate = @exportDate" +
                    ", total = @total" +
                    " WHERE idExport = @idExport", new Export
                    {
                        idExport = id,
                        idProduct = export.idProduct,
                        idWarehouse = export.idWarehouse,
                        exportDate = export.exportDate,
                        total = export.total,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
