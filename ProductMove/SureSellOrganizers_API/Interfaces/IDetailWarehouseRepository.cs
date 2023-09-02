using ProductMove_Model;

namespace SureSellOrganizers_API.Interfaces
{
    public interface IDetailWarehouseRepository
    {
        List<DetailWarehouse> GetDetailWarehouses();
        DetailWarehouse GetDetailWarehouse(int id);
        void DeleteDetailWarehouse(int id);
        void UpdateDetailWarehouse(int id, DetailWarehouse detailWarehouse);
        void AddDetailWarehouse(DetailWarehouse detailWarehouse);
    }
}
