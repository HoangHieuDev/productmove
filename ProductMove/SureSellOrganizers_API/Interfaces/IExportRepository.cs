using ProductMove_Model;

namespace SureSellOrganizers_API.Interfaces
{
    public interface IExportRepository
    {
        List<Export> GetExports();
        Export GetExport(int id);
        void DeleteExport(int id);
        void UpdateExport(int id, Export export);
        void AddExport(Export export);
    }
}
