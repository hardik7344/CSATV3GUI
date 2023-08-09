using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

using MySql.Data.MySqlClient;

namespace OwnYITCSAT.DataAccessLayer
{
     
    public class DatabaseHandler
    {
        // private int DB_SERVER_TYPE = 0;
        public int DB_SERVER_TYPE = 0;
        //private int DBServer_Port = 1433;

        //private String strDBServer = "localhost";
        //private String strUser = "sa";
        //private String strPassword = "demo@123";
        //private String strDBName = "ownyitcsat";

        public SqlConnection objConn = null;
        private SqlCommand objCommand = null;
        private SqlDataAdapter objAdapter = null;

        private MySqlConnection objConn_mysql = null;
        private MySqlCommand objCommand_mysql = null;
        private MySqlDataAdapter objAdapter_mysql = null;
        OwnYITCommon objcommon = new OwnYITCommon();
        public DatabaseHandler(DBSettings db_settings)
        {
            // Handle Database Connection Here

            this.DB_SERVER_TYPE = OwnYITConstant.db_settings.getServerType();

            DatabaseConnection connection = new DatabaseConnection(db_settings);

            if (this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER)
            {
                this.objConn_mysql = connection.CreateMySQLConnection();
               // this.objConn_mysql.Open();
                this.objCommand_mysql = new MySqlCommand("", this.objConn_mysql);
                this.objAdapter_mysql = new MySqlDataAdapter(this.objCommand_mysql);
               

            }
            else
            {
                this.objConn = connection.CreateSQLConnection();

                this.objCommand = new SqlCommand("", this.objConn);

                this.objAdapter = new SqlDataAdapter(this.objCommand);
            }
        }

        public bool isAlive()
        {
            bool bFlag = false;

            if (this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER && this.objConn_mysql != null)
            {

                if (this.objConn_mysql.State == ConnectionState.Open)
                {
                    bFlag = true;
                }

            }else if (this.objConn != null){

                if (this.objConn.State == ConnectionState.Open)
                {
                    bFlag = true;
                }
            }
            return bFlag;
        }
        public string getSQLDateFunc()
        {

            String strFuncName = null;

            if (this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER)
            {
                strFuncName = "now()";
            }
            else
            {
                strFuncName = "getdate()";
            }
            return strFuncName;
        }

