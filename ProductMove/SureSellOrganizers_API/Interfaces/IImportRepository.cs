using ProductMove_Model;

namespace SureSellOrganizers_API.Interfaces
{
    public interface IImportRepository
    {
        List<Import> GetImports();
        Import GetImport(int id);
        void DeleteImport(int id);
        void UpdateImport(int id, Import Import);
        void AddImport(Import Import);
    }
}
