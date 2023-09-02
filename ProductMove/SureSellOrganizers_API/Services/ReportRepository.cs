using Dapper;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;
using System.Collections.Generic;

namespace SureSellOrganizers_API.Services
{
    public class ReportRepository : IReportRepository
    {
        public void AddReport(Report report)
        {
            try
            {
                Program.Sql.Execute("INSERT INTO Report (typeOfReport, Time, idWarehouse) VALUES(@typeOfReport, @time, @idWarehouse)",
                    new Report
                    {
                        typeOfReport = report.typeOfReport,
                        time = report.time,
                        idWarehouse = report.idWarehouse,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteReport(int id)
        {
            try
            {
                var data = Program.Sql.Execute("Delete From Report where idReport = @idReport", new Report
                {
                    idReport = id
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Report GetReport(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Report>("Select * from Report where idReport = @idReport", new Report
                {
                    idReport = id
                });

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Report> GetReports()
        {
            try
            {
                var data = Program.Sql.Query<Report>("Select * from Report").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateReport(int id, Report report)
        {
            try
            {
                var data = Program.Sql.Execute("UPDATE Report SET " +
                    "typeOfReport = @typeOfReport, " +
                    "time = @time, " +
                    "idWarehouse = @idWarehouse " +
                    "WHERE idReport = @idReport",
                    new Report
                    {
                        idReport = id,
                        typeOfReport = report.typeOfReport,
                        idWarehouse = report.idWarehouse,
                        time = report.time,
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
