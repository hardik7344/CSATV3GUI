using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class StackedBarchart
    {
        public string OU_Name { get; set; }

        //public int Value { get; set; }
        public Int64 DataLeakage { get; set; }

        //public Int64 Printer { get; set; }
        public Int64 Internet_Modem { get; set; }
        public Int64 Removable_media { get; set; }

    }
}
