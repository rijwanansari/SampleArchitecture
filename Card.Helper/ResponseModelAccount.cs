using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Helper
{
   public class ResponseModelAccount
    {
        public bool success { get; set; }
        public string message { get; set; }
        public long accountNumber { get; set; }
        public decimal balance { get; set; }
        public string currency { get; set; }
    }
}
