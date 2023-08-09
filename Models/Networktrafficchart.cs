using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class Networktrafficchart
    {
        public String Date { get; set; }
        public string hours { get; set; }
      
        public Int64 send_packets { get; set; }
    

        public Int64 received_packets { get; set; }


        //public decimal send_packets { get; set; }
        //public decimal received_packets { get; set; }
        public Int64 send_size { get; set; }
        public Int64 received_size { get; set; }
    }
}
