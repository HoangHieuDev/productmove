using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMove_Model
{
    public class User
    {
        public int idUser { get; set; }
        public string userName { get; set; } = null!;
        public string passWord { get; set; } = null!;
        public string decentralization { get; set; } = null!;
        public string address { get; set; } = null!;
        public string email { get; set; } = null!;
    }
}
