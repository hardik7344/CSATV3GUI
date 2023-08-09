using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class User
    {
       public Int64 device_id { get; set; }
        public string UserName { get; set; }
        public string groupname { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public string InstalledDate { get; set; }
        public string status { get; set; }
    }
}
