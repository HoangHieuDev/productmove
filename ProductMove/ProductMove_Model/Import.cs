using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class Import
    {
        public int idImport { get; set; }
        public int idProduct { get; set; }
        public int? idWarehouse { get; set; }
        public int total { get; set; }
        public string importDate { get; set; } = default!;
        //public string productSeri { get; set; } = null!;
        public int idCategory { get; set; }
        public string? productName { get; set; } = default!;
        public string? price { get; set; } = default!;
        public string? description { get; set; } = default!;
        public string? warrantyPeriod { get; set; } = default!;
    }
}
