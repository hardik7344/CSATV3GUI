using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class LoadData
    {
        public int draw { get; set; }
        public object data { get; set; }
        public Int64 recordsTotal { get; set; }
        public Int64 recordsFiltered { get; set; }

    }
        public class agent
        {
            public long device_id { get; set; }
            public string device_name { get; set; }
            public string ip { get; set; }
            public string mac { get; set; }
            public string ou_longname { get; set; }
            public string client_version { get; set; }
            public string date { get; set; }
            public string login_user { get; set; }
            public string data1 { get; set; }
            public string ostype { get; set; }
            public string generatedate { get; set; }
            public int status { get; set; }
            public int active { get; set; }
            public long ou_id { get; set; }
        }

}
