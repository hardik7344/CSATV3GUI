using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OwnYITCSAT.DataAccessLayer
{
    public class Configuration
    {
        private DataTable data_table = null;
        public static string username = "";
        public static string groupid = "";
        public static string logo = "";
        public static string productname = "";
        public Configuration(){}

        public Configuration(DataTable data_table) {
            this.data_table = data_table;
        }

        public void load(DataTable data_table)
        {
            this.data_table = data_table;
        }

        //public string getProperty(String strSection, String strParameter)
        //{

        //}

        //public void setProperty(String strSection, String strParameter, string strValue)
        //{

        //}

        public void save()
        {

        }

        public void close()
        {

        }
    }
}