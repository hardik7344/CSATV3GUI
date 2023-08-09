//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace OwnYITCSAT.DataAccessLayer
{
    public class XMLHandler
    {
        OwnYITCommon objcom = new OwnYITCommon();
        private String strFileName = null;

        private DataSet data_set = null;
        string Path = null;
        public XMLHandler(String strFileName)
        {
           // objcom.WriteLog("XMLHandler", "XMLHandler Start Path :" + strFileName);
            this.strFileName = strFileName;

            if (isFileExists())
            {
                this.data_set = new DataSet();
              
                //this.data_set.ReadXml(HttpContext.Current.Server.MapPath(this.strFileName));
                // Path = System.IO.Directory.GetCurrentDirectory() + this.strFileName;
                Path = OwnYITConstant.LINUX_WWW_PATH + this.strFileName;
                
               // objcom.WriteLog("XMLHandler", "DB Setting File Name With Path :" + Path);
                this.data_set.ReadXml(Path);

            }
        }

        public bool isFileExists()
        {
            bool bFlag = false;

            try
            {
                //bFlag = File.Exists(HttpContext.Current.Server.MapPath(this.strFileName));
                // Path = System.IO.Directory.GetCurrentDirectory() + this.strFileName;
                Path = OwnYITConstant.LINUX_WWW_PATH + this.strFileName;
               // objcom.WriteLog("XMLHandler", "isFileExists DB Setting File Name With Path :" + Path);
                bFlag = File.Exists(Path);
            }
            catch (Exception)
            {
            }
            return bFlag;
        }

        public string getValue(String strTag)
        {

            String strValue = null;
            try
            {
                if (this.data_set == null)
                {
                    return null;
                }

                int records = this.data_set.Tables[0].Rows.Count;

                if (records > 0)
                {
                    strValue = (String)this.data_set.Tables[0].Rows[0][strTag];
                }
            }
            catch (Exception)
            {


            }
            return strValue;
        }

        public int setValue(String strTag, String strValue)
        {
            try
            {
                if (this.data_set == null)
                {
                    return 0;
                }
                this.data_set.Tables[0].Rows[0][strTag] = strValue;

                this.data_set.WriteXml(this.strFileName);

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void close()
        {
            this.data_set.Clear();
        }
    }
}