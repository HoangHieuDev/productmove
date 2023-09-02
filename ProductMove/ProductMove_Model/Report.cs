using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class Report
    {
        public int idReport { get; set; }
        public int typeOfReport { get; set; }
        public string time { get; set; } = null!;
        public int idWarehouse { get; set; }
    }
}
