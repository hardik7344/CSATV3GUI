using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using OwnYITCSAT.DataAccessLayer;

namespace OwnYITCSAT.DataAccessLayer
{
    
    public class DatabaseConnection
    {
        OwnYITCommon Objcom = new OwnYITCommon();
        private int DBServer_Type = 0;

        private int DBServer_Port = 1433;

        private string strDBServer = "localhost";
        private String strUser = "sa";
        private String strPassword = "demo@123";
        private String strDBName = "ownyitcsat";

        //private string strDBServer = OwnYITCommon.ReadSettingFiles("DBSettings.xml", "DBServer");
        //private String strUser = OwnYITCommon.ReadSettingFiles("DBSettings.xml", "UserName");
        //private String strPassword = OwnYITCommon.ReadSettingFiles("DBSettings.xml", "Password");
        //private String strDBName = OwnYITCommon.ReadSettingFiles("DBSettings.xml", "DBName");

        public DatabaseConnection(DBSettings db_settings)
        {
            this.DBServer_Type = db_settings.getServerType();

            this.DBServer_Port = db_settings.getServerPort();

            this.strDBServer = db_settings.getServer();
            this.strUser = db_settings.getUser();
            this.strPassword = db_settings.getPassword();
            this.strDBName = db_settings.getName();

            //this.DBServer_Type = OwnYITConstant.MSSQL_SERVER;
        }

        //public DatabaseConnection(string strDBServer, int DBServer_Port, String strUser, String strPassword, String strDBName)
        //{
        //    // Handle Database Connection Here
        //    this.strDBServer = strDBServer;

        //    this.DBServer_Port = DBServer_Port;

        //    this.strUser = strUser;
        //    this.strPassword = strPassword;
        //    this.strDBName = strDBName;

        //    this.DBServer_Type = OwnYITConstant.MYSQL_SERVER;
        //}

        //public DatabaseConnection(int DBServer_Type, string strDBServer, int DBServer_Port, String strUser, String strPassword, String strDBName)
        //{
        //    // Handle Database Connection Here
        //    this.strDBServer = strDBServer;

        //    this.DBServer_Port = DBServer_Port;

        //    this.strUser = strUser;
        //    this.strPassword = strPassword;
        //    this.strDBName = strDBName;

        //    this.DBServer_Type = DBServer_Type;
        //}

        public void SetDBServerType(int DBServer_Type)
        {
            this.DBServer_Type = DBServer_Type;
        }

        public int GetDBServerType()
        {
            return this.DBServer_Type;
        }
        
        public SqlConnection CreateSQLConnection()
        {
            SqlConnection objConn = null;

            string strConnstr = null;

            if (this.DBServer_Type == OwnYITConstant.MSSQL_SERVER)
            {
                strConnstr = "workstation id=localhost"; //+ readconfig("workstationid");
                strConnstr += ";packet size=4096;data source=" + this.strDBServer;
                strConnstr += ";user id=" + this.strUser;
                strConnstr += ";password=" + this.strPassword;
                strConnstr += ";persist security info = False;initial catalog=" + this.strDBName + ";Max Pool Size=1000";

                objConn = new SqlConnection();
                Objcom.WriteLog("DatabaseConnection", "ConnectionString :"+ strConnstr);
                objConn.ConnectionString = strConnstr;

                strConnstr = null;
            }
            return objConn;
        }

        public MySqlConnection CreateMySQLConnection()
        {
            MySqlConnection objConn = null;

            string strConnstr = null;

            if (DBServer_Type == OwnYITConstant.MYSQL_SERVER)
            {
                strConnstr = "Server=" + this.strDBServer;
                strConnstr += ";Port=" + this.DBServer_Port;
                strConnstr += ";Database=" + this.strDBName;
                strConnstr += ";user id=" + this.strUser;
                strConnstr += ";password=" + this.strPassword;
                strConnstr += ";Pooling = False;respect binary flags=false";
                //strConnstr = "server=localhost";
                //strConnstr += ";port=3306";
                //strConnstr += ";database=ownyitcsat";
                //strConnstr += ";user id=root";
                //strConnstr += ";password=demo@123";
                // strConnstr += ";Pooling = False;respect binary flags=false";

                objConn = new MySqlConnection();
                objConn.ConnectionString = strConnstr;
            }
            return objConn;
        }

        
    }
}