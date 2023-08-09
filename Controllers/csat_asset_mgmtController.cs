using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OwnYITCSAT.DataAccessLayer;
using OwnYITCSAT.Models;

namespace OwnYITCSAT.Controllers
{
    public class csat_asset_mgmtController : Controller
    {
        private int DBServer_Type = 0;
        private DatabaseHandler database = null;
        OwnYITConstant.DatabaseTypes dbtype;
        DBQueryHandler Objquery = new DBQueryHandler();
        OwnYITCommon Objcom = new OwnYITCommon();
        //string Auditdata = "";
        //string lastpolldata = "";
        //string PCdata = "";
        DataTable Dtsystemdrpdata = new DataTable();
        DataTable Dtsystempcdata = new DataTable();
        DataTable Dtsystemdata = new DataTable();
        DataTable DtAlerts = new DataTable();
        DataTable DtLastUser = new DataTable();
        DataTable DtDeviceStatus = new DataTable();
        DataTable DtOS = new DataTable();
        DataTable DtGroup = new DataTable();
        DataTable dttemp = new DataTable();
        DataTable HwData = new DataTable();
        DataTable DTfirewall = new DataTable();
        DataTable dtTable = new DataTable();
        string strmsg = "";
        DataTable DtShare = new DataTable();
        DataTable DtUser = new DataTable();
        DataTable dt = new DataTable();
        DataTable DtProcessor = new DataTable();
        DataTable DtDomain = new DataTable();
        DataTable DtDisk = new DataTable();
        DataTable DtRAM = new DataTable();
        DataTable dthw = new DataTable();
        DataTable dtsoft = new DataTable();
        int cnt = 0;
        Random generator = new Random();
        string groupid = Configuration.groupid;
        public IActionResult csat_asset_mgmt(int id)
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                // string abc = HttpContext.Session.GetString("user");
                HttpContext.Session.SetString("Flag", "");
            HttpContext.Session.SetString("DeviceID", "");
            try
            {
                if (OwnYITConstant.DT_ASSETMGMT_MENU == null)
                {
                    OwnYITConstant.DT_ASSETMGMT_MENU = Objquery.Get_SubMenu(id);
                }
                if (id.ToString() == null)
                    return RedirectToAction("csat_login", "csat_login");
                if (OwnYITConstant.DT_ASSETMGMT_MENU == null)
                    return RedirectToAction("csat_login", "csat_login");

                #region ViewBag Code
                //// Top 10 Asset ALert 
                //try
                //{
                //    ViewBag.alerts = Objcom.CreateTable(Objquery.Get_TopAlert());

                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Top 10 Asset ALert Table : " + ViewBag.alerts);
                //}
                //catch (Exception ex)
                //{
                //    ViewBag.alerts = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Top 10 Asset ALert Exception : " + ex.Message.ToString());
                //}

                //// OS Summary
                //try
                //{
                //    ViewBag.os = Objcom.CreateTable(Objquery.Get_OS());
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt OS Summary Table : " + ViewBag.os);
                //}
                //catch (Exception ex)
                //{
                //    ViewBag.os = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt OS Summary Exception : " + ex.Message.ToString());
                //}

                //// Processors Datails
                //try
                //{
                //    ViewBag.Processors = Objcom.CreateTable(Objquery.Get_Processor());
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Processors Datails Table : " + ViewBag.Processors);
                //}
                //catch (Exception ex)
                //{
                //    ViewBag.Processors = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Processors Datails Exception : " + ex.Message.ToString());
                //}

                //// User Summary
                //try
                //{
                //    ViewBag.UserSummary = Objcom.CreateTable(Objquery.Get_UserSummary());
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt User Summary Table : " + ViewBag.UserSummary);
                //}
                //catch (Exception ex)
                //{
                //    ViewBag.UserSummary = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt User Summary Exception : " + ex.Message.ToString());
                //}

                //// Domain Summary
                //try
                //{
                //    ViewBag.Domain = Objcom.CreateTable(Objquery.Get_Domain());
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Domain Summary Table : " + ViewBag.Domain);
                //}
                //catch (Exception ex)
                //{
                //    ViewBag.Domain = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Domain Summary Exception : " + ex.Message.ToString());
                //}

                //// Disk Utilization
                //try
                //{

                //    ViewBag.DiskUtilization = Objcom.CreateTable(Objquery.Get_HDDUsage());
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Disk Utilization Table : " + ViewBag.DiskUtilization);
                //}
                //catch (Exception ex)
                //{
                //    ViewBag.DiskUtilization = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Disk Utilization Exception : " + ex.Message.ToString());
                //}

                //// RAM Details
                //try
                //{

                //    ViewBag.RAM = Objcom.CreateTable(Objquery.Get_RAM_Details());
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt RAM Details Table : " + ViewBag.RAM);
                //}
                //catch (Exception ex)
                //{
                //    ViewBag.RAM = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt RAM Details Exception : " + ex.Message.ToString());
                //}


                //// Hardware Details
                //try
                //{

                //    ViewBag.Hardware = Objcom.CreateTable(Objquery.Get_Hardware_Details());
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Hardware Details Table : " + ViewBag.Hardware);
                //}
                //catch (Exception ex)
                //{
                //    ViewBag.Hardware = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Hardware Details Exception : " + ex.Message.ToString());
                //}

                //// Software Details
                //try
                //{
                //    DataTable dts = Objquery.Get_Software_Details();
                //    DTfirewall = Objquery.Get_Software_Details_Network_ON_OFF();

                //    string stroncnt = "";
                //    string stroffcnt = "";
                //    for (int i = 0; i < DTfirewall.Rows.Count; i++)
                //    {
                //        if ("ON" == DTfirewall.Rows[i]["TYPE"].ToString())
                //        {
                //            stroncnt = DTfirewall.Rows[i]["Counts"].ToString();
                //        }

                //        if ("OFF" == DTfirewall.Rows[i]["TYPE"].ToString())
                //        {
                //            stroffcnt = DTfirewall.Rows[i]["Counts"].ToString();
                //        }

                //    }
                //    string stronoff = stroncnt.ToString() + "/" + stroffcnt.ToString();

                //    //  ViewBag.firewallserviceonoff = stroncnt.ToString() + "/" + stroffcnt.ToString();
                //    DataRow dr;
                //    dr = dts.NewRow();
                //    dr["Software Details"] = "Firewall Service ON/OFF";
                //    dr["Counts"] =stronoff.ToString();
                //    dts.Rows.Add(dr);

                //    ViewBag.Software = Objcom.CreateTable(dts);
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Hardware Details Table : " + ViewBag.Software);

                //}
                //catch (Exception ex)
                //{
                //    ViewBag.Software = "0";
                //    Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Hardware Details Exception : " + ex.Message.ToString());
                //}
                #endregion
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt  Exception : " + ex.Message.ToString());
            }
            return View();
        }

