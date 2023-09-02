using ProductMove_Model;

namespace SureSellOrganizers_API.Interfaces
{
    public interface IReportRepository
    {
        List<Report> GetReports();
        Report GetReport(int id);
        void DeleteReport(int id);
        void UpdateReport(int id, Report report);
        void AddReport(Report report);
    }
}
