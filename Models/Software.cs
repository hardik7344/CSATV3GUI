using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class Software
    {
        //public string device_name { get; set; }
        //public string ip { get; set; }
        public Int64 deviceid { get; set; }
        public string software { get; set; }
        public string publisher { get; set; }

        public string version { get; set; }
        public string date { get; set; }
        public string location { get; set; }
        public string source { get; set; }
        public string uninstallstring { get; set; }
        public string software_guid { get; set; }
        public string authorizetype { get; set; }
        
    }
}
