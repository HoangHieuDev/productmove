using ProductMove_Model;

namespace SureSellOrganizers_API.Interfaces
{
    public interface IBillRepository
    {
        List<Bill> GetBills();
        Bill GetBill(int id);
        void DeleteBill(int id);
        void UpdateBill(int id, Bill bill);
        void AddBill(Bill bill);
    }
}
