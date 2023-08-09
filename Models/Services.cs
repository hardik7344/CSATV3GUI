using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class Services
    {

        public Int64 device_id { get; set; }
        public string service_name { get; set; }
        public string display_name { get; set; }

        public string state { get; set; }
        public string startmode { get; set; }
        public string startname { get; set; }
       
    }
}
