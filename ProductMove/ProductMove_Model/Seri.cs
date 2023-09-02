using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class Seri
    {
        public int idSeri { get; set; }
        public int idProduct { get; set; }
        public int? idWarehouse { get; set; }
        public string seriName { get; set; } = null!;
        public string productionTime { get; set; } = null!;
        public string productStatus { get; set; } = null!;
    }
}
