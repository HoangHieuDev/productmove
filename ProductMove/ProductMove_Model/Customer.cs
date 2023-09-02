using System;

namespace ProductMove_Model
{
    public class Customer
    {
        public int idCustomer { get; set; }
        public string customerName { get; set; } = null!;
        public string customerPhone { get; set; } = null!;
        public string customerAddress { get; set; } = null!;
    }
}
