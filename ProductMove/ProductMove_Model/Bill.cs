using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class Bill
    {
        public int idBill { get; set; }
        public int idCustomer { get; set; }
        public int idSeri { get; set; }
        public string dateOfBill { get; set; } = null!;
        public string address { get; set; } = null!;
    }
}
