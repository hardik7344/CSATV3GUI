using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OwnYITCSAT.DataAccessLayer
{
    public class OwnYITConstant
    {

        public static int MSSQL_SERVER = 0;
        public static int MYSQL_SERVER = 1;

        public enum DatabaseTypes
        {
            MSSQL_SERVER = 0, MYSQL_SERVER = 1
        }
        public static string CONFIGURATION_PATH = "\\AssertyIt\\Configuration";

        public static string LOGFILE_PATH = "\\AssertYIT\\OwnYITEE4.7\\OwnYITEEGUI\\Log";

        public static string LOGFILE_PATH_LINUX = "/Log";
        public static string LINUX_ROOT_PATH = "";
        public static string LINUX_WWW_PATH = "";
        public static string PDF_LOGO_PATH_LEFT = "";
        public static string PDF_LOGO_PATH_RIGHT = "";
        // public static string DB_SETTINGS_FILENAME = "\\xml\\DBSettings.xml";

        public static string DB_SETTINGS_FILENAME = "/xml/DBSettings.xml";

        public static string SYSTEMTYPE = "";

        public static DBSettings db_settings = null;
        public static DBSettings db_settings_printer = null;

        public static Configuration configuration = null;

        public static DataTable DT_MAIN_MENU = null;

        public static DataTable DT_ASSETMGMT_MENU = null;

        public static DataTable DT_SETTINGS_MENU = null;

        public static DataTable DT_POLICY_MENU = null;

        public static DataTable DT_REPORT_MENU = null;

        public static DataTable DT_ASSETMGMT_SUB_MENU = null;
        public static DataTable DT_SETTINGS_SUB_MENU = null;
        public static DataTable DT_ASSET_DEVICE_LIST = null;
        public static DataTable DT_ASSET_DEVICE_LIST1 = null;
        public static DataTable DT_ASSET_DEVICE_HWDetailsMenu = null;
        public static DataTable DT_ASSET_DEVICE_HWDetailsMenu1 = null;
        public static DataTable DT_ASSET_DEVICE_HWDetailsMenuData = null;
        public static string DeviceID = "";
        public static string LoginDate = "";

        public static string AUDITTRAIL_CHART_ALERTS = "";
        public static string AUDITTRAIL_CHART_ALERTS_COUNT = "";

        public static string SYSTEM_DETAILS = "";
        public static string SYSTEM_DETAILS_COUNT = "";

        public static string SYSTEM_LIST = "";
        public static string SYSTEM_LIST_COUNT = "";

        public static DataTable DT_Alert = null;

        public static DataTable DT_GetSystemlistDeviceData = null;
        public static DataTable DT_auditraildata = null;
        public static DataTable DT_auditraildata_syswise = null;
        public static DataTable DT_Getuninstalledswdetail = null;
        public static DataTable DT_Setuninstalledswdetail = null;
        public static string TreeactionProperty = "";
        public static string CommanStartDate = null;
        public static string CommanEndDate = null;
        public static string CommanStrtime = null;
    }


}