        public JsonResult GetAssetMgtTables()
        {
            // Top 10 Asset ALert 
            try
            {
                dtTable = Objquery.Get_TopAlert();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Top 10 Asset ALert Exception : " + ex.Message.ToString());
            }
            //OS
            try
            {
                DtOS = Objquery.Get_OS();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt OS Summary Exception : " + ex.Message.ToString());
            }
            //// Processors Datails
            try
            {
                DtProcessor = Objquery.Get_Processor();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Processors Datails Exception : " + ex.Message.ToString());
            }
            //// User Summary
            try
            {
                DtUser = Objquery.Get_UserSummary();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt User Summary Exception : " + ex.Message.ToString());
            }
            //// Domain Summary
            try
            {
                DtDomain = Objquery.Get_Domain();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Domain Summary Exception : " + ex.Message.ToString());
            }
            //// Disk Utilization
            try
            {

                DtDisk = Objquery.Get_HDDUsage();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Disk Utilization Exception : " + ex.Message.ToString());
            }
            //// RAM Details
            try
            {
                DtRAM = Objquery.Get_RAM_Details();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt RAM Details Exception : " + ex.Message.ToString());
            }
            //// Hardware Details
            try
            {
                dthw = Objquery.Get_Hardware_Details();
                DTfirewall = Objquery.Get_Software_Details_Network_ON_OFF();

                string stroncnt = "";
                string stroffcnt = "";
                for (int i = 0; i < DTfirewall.Rows.Count; i++)
                {
                    if ("ON" == DTfirewall.Rows[i]["TYPE"].ToString())
                    {
                        stroncnt = DTfirewall.Rows[i]["counts"].ToString();
                    }

                    if ("OFF" == DTfirewall.Rows[i]["TYPE"].ToString())
                    {
                        stroffcnt = DTfirewall.Rows[i]["counts"].ToString();
                    }
                }
                string stronoff = stroncnt.ToString() + "/" + stroffcnt.ToString();

                DataRow dr;
                dr = dthw.NewRow();
                dr["hardwareDetails"] = "Firewall Service ON/OFF";
                dr["counts"] = stronoff;
                dthw.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Hardware Details Exception : " + ex.Message.ToString());
            }
            try
            {
                dtsoft = Objquery.Get_Software_Details();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "csat_asset_mgmt Software Details Exception : " + ex.Message.ToString());
            }
            var data = new { topalert = dtTable, osdata = DtOS, processordata = DtProcessor, userdata = DtUser, domaindata = DtDomain, diskdata = DtDisk, ramdata = DtRAM, hardwaredata = dthw, softwaredata = dtsoft };
            return Json(data);
        }
        public JsonResult Get_Asset_summary_details(string SummaryType, string ParameterValue1, string ParameterValue2)
        {
            // Top 10 Asset ALert 
            try
            {
                if (SummaryType == "OS_SUMMARY")
                {
                    if (ParameterValue2 == null)
                        dtTable = Objquery.Get_OS_summary_details(ParameterValue1);
                    else
                        dtTable = Objquery.Get_OS_summary_details(ParameterValue1, ParameterValue2);

                }
                else if (SummaryType == "PROCESSOR_SUMMARY")
                    dtTable = Objquery.Get_Processor_summary_details(ParameterValue1);
                else if (SummaryType == "DISK_SUMMARY")
                {
                    string strCondition = "";
                    ParameterValue1 = ParameterValue1.Replace("<=", "").Replace("GB", "").Replace("TB", "");
                    if (Convert.ToInt32(ParameterValue1.Trim()) == 256)
                        strCondition = " gb <= " + ParameterValue1;
                    else if (Convert.ToInt32(ParameterValue1.Trim()) == 320)
                        strCondition = " gb > 256 and gb <= 320";
                    else if (Convert.ToInt32(ParameterValue1.Trim()) == 500)
                        strCondition = " gb > 320 and gb <= 500";
                    else if (Convert.ToInt32(ParameterValue1.Trim()) == 1)
                        strCondition = " gb > 500 and gb <= 1024";
                    else if (Convert.ToInt32(ParameterValue1.Trim()) == 2)
                        strCondition = " gb > 1024 ";

                    dtTable = Objquery.Get_Disk_summary_details(strCondition);
                }
                else if (SummaryType == "RAM_SUMMARY")
                {
                    string strCondition = "";
                    if (ParameterValue1 == "0-1 GB")
                        strCondition = " GB <=1 ";
                    else if (ParameterValue1 == "1-2 GB")
                        strCondition = " GB > 1 and GB <= 2 ";
                    else if (ParameterValue1 == "2-4 GB")
                        strCondition = " GB > 2 and GB <= 4 ";
                    else if (ParameterValue1 == "4-8 GB")
                        strCondition = " GB > 4 and GB <= 8 ";
                    else if (ParameterValue1 == "8 Above")
                        strCondition = " GB > 8";
                    dtTable = Objquery.Get_RAM_summary_details(strCondition);
                }
                else if (SummaryType == "HARDWARE_SUMMARY")
                {
                    if (ParameterValue1 == "Multiple LAN Card on PC")
                        dtTable = Objquery.Get_multiple_lan_summary_details();
                    else if (ParameterValue1 == "Printer")
                        dtTable = Objquery.Get_printer_summary_details();
                    else if (ParameterValue1 == "WiFi on PC")
                        dtTable = Objquery.Get_wi_fi_on_pc_summary_details();
                    else if (ParameterValue1 == "Total Shared Resources")
                        dtTable = Objquery.Get_shared_summary_details();
                    else if (ParameterValue1 == "Firewall Service ON/OFF")
                        dtTable = Objquery.Get_firewall_on_off_summary_details();
                }
                else if (SummaryType == "SOFTWARE_SUMMARY")
                {
                    if (ParameterValue1 == "Unauthorised Software")
                        dtTable = Objquery.Get_unauthorise_software_summary_details();
                    else if (ParameterValue1 == "Antivirus Not Installed")
                        dtTable = Objquery.Get_antivirus_not_installed_summary_details();
                }
                else if (SummaryType == "USER_SUMMARY")
                {
                    string strCond = "";
                    if (ParameterValue1 == "Administrator")
                        strCond = " and username = '" + ParameterValue1 + "'";
                    else if (ParameterValue1 == "Guest")
                        strCond = " and username = '" + ParameterValue1 + "'";
                    else if (ParameterValue1 == "Local Users")
                        strCond = " and username not in ('Administrator','Guest')";

                    dtTable = Objquery.Get_user_summary_details(strCond);
                }
                else if (SummaryType == "DOMAIN_SUMMARY")
                    dtTable = Objquery.Get_domain_summary_details(ParameterValue1);

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_Asset_summary_details Exception : " + ex.Message.ToString());
            }
            var data = new { asset_summary_details = dtTable };
            return Json(data);
        }
        string asset_deviceid = "";
        public IActionResult asset_system_detail(String device_id)
        {
            asset_deviceid = device_id;
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                try
                {
                    try
                    {
                        //System Data
                        ViewData["devicedata"] = Objquery.Get_Devicedetails(device_id);

                    }
                    catch (Exception ex)
                    {
                        ViewData["devicedata"] = null;
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail System Data Exception : " + ex.Message.ToString());
                    }
                    try
                    {
                        //DIsk Data
                        ViewData["Drivedata"] = Objquery.Get_drivedetails(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewData["Drivedata"] = null;
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail Disk Data Exception : " + ex.Message.ToString());
                    }
                    //Disk Utilization
                    try
                    {
                        ViewData["DiskUtilization"] = Objquery.Get_DiskUtilization(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewData["DiskUtilization"] = null;
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail Disk Utilization Exception : " + ex.Message.ToString());
                    }
                    //Processor Name
                    try
                    {
                        ViewBag.ProcessorName = Objquery.Get_ProcessorName(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ProcessorName = "";
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail ProcessorName Exception : " + ex.Message.ToString());
                    }
                    //HDD Capacity
                    try
                    {
                        ViewBag.HDDCapacity = Objquery.Get_HDDCapacity(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.HDDCapacity = "";
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail HDD Exception : " + ex.Message.ToString());
                    }
                    //RamSize
                    try
                    {
                        ViewBag.RamSize = Objquery.Get_RAMSize(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.RamSize = "";
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail RAM Exception : " + ex.Message.ToString());
                    }
                    //Device Mode
                    try
                    {
                        ViewBag.DeviceMode = Objquery.Get_DeviceMode(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.DeviceMode = "";
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail Device Type Exception : " + ex.Message.ToString());
                    }
                    //software Total
                    try
                    {
                        ViewBag.softwaretotal = Objquery.Get_Software_Total(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.softwaretotal = "";
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail software total Exception : " + ex.Message.ToString());
                    }

                    //LANCard
                    try
                    {
                        ViewBag.LANCard = Objquery.Get_Lancard_Total(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.LANCard = "";
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail LAN Card Exception : " + ex.Message.ToString());
                    }
                    //Firewall Status
                    try
                    {
                        ViewBag.Firewall = Objquery.Get_Firewall_ON_OFF(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Firewall = "";
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail Firewall Exception : " + ex.Message.ToString());
                    }
                    //Screen Saver Password
                    try
                    {
                        ViewBag.ScreenSaver = Objquery.Get_ScreenSaver(device_id);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ScreenSaver = "";
                        Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail ScreenSaver Exception : " + ex.Message.ToString());
                    }
                    GetSoftwareData(device_id);
                    Getsysteminfo(device_id);
                    GetServiceData(device_id);
                    GetProcessData(device_id);
                    GetShareSystemData(device_id);
                    GetUserSystemData(device_id);
                    ViewBag.deviceid = device_id;
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("assetmgmtController", "asset_system_detail Exception : " + ex.Message.ToString());
                }
            return View();
        }
        //public JsonResult asset_system_detail_info(string asset_deviceid)
        //{
        //    int drives = 0;
        //    int softwaretotals = 0;
        //    int RamSizes = 0;
        //    int LANCards = 0;
        //    string processnames = "";
        //    string HDDCapacity = "";
        //    string DeviceModes = "";
        //    string Firewalls = "";
        //    string ScreenSavers = "";
        //    DataTable devicedatas = new DataTable();
        //    DataTable diskutilitys = new DataTable();
        //    devicedatas = Objquery.Get_Devicedetails(asset_deviceid);
        //    drives = Objquery.Get_drivedetails(asset_deviceid);
        //    diskutilitys = Objquery.Get_DiskUtilization(asset_deviceid);
        //    processnames = Objquery.Get_ProcessorName(asset_deviceid);
        //    HDDCapacity = Objquery.Get_HDDCapacity(asset_deviceid);
        //    RamSizes = Objquery.Get_RAMSize(asset_deviceid);
        //    DeviceModes = Objquery.Get_DeviceMode(asset_deviceid);
        //    softwaretotals = Objquery.Get_Software_Total(asset_deviceid);
        //    LANCards = Objquery.Get_Lancard_Total(asset_deviceid);
        //    Firewalls = Objquery.Get_Firewall_ON_OFF(asset_deviceid);
        //    ScreenSavers = Objquery.Get_ScreenSaver(asset_deviceid);
        //    var data = new { devicedata = devicedatas, drive = drives, diskutility = diskutilitys, processname = processnames, HDDCapacit = HDDCapacity, RamSize = RamSizes, DeviceMode = DeviceModes, softwaretotal = softwaretotals, LANCard = LANCards, Firewall = Firewalls, ScreenSaver = ScreenSavers, device_id = asset_deviceid };            
        //    return Json(data);
        //}
        public JsonResult asset_systeminfo_detail_table(string device_id, string option_value)
        {
            try
            {
                // System pc Detail 
                Dtsystempcdata = Objquery.Get_systempcdetails_list(device_id, option_value);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "asset_systeminfo_detail_table System PC Detail Exception : " + ex.Message.ToString());
            }
            try
            {
                // System  os Detail 
                Dtsystemdata = Objquery.Get_systemdetails_list(device_id, option_value);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "asset_systeminfo_detail_table System Detail Exception : " + ex.Message.ToString());
            }
            var data = new { systempcinfodata = Dtsystempcdata, systeminfodata = Dtsystemdata };
            return Json(data);
        }
        public JsonResult asset_system_detail_table(string device_id)
        {
            // Top 10 System ALert 
            try
            {
                DtAlerts = Objquery.Get_TopAlert_Sysetm(device_id);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail Top 10 System Alert Exception : " + ex.Message.ToString());
            }
            try
            {
                // Systemdrp list Detail 
                Dtsystemdrpdata = Objquery.Get_systemdrp_list(device_id);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail System PC Detail Exception : " + ex.Message.ToString());
            }
            // System Utilization
            try
            {
                dttemp = Objquery.Get_System_Utilization(device_id);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "asset_system_detail System Utilization Exception : " + ex.Message.ToString());
            }
            var data = new { systemdata = DtAlerts, systeutilizationdata = dttemp, systemdrpdata = Dtsystemdrpdata };
            return Json(data);
        }
        public JsonResult Getsysteminfo(string device_id)
        {
            DataTable dtsoftinfo = new DataTable();
            try
            {
                dtsoftinfo = Objquery.Get_systeminfo(device_id);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Getsysteminfo Exception : " + ex.Message.ToString());
            }
            var data = new { systeminfo = dtsoftinfo };
            return Json(data);
        }
        public JsonResult GetSoftwareData(string deviceid)
        {
            string value = string.Empty;
            try
            {

                DataTable dt = Objquery.GetSoftwareData(deviceid);
                List<Software> AllSoftware = Objcom.ConvertDataTable<Software>(dt);

                value = JsonConvert.SerializeObject(AllSoftware, Formatting.Indented, new JsonSerializerSettings
                {

                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "GetSoftwareData Exception : " + ex.Message.ToString());
            }
            ViewBag.Softwaredata = value;
            return Json(ViewBag.Softwaredata);
        }
        public JsonResult SoftwareAction(string DeviceID, string software, string ActionType, string guid)
        {

            int querystatus = 0;
            String software_type = "";
            bool result = false;

            try
            {
                switch (ActionType.ToUpper())
                {
                    case "AUTHORISE":
                        software_type = Objquery.Check_Authorized_Type(1, software);
                        if (software_type == null)
                            querystatus = Objquery.Insert_Un_Authorized(1, software, guid, 1);
                        else if (software_type == "0")
                            querystatus = Objquery.Update_Un_Authorized(1, 1, software);
                        break;
                    case "UNAUTHORISE":
                        software_type = Objquery.Check_Authorized_Type(1, software);
                        if (software_type == null)
                            querystatus = Objquery.Insert_Un_Authorized(1, software, guid, 0);
                        else if (software_type == "1")
                            querystatus = Objquery.Update_Un_Authorized(0, 1, software);
                        break;
                    case "UNINSTALL":
                        int strDeployID = 0;
                        strDeployID = Objquery.getdeploymentId();
                        if (strDeployID == 0)
                            strDeployID = 1;
                        else
                        {
                            try
                            {
                                strDeployID = Convert.ToInt32(strDeployID) + 1;
                            }
                            catch (Exception)
                            {

                                strDeployID = 1;
                            }
                        }

                        querystatus = Objquery.insertdeploymentId(DeviceID, software, strDeployID);
                        strmsg = "#222@" + DeviceID + "!" + strDeployID + "!" + software.Trim() + "!@222#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        querystatus = Objquery.Set_GUILog(DeviceID, "Uninstall software", "Uninstall software : " + software, "Admin");
                        break;
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "SoftwareAction Exception : " + ex.Message.ToString());
                result = false;
            }

            if (querystatus == 0)
                result = false;
            else
                result = true;

            return Json(result);
        }
        public JsonResult GetServiceData(string deviceid)
        {
            string value = string.Empty;
            try
            {

                DataTable dt = Objquery.GetServiceData(deviceid);
                List<Services> AllSoftware = Objcom.ConvertDataTable<Services>(dt);

                value = JsonConvert.SerializeObject(AllSoftware, Formatting.Indented, new JsonSerializerSettings
                {

                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "GetServiceData Exception : " + ex.Message.ToString());
            }
            ViewBag.Servicedata = value;
            return Json(ViewBag.Servicedata);
        }

        public JsonResult ServicesAction(string DeviceID, string services, string ActionType)
        {
            int querystatus = 0;

            bool result = false;
            try
            {
                switch (ActionType.ToUpper())
                {
                    case "AUTOMATIC":
                        strmsg = "#77@" + services + "!2!-1!@77#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        querystatus = Objquery.Set_GUILog(DeviceID, "Change Service Type", "Change Service Type : " + services, "Admin");
                        strmsg = "#17@2!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "MANUAL":
                        strmsg = "#77@" + services + "!1!-1!@77#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        querystatus = Objquery.Set_GUILog(DeviceID, "Change Service Type", "Change Service Type : " + services, "Admin");
                        strmsg = "#17@2!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "DISABLE":
                        strmsg = "#77@" + services + "!0!-1!@77#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        querystatus = Objquery.Set_GUILog(DeviceID, "Change Service Type", "Change Service Type : " + services, "Admin");
                        strmsg = "#17@2!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "START":
                        //strmsg = "#19@" + Objquery.Get_SystemIP(DeviceID) + "!" + services + "!" + DeviceID + "!START@19#";
                        strmsg = "#77@" + services + "!-1!3!@77#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        querystatus = Objquery.Set_GUILog(DeviceID, "Service Start", "Service Start : " + services, "Admin");
                        strmsg = "#17@2!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "STOP":
                        //strmsg = "#19@" + Objquery.Get_SystemIP(DeviceID) + "!" + services + "!" + DeviceID + "!STOP@19#";
                        strmsg = "#77@" + services + "!-1!1!@77#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        querystatus = Objquery.Set_GUILog(DeviceID, "Service Stop", "Service Stop : " + services, "Admin");
                        strmsg = "#17@2!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "DELETE":
                        //strmsg = "#19@" + Objquery.Get_SystemIP(DeviceID) + "!" + services + "!" + DeviceID + "!DELETE@19#";
                        strmsg = "#77@" + services + "!5!5!@77#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        querystatus = Objquery.Set_GUILog(DeviceID, "Service Delete", "Service Delete : " + services, "Admin");
                        strmsg = "#17@2!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "ServicesAction Exception : " + ex.Message.ToString());
                result = false;
            }

            if (querystatus == 0)
                result = false;
            else
                result = true;

            return Json(result);
        }
        public JsonResult GetProcessData(string deviceid)
        {
            string value = string.Empty;
            try
            {

                DataTable dt = Objquery.GetProcessData(deviceid);
                List<Process> AllSoftware = Objcom.ConvertDataTable<Process>(dt);

                value = JsonConvert.SerializeObject(AllSoftware, Formatting.Indented, new JsonSerializerSettings
                {

                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "GetProcessData Exception : " + ex.Message.ToString());
            }
            ViewBag.Processdata = value;
            return Json(ViewBag.Processdata);
        }
        public JsonResult ProcessAction(string DeviceID, string process, string ActionType)
        {

            int querystatus = 0;
            string type = "";
            bool result = false;
            try
            {
                switch (ActionType.ToUpper())
                {
                    case "KILLPROCESS":
                        strmsg = "#18@" + Objquery.Get_SystemIP(DeviceID) + "!" + process + "!" + DeviceID + "@18#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        strmsg = "#10@1034!" + process + "!0!0!@10#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        //querystatus = Objquery.Set_GUILog(DeviceID, "Kill Process", "Kill Process : " + Objquery.Get_ProcessName(DeviceID, process), "Admin");
                        querystatus = Objquery.Set_GUILog(DeviceID, "Kill Process", "Kill Process : " + Objquery.Get_ProcessName(process), "Admin");
                        break;
                    case "WHITELIST":
                        //type = Objquery.Check_Authorized_Type(2, Objquery.Get_ProcessName(DeviceID, process));
                        type = Objquery.Check_Authorized_Type(2, Objquery.Get_ProcessName(process));
                        if (type == null)
                            //querystatus = Objquery.Insert_Un_Authorized(2, Objquery.Get_ProcessName(DeviceID, process), process, 1);
                            querystatus = Objquery.Insert_Un_Authorized(2, Objquery.Get_ProcessName(process), process, 1);
                        else if (type == "0")
                            //querystatus = Objquery.Update_Un_Authorized(1, 2, Objquery.Get_ProcessName(DeviceID, process));
                            querystatus = Objquery.Update_Un_Authorized(1, 2, Objquery.Get_ProcessName(process));
                        break;
                    case "BLACKLIST":
                        //type = Objquery.Check_Authorized_Type(2, Objquery.Get_ProcessName(DeviceID, process));
                        type = Objquery.Check_Authorized_Type(2, Objquery.Get_ProcessName(process));
                        if (type == null)
                            //querystatus = Objquery.Insert_Un_Authorized(2, Objquery.Get_ProcessName(DeviceID, process), process, 0);
                            querystatus = Objquery.Insert_Un_Authorized(2, Objquery.Get_ProcessName(process), process, 0);
                        else if (type == "1")
                            //querystatus = Objquery.Update_Un_Authorized(0, 2, Objquery.Get_ProcessName(DeviceID, process));
                            querystatus = Objquery.Update_Un_Authorized(0, 2, Objquery.Get_ProcessName(process));
                        break;
                    case "REALTIME":
                        strmsg = "#10@1037!" + process + "!1!0!@10#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        //strmsg = " priority converted of " + Objquery.Get_ProcessName(DeviceID, process) + " change to Real time";
                        strmsg = " priority converted of " + Objquery.Get_ProcessName(process) + " change to Real time";
                        querystatus = Objquery.Set_GUILog(DeviceID, "Realtime Process", strmsg, "Admin");
                        strmsg = "#17@1!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "HIGH":
                        strmsg = "#10@1037!" + process + "!2!0!@10#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        //strmsg = " priority converted of " + Objquery.Get_ProcessName(DeviceID, process) + " change to High";
                        strmsg = " priority converted of " + Objquery.Get_ProcessName(process) + " change to High";
                        querystatus = Objquery.Set_GUILog(DeviceID, "Change Process priority", strmsg, "Admin");
                        strmsg = "#17@1!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "ABOVENORMAL":
                        strmsg = "#10@1037!" + process + "!5!0!@10#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        //strmsg = " priority converted of " + Objquery.Get_ProcessName(DeviceID, process) + " change to Above Normal";
                        strmsg = " priority converted of " + Objquery.Get_ProcessName(process) + " change to Above Normal";
                        querystatus = Objquery.Set_GUILog(DeviceID, "Change Process priority", strmsg, "Admin");
                        strmsg = "#17@1!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "NORMAL":
                        strmsg = "#10@1037!" + process + "!3!0!@10#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        //strmsg = " priority converted of " + Objquery.Get_ProcessName(DeviceID, process) + " change to Normal";
                        strmsg = " priority converted of " + Objquery.Get_ProcessName(process) + " change to Normal";
                        querystatus = Objquery.Set_GUILog(DeviceID, "Change Process priority", strmsg, "Admin");
                        strmsg = "#17@1!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "BELOWNORMAL":
                        strmsg = "#10@1037!" + process + "!6!0!@10#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        //strmsg = " priority converted of " + Objquery.Get_ProcessName(DeviceID, process) + " change to Below Normal";
                        strmsg = " priority converted of " + Objquery.Get_ProcessName(process) + " change to Below Normal";
                        querystatus = Objquery.Set_GUILog(DeviceID, "Change Process priority", strmsg, "Admin");
                        strmsg = "#17@1!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                    case "IDLE":
                        strmsg = "#10@1037!" + process + "!4!0!@10#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        //strmsg = " priority converted of " + Objquery.Get_ProcessName(DeviceID, process) + " change to Idle";
                        strmsg = " priority converted of " + Objquery.Get_ProcessName(process) + " change to Idle";
                        querystatus = Objquery.Set_GUILog(DeviceID, "Change Process priority", strmsg, "Admin");
                        strmsg = "#17@1!@17#";
                        querystatus = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                        break;
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "ProcessAction Exception : " + ex.Message.ToString());
                result = false;
            }

            if (querystatus == 0)
                result = false;
            else
                result = true;

            return Json(result);
        }

        // Get Share System Data
        public JsonResult GetShareSystemData(string deviceid)
        {
            List<Share> ShareSystem = null;
            string value = string.Empty;
            try
            {
                DtShare = Objquery.Get_SystemShareData(deviceid);
                ShareSystem = Objcom.ConvertDataTable<Share>(DtShare);
                value = JsonConvert.SerializeObject(ShareSystem, Formatting.Indented, new JsonSerializerSettings
                {

                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("assetmgmtController", "GetShareSystemData Exception : " + ex.Message.ToString());
            }
            ViewBag.ShareSystemData = value;
            return Json(ViewBag.ShareSystemData);
        }

        // Remove Share Policy
        public void RemoveShare(string DeviceID, string ShareName)
        {
            try
            {

                strmsg = "#73@" + ShareName + "!0!@73#";
                try
                {
                    int insertQuery = Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("assetmgmtController", "RemoveShare Querylog Exception : " + ex.Message.ToString());
                }
                try
                {
                    int insertGUI = Objquery.Set_GUILog(DeviceID, "Remove Share", "Remove share : " + ShareName, "Admin");
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("assetmgmtController", "RemoveShare GUIlog Exception : " + ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("assetmgmtController", "RemoveShare Exception : " + ex.Message.ToString());
            }

        }
        // Get User System Data
        public JsonResult GetUserSystemData(string deviceid)
        {
            List<User> UserSystem = null;
            string value = string.Empty;
            try
            {
                DtUser = Objquery.Get_SystemUserData(deviceid);
                UserSystem = Objcom.ConvertDataTable<User>(DtUser);
                value = JsonConvert.SerializeObject(UserSystem, Formatting.Indented, new JsonSerializerSettings
                {

                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("assetmgmtController", "GetUserSystemData Exception : " + ex.Message.ToString());
            }
            ViewBag.UserSystemData = value;
            return Json(ViewBag.UserSystemData);
        }



        // Set user Action(Enable,Disable,Delete)
        public void SetUserAction(string DeviceID, string UserName, string UserAction)
        {
            int InsertQuery = 0;
            int UpdateQuery = 0;
            int UserActionType = 0;
            string strmsg = "";
            if (UserAction == "Enable")
            {
                UserActionType = 1;
                strmsg = "#78@" + UserName + "!1!@78#";

            }
            else if (UserAction == "Disable")
            {
                UserActionType = 0;
                strmsg = "#78@" + UserName + "!2!@78#";

            }
            else if (UserAction == "Delete")
            {
                UserActionType = 2;
                strmsg = "#78@" + UserName + "!3!@78#";
            }
            try
            {
                Objquery.InsertQueryLog(DeviceID, strmsg, Objquery.Get_Location_ID(DeviceID));
                if (Objquery.Check_UserAction(DeviceID, UserName) > 0)
                    UpdateQuery = Objquery.Update_UserAction(DeviceID, UserName, UserActionType);
                else
                    InsertQuery = Objquery.Set_UserAction(DeviceID, UserName, UserActionType);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("assetmgmtController", "SetUserAction Exception : " + ex.Message.ToString());
            }
        }



        public DataTable getalerts()
        {
            OwnYITConstant.DT_Alert = Objquery.Get_TopAlert();
            return OwnYITConstant.DT_Alert;
        }

        public IActionResult asset_system_list()
        {
            //try
            //{
            //    Objcom.WriteLog("csat_asset_mgmtController", "asset_system_list Start");
            //    ViewData["OUList"] = Objquery.Get_OUList("", "1");
            //    if (HttpContext.Session.GetString("Flag") == "")
            //        OwnYITConstant.DT_ASSET_DEVICE_LIST = null;
            //    Objcom.WriteLog("csat_asset_mgmtController", "asset_system_list End");
            //}
            //catch (Exception e)
            //{
            //    Objcom.WriteLog("csat_asset_mgmtController", "asset_system_list Exception : " + e.Message.ToString());
            //}
            return View();
        }

        public JsonResult Get_System_Action(string deviceid, string strvalue, string ActionType)
        {
            int querystatus = 0;
            bool result = false;

            try
            {
                switch (ActionType.ToUpper())
                {
                    case "SENDMESSAGE":
                        strmsg = "#19@" + Objcom.GetHostName() + "!" + Objcom.GetUserName() + "!" + strvalue + "!@19#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid.Substring(1), "Send Message", strvalue, "Admin");
                        break;
                    case "EXECUTECOMMAND":
                        strmsg = "#10@1039!0!0!" + strvalue + "!@10#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid.Substring(1), "Execute Command", strvalue, "Admin");
                        break;
                    case "SHUTDOWN":
                        strmsg = "#10@1038!3!" + strvalue + "!" + ActionType + "!@10#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid.Substring(1), "Shut Down", "Shut Down", "Admin");
                        break;
                    case "RESTART":
                        strmsg = "#10@1038!2!" + strvalue + "!" + ActionType + "!@10#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid.Substring(1), "Restart", ActionType, "Admin");
                        break;
                    case "LOGOFF":
                        strmsg = "#10@1038!1!" + strvalue + "!" + ActionType + "!@10#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid.Substring(1), "Log Off", ActionType, "Admin");
                        break;
                    case "FATCHALL":
                        string jj = strvalue.Substring(1);
                        string[] values = jj.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i].Trim() == "Hardware")
                            {
                                //querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), "#25@4!@25#", "2");
                                querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), "#6065@2!@6065#", "2");

                            }
                            else if (values[i].Trim() == "Software")
                            {
                                //querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), "#25@5!@25#", "2");
                                querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), "#6065@1!@6065#", "2");

                            }
                            else if (values[i].Trim() == "Process")
                            {
                                querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), "#6065@3!@6065#", "2");

                            }
                            else if (values[i].Trim() == "Service")
                            {
                                querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), "#6065@4!@6065#", "2");
                            }
                        }
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid.Substring(1), strvalue.Substring(1) + " data refresh", ActionType, "Admin");
                        break;
                    case "CLEANTEMPFILE":
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), "#2018@Temp!@2018#", "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid.Substring(1), "Clean Temp File", ActionType, "Admin");
                        break;
                }
                if (strvalue.Contains("0"))
                    querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid.Substring(1), "#6065@0!@6065#", "2");
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_System_Action Exception : " + ex.Message.ToString());
                result = false;
            }
            if (querystatus == 0)
                result = false;
            else
                result = true;
            return Json(result);
        }
        public JsonResult Get_System_Action_Systemdetails(string deviceid, string strvalue, string ActionType)
        {
            int querystatus = 0;
            bool result = false;

            try
            {
                switch (ActionType.ToUpper())
                {
                    case "SENDMESSAGE":
                        strmsg = "#19@" + Objcom.GetHostName() + "!" + Objcom.GetUserName() + "!" + strvalue + "!@19#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid, "Send Message", strvalue, "Admin");
                        break;
                    case "EXECUTECOMMAND":
                        strmsg = "#10@1039!0!0!" + strvalue + "!@10#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid, "Execute Command", strvalue, "Admin");
                        break;
                    case "SHUTDOWN":
                        strmsg = "#10@1038!3!" + strvalue + "!" + ActionType + "!@10#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid, "Shut Down", "Shut Down", "Admin");
                        break;
                    case "RESTART":
                        strmsg = "#10@1038!2!" + strvalue + "!" + ActionType + "!@10#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid, "Restart", ActionType, "Admin");
                        break;
                    case "LOGOFF":
                        strmsg = "#10@1038!1!" + strvalue + "!" + ActionType + "!@10#";
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, strmsg, "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid, "Log Off", ActionType, "Admin");
                        break;
                    case "FATCHALL":
                        string jj = strvalue.Substring(1);
                        string[] values = jj.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i].Trim() == "Hardware")
                            {
                                //querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#25@4!@25#", "2");
                                querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#6065@2!@6065#", "2");
                            }
                            else if (values[i].Trim() == "Software")
                            {
                                //querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#25@5!@25#", "2");
                                querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#6065@1!@6065#", "2");
                            }
                            else if (values[i].Trim() == "Process")
                            {
                                //querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#17@1!@17#", "2");
                                querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#6065@3!@6065#", "2");
                            }
                            else if (values[i].Trim() == "Service")
                            {
                                //querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#17@2!@17#", "2");
                                querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#6065@4!@6065#", "2");
                            }
                        }
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid, strvalue.Substring(1) + " data refresh", ActionType, "Admin");
                        break;
                    case "CLEANTEMPFILE":
                        querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#2018@Temp!@2018#", "2");
                        querystatus = Objquery.Set_GUILoginMultipleDevice(deviceid, "Clean Temp File", ActionType, "Admin");
                        break;
                }
                if (strvalue.Contains("0"))
                    querystatus = Objquery.InsertQueryLogMultipleDevice(deviceid, "#6065@0!@6065#", "2");
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_System_Action_Systemdetails Exception : " + ex.Message.ToString());
                result = false;
            }
            if (querystatus == 0)
                result = false;
            else
                result = true;
            return Json(result);
        }
        public JsonResult GetOU()
        {
            var data = new { Ou = Objquery.Get_OUList("", "1"), hardfware = Objquery.Get_offline_hardwarename(), vendor = Objquery.Get_offline_vendor(), amcvendor = Objquery.Get_offline_AMC_vendor(), username = Objquery.Get_offline_User_Name() };
            return Json(data);
        }
        // Add Asset info 
        //public void store_assetinfo(string deviceid, string motherboard, string manufacturer, string processor, string model, string hddcapacity, string ramtype, string ramsize, string keyboard, string mouse, string monitor, string os, string osdate)
        public void store_assetinfo(string deviceid, string motherboard, string manufacturer, string processor, string model, string hddtype, string hddlist, string hddcapacity, string ramtype, string ramsize, string ramslot, string keyboard, string mouse, string monitor, string floppy, string cdrom, string nic, string os, string osdate)
        {
            try
            {
                cnt = Objquery.count_assetinfo(deviceid);
                if (cnt == 0)
                {
                    Objquery.Insert_AssetInfo(deviceid, motherboard, manufacturer, processor, model, hddtype, hddlist, hddcapacity, ramtype, ramsize, ramslot, keyboard, mouse, monitor, floppy, cdrom, nic, os, osdate);
                    //Objquery.Insert_AssetInfo(deviceid, motherboard, manufacturer, processor, model, hddcapacity, ramtype.Substring(1), ramsize, keyboard, mouse, monitor, os, osdate);
                }
                else
                {
                    Objquery.Update_AssetInfo(deviceid, motherboard, manufacturer, processor, model, hddtype, hddlist, hddcapacity, ramtype, ramsize, ramslot, keyboard, mouse, monitor, floppy, cdrom, nic, os, osdate);
                    //Objquery.Update_AssetInfo(deviceid, motherboard, manufacturer, processor, model, "", "", hddcapacity, ramtype, ramsize, "0", keyboard, mouse, monitor, "", "", "", os, osdate);
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_assetinfo Exception : " + ex.Message.ToString());
            }
        }

        public JsonResult store_Hardwaredata(string hrd_name, string manufacture, string model, string m_no, string p_date, string vName, string po_no, string i_no, string w_from, string w_end, string Avendor, string A_to, string A_end, string amount, string u_name, string asset_id, string remark, string allocate_user, string ouname, string ou_id, string quantity, string attachment)
        {
            int querystatus = 0;
            string result = "";
            string filename = "";
            string filemovepath = "";
            string Amcstartdate = "";
            string Amcenddate = "";
            string Wstartdate = "";
            string Wenddate = "";
            string purchasedate = "";
            if (ou_id == "-1")
            {
                ou_id = "";
            }
            if (A_to != null && A_end != null)
            {
                Amcstartdate = System.DateTime.ParseExact(A_to.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Amcenddate = System.DateTime.ParseExact(A_end.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";

            }
            if (w_from != null && w_end != null)
            {
                Wstartdate = System.DateTime.ParseExact(w_from.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Wenddate = System.DateTime.ParseExact(w_end.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            if (p_date != null)
            {
                purchasedate = System.DateTime.ParseExact(p_date.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
            }
            try
            {
                if (attachment != null)
                {
                    attachment = attachment.Replace(@"\", "\\");
                    int filepathlength = attachment.LastIndexOf("\\");
                    filename = attachment.Substring(filepathlength + 1);
                }

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_Hardwaredata File Index Exception : " + ex.Message.ToString());
            }
            try
            {
                filemovepath = OwnYITConstant.LINUX_WWW_PATH + "//Attachments//Hardware//";
                if (!System.IO.Directory.Exists(filemovepath)) System.IO.Directory.CreateDirectory(filemovepath);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_Hardwaredata Move Folder Create Exception : " + ex.Message.ToString());
            }

            try
            {

                if (attachment != null && filename != null)
                {
                    System.IO.File.Copy(attachment, filemovepath + filename, true);
                }
                Objcom.WriteLog("csat_asset_mgmtController", "store_Hardwaredata File Destinaction path : " + filemovepath + filename);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_Hardwaredata File copy Exception : " + ex.Message.ToString());

            }



            try
            {
                if (hrd_name != null)
                {
                    querystatus = Objquery.Insert_offline_hardwaremaster(hrd_name, manufacture, model, m_no, purchasedate, vName, po_no, i_no, Wstartdate, Wenddate, Avendor, Amcstartdate, Amcenddate, amount, u_name, asset_id, remark, allocate_user, ouname, ou_id, quantity, attachment, filename);
                    if (querystatus == 0)
                        result = "hardware information inserted failed.";
                    else
                        result = "hardware information inserted successfully.";
                }
                //else
                //{
                //    result = "Please enter hardware details.";
                //}
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_Hardwaredata Exception : " + ex.Message.ToString());

            }

            return Json(result);
        }
        public JsonResult get_offline_hardware_data()
        {
            var data = new { hrddata = Objquery.Get_offline_hardware_details() };
            return Json(data);
        }
        public JsonResult delete_hrddata(string hrdid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.delete_offline_hardwaremaster(hrdid);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "delete_hrddata Exception : " + ex.Message.ToString());

            }
            if (querystatus == 0)
                result = "hardware information deleted failed.";
            else
                result = "hardware information deleted successfully.";
            return Json(result);
        }
        public JsonResult get_show_offline_hardware_data(string hrdid)
        {
            var data = new { hrddatahrdwise = Objquery.Get_offline_hardware_details_hrdwise(hrdid), Ou = Objquery.Get_OUList("", "1"), hardfware = Objquery.Get_offline_hardwarename(), vendor = Objquery.Get_offline_vendor(), amcvendor = Objquery.Get_offline_AMC_vendor(), username = Objquery.Get_offline_User_Name() };
            return Json(data);
        }
        public JsonResult Edit_offlineHardwaredata(string hrd_id, string hrd_name, string manufacture, string model, string m_no, string p_date, string vName, string po_no, string i_no, string w_from, string w_end, string Avendor, string A_to, string A_end, string amount, string u_name, string asset_id, string remark, string allocate_user, string ouname, string ou_id, string quantity, string attachment)
        {
            int querystatus = 0;
            string result = "";
            string filename = "";
            string filemovepath = "";
            string Amcstartdate = "";
            string Amcenddate = "";
            string Wstartdate = "";
            string Wenddate = "";
            string purchasedate = "";

            Amcstartdate = System.DateTime.ParseExact(A_to.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
            Amcenddate = System.DateTime.ParseExact(A_end.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
            Wstartdate = System.DateTime.ParseExact(w_from.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
            Wenddate = System.DateTime.ParseExact(w_end.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
            purchasedate = System.DateTime.ParseExact(p_date.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
            try
            {
                //int filepathlength = attachment.LastIndexOf("\\");
                //filename = attachment.Substring(filepathlength + 1);
                attachment = attachment.Replace(@"\", "\\");
                //.Replace(@"\",@"\\")
                int filepathlength = attachment.LastIndexOf("\\");
                filename = attachment.Substring(filepathlength + 1);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Edit_offlineHardwaredata File Index Exception : " + ex.Message.ToString());
            }
            try
            {
                filemovepath = OwnYITConstant.LINUX_WWW_PATH + "//Attachments//Hardware//";
                if (!System.IO.Directory.Exists(filemovepath)) System.IO.Directory.CreateDirectory(filemovepath);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Edit_offlineHardwaredata Move Folder Create Exception : " + ex.Message.ToString());
            }

            try
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Edit_offlineHardwaredata File Destinaction path : " + filemovepath + filename);
                System.IO.File.Copy(attachment, filemovepath + filename, true);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Edit_offlineHardwaredata File copy Exception : " + ex.Message.ToString());
            }
            try
            {
                querystatus = Objquery.Update_offline_hardwaremaster(hrd_id, hrd_name, manufacture, model, m_no, purchasedate, vName, po_no, i_no, Wstartdate, Wenddate, Avendor, Amcstartdate, Amcenddate, amount, u_name, asset_id, remark, allocate_user, ouname, ou_id, quantity, attachment, filename);
                if (querystatus == 0)
                    result = "hardware information updated failed.";
                else
                    result = "hardware information updated successfully.";
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Edit_offlineHardwaredata Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public IActionResult asset_vendor_master()
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                return View();
        }


        public JsonResult get_Vendor_data()
        {
            var data = new { vendordata = Objquery.Get_Vendor_details() };
            return Json(data);
        }

        // Get AMC /Insurance Dates


        public JsonResult get_Vendor_master_details(string vendorid)
        {
            var data = new { vendordata = Objquery.Get_Vendor_data(vendorid) };
            return Json(data);
        }
        public JsonResult insert_Vendordata(string vname, string supplier, string address, string city, string c_person, string p_no1, string p_no2, string m_no1, string m_no2, string fax_no, string emailid, string gst, string pin_no)
        {
            int querystatus = 0;
            string result = "";
            try
            {


                if (vname != null)
                {
                    cnt = Objquery.count__Vendormaster(vname);
                    if (cnt == 0)
                    {
                        querystatus = Objquery.Insert__Vendormaster(vname, supplier, address, city, c_person, p_no1, p_no2, m_no1, m_no2, fax_no, emailid, gst, pin_no);

                        if (querystatus == 0)
                            result = "Vendor information inserted failed.";
                        else
                            result = "Vendor information inserted successfully.";
                    }
                    else
                    {
                        result = "Vendor information already exists.";
                    }
                }
                else
                {
                    result = "Please enter Vendor";
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "insert_Vendordata Exception : " + ex.Message.ToString());

            }

            return Json(result);
        }
        public JsonResult Update_Vendordata(string vname, string supplier, string address, string city, string c_person, string p_no1, string p_no2, string m_no1, string m_no2, string fax_no, string emailid, string gst, string pin_no, string vendorid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.Update_Vendormaster(vendorid, vname, supplier, address, city, c_person, p_no1, p_no2, m_no1, m_no2, fax_no, emailid, gst, pin_no);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Update_Vendordata Exception : " + ex.Message.ToString());

            }
            if (querystatus == 0)
                result = "Vendor information updated failed.";
            else
                result = "Vendor information updated successfully.";
            return Json(result);
        }
        public JsonResult delete_Vendordata(string vendorid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.delete__Vendormaster(vendorid);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "delete_Vendordata Exception : " + ex.Message.ToString());

            }
            if (querystatus == 0)
                result = "Vendor information deleted failed.";
            else
                result = "Vendor information deleted successfully.";
            return Json(result);
        }
        public IActionResult asset_assetmaster()
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                return View();
        }
        public IActionResult asset_document_info()
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                return View();
        }
        public IActionResult asset_hardware_master()
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                return View();
        }
        public IActionResult asset_maintanance()
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                return View();
        }
        public IActionResult asset_offline()
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                return View();
        }
        public IActionResult asset_software_master()
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                return View();
        }

        public IActionResult asset_inventory_history()
        {
            //session time out start
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == null && ViewBag.password == null)
            {
                return RedirectToAction("session_time_out", "csat_login");
            }
            else if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            else
                //session time out end
                return View();
        }
        public JsonResult Get_Systemutilizationchart(string deviceid)
        {
            string syatemdata = "";
            string systemdataalert = "";
            try
            {
                dt = Objquery.Get_System_utilization_Chart(deviceid);
                List<Diskutillizationchart> media = Objcom.ConvertDataTable<Diskutillizationchart>(dt);
                syatemdata = JsonConvert.SerializeObject(media);

                DtAlerts = Objquery.Get_System_utilization_Chart_alert(deviceid);
                List<Diskutillizationchartalert> alert = Objcom.ConvertDataTable<Diskutillizationchartalert>(DtAlerts);
                systemdataalert = JsonConvert.SerializeObject(alert);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_Systemutilizationchart Exception : " + ex.Message.ToString());
            }

            var data = new { Systemutilizationdata = syatemdata, Systemutilizationdata_alert = systemdataalert };
            return Json(data);
        }

        public JsonResult Get_offlinesysteminfo(string deviceid)
        {
            try
            {
                int cnt = Objquery.check_offlinesysteminfo(deviceid);
                if (cnt <= 0)
                {
                    Objquery.insert_offlinesysteminfo(deviceid);
                }
                else
                {
                    Objquery.update_offlinesysteminfo(deviceid);
                    deviceid = "and dm.device_id =" + deviceid + "";
                    dt = Objquery.Get_offlinesysteminfo(deviceid);
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_offlinesysteminfo Exception : " + ex.Message.ToString());

            }

            var data = new { offlinesystem = dt };
            return Json(data);
        }
        public JsonResult update_offlinesysteminfo(string deviceid, string virtualname, string departmentname, string emailid)
        {
            int querystatus = 0;
            string result = "";
            try
            {

                querystatus = Objquery.update_offlinesysteminfo(deviceid, virtualname, departmentname, emailid);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "update_offlinesysteminfo Exception : " + ex.Message.ToString());

            }

            if (querystatus == 0)
                result = "System information updated failed.";
            else
                result = "System information updated successfully.";
            return Json(result);
        }

        public JsonResult Get_hardwareinfo(string deviceid)
        {
            //string strprocessor = "";
            //string stros = "";
            //string strosdate = "";
            //string strmouse = "";
            //string strlancard = "";
            //string strFloppy = "";
            DataTable dtdate = new DataTable();
            try
            {

                dt = Objquery.Get_assetinfo(deviceid);
                //strprocessor = Objquery.Get_ProcessorName(deviceid);
                // stros = Objquery.Get_OSName(deviceid);
                // strosdate = Objquery.Get_OSInstallationdate(deviceid);
                //HwData = Objquery.Get_hdd_count_detail(deviceid);
                //strlancard = Objquery.Get_Lancard_Data(deviceid);
                //DTfirewall = Objquery.Get_ram_count_detail(deviceid);
                //strkeyboard = Objquery.Get_Keyboard_Detail(deviceid);
                //strmonitor = Objquery.Get_Monitor_Detail(deviceid);
                //strmouse = Objquery.Get_Mouse_Detail(deviceid);
                //strFloppy = Objquery.Get_Floppy_Detail(deviceid);
                //strcd = Objquery.Get_CD_Detail(deviceid);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_hardwareinfo Exception : " + ex.Message.ToString());

            }
            //var data = new { moterboardinfo = dt, os = stros, date = strosdate };
            var data = new { moterboardinfo = dt };
            //var data = new { moterboardinfo = dt, processor = strprocessor, os = DtOS, hdd = HwData, niccount = strlancard, ram = DTfirewall, keyboard = strkeyboard, monitor = strmonitor, mouse = strmouse, floppy = strFloppy, cd = strcd };
            return Json(data);
        }

        public JsonResult update_Get_hardwareinfo(string deviceid, string mothboard, string manafectural, string model, string processor, string os, string installdate, string hddlist, string hddsize, string hddtype, string nic, string ramtype, string ramsize, string ramslots, string keyboard, string monitor, string mouse, string floppy, string cdrom)
        {
            int querystatus = 0;
            string result = "";
            if (ramslots == null)
                ramslots = "0";
            //string strmotherboard = "";
            //string strquery = "";
            //string stros = "";
            //string strram = "";
            //string strhdd = "";
            try
            {
                //querystatus = Objquery.Update_AssetInfo(deviceid, mothboard, manafectural, processor, model, hddtype, hddlist, hddcapacity, ramtype, ramsize, ramslot, keyboard, mouse, monitor, floppy, cdrom, nic, os, osdate);
                querystatus = Objquery.Update_AssetInfo(deviceid, mothboard, manafectural, processor, model, hddtype, hddlist, hddsize, ramtype, ramsize, ramslots, keyboard, mouse, monitor, floppy, cdrom, nic, os, installdate);

                //if (mothboard != null)
                //    strmotherboard = " details='" + mothboard + "'";
                //if (manafectural != null)
                //{
                //    if (strmotherboard == "")
                //        strmotherboard += " manufacture='" + manafectural + "'";
                //    else
                //        strmotherboard += " ,manufacture='" + manafectural + "'";
                //}
                //if (model != null)
                //{
                //    if (strmotherboard == "")
                //        strmotherboard += " model_name='" + model + "'";
                //    else
                //        strmotherboard += " ,model_name='" + model + "'";

                //}

                //if (strmotherboard != null)
                //{
                //    cnt = Objquery.Get_Motherboard_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_Motherboard(deviceid, mothboard, manafectural, model);
                //    else
                //        strquery += Objquery.Update_motherboard(deviceid, strmotherboard);
                //}

                //if (ramtype != null)
                //    strram = " subtype_name='" + ramtype + "'";
                //if (ramsize != null)
                //{
                //    if (strram == "")
                //        strram += " capacity='" + ramsize + "'";
                //    else
                //        strram += " ,capacity='" + ramsize + "'";
                //}
                //if (ramslots != null)
                //{
                //    if (strram == "")
                //        strram += " slot_connector='" + ramslots + "'";
                //    else
                //        strram += " ,slot_connector='" + ramslots + "'";

                //}

                //if (strram != null)
                //{
                //    cnt = Objquery.Get_RAM_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_RAM(deviceid, ramtype, ramsize, ramslots);
                //    else
                //        strquery += Objquery.Update_RAM(deviceid, strram);
                //}

                ////if (hddlist != null)
                ////    strhdd = " subtype_name='" + mothboard + "'";
                //if (hddsize != null)
                //{
                //    // if (strhdd == "")
                //    strhdd += " capacity='" + hddsize + "'";
                //    //else
                //    //    strhdd += " ,capacity='" + hddsize + "'";
                //}
                //if (hddtype != null)
                //{
                //    if (strhdd == "")
                //        strhdd += " details='" + hddtype + "'";
                //    else
                //        strhdd += " ,details='" + hddtype + "'";

                //}
                //if (strhdd != null)
                //{
                //    cnt = Objquery.Get_HDD_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_HDD(deviceid, hddtype, hddsize);
                //    else
                //        strquery += Objquery.Update_HDD(deviceid, strhdd);
                //}

                //if (processor != null)
                //{
                //    cnt = Objquery.Get_Processor_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_Processor(deviceid, processor);
                //    else
                //        strquery += Objquery.Update_processor(deviceid, processor);
                //}
                //if (os != null || installdate != null)
                //{
                //    cnt = Objquery.Get_os_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_os(deviceid, os, installdate);
                //    else
                //    {
                //        if (os != null)
                //            stros = " os='" + os + "'";

                //        if (installdate != null)
                //        {
                //            if (stros != "")
                //                stros += " ," + Objquery.os_date(installdate);
                //            else
                //                stros += Objquery.os_date(installdate);
                //        }
                //        strquery += Objquery.Update_os(deviceid, stros);
                //    }

                //}
                //if (keyboard != null)
                //{
                //    cnt = Objquery.Get_Keyboard_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_Keyboard(deviceid, keyboard);
                //    else
                //        strquery += Objquery.Update_keyboard(deviceid, keyboard);
                //}
                //if (mouse != null)
                //{
                //    cnt = Objquery.Get_Mouse_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_Mouse(deviceid, mouse);
                //    else
                //        strquery += Objquery.Update_Mouse(deviceid, mouse);
                //}
                //if (monitor != null)
                //{
                //    cnt = Objquery.Get_Monitor_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_Monitor(deviceid, monitor);
                //    else
                //        strquery += Objquery.Update_Monitor(deviceid, monitor);
                //}
                //if (nic != null)
                //{
                //    cnt = Objquery.Get_NIC_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_NIC(deviceid, nic);
                //    else
                //        strquery += Objquery.Update_NIC(deviceid, nic);
                //}
                //if (floppy != null)
                //{
                //    cnt = Objquery.Get_Floppy_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_Floppy(deviceid, floppy);
                //    else
                //        strquery += Objquery.Update_Floppy(deviceid, floppy);
                //}
                //if (cdrom != null)
                //{
                //    cnt = Objquery.Get_CDROM_count(deviceid);
                //    if (cnt == 0)
                //        querystatus = Objquery.Insert_CDROM(deviceid, cdrom);
                //    else
                //        strquery += Objquery.Update_CDROM(deviceid, cdrom);
                //}
                //if (strquery != null)
                //    querystatus = Objquery.Update_Hardwaredata(strquery);

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "update_Get_hardwareinfo Exception : " + ex.Message.ToString());

            }

            if (querystatus == 0)
                result = "Hardware information updated failed.";
            else
                result = "Hardware information updated successfully.";
            return Json(result);
        }

        public JsonResult Get_purchaseinfo(string deviceid)
        {
            try
            {
                dt = Objquery.Get_assetinfo_purchase(deviceid);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_purchaseinfo Exception : " + ex.Message.ToString());

            }
            var data = new { purchaseinfo = dt };
            return Json(data);
        }

        public JsonResult store_purchaseinfo(string deviceid, string pcost, string pno, string ino, string attachment, string remarkr)
        {
            int querystatus = 0;
            string result = "";
            int filepathlength;
            // string strserverip = Objquery.get_serverip();
            // string strguiip = Objcom.Get_GUIIP();
            string strattachment = "";
            string filename = "";
            string filemovepath = "";
            //if (strserverip != strguiip)
            //    strattachment = "\\" + Objcom.Get_GUIIP() + "\\" + attachment.Replace(":", "$");
            //else
            strattachment = attachment;

            Objcom.WriteLog("csat_asset_mgmtController", "store_purchaseinfo ServerIP : " + strattachment);
            try
            {
                try
                {
                    // Objcom.WriteLog("csat_asset_mgmtController", "store_purchaseinfo File Source path : " + strserverip +" GUIIP : " + strguiip);
                    filepathlength = strattachment.LastIndexOf("\\");
                    filename = strattachment.Substring(filepathlength + 1);
                    filemovepath = OwnYITConstant.LINUX_WWW_PATH + "//Attachments//Purchase//";
                    if (!System.IO.Directory.Exists(filemovepath)) System.IO.Directory.CreateDirectory(filemovepath);
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("csat_asset_mgmtController", "store_purchaseinfo Move Folder Create Exception : " + ex.Message.ToString());
                }
                try
                {
                    //if (OwnYITConstant.SYSTEMTYPE.ToUpper() != "LINUX")
                    //    filemovepath = "\\" + Objcom.Get_GUIIP() + "\\" + filemovepath.Replace("C:", "C$");

                    //if (OwnYITConstant.SYSTEMTYPE.ToUpper() != "LINUX")
                    //{
                    //    if (strserverip != strguiip)
                    //        filemovepath = "\\" + strserverip + "\\" + filemovepath.Replace(":", "$");
                    //}

                    Objcom.WriteLog("csat_asset_mgmtController", "store_purchaseinfo File Destinaction path : " + filemovepath + filename);
                    // System.IO.File.Move(strattachment, filemovepath + filename);
                    System.IO.File.Copy(strattachment, filemovepath + filename, true);
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("csat_asset_mgmtController", "store_purchaseinfo File copy Exception : " + ex.Message.ToString());

                }

                if (Objquery.purchaseinfo_exist(deviceid) > 0)
                    querystatus = Objquery.Update_Purchasedata(deviceid, pcost, pno, ino, strattachment, filename, remarkr);
                else
                    querystatus = Objquery.Insert_Purchasedata(deviceid, pcost, pno, ino, strattachment, filename, remarkr);
                if (querystatus == 0)
                    result = "Purchase Information Inserted failed.";
                else
                    result = "Purchase information Inserted successfully.";

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_purchaseinfo Exception : " + ex.Message.ToString());

            }

            return Json(result);
        }
        public JsonResult store_AMCinfo(string deviceid, string warrentfrom, string warrentto, string amcfrom, string amcto, string vendorname, string cost, string location)
        {
            int querystatus = 0;
            string result = "";
            if (warrentfrom != null && warrentto != null)
            {
                warrentfrom = System.DateTime.ParseExact(warrentfrom.ToString(), "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
                warrentto = System.DateTime.ParseExact(warrentto.ToString(), "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
            }
            if (amcfrom != null && amcto != null)
            {
                amcfrom = System.DateTime.ParseExact(amcfrom.ToString(), "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
                amcto = System.DateTime.ParseExact(amcto.ToString(), "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
            }
            try
            {
                if (Objquery.AMCinfo_exist(deviceid) > 0)
                {
                    querystatus = Objquery.Update_AMCdata(deviceid, warrentfrom, warrentto, amcfrom, amcto, vendorname, cost, location);
                }
                else
                {
                    querystatus = Objquery.Insert_AMCdata(deviceid, warrentfrom, warrentto, amcfrom, amcto, vendorname, cost, location);
                }
                //querystatus = Objquery.Insert_AMCdata(deviceid, warrentfrom, warrentto, amcfrom, amcto, vendorname, cost, location);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_AMCinfo Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0)
                result = "AMC information inserted failed.";
            else
                result = "AMC information inserted successfully.";
            return Json(result);
        }

        // Get Vendors and AMC /Insurance Dates
        public JsonResult get_vendor_details(string device_id)
        {
            try
            {
                dt = Objquery.Get_vendor_name();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "get_vendor_details Exception : " + ex.Message.ToString());

            }
            var data = new { amcvendor = dt, amcinfo = Objquery.Get_assetinfo_AMCinfo(device_id) };
            //var data = new { amcvendor = dt, amcdates = Objquery.Get_AMCInsuranceDates(device_id) };
            return Json(data);
        }

        public JsonResult Get_insuranceinfo(string deviceid)
        {
            try
            {
                dt = Objquery.Get_assetinfo_insurance(deviceid);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_insuranceinfo Exception : " + ex.Message.ToString());

            }
            var data = new { insuranceinfo = dt };
            return Json(data);
        }

        public JsonResult store_Insuranceinfo(string deviceid, string party, string partyname, string insfrom, string insto, string amount)
        {
            int querystatus = 0;
            string result = "";
            if (insfrom != null && insto != null)
            {
                insfrom = System.DateTime.ParseExact(insfrom.ToString(), "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
                insto = System.DateTime.ParseExact(insto.ToString(), "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
            }
            try
            {
                if (Objquery.Insuranceinfo_exist(deviceid) > 0)
                {
                    querystatus = Objquery.Update_Insurance_data(deviceid, party, partyname, insfrom, insto, amount);
                }
                else
                {
                    querystatus = Objquery.Insert_Insurance_data(deviceid, party, partyname, insfrom, insto, amount);
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_Insuranceinfo Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0)
                result = "Insurance information inserted failed.";
            else
                result = "Insurance information inserted successfully.";
            return Json(result);
        }
        public JsonResult Gethrdjsondata(string deviceid)
        {
            DataTable Dtram = new DataTable();
            DataTable Dtprinter = new DataTable();
            DataTable DtBaseboard = new DataTable();
            DataTable DtBios = new DataTable();
            DataTable DtComputer = new DataTable();
            DataTable DtMonitor = new DataTable();
            DataTable DtKeyboard = new DataTable();
            DataTable DtMouse = new DataTable();
            DataTable DtProcessor = new DataTable();
            DataTable DtUSBPort = new DataTable();
            DataTable DtVideo = new DataTable();
            DataTable DtComputerProduct = new DataTable();
            DataTable DtSound = new DataTable();
            DataTable DtHardDisk = new DataTable();
            DataTable DtNicCard = new DataTable();
            DataTable DtCdRom = new DataTable();
            string stros = "";
            string strosdate = "";
            //int RamSize = 0;
            //int HDDSize = 0;
            try
            {
                dt = Objquery.Get_Hardware_type(deviceid);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ("BASEBOARD" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtBaseboard = Objquery.Get_hrdjsondatatable("BaseBoard", deviceid);
                    else if ("BIOS" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtBios = Objquery.Get_hrdjsondatatable("BIOS", deviceid);
                    else if ("COMPUTERSYSTEM" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtComputer = Objquery.Get_hrdjsondatatable("ComputerSystem", deviceid);
                    else if ("DESKTOPMONITOR" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtMonitor = Objquery.Get_hrdjsondatatable("DesktopMonitor", deviceid);
                    else if ("KEYBOARD" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtKeyboard = Objquery.Get_hrdjsondatatable("Keyboard", deviceid);
                    else if ("PHYSICALMEMORY" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        Dtram = Objquery.Get_hrdjsondatatable("PhysicalMemory", deviceid);
                    else if ("POINTINGDEVICE" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtMouse = Objquery.Get_hrdjsondatatable("PointingDevice", deviceid);
                    else if ("PRINTER" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        Dtprinter = Objquery.Get_hrdjsondatatable("Printer", deviceid);
                    else if ("PROCESSOR" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtProcessor = Objquery.Get_hrdjsondatatable("Processor", deviceid);
                    else if ("USBHUB" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtUSBPort = Objquery.Get_hrdjsondatatable("USBHUB", deviceid);
                    else if ("VIDEO" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtVideo = Objquery.Get_hrdjsondatatable("Video", deviceid);
                    else if ("COMPUTERSYSTEMPRODUCT" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtComputerProduct = Objquery.Get_hrdjsondatatable("ComputerSystemProduct", deviceid);
                    else if ("SOUNDDEVICE" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtSound = Objquery.Get_hrdjsondatatable("SoundDevice", deviceid);
                    else if ("HARDDISK" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtHardDisk = Objquery.Get_hrdjsondatatable("HardDisk", deviceid);
                    else if ("NICCARD" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtNicCard = Objquery.Get_hrdjsondatatable("NicCard", deviceid);
                    else if ("CDROMDRIVE" == dt.Rows[i]["hardware_class"].ToString().ToUpper())
                        DtCdRom = Objquery.Get_hrdjsondatatable("CdRomDrive", deviceid);
                }
                stros = Objquery.Get_OSName(deviceid);
                strosdate = Objquery.Get_OSInstallationdate(deviceid);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Gethrdjsondata Exception : " + ex.Message.ToString());
            }
            var data = new { hardwaretype = dt, Baseboard = DtBaseboard, Bios = DtBios, Comp = DtComputer, monitor = DtMonitor, key = DtKeyboard, Pm = Dtram, Pd = DtMouse, printer = Dtprinter, Proc = DtProcessor, Usbd = DtUSBPort, video = DtVideo, dtcompproduct = DtComputerProduct, sound = DtSound, dtharddisk = DtHardDisk, dtniccard = DtNicCard, cdrom = DtCdRom, RamSize = Objquery.Get_RAMSize(deviceid), HDDSize = Objquery.Get_HDDCapacity(deviceid), os = stros, date = strosdate };
            return Json(data);
        }

        // Software Master 
        public JsonResult get_offline_software_data()
        {
            var data = new { softdata = Objquery.Get_offline_software_details() };
            return Json(data);
        }


        public JsonResult get_offline_software_dropdowndata()
        {
            var data = new { software = Objquery.Get_softoffline_software_name(), vendor = Objquery.Get_softoffline_vendorname(), key = Objquery.Get_softoffline_key_number(), licensetype = Objquery.Get_softoffline_licenses_type() };
            return Json(data);
        }
        public JsonResult store_sofwaredata(string soft_name, string prodesc, string key, string textvendor, int vendor, string p_date, string po_no, string licensetype, int licenseno, string softtype, string expdate, string invoiceno, string invoiceamt)
        {
            int querystatus = 0;
            string result = "";
            string purchasedate = "";
            string expirydate = "";
            if (p_date != "" && p_date != null)
            {
                purchasedate = System.DateTime.ParseExact(p_date.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
            }

            if (expdate != "" && expdate != null)
            {
                expirydate = System.DateTime.ParseExact(expdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
            }
            try
            {
                if (textvendor != "")
                {
                    if (textvendor != null)
                    {
                        querystatus = Objquery.insert_vendorname(textvendor);
                        vendor = Objquery.getvendorid(textvendor);
                    }
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_sofwaredata vendor data insert Exception : " + ex.Message.ToString());

            }
            try
            {
                if (soft_name != "" || soft_name != null)
                {
                    querystatus = Objquery.insert_offline_softwaremaster(soft_name, prodesc, key, vendor, purchasedate, po_no, licensetype, licenseno, softtype, expirydate, invoiceno, invoiceamt);
                    if (querystatus == 0)
                        result = "Software information inserted failed.";
                    else
                        result = "Software information inserted successfully.";
                }
                else
                {
                    result = "Please enter software";
                }

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "store_sofwaredata Exception : " + ex.Message.ToString());

            }
            return Json(result);
        }

        public JsonResult delete_softdata(string softid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.delete_offline_softwaremaster(softid);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "delete_softdata Exception : " + ex.Message.ToString());

            }
            if (querystatus == 0)
                result = "Software information deleted failed.";
            else
                result = "Software information deleted successfully.";
            return Json(result);
        }

        public JsonResult get_show_offline_software_data(string softid)
        {
            var data = new { softdatasoftwise = Objquery.Get_offline_software_details_softwise(softid), software = Objquery.Get_softoffline_software_name(), vendor = Objquery.Get_softoffline_vendorname(), key = Objquery.Get_softoffline_key_number(), licensetype = Objquery.Get_softoffline_licenses_type() };
            return Json(data);
        }

        public JsonResult Edit_offlinesoftwaredata(string soft_id, string soft_name, string prodesc, string key, string textvendor, int vendor, string p_date, string pono, string licensetype, string nolicense, string softtype, string expdate, string invnumber, string invamt)
        {
            int querystatus = 0;
            string result = "";
            string purchasedate = "";
            string expirydate = "";

            purchasedate = System.DateTime.ParseExact(p_date.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";

            if (expdate != "")
            {
                if (expdate != null)
                {
                    expirydate = System.DateTime.ParseExact(expdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                }
            }
            try
            {
                if (textvendor != "")
                {
                    if (textvendor != null)
                    {
                        querystatus = Objquery.insert_vendorname(textvendor);
                        vendor = Objquery.getvendorid(textvendor);
                    }
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Edit data vendor Code insert Exception : " + ex.Message.ToString());

            }

            try
            {
                if (soft_name != "")
                {
                    if (soft_name != null)
                    {
                        querystatus = Objquery.Update_offline_softwaremaster(soft_id, soft_name, prodesc, key, vendor, purchasedate, pono, licensetype, nolicense, softtype, expirydate, invnumber, invamt);
                        if (querystatus == 0)
                            result = "Software information Update failed.";
                        else
                            result = "Software information Update successfully.";
                    }
                    else
                    {
                        result = "Please enter software";
                    }
                }
                else
                {
                    result = "Please enter software";
                }

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Edit_offlinesoftwaredata Exception : " + ex.Message.ToString());

            }
            return Json(result);
        }

        // System network traffic details
        public JsonResult Get_Systemnetworktrafficchart(string deviceid, string type, string Days)
        {
            string Systemnetworktraffic = "";
            string startdate = "";
            string enddate = "";
            string strtime = "";
            string ip = "";
            enddate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ip = Objquery.Get_network_traffic_Chart_ip(deviceid);
            if (Days == "01" || Days == null)
            {
                startdate = System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                strtime = " where startdate between '" + startdate + "' and '" + enddate + "'";
                dt = Objquery.Get_network_traffic_ChartHourwise(type, ip, strtime);
            }
            else if (Days == "02")
            {
                startdate = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 00:00:00";
                strtime = " where startdate between '" + startdate + "' and '" + enddate + "'";
                dt = Objquery.Get_network_traffic_Chart(type, ip, strtime);
            }
            else if (Days == "07")
            {
                startdate = System.DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd") + " 00:00:00";
                strtime = " where startdate between '" + startdate + "' and '" + enddate + "'";
                dt = Objquery.Get_network_traffic_Chart(type, ip, strtime);
            }
            else if (Days == "15")
            {
                startdate = System.DateTime.Now.AddDays(-16).ToString("yyyy-MM-dd") + " 00:00:00";
                strtime = " where startdate between '" + startdate + "' and '" + enddate + "'";
                dt = Objquery.Get_network_traffic_Chart(type, ip, strtime);
            }
            try
            {
                this.database = new DatabaseHandler(OwnYITConstant.db_settings);
                this.DBServer_Type = this.database.DB_SERVER_TYPE;
                switch (this.database.DB_SERVER_TYPE)
                {
                    case 0:
                        dbtype = OwnYITConstant.DatabaseTypes.MSSQL_SERVER;
                        List<assetnwtraficchart> trafficdetails = Objcom.ConvertDataTable<assetnwtraficchart>(dt);
                        Systemnetworktraffic = JsonConvert.SerializeObject(trafficdetails);
                        break;
                    case 1:
                        dbtype = OwnYITConstant.DatabaseTypes.MYSQL_SERVER;
                        List<assetnwtraficchart_decimal> trafficdetails1 = Objcom.ConvertDataTable<assetnwtraficchart_decimal>(dt);
                        Systemnetworktraffic = JsonConvert.SerializeObject(trafficdetails1);
                        break;
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Get_Systemnetworktrafficchart Exception : " + ex.Message.ToString());
            }
            var data = new { systemnetworktrafficdata = Systemnetworktraffic, packetype = type, Days1 = Days };
            return Json(data);
            //try
            //{
            //    List<assetnwtraficchart> trafficdetails = Objcom.ConvertDataTable<assetnwtraficchart>(dt);
            //    Systemnetworktraffic = JsonConvert.SerializeObject(trafficdetails);
            //}
            //catch (Exception ex)
            //{
            //    List<assetnwtraficchart> trafficdetails = Objcom.ConvertDataTable<assetnwtraficchart>(dt);
            //    Systemnetworktraffic = JsonConvert.SerializeObject(trafficdetails);
            //    Objcom.WriteLog("csat_asset_mgmtController", "Get_Systemnetworktrafficchart Exception : " + ex.Message.ToString());
            //}
        }

        // Set Uninstall password
        public JsonResult set_unstall_password(string oldpassword, string newpassword)
        {
            string strreturn = "";
            int cnt = 0;
            cnt = Objquery.csat_set_unstall_password(oldpassword, newpassword);
            if (cnt == -1)
                strreturn = "Old password not correct";
            else if (cnt == -2)
                strreturn = "Some problem while submit set uninstall password";
            else if (cnt == 0)
                strreturn = "Please enter password";
            else
                strreturn = "Uninstall password set successfully";
            return Json(strreturn);
        }
        // Set Server IP

        public JsonResult set_serverip(string ip1, string ip2, string ip3, string ip4, string deviceid)
        {
            string strreturn = "";
            string deviceidlist = "";
            string MSG = "";
            if (ip2 == "" || ip2 == null)
                ip2 = "0.0.0.0";
            if (ip3 == "" || ip3 == null)
                ip3 = "0.0.0.0";
            if (ip4 == "" || ip4 == null)
                ip4 = "0.0.0.0";

            int cnt = 0;
            deviceidlist = deviceid.Substring(1);
            string[] strdeviceIDArr = deviceidlist.Split(',');
            MSG = "#3@1022!" + ip1 + "!" + ip2 + "!" + ip3 + "!" + ip4 + "!@3#";
            for (int i = 0; i < strdeviceIDArr.Length; i++)
            {
                cnt = Objquery.InsertQueryLog(strdeviceIDArr[i], MSG, Objquery.Get_Location_ID(strdeviceIDArr[i]));
            }
            if (cnt > 0)
                strreturn = "Set Server IP Successfully";
            else
                strreturn = "Set Server IP Failed";
            return Json(strreturn);
        }

        // Show Manage Agent Registry
        public JsonResult GetDeviceRegistry(string deviceid)
        {
            try
            {
                deviceid = deviceid.Substring(1);
                dt = Objquery.Get_DeviceRegistry(deviceid);
            }
            catch (Exception ex)
            {

                Objcom.WriteLog("csat_asset_mgmtController", "GetDeviceRegistry Exception : " + ex.Message.ToString());
            }
            var data = new { deviceregistry = dt };
            return Json(data);


        }

        // Add Maange Agent Registry
        public JsonResult AddDeviceRegistry(string deviceid, string key, string keytype, string keyvalue, string keyaction, string path)
        {
            string strDeviceID = (deviceid.Length > 0) ? " and device_id in(" + deviceid.Substring(1) + ")" : "";
            return Json(Objquery.Get_LocationIDForDeviceRegIns(strDeviceID, key, path, keytype, keyvalue, keyaction));
        }

        // New System List 
        public JsonResult GetSystemlist(string ouid, string IP, string Device, string Status, string MACAddress)
        {
            try
            {
                string search = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name ='" + Device + "'";
                }
                if (MACAddress != "" && MACAddress != null)
                {
                    search += " and dl.device_mac like '%" + MACAddress + "%'";
                }
                if (IP != null)
                {
                    if (IP != "-1")
                        search += " and dl.ip like '%" + IP + "%'";
                    //search += " and dm.ip like '%" + IP + "%'";
                }
                string StatusCond = "";
                if (Status != null)
                {
                    if (Status != "-1")
                    {
                        StatusCond = " where status ='" + Status + "'";
                        //search += " and dl.status ='" + Status + "'";
                    }
                    //search += " and dm.status ='" + Status + "'";
                }
                dt = Objquery.Get_DeviceDataList(search, StatusCond);
            }
            catch (Exception ex)
            {

                Objcom.WriteLog("csat_asset_mgmtController", "GetSystemlist Exception : " + ex.Message.ToString());
            }
            var data = new { sysdata = dt };
            return Json(data);
        }

        // RDV

        public JsonResult GetRDVData(string IP, string Deviceid, string type)
        {
            int querystatus = 0;
            String r = generator.Next(0, 999999).ToString("D6");
            string strmsg = "";

            //string result = "";
            string TunnelIP = "";
            string strAuth = "";
            string strType = "";
            string strPrimaryIP = "";
            string strSecondaryIP = "";
            string strTmpPrimaryIP = "";
            string strTmpSecondaryIP = "";
            string strTmpPrimaryIP1 = "";
            string strTmpSecondaryIP1 = "";
            string chart_url = "";
            string UserName = HttpContext.Session.GetString("user");
            TunnelIP = Objquery.Get_TunnelIP();
            int time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            strmsg = "#91@" + time + r + "!" + Deviceid + "!" + "RDV" + "!" + time + "@91#";
            Objquery.InsertQueryLog(Deviceid, strmsg, Objquery.Get_Location_ID(Deviceid));
            strType = Objquery.Get_RDVClientType(Deviceid);
            strAuth = Objquery.Get_RDVAuth(Deviceid, IP);
            if (strAuth == "1")
            {
                // querystatus = 1;
            }
            else
            {
                querystatus = Objquery.Set_GUILog(Deviceid, "RDV", "RDV Taken", UserName);
                //chart_url = Objcom.GetGUIconfig("chart_url");
                chart_url = GetGUIconfig("chart_url");
                HttpContext.Session.SetString("RDVServerPort", "5999");
                HttpContext.Session.SetString("RDVDeviceID", Deviceid);
                HttpContext.Session.SetString("RDVServerIP", Objquery.Get_RDVIP(Deviceid));
                strPrimaryIP = HttpContext.Request.Host.ToString();

                //strTmpPrimaryIP = Objcom.GetGUIconfig("PrimaryServerIP");
                //strTmpSecondaryIP = Objcom.GetGUIconfig("SecondaryServerIP");
                //strTmpPrimaryIP1 = Objcom.GetGUIconfig("PrimaryServerIP1");
                //strTmpSecondaryIP1 = Objcom.GetGUIconfig("SecondaryServerIP1");
                strTmpPrimaryIP = GetGUIconfig("PrimaryServerIP");
                strTmpSecondaryIP = GetGUIconfig("SecondaryServerIP");
                strTmpPrimaryIP1 = GetGUIconfig("PrimaryServerIP1");
                strTmpSecondaryIP1 = GetGUIconfig("SecondaryServerIP1");

                Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData strPrimaryIP : " + strPrimaryIP);
                Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData strTmpPrimaryIP : " + strTmpPrimaryIP);
                Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData strTmpSecondaryIP : " + strTmpSecondaryIP);
                Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData strTmpPrimaryIP1 : " + strTmpPrimaryIP1);
                Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData strTmpSecondaryIP1 : " + strTmpSecondaryIP1);

                if (strPrimaryIP == strTmpPrimaryIP)
                    strSecondaryIP = strTmpSecondaryIP;
                else if (strPrimaryIP == strTmpSecondaryIP)
                {
                    strPrimaryIP = strTmpSecondaryIP;
                    strSecondaryIP = strTmpPrimaryIP;
                }
                else
                {
                    strPrimaryIP = strTmpPrimaryIP;
                    strSecondaryIP = strTmpSecondaryIP;
                }

                Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData Primary Server IP : " + strPrimaryIP);
                Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData Secondary Server IP : " + strSecondaryIP);
                try
                {
                    if (strType.Trim().Length > 0)
                    {
                        if (Convert.ToInt32(strType) >= 10)
                        {
                            Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData Primary Server IP Final : " + strTmpPrimaryIP);
                            Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData Secondary Server IP Final: " + strTmpSecondaryIP);
                            Objcom.WriteLog("csat_asset_mgmtController", "Primary Server IP1 Final : " + strTmpPrimaryIP1);
                            Objcom.WriteLog("csat_asset_mgmtController", "GetRDVData Secondary Server IP1 Final: " + strTmpSecondaryIP1);
                        }
                        else
                        {
                            HttpContext.Session.SetString("RDVServerPort", "6460");
                            HttpContext.Session.SetString("RDVServerIP", IP);
                            strPrimaryIP = IP;
                            strSecondaryIP = "";
                            strTmpPrimaryIP1 = "";
                            strTmpSecondaryIP1 = "";


                        }
                    }
                    else
                    {
                        HttpContext.Session.SetString("RDVServerPort", "6460");
                        HttpContext.Session.SetString("RDVServerIP", IP);
                        strPrimaryIP = IP;
                        strSecondaryIP = "";
                        strTmpPrimaryIP1 = "";
                        strTmpSecondaryIP1 = "";
                    }
                }
                catch (Exception)
                {
                    Objcom.WriteLog("csat_asset_mgmtController", "Exception GetRDVData Primary Server IP Final : " + strTmpPrimaryIP);
                    Objcom.WriteLog("csat_asset_mgmtController", "Exception GetRDVData Secondary Server IP Final: " + strTmpSecondaryIP);
                    Objcom.WriteLog("csat_asset_mgmtController", "Exception GetRDVData Primary Server IP1 Final : " + strTmpPrimaryIP1);
                    Objcom.WriteLog("csat_asset_mgmtController", "Exception GetRDVData Secondary Server IP1 Final: " + strTmpSecondaryIP1);
                }
            }

            var data = new { auth = strAuth, host = strPrimaryIP, host1 = strTmpPrimaryIP1, secondaryhost = strSecondaryIP, secondaryhost1 = strTmpSecondaryIP1, port = HttpContext.Session.GetString("RDVServerPort"), viewonly = type, session = HttpContext.Session.GetString("RDVDeviceID"), tunnelip = TunnelIP, deviceip = HttpContext.Session.GetString("RDVServerIP"), chart_url = chart_url };
            return Json(data);

        }

        public JsonResult InsertViewHighRDV(string Deviceid)
        {
            string chart_url1 = "";
            try
            {
                //chart_url1 = Objcom.GetGUIconfig("chart_url");
                chart_url1 = GetGUIconfig("chart_url");
                String r = generator.Next(0, 999999).ToString("D6");
                int time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                strmsg = "#91@" + time + r + "!" + Deviceid + "!" + "RDV" + "!" + time + "@91#";
                Objquery.InsertQueryLog(Deviceid, strmsg, Objquery.Get_Location_ID(Deviceid));
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Exception InsertViewHighRDV Final : " + ex.ToString());
            }
            var data = new { chart_url = chart_url1 };
            return Json(data);
        }

        public JsonResult insert_hwvendor(string vname)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                if (vname != null)
                {
                    cnt = Objquery.count__Vendormaster(vname);
                    if (cnt == 0)
                        querystatus = Objquery.insert_hwvendor(vname);
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "insert_hwvendor Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }

        public JsonResult insert_swvendor(string vname)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                if (vname != null)
                {
                    cnt = Objquery.count__Vendormaster(vname);
                    if (cnt == 0)
                        querystatus = Objquery.insert_swvendor(vname);
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "insert_swvendor Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }

        public JsonResult Asset_hrdreport(string ouid, string startdate, string enddate, string dateduration, string serchtype, string searchval)
        {
            string Sdate = "";
            string Edate = "";
            string search = "";
            DataTable dt = new DataTable();
            if (startdate != null && enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
            }
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += "and ou_id in (" + ouid + ")";
                }
                if (dateduration != null)
                {
                    if (dateduration != "-1")
                    {
                        if (dateduration == "Purchase_Date")
                            search += "and purchase_date >= '" + Sdate + "' and purchase_date <= '" + Edate + "'";
                        else if (dateduration == "AMC_Upto")
                            search += " and amc_fromto >= '" + Sdate + "' and amc_end <= '" + Edate + "'";
                        else if (dateduration == "Warranty_Period")
                            search += " and warranty_period_fromto >= '" + Sdate + "' and warranty_end <= '" + Edate + "'";
                    }
                }
                if (serchtype != null && searchval != null)
                {
                    search += " and " + serchtype + " like '%" + searchval + "%'";
                }
                dt = Objquery.Get_hrdreport(search);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Asset_hrdreport Exception : " + ex.Message.ToString());
            }
            var data = new { hrdreportdata = dt };
            return Json(data);
        }

        public JsonResult Asset_swreport(string serchtype, string searchval, string licensetype1, string startdate, string enddate, string dateduration, string softtypevalue)
        {
            string Sdate = "";
            string Edate = "";
            string search = "";
            DataTable dt = new DataTable();
            if (startdate != null && enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
            }
            try
            {
                if (softtypevalue != null)
                    search += "and software_type = '" + softtypevalue + "'";
                if (serchtype != null && searchval != null)
                {
                    search += " and " + serchtype + " like '%" + searchval + "%'";
                }
                if (licensetype1 != null && licensetype1 != "-1")
                    search += "and licenses_type in ('" + licensetype1 + "')";
                if (dateduration != null && dateduration != "-1")
                {
                    if (dateduration == "Purchase")
                        search += "and purchase_date >= '" + Sdate + "' and purchase_date <= '" + Edate + "'";
                    else if (dateduration == "Expiry")
                        search += " and expiry_date >= '" + Sdate + "' and expiry_date <= '" + Edate + "'";
                }
                dt = Objquery.Get_swreport(search);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Asset_swreport Exception : " + ex.Message.ToString());
            }
            var data = new { swreportdata = dt };
            return Json(data);
        }

        public JsonResult get_offline_software_dropdowndata_report()
        {
            var data = new { licensetype = Objquery.Get_softoffline_licenses_type() };
            return Json(data);
        }

        public JsonResult Asset_vendorreport(string serchtype, string searchval)
        {
            string search = "";
            DataTable dt = new DataTable();
            try
            {
                if (serchtype != null && searchval != null)
                {
                    search += " and " + serchtype + " like '%" + searchval + "%'";
                }
                dt = Objquery.Get_vendorreport(search);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_asset_mgmtController", "Asset_vendorreport Exception : " + ex.Message.ToString());
            }
            var data = new { vendorreportdata = dt };
            return Json(data);
        }

        public string GetGUIconfig(String strKey)
        {
            String retVal = "";
            try
            {
                retVal = Objquery.read_guisetting(strKey);
                return retVal;
            }
            catch (Exception) { }
            return retVal;
        }
    }
}