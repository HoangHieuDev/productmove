using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class DetailWarehouse
    {
        public int idDetailWarehouse { get; set; }
        public int? idWarehouse { get; set; }
        public int totalProduct { get; set; }
        public int idProduct { get; set; }
        public string? productStatus { get; set; } = default!;
        public string? productName { get; set; } = default!;
        public string? address { get; set; } = default!;
        public string? decentralization { get; set; } = default!;

    }
}
