using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnYITCSAT.DataAccessLayer
{
    public class DBSettings
    {
        private int SERVER_TYPE = 0;

        private int SERVER_PORT = 1433;

        private String strServer = "localhost";
        private String strUser = "sa";
        private String strPassword = "demo@123";
        private String strName = "ownyitcsat";

        public DBSettings() { }

        public int getServerType()
        {
            return this.SERVER_TYPE;
        }

        public void setServerType(int SERVER_TYPE)
        {
            this.SERVER_TYPE = SERVER_TYPE;
        }

        public int getServerPort()
        {
            return this.SERVER_PORT;
        }

        public void setServerPort(int SERVER_PORT)
        {
            this.SERVER_PORT = SERVER_PORT;
        }

        public string getServer()
        {
            return this.strServer;
        }
        
        public void setServer(String strServer)
        {
            this.strServer = strServer;
        }

        public string getUser()
        {
            return this.strUser;
        }

        public void setUser(String strUser)
        {
            this.strUser = strUser;
        }

        public string getPassword()
        {
            return this.strPassword;
        }

        public void setPassword(String strPassword)
        {
            this.strPassword = strPassword;
        }

        public string getName()
        {
            return this.strName;
        }

        public void setName(String strName)
        {
            this.strName = strName;
        }
    }
}