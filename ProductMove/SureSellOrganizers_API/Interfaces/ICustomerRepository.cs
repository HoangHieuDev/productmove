using ProductMove_Model;

namespace SureSellOrganizers_API.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        Customer GetCustomer(int id);
        void DeleteCustomer(int id);
        void UpdateCustomer(int id, Customer customer);
        void AddCustomer(Customer customer);
    }
}
