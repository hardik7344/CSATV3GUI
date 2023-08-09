using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class Networktrafficchart1
    {
        public String Date { get; set; }
        public string hours { get; set; }

        //public UInt64 send_packets { get; set; }


        //public UInt64 received_packets { get; set; }


        public decimal send_packets { get; set; }
        public decimal received_packets { get; set; }
        public decimal send_size { get; set; }
        public decimal received_size { get; set; }
    }
}
