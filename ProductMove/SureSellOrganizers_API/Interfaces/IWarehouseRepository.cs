using ProductMove_Model;

namespace SureSellOrganizers_API.Interfaces
{
    public interface IWarehouseRepository
    {
        List<Warehouse> GetWarehouses();
        Warehouse GetWarehouse(int id);
        void DeleteWarehouse(int id);
        void UpdateWarehouse(int id, Warehouse warehouse);
        void AddWarehouse(Warehouse warehouse);
    }
}
