using Dapper;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Services
{
    public class BillRepository : IBillRepository
    {
        public void AddBill(Bill bill)
        {
            try
            {
                Program.Sql.Execute("Insert into Bill(dateOfBill,address,idCustomer,idSeri)  " +
                                                              "values (@dateOfBill,@address,@idCustomer,@idSeri);", new Bill
                                                              {
                                                                  dateOfBill = bill.dateOfBill,
                                                                  address = bill.address,
                                                                  idCustomer = bill.idCustomer,
                                                                  idSeri = bill.idSeri
                                                              });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteBill(int id)
        {
            try
            {
                var data = Program.Sql.Execute("DELETE FROM Bill WHERE idBill = @idBill", new Bill
                {
                    idBill = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Bill GetBill(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<Bill>("Select * from Bill where idBill = @idBill", new Bill
                {
                    idBill = id,
                });
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Bill> GetBills()
        {
            try
            {
                var data = Program.Sql.Query<Bill>("Select * from Bill").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBill(int id, Bill bill)
        {
            try
            {
                var data = Program.Sql.Execute("Update Bill Set " +
                "dateOfBill = @dateOfBill, " +
                "address = @address, " +
                "idCustomer = @idCustomer, " +
                "idSeri = @idSeri  " +
                "where idBill = @idBill;",
                new Bill
                {
                    dateOfBill = bill.dateOfBill,
                    address = bill.address,
                    idCustomer = bill.idCustomer,
                    idSeri = bill.idSeri,
                    idBill = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
