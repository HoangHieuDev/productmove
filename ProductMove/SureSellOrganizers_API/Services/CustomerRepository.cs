using Dapper;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            try
            {
                Program.Sql.Execute("Insert into Customer(customerName, customerPhone, customerAddress) values " +
                                    "(@customerName, @customerPhone, @customerAddress)", new Customer
                                    {
                                        customerName = customer.customerName,
                                        customerPhone = customer.customerPhone,
                                        customerAddress = customer.customerAddress,
                                    });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCustomer(int id)
        {
            try
            {
                var data = Program.Sql.Execute("Delete From Customer where idCustomer = @idCustomer", new Customer
                {
                    idCustomer = id
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer GetCustomer(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Customer>("Select * from Customer where idCustomer = @idCustomer", new Customer
                {
                    idCustomer = id
                });

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                var data = Program.Sql.Query<Customer>("Select * from Customer").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            try
            {
                var data = Program.Sql.Execute("UPDATE Customer SET customerName = @customerName, " +
                                                "customerPhone = @customerPhone, " +
                                                 "customerAddress = @customerAddress " +
                                                 "Where idCustomer = @idCustomer", new Customer
                                                 {
                                                     idCustomer = id,
                                                     customerName = customer.customerName,
                                                     customerPhone = customer.customerPhone,
                                                     customerAddress = customer.customerAddress,
                                                 });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