        public int execute(String strQuery)
        {
            //objcommon.WriteLog("DatabaseHandler", "execute Query : " + strQuery.ToString());
            int counter = 1;

            try
            {
                if (!isAlive())
                {
                    //this.objConn.Open();
                    if (this.DB_SERVER_TYPE==0)
                     this.objConn.Open();
                    else
                        this.objConn_mysql.Open();
                }
                if(this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER){

                    this.objCommand_mysql.CommandText = strQuery;
                    this.objCommand_mysql.CommandType = CommandType.Text;
                    
                    counter = this.objCommand_mysql.ExecuteNonQuery();
                    objConn_mysql.Close();

                }
                else if(this.DB_SERVER_TYPE == OwnYITConstant.MSSQL_SERVER){

                    this.objCommand.CommandText = strQuery;
                    this.objCommand.CommandType = CommandType.Text;
                    this.objCommand.CommandTimeout = 120;
                    this.objCommand.ExecuteNonQuery();
                    objConn.Close();
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("DatabaseHandler", "Exception From execute Query : " + ex.Message.ToString()+ " , Query : " + strQuery.ToString());
                objConn_mysql.Close();
                ResetDBConnection();
                counter = -1;
            }
            return counter;
        }
      
        public String getValue(String strQuery)
        {
            objcommon.WriteLog("DatabaseHandler", "getValue Query : " + strQuery.ToString());
            String strValue = null;

            try
            {
                if(!isAlive()){
                    // this.objConn.Open();
                    if (this.DB_SERVER_TYPE == 0)
                        this.objConn.Open();
                    else
                        this.objConn_mysql.Open();
                }
                object data = null;

                if (this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER)
                {

                    this.objCommand_mysql.CommandText = strQuery;
                    this.objCommand_mysql.CommandType = CommandType.Text;

                    data = this.objCommand_mysql.ExecuteScalar();
                    objConn_mysql.Close();
                }
                else if (this.DB_SERVER_TYPE == OwnYITConstant.MSSQL_SERVER)
                {

                    this.objCommand.CommandText = strQuery;
                    this.objCommand.CommandType = CommandType.Text;
                    this.objCommand.CommandTimeout = 120;
                    data = this.objCommand.ExecuteScalar();
                    objConn.Close();
                }
                if(data != null){
                    strValue = data.ToString();
                }
                
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("DatabaseHandler", "Exception From getValue Query : " + ex.Message.ToString());
                objConn_mysql.Close();
                ResetDBConnection();
            }
            finally { 

            }
            return strValue;
        }

        public int getIntValue(String strQuery)
        {
            int value = 0;
            try
            {
                String strValue = getValue(strQuery);

                if (strValue != null)
                {
                    value = Convert.ToInt16(strValue);
                }
            }
            catch (Exception)
            {
                ResetDBConnection();
            }
            return value;
        }

        public Int64 getLongValue(String strQuery)
        {
            Int64 value = 0;
            try
            {
                String strValue = getValue(strQuery);

                if (strValue != null)
                {
                    value = Convert.ToInt64(strValue);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("DatabaseHandler", "Exception From getLongValue  : " + ex.Message.ToString());
                ResetDBConnection();
            }
            return value;
        }

        public DataTable getDataTable(String strQuery)
        {
            DataTable data_table = new DataTable();
            try
            {
                 if(!isAlive()){
                    // this.objConn.Open();
                    if (this.DB_SERVER_TYPE == 0)
                        this.objConn.Open();
                    else
                        this.objConn_mysql.Open();
                }

                data_table.Clear();

                if (this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER){
                    
                    this.objCommand_mysql.CommandText = strQuery;
                    this.objCommand_mysql.CommandType = CommandType.Text;
                    this.objCommand_mysql.CommandTimeout = 120;
                    this.objAdapter_mysql.SelectCommand = this.objCommand_mysql;
                    this.objAdapter_mysql.Fill(data_table);

                    Int64 row_count_mysql = data_table.Rows.Count;

                    if (row_count_mysql == 0)
                    {
                        this.objAdapter_mysql.FillSchema(data_table, SchemaType.Source);
                    }

                    objConn_mysql.Close();
                }
                else if (this.DB_SERVER_TYPE == OwnYITConstant.MSSQL_SERVER){
                    objcommon.WriteLog("DatabaseHandler", "getDataTable start Query : " + strQuery.ToString());
                    this.objCommand.CommandText = strQuery;
                     this.objCommand.CommandType = CommandType.Text;
                     this.objCommand.CommandTimeout = 120;                    
                     this.objAdapter.SelectCommand = this.objCommand;
                     this.objAdapter.Fill(data_table);

                     Int64 row_count = data_table.Rows.Count;

                     if (row_count == 0)
                     {
                         this.objAdapter.FillSchema(data_table, SchemaType.Source);
                     }
                    objConn.Close();
                    objcommon.WriteLog("DatabaseHandler", "getDataTable Completed Query : " + strQuery.ToString());
                }

            }
            catch (Exception ex)
            {

                objcommon.WriteLog("DatabaseHandler", "Exception From getDataTable Query : " + ex.Message.ToString());
                objConn_mysql.Close();
                ResetDBConnection();
            }
            return data_table;
        }
        public DataTable getStoreprocedure(String strQuery)
        {
            objcommon.WriteLog("DatabaseHandler", "getStoreprocedure Query : " + strQuery.ToString());

            DataTable data_table = new DataTable();
            try
            {
                if (!isAlive())
                {
                    // this.objConn.Open();
                    if (this.DB_SERVER_TYPE == 0)
                        this.objConn.Open();
                    else
                        this.objConn_mysql.Open();
                }

                data_table.Clear();

                if (this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER)
                {

                    this.objCommand_mysql.CommandText = strQuery;
                    this.objCommand_mysql.CommandType = CommandType.StoredProcedure;
                    this.objCommand_mysql.CommandTimeout = 120;

                    this.objAdapter_mysql.SelectCommand = this.objCommand_mysql;
                    this.objAdapter_mysql.Fill(data_table);

                    Int64 row_count_mysql = data_table.Rows.Count;

                    if (row_count_mysql == 0)
                    {
                        this.objAdapter_mysql.FillSchema(data_table, SchemaType.Source);
                    }
                    objConn_mysql.Close();
                }
                else if (this.DB_SERVER_TYPE == OwnYITConstant.MSSQL_SERVER)
                {

                    this.objCommand.CommandText = strQuery;
                    this.objCommand.CommandType = CommandType.StoredProcedure;
                    this.objCommand.CommandTimeout = 120;

                    this.objAdapter.SelectCommand = this.objCommand;
                    this.objAdapter.Fill(data_table);

                    Int64 row_count = data_table.Rows.Count;

                    if (row_count == 0)
                    {
                        this.objAdapter.FillSchema(data_table, SchemaType.Source);
                    }
                    objConn.Close();
                }

            }
            catch (Exception ex)
            {

                objcommon.WriteLog("DatabaseHandler", "Exception From getStoreprocedure Query : " + ex.Message.ToString());
                objConn_mysql.Close();
            }
            return data_table;
        }
        public void close()
        {
            if (isAlive())
            {
                if (this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER)
                {
                    if (this.objConn_mysql != null)
                    {

                        try
                        {
                            this.objConn_mysql.Close();
                        }
                        catch (Exception) { }

                        this.objConn_mysql = null;
                    }
                }
                else
                {
                    if (this.objConn != null)
                    {

                        try
                        {
                            this.objConn.Close();
                        }
                        catch (Exception) { }

                        this.objConn = null;
                    }
                }
                
            }
        }
        public void ResetDBConnection()
        {
            try
            {
                this.objConn = null;
                this.objConn_mysql = null;
            }
            catch (Exception)
            {
            }
            try
            {
                this.DB_SERVER_TYPE = OwnYITConstant.db_settings.getServerType();
                DatabaseConnection connection = new DatabaseConnection(OwnYITConstant.db_settings);
                if (this.DB_SERVER_TYPE == OwnYITConstant.MYSQL_SERVER)
                {
                    this.objConn_mysql = connection.CreateMySQLConnection();
                    this.objCommand_mysql = new MySqlCommand("", this.objConn_mysql);
                    this.objAdapter_mysql = new MySqlDataAdapter(this.objCommand_mysql);
                }
                else
                {
                    this.objConn = connection.CreateSQLConnection();
                    this.objCommand = new SqlCommand("", this.objConn);
                    this.objAdapter = new SqlDataAdapter(this.objCommand);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
