using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace OwnYITCSAT.Models
{
    public class Menu
    {
        public int MID;
        public string MenuName;
        public string MenuURL;
        public int MenuParentID;
    }
}