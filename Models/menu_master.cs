using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public partial class menu_master
    {      
        public int menu_id { get; set; }
        public string menu_name { get; set; }
        public int menu_parent_id { get; set; }
        public int menu_level { get; set; }
        public int active { get; set; }
        public string menu_url { get; set; }
        public int menu_priority { get; set; }
        public int ownyit { get; set; }
        public int ownyitcsat { get; set; }      
    }
   
}
