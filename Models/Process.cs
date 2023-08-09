using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class Process
    {

        public Int64 device_id { get; set; }
        public string process_name { get; set; }

        public string process_id { get; set; }

        public string process_parentid_id { get; set; }
        public string process_desc { get; set; }
        public string process_exec_path { get; set; }

        public string time { get; set; }

        public string authorizetype { get; set; }
        // public string authorize { get; set; }
    }
}
