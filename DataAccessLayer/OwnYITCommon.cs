//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OwnYITCSAT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace OwnYITCSAT.DataAccessLayer
{

    public class OwnYITCommon
    {

        public static void loadDBSettings(String strFileName)
        {
            OwnYITConstant.db_settings = new DBSettings();

            XMLHandler xml_handler = null;
            try
            {
                OwnYITCommon objcom = new OwnYITCommon();

                xml_handler = new XMLHandler(strFileName);
                if (OwnYITConstant.SYSTEMTYPE == "")
                    OwnYITConstant.SYSTEMTYPE = xml_handler.getValue("systemtype");
                objcom.WriteLog("OwnYITCommon", "DB Setting File Name :" + strFileName);
                objcom.WriteLog("OwnYITCommon", "DB Setting File Name With Path :" + OwnYITConstant.LINUX_WWW_PATH + strFileName);
                int DB_SERVER_TYPE = Convert.ToInt16(xml_handler.getValue("Database_Name"));

                int DB_SERVER_PORT = Convert.ToInt16(xml_handler.getValue("DBPORT"));

                string strDBServer = xml_handler.getValue("DBServer");
                String strUser = xml_handler.getValue("UserName");
                String strPassword = xml_handler.getValue("Password");
                String strDBName = xml_handler.getValue("DBName");
                objcom.WriteLog("OwnYITCommon", "DB Setting File values : Database_Name=" + DB_SERVER_TYPE + " : DBPORT=" + DB_SERVER_PORT + " : DBServer = " + strDBServer + " : UserName = " + strUser + " : strPassword=" + strPassword + " : strDBName=" + strDBName);
                OwnYITConstant.db_settings.setServerType(DB_SERVER_TYPE);
                OwnYITConstant.db_settings.setServerPort(DB_SERVER_PORT);

                OwnYITConstant.db_settings.setServer(strDBServer);
                OwnYITConstant.db_settings.setUser(strUser);
                OwnYITConstant.db_settings.setPassword(strPassword);
                OwnYITConstant.db_settings.setName(strDBName);




            }
            catch (Exception)
            {


            }
            finally
            {

                if (xml_handler != null)
                {
                    xml_handler.close();
                    xml_handler = null;
                }
            }
        }

        public static string GetSystemFolder()
        {

            String strPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            return strPath;
        }
        public static string GetSystemPath()
        {
            string _systemPath = "";

            String strPath = GetSystemFolder().ToUpper();

            if (Is64BitSystem())
            {
                _systemPath = strPath.Replace("SYSTEM32", "SysWOW64");

            }
            else
            {
                _systemPath = strPath.Replace("SysWOW64", "SYSTEM32");
            }
            return _systemPath;
        }
        public static bool Is64BitSystem()
        {
            return Environment.Is64BitOperatingSystem;
        }

        private string GetLogFileTimeFormat()
        {
            String strFormat = System.DateTime.Now.ToString("yyyy-MM-dd_HH");
            return strFormat;
        }

        public static string GetConfigPath()
        {

            String strConfigPath = GetSystemFolder() + OwnYITConstant.CONFIGURATION_PATH;

            return strConfigPath;
        }

        public static string GetAppDataFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        }

        private string GetLogFileFolder()
        {

            // String strLogPath = GetAppDataFolder() + OwnYITConstant.LOGFILE_PATH;
            String strLogPath = "";
            if (OwnYITConstant.SYSTEMTYPE.ToUpper() == "LINUX")
                strLogPath = OwnYITConstant.LINUX_ROOT_PATH + OwnYITConstant.LOGFILE_PATH_LINUX;
            else
                strLogPath = GetAppDataFolder() + OwnYITConstant.LOGFILE_PATH;

            // String strLogPath = OwnYITConstant.LINUX_ROOT_PATH + OwnYITConstant.LOGFILE_PATH_LINUX;

            try
            {
                if (!System.IO.Directory.Exists(strLogPath)) System.IO.Directory.CreateDirectory(strLogPath);
            }
            catch (Exception)
            {
            }
            return strLogPath;
        }

        private string GetLogEntryTime()
        {
            return System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private string GetLogFilePath(String strFile)
        {

            //  String strLogFilePath = GetLogFileFolder() + "\\" + strFile + GetLogFileTimeFormat() + ".log";
            String strLogFilePath = GetLogFileFolder() + "/" + strFile + "_" + GetLogFileTimeFormat() + ".log";
            return strLogFilePath;
        }

        public bool WriteLog(string strFileName, string strMessage)
        {
            bool bFlag = false;

            FileStream fs = null;
            StreamWriter writer = null;

            try
            {
                String strLogFilePath = GetLogFilePath(strFileName);

                //  fs = new FileStream(strFileName, FileMode.Append);
                fs = new FileStream(strLogFilePath, FileMode.Create | FileMode.Append, FileAccess.Write);

                writer = new StreamWriter(fs);

                String strLogMsg = GetLogEntryTime() + " : " + strMessage;

                writer.WriteLine(strLogMsg);

                writer.Flush();

                bFlag = true;

            }
            catch (Exception)
            {
            }
            finally
            {
                if (writer != null)
                {
                    try
                    {
                        writer.Close();
                    }
                    catch (Exception) { }
                }
                if (fs != null)
                {
                    try
                    {
                        fs.Close();
                    }
                    catch (Exception) { }
                }
            }
            return bFlag;
        }
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            data.Clear();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public string CreateTable(DataTable table)
        {

            //string htmltable = "<thead><Tr>";
            string htmltable = "";
            try
            {

                if (table.Rows.Count > 0)
                {
                    //am getting my grid's column headers
                    int columnscount = 0;
                    //int columpixel = 0;
                    if (table.Columns.Count != 0)
                    {
                        columnscount = table.Columns.Count;
                        //columpixel = 576 / columnscount;

                        for (int j = 0; j < columnscount; j++)
                        {
                            htmltable += "<th " + GetCssData(table.Columns[j].ToString()).ToString() + ">" + table.Columns[j].ToString() + "</th>";
                        }
                    }
                    htmltable += "</Tr></thead><tbody>";
                    foreach (DataRow row in table.Rows)
                    {
                        htmltable += "<Tr>";
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            //htmltable += "<Td class='date_time' style='width:" + columpixel.ToString().Trim() + "px;word-wrap: break-word;'>";
                            htmltable += "<Td " + GetCssData(table.Columns[i].ToString()).ToString() + ">";
                            htmltable += row[i].ToString();
                            htmltable += "</Td>";
                        }
                        htmltable += "</Tr>";
                    }
                    htmltable += "</tbody>";
                }
                else
                    htmltable = "0";
            }
            catch (Exception ex)
            {
                WriteLog("OwnYITCommon", "CreateTable Exception :" + ex.Message.ToString());
            }
            return htmltable;
        }

        public string GetCssData(string css)
        {
            string strcss = "";
            try
            {
                //Top 10 ALerts Class
                if ("Date-Time" == css)
                    strcss = "class='date_time'";
                else if ("System Name" == css)
                    strcss = "class='system_name'";
                else if ("IP" == css)
                    strcss = "class='ip'";
                else if ("Category" == css)
                    strcss = "class='category'";
                else if ("Status" == css)
                    strcss = "class='status'";
                else if ("Type" == css)  //OS Class
                    strcss = "class='system_type'";
                else if ("Counts" == css)
                    strcss = "class='count'";
                else if ("TimeStamp" == css)
                    strcss = "class='date_time'";


            }
            catch (Exception ex)
            {

                WriteLog("OwnYITCommon", "GetCssData Exception : " + ex.Message.ToString());
            }
            return strcss;
        }
        // Get GUI IP
        public string Get_GUIIP()
        {
            string myHost = System.Net.Dns.GetHostName();
            string myIP = null;

            for (int i = 0; i <= System.Net.Dns.GetHostEntry(myHost).AddressList.Length - 1; i++)
            {
                if (System.Net.Dns.GetHostEntry(myHost).AddressList[i].IsIPv6LinkLocal == false)
                {
                    myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[i].ToString();
                }
            }
            return myIP.ToString();

        }
        public string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }
        public string GetUserName()
        {
            return System.Environment.UserName;
        }

        #region read guisetting
        public string configPath = "\\AssertyIt\\Configuration";
        public string systemcheck()
        {
            string _systemPath = "";
            if (Is64BitSystem())
            {
                _systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System).ToUpper().Replace("SYSTEM32", "SysWOW64");

            }
            else
            {
                _systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System).ToUpper().Replace("SysWOW64", "SYSTEM32");
            }
            return _systemPath;

        }
        //public string GetGUIconfig(String strKey)
        //{
        //    //string path = "";
        //    //path = systemcheck() + configPath;
        //    String retVal = "";
        //    try
        //    {
        //        //DataSet ds = new DataSet();
        //        //ds.ReadXml(path + "\\GUISetting.Xml");
        //        //retVal = (string)(ds.Tables[0].Rows[0][strKey]);
        //        retVal = Objquery.read_guisetting(strKey);
        //        return retVal;
        //    }
        //    catch (Exception) { }
        //    return retVal;
        //}
        #endregion

        public DataTable ConvertToDataTable(DataRow[] result, DataTable dtExist)
        {
            DataTable dtnew = new DataTable();
            dtnew = dtExist.Clone();
            foreach (DataRow dr in result)
            {
                dtnew.ImportRow(dr);
            }
            return dtnew;
        }        
        public LoadData DatatableToObject(DataTable dt)
        {
            LoadData loaddata = new LoadData();
            loaddata.draw = 0;
            string JSONdata = JsonConvert.SerializeObject(dt);
            dynamic dyobj = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<object>>(JSONdata);
            loaddata.data = dyobj;
            loaddata.recordsTotal = dt.Rows.Count;
            loaddata.recordsFiltered = dt.Rows.Count;
            return loaddata;
        }
        public int ExecuteProcess(string FolderPath, string filename)
        {
            int int_return = 0;
            try
            {
                WriteLog("ExecuteProcess", "Folder Path : " + FolderPath + " , File Name : " + filename);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = @"C:\Windows\system32\cmd.exe";
                process.StartInfo.WorkingDirectory = FolderPath;
                process.StartInfo.Arguments = "/c title " + filename + " - Build Mode \"P\" & MM.CMD \"" + filename + "\" P";
                process.StartInfo.Verb= "runas";
                WriteLog("ExecuteProcess", "Working Directory : " + process.StartInfo.WorkingDirectory);
                WriteLog("ExecuteProcess", "Arguments : " + process.StartInfo.Arguments);
                process.Start();
                WriteLog("ExecuteProcess", "After start process");
                process.WaitForExit();
                WriteLog("ExecuteProcess", "Process completed");
            }
            catch (Exception ex)
            {
                WriteLog("ExecuteProcess", "Process execution exception : " + ex.Message.ToString());
            }
            return int_return;
        }

        public int ExecuteProcessLinux()
        {
            int int_return = 0;
            try
            {
                //WriteLog("ExecuteProcess", "Folder Path : " + FolderPath + " , File Name : " + filename);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = @"/usr/local/lib/node_modules/msi-packager";
                process.StartInfo.WorkingDirectory = "msi - packager";
                process.StartInfo.Arguments = "/var/local/InputFiles/ /var/local/OutputFiles/sample_service.msi --name Sample_Service --version 1.0 --64 --upgrade-code 12345 --icon /var/local/image.ico --executable CreateService.exe --run-after true";
                process.StartInfo.Verb = "runas";
                WriteLog("ExecuteProcess", "Working Directory : " + process.StartInfo.WorkingDirectory);
                WriteLog("ExecuteProcess", "Arguments : " + process.StartInfo.Arguments);
                process.Start();
                WriteLog("ExecuteProcess", "After start process");
                process.WaitForExit();
                WriteLog("ExecuteProcess", "Process completed");
            }
            catch (Exception ex)
            {
                WriteLog("ExecuteProcess", "Process execution exception : " + ex.Message.ToString());
            }
            return int_return;
        }
      
    }
}