using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class SatckedBarchart1
    {
        public string Alert_type { get; set; }

        //public int Value { get; set; }
        public Int64 Authorise { get; set; }

        public Int64 Unauthorise { get; set; }

        public Int64 Remaining { get; set; }
    }
}
