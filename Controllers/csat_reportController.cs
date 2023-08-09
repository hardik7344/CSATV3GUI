using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using OwnYITCSAT.DataAccessLayer;
using OwnYITCSAT.Models;

namespace OwnYITCSAT.Controllers
{
    public class csat_reportController : Controller
    {
        private int DBServer_Type = 0;
        private DatabaseHandler database = null;
        OwnYITConstant.DatabaseTypes dbtype;
        DBQueryHandler Objquery = new DBQueryHandler();
        OwnYITCommon objcommon = new OwnYITCommon();
        DataTable dtsubtosubmenu = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtreortlogo = new DataTable();
        DataTable dt1 = new DataTable();
        string search = "";
        private readonly IHostingEnvironment _env;
        private IConverter _convertor;
        public csat_reportController(IConverter convertor, IHostingEnvironment hostingEnv)
        {
            _convertor = convertor;
            _env = hostingEnv;
        }
        public IActionResult csat_report(int id)
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
                try
                {
                    if (OwnYITConstant.DT_REPORT_MENU == null)
                    {
                        OwnYITConstant.DT_REPORT_MENU = Objquery.Get_SubMenu(id);

                    }
                    if (id.ToString() == null)
                        return RedirectToAction("csat_login", "csat_login");
                    if (OwnYITConstant.DT_REPORT_MENU == null)
                        return RedirectToAction("csat_login", "csat_login");
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csat_reportController", "csat_report Exception : " + ex.Message.ToString());
                }
            return View();
        }
        public ActionResult get_Submenu(int catid)
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
                try
                {
                    dtsubtosubmenu = Objquery.Get_SubMenu(catid);
                    DataColumn dcolColumn = new DataColumn("class", typeof(string));
                    dtsubtosubmenu.Columns.Add(dcolColumn);
                    OwnYITConstant.DT_ASSETMGMT_SUB_MENU = GetCssData(dtsubtosubmenu);
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csat_reportController", "get_Submenu Exception : " + ex.Message.ToString());
                }
            return View("csat_report");

        }

        public IActionResult audit_report1(string GridHtml)
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var globalSettings = new GlobalSettings
                {
                    //ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 4 },
                    //Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",
                    //Out = @"D:\Source\Projects\ElectronicInvoicing\ElectronicInvoicing\PDFs\Doc" + DateTime.Now +".pdf" // USE THIS PROPERTY TO SAVE PDF TO A PROVIDED LOCATION
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = GridHtml,
                    WebSettings = { DefaultEncoding = "utf-8", MinimumFontSize = 10, UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/css/", "styles.css") },
                    HeaderSettings = { FontName = "Times New Roman", FontSize = 8, Line = false },
                    FooterSettings = { FontName = "Times New Roman", FontSize = 7, Right = "Page [page] of [toPage]", Line = false, Left = "Created on:" + DateTime.Now.ToString("dd/MM/yyyy HH:mm").ToString() }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                var file = _convertor.Convert(pdf);
                //return Ok("Successfully created PDF document.");
                return File(file, "application/pdf", "CSAT_Audit_report_" + DateTime.Now + ".pdf");
            }
            else
            {

            }
            return View();
        }
        public DataTable GetCssData(DataTable dtcss)
        {
            try
            {
                for (int i = 0; i < dtcss.Rows.Count; i++)
                {

                    if ("Agent Installed" == dtcss.Rows[i]["menu_name"].ToString())//Agent Management Menu start
                        dtcss.Rows[i]["class"] = "scan-ip-range";
                    else if ("Agent Inactive" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-installation";
                    else if ("Agent Information" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-installatin-status";
                    else if ("Agent Settings" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-uninstall";
                    else if ("System Registry" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "authorize-ip-range";
                    else if ("Agent Release License" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-realease-license";
                    else if ("System CPU Usages" == dtcss.Rows[i]["menu_name"].ToString())//Organization Structure Management Menu start
                        dtcss.Rows[i]["class"] = "branch-level-creation-1";
                    else if ("System Firewall Status" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "manage-branch-unit";
                    else if ("Internet Modem Usages" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "unlink-agent";
                    else if ("Active User Login" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "unlink-agent";
                    else if ("Network ON/OFF" == dtcss.Rows[i]["menu_name"].ToString())//Admin User Management Menu start
                        dtcss.Rows[i]["class"] = "unlink-agent";
                    else if ("Network Card Details" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "unlink-agent";
                    else if ("Printer Usages" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "unlink-agent";
                    else if ("Agent Configuration" == dtcss.Rows[i]["menu_name"].ToString())//Configuration Management Menu start
                        dtcss.Rows[i]["class"] = "agent-configuration";
                    else if ("Authorize IP for Accessing GUI" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "authorize-ip-for-accessing-gui";
                    else if ("Manage Process Category" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "manage-process-category";
                    else if ("Database Maintanance" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "database-maintanance";
                    else if ("Manage Disk Clean Up" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "manage-disk-cleanup";
                    else if ("Setting For Mail Report" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "setting-for-mail-report";
                    else if ("Remote File Execution" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "remote-file-execute";
                    else if ("Event Viewer Monitoring" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "remote-file-execute";
                    else if ("Agent Patch Management" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-patch-management";
                    else if ("Service Setting" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "service-setting";
                    else if ("Configuration Setting" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "configuration-settings";
                    else if ("SSH Configuration" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "configuration-settings";
                    else if ("Create Notification" == dtcss.Rows[i]["menu_name"].ToString())//Alert Notification Handling
                        dtcss.Rows[i]["class"] = "create-notification-1";
                    else if ("Desired Configuration Management" == dtcss.Rows[i]["menu_name"].ToString())//Compliance Management
                        dtcss.Rows[i]["class"] = "desired-configuration-mgmt";
                    else if ("Exclusion IP for Compliance" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "exclution-ip-for-compliance";




                }
            }
            catch (Exception ex)
            {

                objcommon.WriteLog("csatsettingController", "GetCssData Exception : " + ex.Message.ToString());
            }
            return dtcss;
        }

        public IActionResult report_data_leakage()
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
                //ViewBag.labels = "[ \"HDDToUSB\", \"USBToHDD\", \"HDDToCD-Drive\", \"ShareToHDD\" ]";
                //ViewBag.labels = " ['HDDToUSB', 'USBToHDD', 'HDDToCD-Drive', 'ShareToHDD']";
                //ViewBag.data = "[32, 43, 28, 56]";

                return View();
        }
        //string color = "";
        public JsonResult GetDatalekage(string startdate, string enddate)
        {

            //color ="'#f95e5e', '#4b92cc', '#0acdc4', '#fea47f', '#00adca', '#f9a056', '#b25797', '#66c5de', '#e76d6f'";

            string strmedia = "";
            string strbranch = "";
            string strip = "";
            string strfiletype = "";
            string AlertData = "";
            string Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
            string Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";

            try
            {
                dt = Objquery.Get_Mediawise_Datalekage(Sdate, Edate);
                List<BarChart> media = objcommon.ConvertDataTable<BarChart>(dt);
                strmedia = JsonConvert.SerializeObject(media);
                objcommon.WriteLog("csat_reportController", "GetDatalekage FileTypeWise Data : " + strmedia);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetDatalekage Mediawise Exception : " + ex.Message.ToString());
            }
            try
            {
                dt = Objquery.Get_BranchOUWise_Datalekage(Sdate, Edate);
                List<BarChart> ou = objcommon.ConvertDataTable<BarChart>(dt);
                strbranch = JsonConvert.SerializeObject(ou);

                objcommon.WriteLog("csat_reportController", "GetDatalekage BranchOUWise Data : " + strbranch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetDatalekage BranchOUWise Exception : " + ex.Message.ToString());
            }

            try
            {
                dt = Objquery.Get_IPwise_Datalekage(Sdate, Edate);
                List<BarChart> ip = objcommon.ConvertDataTable<BarChart>(dt);
                strip = JsonConvert.SerializeObject(ip);

                objcommon.WriteLog("csat_reportController", "GetDatalekage IPwise Data : " + strip);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetDatalekage IPWise Exception : " + ex.Message.ToString());
            }

            try
            {
                dt = Objquery.Get_FileTypeWise_Datalekage(Sdate, Edate);
                List<BarChart> filetype = objcommon.ConvertDataTable<BarChart>(dt);
                strfiletype = JsonConvert.SerializeObject(filetype);
                objcommon.WriteLog("csat_reportController", "GetDatalekage FileTypeWise Data : " + strfiletype);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetDatalekage FileTypeWise Exception : " + ex.Message.ToString());
            }


            try
            {
                AlertData = objcommon.CreateTable(Objquery.Get_TopDataLeakageAlert(Sdate, Edate));
                objcommon.WriteLog("csat_reportController", "csat_report Top 10 Dataleakage ALert Table : " + AlertData);
            }
            catch (Exception ex)
            {
                AlertData = "";
                objcommon.WriteLog("csat_reportController", "csat_report Top 10 Dataleakage ALert Exception : " + ex.Message.ToString());
            }


            var data = new { Media = strmedia, Branch = strbranch, Ip = strip, File = strfiletype, Alerts = AlertData };
            return Json(data);
        }

        public JsonResult GetOU()
        {
            //Objquery.Get_OUList("", "1");
            // var data = new { Ou = Objquery.Get_OUList("", "1") };
            var data = new { Ou = Objquery.Get_OUList("", "1"), Printer = Objquery.Get_Printer(), OS = Objquery.Get_OSList(), Extention = Objquery.Get_FileExtention() };
            return Json(data);
        }
        public JsonResult Getlocalip(string ip, string packetstype)
        {
            DataTable local = new DataTable();
            DataTable remote = new DataTable();
            if (packetstype == "1" || packetstype == "3")
            {
                local = Objquery.Get_single_localip(ip);
                remote = Objquery.Get_multi_remoteip(ip);
            }
            else
            {
                local = Objquery.Get_multi_localip(ip);
                remote = Objquery.Get_single_remoteip(ip);
            }
            var data = new { localip = local, remoteip = remote };
            return Json(data);
        }
        public JsonResult Get_syatemname_ip(string ouid)
        {

            //   var data = new { systemip = Objquery.Get_syatemname_ip(ouid) };
            // var data = new { systemip = Objquery.Get_syatemname_ip(Objquery.Get_ParentOu_id(ouid)) };
            var data = new { systemip = Objquery.Get_syatemname_ip(Objquery.Get_ParentOu_id(ouid)), usernamefirewall = Objquery.Get_usernamefirewall(Objquery.Get_ParentOu_id(ouid)), username = Objquery.Get_Firewallusername(Objquery.Get_ParentOu_id(ouid)), exception = Objquery.Get_FWException(Objquery.Get_ParentOu_id(ouid)), SSusername = Objquery.Get_usernameouwise(Objquery.Get_ParentOu_id(ouid)) };
            return Json(data);
        }
        public JsonResult Get_syatemname_ip_file_enum(string ouid, string devicename, string ipaddress)
        {
            string strCond = "";
            if (ipaddress != null)
                strCond += " and dl.ip like '%" + ipaddress + "%'";
            if (devicename != null && devicename.Trim() != "")
                strCond += " and dl.device_name like '%" + devicename.Trim() + "%'";
            var data = new { systemip = Objquery.Get_syatemname_ip(Objquery.Get_ParentOu_id(ouid), strCond) };
            return Json(data);
        }

        public JsonResult Get_syatemname_ip_PresentlyLinked(string ouid)
        {
            var data = new { systemip = Objquery.Get_syatemname_ip_PresentlyLinked(Objquery.Get_ParentOu_id(ouid)) };
            return Json(data);
        }

        public JsonResult Get_syatemname_ip_PresentlyNotLinked(string ouid)
        {
            var data = new { systemip = Objquery.Get_syatemname_ip_PresentlyNotLinked(Objquery.Get_ParentOu_id(ouid)) };
            return Json(data);
        }

        public JsonResult Get_systemname_ip_NotMonitoring()
        {
            var data = new { systemip = Objquery.Get_systemname_ip_NotMonitoring() };
            return Json(data);
        }
        public JsonResult Get_syatem_Data(string startdate, string enddate, string ip, string system, string ouid)
        {
            try
            {
                string Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                string Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";

                string search = "";
                if (ip != null)
                {
                    search += " and d.ip like '%" + ip + "%'";
                }

                if (system != null && system != "-1")
                {

                    search += " and d.device_name like '%" + system + "%'";
                }

                if (ouid != null)
                {
                    if (ouid != "-1")
                        //search += " and d.ou_id ='" + ouid + "' ";
                        search += " and d.ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                dt = Objquery.Get_syatem_Data(Sdate, Edate, search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Get_syatem_Data Exception : " + ex.Message.ToString());
            }
            var data = new { syatem_data = dt };

            return Json(data);
        }
        public JsonResult Get_syatem_Device_Data(string startdate, string enddate, string deviceid, string media, string searchtype, string search)
        {
            try
            {
                string Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                string Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                string strmedia = "";
                string searching = "";
                if (media != null)
                {
                    if (media != "-1")
                        strmedia = "and type = " + media;
                }

                if (searchtype != null || search != null)
                {
                    searching = Objquery.datalekage_Serching(searchtype, search);

                }


                dt = Objquery.Get_syatem_datalekage_Data(Sdate, Edate, deviceid, strmedia, searching);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Get_syatem_Device_Data Exception : " + ex.Message.ToString());
            }
            var data = new { datalekage_data = dt };

            return Json(data);
        }
        public JsonResult GetRawDataLeakage(string startdate, string enddate, string IP, string Device, string Media, string ouid, string FieldSerach, string ValueSerach)
        {
            string GetRawData = string.Empty;
            string Sdate = "";
            string Edate = "";
            if (startdate != null & enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                string search = "";
                string strFieldSearch = "";
                if (IP != null)
                {
                    if (IP != "-1")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null && Device != "Select System Name")
                {
                    search += " and dl.device_name like '%" + Device + "%'";
                }

                if (ouid != null)
                {
                    if (ouid != "-1")
                        //search += " and dl.ou_id ='" + ouid + "' ";
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (Media != null)
                {
                    if (Media != "-1")
                        search += " and type='" + Media + "' ";
                }
                if (FieldSerach != null && ValueSerach != null)
                {

                    strFieldSearch = Objquery.datalekage_Serching(FieldSerach, ValueSerach.Trim().ToString());

                }
                dt = Objquery.GetRawDataLeakage(Sdate, Edate, search, strFieldSearch);


            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetRawDataLeakage Exception : " + ex.Message.ToString());
            }
            var data = new { Rawdata = dt };
            return Json(data);
        }
        public JsonResult GetDataLeakageChartDetails(string startdate, string enddate, string ChartType, string ChartClickValue)
        {
            string GetRawData = string.Empty;
            string Sdate = "";
            string Edate = "";
            if (startdate != null & enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                if (ChartType == "FileStatusSummary")
                {
                    string type = "1";
                    if (ChartClickValue.ToUpper() == "CREATE")
                        type = "1";
                    else if (ChartClickValue.ToUpper() == "CHANGE")
                        type = "2";
                    else if (ChartClickValue.ToUpper() == "RENAME")
                        type = "3";
                    else if (ChartClickValue.ToUpper() == "DELETE")
                        type = "4";
                    search = " and type = " + type;
                    dt = Objquery.GetDataLeakageChartDetails(Sdate, Edate, search);
                }
                else if (ChartType == "BranchUnitSummary")
                {
                    search = " and ou_node_name = '" + ChartClickValue + "'";
                    dt = Objquery.GetDataLeakageChartDetails(Sdate, Edate, search);
                }
                else if (ChartType == "IPSummary")
                {
                    search = " and dl.ip = '" + ChartClickValue + "'";
                    dt = Objquery.GetDataLeakageChartDetails(Sdate, Edate, search);
                }
                else if (ChartType == "FileTypeSummary")
                {
                    if (ChartClickValue.ToUpper() == "AUDIO/VIDEO")
                        search = " and (source_path like '%.mp3' or source_path like '%.mp4' or source_path like '%.wav' or source_path like '%.mkv')";
                    else if (ChartClickValue.ToUpper() == "AUDIO/VIDEO")
                        search = " ";
                    else if (ChartClickValue.ToUpper() == "MS-OFFICE")
                        search = " and (source_path like '%.doc' or source_path like '%.docx' or source_path like '%.xlsx' or source_path like '%.xls' or source_path like '%.ppt' or source_path like '%.xps' or source_path like '%.mdb' or source_path like '%.accdb' )";
                    else if (ChartClickValue.ToUpper() == "PDF")
                        search = " and (source_path like '%.pdf' )";
                    else if (ChartClickValue.ToUpper() == "EXE/MSI")
                        search = " and (source_path like '%.exe' or source_path like '%.msi')";
                    else if (ChartClickValue.ToUpper() == "RAR/ZIP")
                        search = " and (source_path like '%.rar' or source_path like '%.zip' )  ";
                    else if (ChartClickValue.ToUpper() == "OTHER")
                        search = " and (source_path not like '%.rar' and source_path not like '%.zip') and ( source_path not like '%.exe' and source_path not like '%.msi') and (source_path not like '%.pdf' ) and (source_path not like '%.doc' and source_path not like '%.docx' and source_path not like '%.xlsx' and source_path not like '%.xls'  and source_path not like '%.ppt' and source_path not like '%.xps' and source_path not like '%.mdb' and source_path not like '%.accdb' )  and (source_path not like '%.mp3' and source_path not like '%.mp4' and source_path not like '%.wav')";
                    dt = Objquery.GetDataLeakageChartDetails(Sdate, Edate, search);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetDataLeakageChartDetails Exception : " + ex.Message.ToString());
            }
            var data = new { chartDetailsdata = dt };
            return Json(data);
        }
        public JsonResult GetRemovablemedia(string startdate, string enddate, string IP, string Device, string ouid, string FieldSerach, string ValueSerach)
        {
            string GetRawData = string.Empty;
            string Sdate = "";
            string Edate = "";
            if (startdate != null & enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                string search = "";
                string strFieldSearch = "";
                if (IP != null)
                {

                    if (IP != "-1")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like'%" + Device + "%'";
                }

                if (ouid != null)
                {
                    if (ouid != "-1")
                        // search += " and dl.ou_id ='" + ouid + "' ";
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (FieldSerach != null && ValueSerach != null)
                {
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";
                    //  strFieldSearch = Objquery.datalekage_Serching(FieldSerach, ValueSerach.Trim().ToString());

                }
                dt = Objquery.GetRemovablemedia(Sdate, Edate, search, strFieldSearch);


            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetRemovablemedia Exception : " + ex.Message.ToString());
            }
            var data = new { Removabledata = dt };
            return Json(data);
        }
        public JsonResult GetPrinterUsageReportPrinter()
        {

            string GetPrinterData = string.Empty;

            try
            {
                dt = Objquery.Get_Printer_Name();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetPrinterUsageReportPrinter Exception : " + ex.Message.ToString());
            }
            var data = new { PrinterUsageName = dt };
            return Json(data);
        }
        public JsonResult GetPrinterUsageReport(string startdate, string enddate, string IP, string Device, string Printer, string ouid, string FieldSerach, string ValueSerach)
        {

            string GetPrinterData = string.Empty;
            string Sdate = "";
            string Edate = "";
            Int32 intTotalPages = 0;
            if (startdate != null & enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                string search = "";
                string strFieldSearch = "";

                //if (IP != null)
                //{

                //    if (IP != "-1")
                //        search += " and ip like '%" + IP + "%'";
                //}

                //if (Device != null && Device != "-1")
                //{
                //    if (Device != "-1")
                //        search += " and dl.device_name like'%" + Device + "%'";
                //}
                //if (Printer != null)
                //{
                //    if (Printer != "-1")
                //        search += " and printer_name ='" + Printer + "'";
                //}
                //if (ouid != null)
                //{
                //    if (ouid != "-1")
                //        //search += " and dl.ou_id ='" + ouid + "' ";
                //        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                //}
                if (IP != null)
                {

                    if (IP != "-1")
                        search += " and ip_address like '%" + IP + "%'";
                }

                if (Device != null && Device != "-1")
                {
                    if (Device != "-1")
                        search += " and device_name like'%" + Device + "%'";
                }
                if (Printer != null)
                {
                    if (Printer != "-1")
                        search += " and printer_name ='" + Printer + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        //search += " and dl.ou_id ='" + ouid + "' ";
                        search += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (FieldSerach != null && ValueSerach != null)
                {
                    strFieldSearch = Objquery.Printer_Searching(FieldSerach, ValueSerach.Trim().ToString());
                }
                //dt = Objquery.GetPrinterUsage(Sdate, Edate, search, strFieldSearch);
                dt = Objquery.GetPrinterUsageNew(Sdate, Edate, search, strFieldSearch);
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    intTotalPages = intTotalPages + Convert.ToInt32(dt.Rows[i]["total_pages"]);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetPrinterUsageReport Exception : " + ex.Message.ToString());
            }
            var data = new { PrinterUsageData = dt, TotalPages = intTotalPages };
            return Json(data);
        }
        public IActionResult agent_report()
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
        public IActionResult usage_report()
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
        public IActionResult audit_report()
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
        public JsonResult audit_report_data(string systemname, string ip)
        {
            string strserch = "";
            string strscreensaver1 = "";
            int nooflancard1 = 0;
            DataTable dtos = new DataTable();
            string strav = "";
            string unwantedsoftware = "";
            string strdomain = "";
            string strsccm = "";
            string strfirewall = "";
            DataTable dtsdesk = new DataTable();
            DataTable dtVcrypt = new DataTable();
            //string strsdesk = "";
            //string strVcrypt = "";
            string fshare = "";
            string dshare = "";
            DataTable dtservices = new DataTable();
            string strpasswordpolicy = "";
            int struserAc = 0;
            string strguestuser = "";
            string stradminrename = "";
            string strctrlaltdelete = "";
            string strusageadmin = "";
            string strusbport = "";
            string strWireless = "";
            string strusbmass = "";
            string strusbmassuseddate = "";
            string strinternetdongle = "";
            string strinternetdongleuseddate = "";
            string strunwanteddata = "";
            Int64 printercount = 0;
            string strusbstatus = "";
            string strcdromwriter = "";
            string strprinter = "";
            string deviceid = "";
            string auditeby = "";
            string strsystemdatetime = "";
            string strmachinesr_no = "";
            string strpcmodel = "";
            string stripv6 = "";
            string stros_lastupdated = "";
            string strpirated_os = "";
            string strnsg_ip = "";
            string strwin_psw = "";
            string stracc_lockout_policy = "";
            string straudit_policy_impl = "";
            string strdisplay_last_username = "";
            string strmobile_phone = "";
            string strmobile_date = "";
            string strnsg_pc_internet = "";
            //  var data = new { null };
            if (systemname == "-1" && ip == "-1")
            {
                string data1 = "";
                return Json(data1);
            }

            if (systemname != "-1" && ip != "-1")
            {
                if (Objquery.Get_device_idby_systemname(systemname) == Objquery.Get_device_id("ip='" + ip + "'"))
                {
                    deviceid = Objquery.Get_device_id("ip='" + ip + "'");
                    //deviceid = Objquery.Get_device_id("ip='" + ip + "'");
                    strserch = " and dm.device_id ='" + deviceid + "'";
                }
                else
                {
                    //var   data = new { Systemdetails = "", Strscreensaver = null, nooflancard = "", Os = "", Av = "", Software = "", Domain = "", Sccm = "", Firewall = "", Sdesk = "", Vcrypt = "", Fshare = "", Dshare = "", Service = "", Passwordpolicy = "", Userac = "", Guestuser = "", Adminrename = "", Usageadmin = "", Usbport = "", Wireless = "", Usbmass = "", Usbmassuseddate = "", Internetdongle = "", Internetdongleuseddate = "", Unwanteddata = "", Printer = "", usbstatus = "", cdromwriter = "", printerstatus = "" };
                    string data1 = "";
                    return Json(data1);
                }
            }
            if (systemname != "-1")
            {
                deviceid = Objquery.Get_device_idby_systemname(systemname);
                strserch = " and dm.device_id ='" + deviceid + "'";
            }
            if (ip != "-1")
            {
                deviceid = Objquery.Get_device_id("ip='" + ip + "'");
                strserch = " and dm.device_id ='" + deviceid + "'";
            }
            // if (deviceid != "-1" && deviceid != null)
            // strserch = " and dm.device_id ='" + deviceid + "'";
            //else if (ip != "-1" && ip != null)
            //strserch = " and dm.device_id ='" + ip +"'";

            if (strserch != "")
            {
                dt = Objquery.Get_Audit_System_details(strserch);
                strscreensaver1 = Objquery.Get_ScreenSaver_Password(deviceid);
                nooflancard1 = Objquery.Get_NoOf_Lancard(deviceid);
                dtos = Objquery.Get_Os_Details(deviceid);
                strav = Objquery.Get_AV_Details(deviceid);
                unwantedsoftware = Objquery.Get_Unwanted_Software(deviceid);
                strdomain = Objquery.Get_ActiveDirectory_Domain(deviceid);
                strsccm = Objquery.Get_SCCM_Details(deviceid);
                strfirewall = Objquery.Get_Firewall_Status(deviceid);
                dtsdesk = Objquery.Get_Encyption_Sdesk(deviceid);
                dtVcrypt = Objquery.Get_Encyption_VCrypt(deviceid);
                fshare = Objquery.Get_Sharing_Folder(deviceid);
                dshare = Objquery.Get_Sharing_Default(deviceid);
                dtservices = Objquery.Get_Audit_Service_Details(deviceid);
                strpasswordpolicy = Objquery.Get_Password_Policy(deviceid);
                struserAc = Objquery.Get_NoOf_UserAc(deviceid);
                strguestuser = Objquery.Get_Guest_User_Status(deviceid);
                stradminrename = Objquery.Get_Administarator_renamed(deviceid);
                strctrlaltdelete = Objquery.Get_ctrl_alt_delete(deviceid);
                strusageadmin = Objquery.Get_Usage_of_Admin(deviceid);
                strusbport = Objquery.Get_USBPort_Status(deviceid);
                strWireless = Objquery.Get_Wireless_Status(deviceid);
                strusbmass = Objquery.Get_USBmass_Status(deviceid);
                strusbmassuseddate = Objquery.Get_USBmass_UsedDate(deviceid);
                strinternetdongle = Objquery.Get_InternetDongal_Status(deviceid);
                strinternetdongleuseddate = Objquery.Get_InternetDongal_Useddate(deviceid);
                strunwanteddata = Objquery.Get_Unwanted_Data(deviceid);
                printercount = Objquery.Get_PrinterData(deviceid);
                strusbstatus = Objquery.Get_USB_Status(deviceid);
                strcdromwriter = Objquery.Get_CDRom_Writer(deviceid);
                strprinter = Objquery.Get_Printerstatus(deviceid);
                auditeby = Configuration.username;
                strsystemdatetime = Objquery.Get_SystemDateTime(deviceid);
                strmachinesr_no = Objquery.Get_MotherBoard_Serialno(deviceid);
                strpcmodel = Objquery.Get_Computer_Model(deviceid);
                stripv6 = Objquery.Get_IPV6_Disabled(deviceid);
                stros_lastupdated = Objquery.Get_OS_LastUpdated(deviceid);
                strpirated_os = Objquery.Get_Pirated_OS(deviceid);
                strnsg_ip = Objquery.Get_NSG_IP(deviceid);
                strwin_psw = Objquery.Get_Win_PSW(deviceid);
                stracc_lockout_policy = Objquery.Get_Acconut_lockout_policy(deviceid);
                straudit_policy_impl = Objquery.Get_Audit_Policy(deviceid);
                strdisplay_last_username = Objquery.Get_Display_last_UserName(deviceid);
                strmobile_phone = Objquery.Get_Mobile_Phone(deviceid);
                strmobile_date = Objquery.Get_Mobile_Date(deviceid);
                strnsg_pc_internet = Objquery.Get_PC_Internet(deviceid);
            }

            //var data = new { Systemdetails  = dt,Strscreensaver= strscreensaver1, nooflancard=nooflancard1,Os= dtos,Av= strav,Software= unwantedsoftware,Domain= strdomain,Sccm= strsccm,Firewall= strfirewall,Sdesk= strsdesk,Vcrypt= strVcrypt,Fshare= fshare,Dshare= dshare,Service= dtservices,Passwordpolicy = strpasswordpolicy,Userac= struserAc, Guestuser=strguestuser, Adminrename= stradminrename, Usageadmin=strusageadmin, Usbport=strusbport, Wireless=strWireless, Usbmass=strusbmass, Usbmassuseddate= strusbmassuseddate, Internetdongle=strinternetdongle, Internetdongleuseddate=strinternetdongleuseddate,Unwanteddata=strunwanteddata,Printer= printercount, usbstatus=strusbstatus, cdromwriter=strcdromwriter,printer= strprinter };
            var data = new { nsg_pc_internet = strnsg_pc_internet, mobile_date = strmobile_date, mobile_phone = strmobile_phone, display_last_username = strdisplay_last_username, audit_policy_impl = straudit_policy_impl, acc_lockout_policy = stracc_lockout_policy, win_psw = strwin_psw, nsg_ip = strnsg_ip, pirated_os = strpirated_os, os_lastupdated = stros_lastupdated, ipv6 = stripv6, auditby = auditeby, Systemdetails = dt, Strscreensaver = strscreensaver1, nooflancard = nooflancard1, Os = dtos, Av = strav, Software = unwantedsoftware, Domain = strdomain, Sccm = strsccm, Firewall = strfirewall, Sdesk = dtsdesk, Vcrypt = dtVcrypt, Fshare = fshare, Dshare = dshare, Service = dtservices, Passwordpolicy = strpasswordpolicy, Userac = struserAc, Guestuser = strguestuser, Adminrename = stradminrename, Ctrlaltdeletedata = strctrlaltdelete, Usageadmin = strusageadmin, Usbport = strusbport, Wireless = strWireless, Usbmass = strusbmass, Usbmassuseddate = strusbmassuseddate, Internetdongle = strinternetdongle, Internetdongleuseddate = strinternetdongleuseddate, Unwanteddata = strunwanteddata, Printer = printercount, usbstatus = strusbstatus, cdromwriter = strcdromwriter, printerstatus = strprinter, systemdatetime = strsystemdatetime, machinesr_no = strmachinesr_no, pcmodel = strpcmodel };


            return Json(data);
        }
        public JsonResult get_audit_report_data(string systemname, string ip, string macaddress)
        {
            string strserch = "";
            string deviceid = "";
            string auditedby = "";
            string pdflogo_left = OwnYITConstant.PDF_LOGO_PATH_LEFT;
            string pdflogo_right = OwnYITConstant.PDF_LOGO_PATH_RIGHT;
            try
            {

                if (systemname != "-1" && systemname != null)
                {
                    deviceid = Objquery.Get_device_idby_systemname(systemname);
                    strserch = " where (device_name like'%" + systemname + "%' or device_id = '" + deviceid + "') ";
                }
                if (ip != "-1")
                {
                    if (strserch.Length > 0)
                        strserch += " and ip_address like '%" + ip + "%'";
                    else
                        strserch += " where ip_address like '%" + ip + "%'";
                }
                if (macaddress != "" && macaddress != null)
                {
                    if (strserch.Length > 0)
                        strserch += " and mac_address like'%" + macaddress + "%'";
                    else
                        strserch += " where mac_address like'%" + macaddress + "%'";
                }
                auditedby = Configuration.username;
                if (strserch.Trim().Length <= 0)
                {
                    strserch = "where device_id=0";
                }
                dt = Objquery.get_audit_report_data(strserch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_audit_report_data Exception : " + ex.Message.ToString());
            }
            var data = new { audit_report = dt, auditby = auditedby, pdf_logo_left = pdflogo_left, pdf_logo_right = pdflogo_right };
            return Json(data);
        }
        public IActionResult report_modem_usage()
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
        public JsonResult Get_modem_Report(string systemname, string ip, string fieldserach, string serachvalue, string ouid)
        {
            string strsearch = "";
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        strsearch += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (systemname != "-1")
                {
                    strsearch += " and dl.device_name like '%" + systemname + "%'";
                    //strsearch = " and dl.device_name ='" + systemname + "'";
                }

                if (ip != "-1")
                {

                    strsearch += " and dl.ip like '%" + ip + "%'";
                }

                if (fieldserach != "-1" && serachvalue != "-1")
                {

                    strsearch += Objquery.Get_modemdata_serching(fieldserach, serachvalue);
                }


                dt = Objquery.Get_ModemUsage_Data(strsearch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Get_modem_Report Exception : " + ex.Message.ToString());
            }
            var data = new { Modemusagedata = dt };
            return Json(data);
        }
        //public IActionResult report_agent_installed()
        //{
        //    ViewData["InstalledAgentData"] = Objquery.Get_AgentInstaledData();
        //    return View();
        //}

        public IActionResult report_agent_information()
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

        // Agent Information Report

        public JsonResult GetAgentInformationReport(string IP, string Device, string OS, string ouid, string status, string lastpolldays, string FieldSerach, string ValueSerach)
        {
            LoadData loaddata = new LoadData();
            string GetAgentInfoData = string.Empty;
            try
            {
                string search = "";
                string strFieldSearch = "";

                if (IP != null)
                {

                    if (IP != "-1")
                        search += " and T.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and T.device_name like '%" + Device + "%'";
                    //search += " and T.device_name ='" + Device + "'";
                }
                if (OS != null)
                {
                    if (OS != "-1")
                        //search += " and ng.os ='" + OS + "'";
                        search += " and nsd.parameter_value ='" + OS + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and T.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (status != null && status != "all" && status != "undefined")
                {
                    if (status == "1")
                        search += " and T.status ='1'";
                    else
                        search += " and T.status IN (0,2)";
                }
                if (lastpolldays != null && lastpolldays != "-1")
                {
                    search += Objquery.Agent_info_Date(lastpolldays);
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";
                }

                dt = Objquery.Get_AgentInfo(search, strFieldSearch);
                loaddata = objcommon.DatatableToObject(dt);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetAgentInformationReport Exception : " + ex.Message.ToString());
            }
            //var data = new { Getagentinfodata = dt };
            return Json(loaddata);
        }

        public IActionResult report_audit_trail()
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
        public JsonResult Getaudittrail(string startdate, string enddate, string ouid, string FieldSerach, string ValueSerach, string audittype)
        {
            string Sdate = "";
            string Edate = "";
            string search = "";
            string serachdate = "";
            string strFieldSearch = "";
            try
            {
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    serachdate = " and ServerTime >= '" + Sdate + "' and ServerTime <= '" + Edate + "'";
                    //serachdate = " and  device_timestamp >= '" + Sdate + "' and device_timestamp <= '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    //strFieldSearch = Objquery.audittrail_Searching(FieldSerach, ValueSerach.Trim().ToString());
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";
                }
                if (audittype == "Category Wise")
                {
                    dt = Objquery.Get_audittrail(search, serachdate, strFieldSearch);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getaudittrail Exception : " + ex.Message.ToString());
            }
            var data = new { Getaudittraildata = dt };
            return Json(data);
        }
        public JsonResult Getaudittrail_syswise(string startdate, string enddate, string ouid, string FieldSerach, string ValueSerach, string audittype, string system, string ip)
        {
            string Sdate = "";
            string Edate = "";
            string search = "";
            string serachdate = "";
            try
            {
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    serachdate = " and ServerTime >= '" + Sdate + "' and ServerTime <= '" + Edate + "'";
                    //serachdate = " and  device_timestamp >= '" + Sdate + "' and device_timestamp <= '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (ip != null)
                {
                    if (ip != "")
                        search += " and dl.ip like '%" + ip + "%'";
                }
                if (system != null)
                {
                    if (system != "-1")
                        search += " and dl.device_name like '%" + system + "%'";
                    //search += " and dl.device_name ='" + system + "'";
                }
                if (audittype == "System Wise")
                {
                    dt = Objquery.Get_audittrail_syswise(search, serachdate);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getaudittrail_syswise Exception : " + ex.Message.ToString());
            }
            var data = new { Getaudittraildata = dt };
            return Json(data);
        }
        public JsonResult Getaudittrailsummary(string startdate, string enddate, string ouid, string type, string subtype, string status)
        {
            OwnYITConstant.DT_auditraildata = null;
            string Sdate = "";
            string Edate = "";
            string serachdate = "";
            if (startdate != null & enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                serachdate = " and  ServerTime >= '" + Sdate + "' and ServerTime <= '" + Edate + "'";
            }
            //if (startdate != null && enddate != null)
            //{
            //    serachdate = " and  device_timestamp >= '" + Sdate + "' and device_timestamp <= '" + Edate + "'";
            //}
            try
            {
                //   string strFieldSearch = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        ouid = " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                OwnYITConstant.DT_auditraildata = Objquery.Get_audittrailsummary(serachdate, type, subtype, status, ouid, "");
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getaudittrailsummary Exception : " + ex.Message.ToString());
            }
            var data = new { Getaudittrailsummarydata = OwnYITConstant.DT_auditraildata };
            return Json(data);
        }
        public JsonResult Getaudittrailsummary_syswise(string startdate, string enddate, string ouid, string device_ids)
        {
            OwnYITConstant.DT_auditraildata_syswise = null;
            string Sdate = "";
            string Edate = "";
            string serachdate = "";
            try
            {
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    serachdate = " and  ServerTime >= '" + Sdate + "' and ServerTime <= '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        ouid = " and ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                OwnYITConstant.DT_auditraildata_syswise = Objquery.Get_audittrailsummary_syswise(serachdate, ouid, device_ids, "");
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getaudittrailsummary_syswise Exception : " + ex.Message.ToString());
            }
            var data = new { Getaudittrailsummary_syswisedata = OwnYITConstant.DT_auditraildata_syswise };
            return Json(data);
        }
        public JsonResult Getaudittrailsummaryfilter(string ip, string device, string FieldSerach, string ValueSerach)
        {
            DataTable dttemp = new DataTable();
            try
            {
                string search = "";
                if (ip == null && device == "-1")
                    dt = OwnYITConstant.DT_auditraildata;

                if (ip != null)
                {
                    if (ip != "-1")
                    {
                        search = "IpAddress like '%" + ip + "%'";
                    }
                }

                if (device != null && device != "-1")
                {
                    if (device != "-1")
                    {
                        if (search == "")
                            search += "DeviceName like'%" + device + "%'";
                        else
                            search += " and DeviceName like'%" + device + "%'";
                    }
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    if (search == "")
                        search += FieldSerach + " like '%" + ValueSerach + "%'";
                    else
                        search += " and " + FieldSerach + " like '%" + ValueSerach + "%'";

                }
                foreach (DataRow dr in OwnYITConstant.DT_auditraildata.Select(search))
                {
                    if (dttemp.Columns.Count == 0)
                    {
                        dttemp = OwnYITConstant.DT_auditraildata.Clone();
                        dttemp.Rows.Clear();
                    }
                    dttemp.Rows.Add(dr.ItemArray);
                }
                dt = dttemp;
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getaudittrailsummaryfilter Exception : " + ex.Message.ToString());
            }
            var data = new { Getaudittrailsummarydata = dt };
            return Json(data);
        }
        public JsonResult Getaudittrailsummaryfilter_syswise(string ip, string device, string FieldSerach, string ValueSerach)
        {
            DataTable dttemp = new DataTable();
            try
            {
                string search = "";
                if (ip == null && device == "-1" && ip == "null")
                    dt = OwnYITConstant.DT_auditraildata_syswise;

                if (ip != null && ip != "null")
                {
                    if (ip != "-1")
                    {
                        search = "IpAddress like '%" + ip + "%'";
                    }
                }

                //if (device != null && device != "-1")
                //{
                //    if (device != "-1")
                //    {
                //        if (search == "")
                //            search += "DeviceName like'%" + device + "%'";
                //        else
                //            search += " and DeviceName like'%" + device + "%'";
                //    }
                //}
                if (FieldSerach != null && ValueSerach != null)
                {
                    if (search == "")
                        search += FieldSerach + " like '%" + ValueSerach + "%'";
                    else
                        search += " and " + FieldSerach + " like '%" + ValueSerach + "%'";

                }
                foreach (DataRow dr in OwnYITConstant.DT_auditraildata_syswise.Select(search))
                {
                    if (dttemp.Columns.Count == 0)
                    {
                        dttemp = OwnYITConstant.DT_auditraildata_syswise.Clone();
                        dttemp.Rows.Clear();
                    }
                    dttemp.Rows.Add(dr.ItemArray);
                }
                dt = dttemp;
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getaudittrailsummaryfilter_syswise Exception : " + ex.Message.ToString());
            }
            var data = new { Getaudittrailsummary_syswisedata = dt };
            return Json(data);
        }

        public IActionResult report_system_firewall_status()
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

        // System Firewall Status
        public JsonResult GetSysFWStatus(string startdate, string enddate, string IP, string Device, string ouid, string username)
        {
            string Sdate = "";
            string Edate = "";
            string search = "";
            string serachdate = "";
            //if (startdate != null & enddate != null)
            //{
            //    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
            //    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
            //    serachdate = " and  dl.datetime >= '" + Sdate + "' and dl.datetime <= '" + Edate + "'";
            //}
            try
            {
                if (IP != null)
                {
                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like '%" + Device + "%'";
                    //search += " and dl.device_name ='" + Device + "'";
                }
                if (username != null)
                {
                    if (username != "-1")
                        search += " and login_user ='" + username + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                dt = Objquery.Get_SysFWStatus(serachdate, search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetSysFWStatus Exception : " + ex.Message.ToString());
            }
            var data = new { sysfirewallstatus = dt };
            return Json(data);
        }
        // System Firewall Exception 
        public JsonResult GetSysFWException(string IP, string Device, string ouid, string exception)
        {

            try
            {
                string search = "";
                //  string strFieldSearch = "";
                if (IP != null)
                {
                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null && Device != "-1")
                {
                    if (Device != "-1")
                        search += " and dl.device_name like'%" + Device + "%'";
                }
                if (exception != null)
                {
                    if (exception != "-1")
                        search += " and c153exceptionentity ='" + exception + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                dt = Objquery.Get_SysFWException(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetSysFWException Exception : " + ex.Message.ToString());
            }
            var data = new { sysfirewallexception = dt };
            return Json(data);
        }

        // Screen saver Password
        public JsonResult GetScreenSaverpwd(string IP, string Device, string ouid, string username, string status)
        {

            try
            {
                string search = "";
                //  string strFieldSearch = "";
                if (IP != null)
                {
                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null && Device != "-1")
                {
                    if (Device != "-1")
                        search += " and dl.device_name like'%" + Device + "%'";
                }
                if (username != null)
                {
                    if (username != "-1")
                        search += " and nui.login_user ='" + username + "'";
                }
                if (status != null)
                {
                    if (status != "-1")
                    {
                        if (status == "All")
                            search += " and T.pswstatus in('Yes','No')";
                        if (status == "Yes")
                            search += " and T.pswstatus ='" + status + "'";
                        if (status == "No")
                            search += " and T.pswstatus ='" + status + "'";
                    }
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                dt = Objquery.Get_ScreensaverPwd(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetScreenSaverpwd Exception : " + ex.Message.ToString());
            }
            var data = new { screensaverpwd = dt };
            return Json(data);
        }

        public IActionResult report_user_information()
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
        public JsonResult GetUserinformation(string IP, string Device, string ouid, string status)
        {
            try
            {
                string search = "";

                if (IP != null)
                {
                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like '%" + Device + "%'";
                    //search += " and dl.device_name ='" + Device + "'";
                }

                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (status != null)
                {
                    if (status != "2")
                        search += " and disabled ='" + status + "'";
                }
                dt = Objquery.Get_User_information(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetUserinformation Exception : " + ex.Message.ToString());
            }
            var data = new { userdata = dt };
            return Json(data);
        }
        public IActionResult report_shared_Resources()
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
        public JsonResult Get_Share_information(string IP, string Device, string ouid, string searchname, string svalue)
        {
            try
            {
                string search = "";

                if (IP != null)
                {
                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like '%" + Device + "%'";
                    //search += " and dl.device_name ='" + Device + "'";
                }

                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (searchname != "-1" && svalue != null)
                {
                    search += " and " + searchname + " like '%" + svalue + "%'";
                }
                dt = Objquery.Get_Share_information(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Get_Share_information Exception : " + ex.Message.ToString());
            }
            var data = new { sharedata = dt };
            return Json(data);


        }
        public IActionResult report_login_logoff_timetrack()
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
        // User login logoff track report 
        public JsonResult GetuserloginlogoffReport(string startdate, string enddate, string IP, string Device, string actiontype, string ouid, string FieldSerach, string ValueSerach)
        {
            string Getusertrackinfodata = string.Empty;
            string search = "";
            string Sdate = "";
            string Edate = "";
            try
            {
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    search = " and np.start_timestamp >= '" + Sdate + "' and np.start_timestamp <= '" + Edate + "'";
                }
                if (IP != null)
                {
                    if (IP != "-1")
                        search += " and dl.ip like '%" + IP + "%'";
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like '%" + Device + "%'";
                    //search += " and dl.device_name ='" + Device + "'";
                }
                if (actiontype != null && actiontype != "All")
                {
                    if (actiontype != "-1")
                        search += " and np.subtype ='" + actiontype + "'";
                }
                if (actiontype == "All")
                {
                    search += " and np.subtype in ('Startup','Login','Lock','Turn Off Monitor','Hibernate','Stand By')";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    search += " and " + FieldSerach + " like '%" + ValueSerach + "%'";
                }
                dt = Objquery.Get_userloginlogoffReport(Sdate, Edate, search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetuserloginlogoffReport Exception : " + ex.Message.ToString());
            }
            var data = new { Getusertrackinfodata = dt };
            return Json(data);
        }

        public IActionResult report_port_information()
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
        public JsonResult Get_Port_information(string IP, string Device, string ouid, string searching, string value, string startdate, string enddate)
        {
            LoadData loaddata = new LoadData();
            try
            {
                string search = "";
                string Sdate = "";
                string Edate = "";
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    //search = " and  dl.datetime >= '" + Sdate + "' and dl.datetime <= '" + Edate + "'";
                    search = "and ((nc.start_time >= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Sdate + "')) and nc.start_time <= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Edate + "'))) or (nc.end_time >= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Sdate + "')) and nc.end_time <= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Edate + "')))) ";
                }
                if (IP != null)
                {
                    if (IP != "-1")
                        search += " and dl.ip like '%" + IP + "%'";
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like '%" + Device + "%'";
                    //search += " and dl.device_name ='" + Device + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (searching != "-1" && value != null)
                {
                    search += " and " + searching + " like '%" + value + "%'";
                }
                dt = Objquery.Get_portinfoReport(Sdate, Edate, search);
                loaddata = objcommon.DatatableToObject(dt);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Get_Port_information Exception : " + ex.Message.ToString());
            }
            //var data = new { portinfodata = dt };
            return Json(loaddata);


        }
        public JsonResult Get_Port_process_information(string IP, string Device, string ouid, string searching, string value, string startdate, string enddate)
        {
            LoadData loaddata = new LoadData();
            try
            {
                string search = "";
                string Sdate = "";
                string Edate = "";
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    //search = " and  dl.datetime >= '" + Sdate + "' and dl.datetime <= '" + Edate + "'";
                    search = "and ((nc.start_time >= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Sdate + "')) and nc.start_time <= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Edate + "'))) or (nc.end_time >= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Sdate + "')) and nc.end_time <= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Edate + "')))) ";
                }
                if (IP != null)
                {
                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and (dl.device_name like '%" + Device + "%' or dl.device_id like '%" + Device + "%')";
                    //search += " and dl.device_name ='" + Device + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (searching != "-1" && value != null)
                {
                    search += " and " + searching + " like '%" + value + "%'";
                }
                dt = Objquery.Get_port_Porcess_infoReport(Sdate, Edate, search);
                //loaddata = objcommon.DatatableToObject(dt);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Get_Port_process_information Exception : " + ex.Message.ToString());
            }
            var data = new { portprocessinfodata = dt };
            //return Json(loaddata);
            return Json(data);

        }
        public IActionResult report_service_details()
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
        public JsonResult GetservicedetailsReport(string IP, string Device, string ouid, string FieldSerach, string ValueSerach)
        {
            LoadData loaddata = new LoadData();
            string Getservicedetailsdata = string.Empty;
            try
            {
                string search = "";
                //   string strFieldSearch = "";

                if (IP != null)
                {

                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like '%" + Device + "%'";
                    //search += " and dl.device_name ='" + Device + "'";
                }

                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (FieldSerach != null && ValueSerach != null)
                {
                    search += " and " + FieldSerach + " like '%" + ValueSerach + "%'";


                }

                dt = Objquery.Get_ServiceDetailReport(search);
                loaddata = objcommon.DatatableToObject(dt);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetservicedetailsReport Exception : " + ex.Message.ToString());
            }
            //var data = new { Getservicedetailsdata = dt };
            return Json(loaddata);
        }


        public JsonResult GetServicename()
        {
            var data = new { servicename = Objquery.Get_Servicename(), underwatchservicename = Objquery.Get_UnderwatchServicename() };
            return Json(data);
        }
        public JsonResult Insert_underwatchservice(string servicename)
        {
            int querystatus = 0;
            string result = "";
            int Rcount = 0;
            try
            {
                if (servicename != "")
                {
                    if (servicename != null)
                    {
                        Rcount = Objquery.Get_Service_count(servicename);
                        if (Rcount == 0)
                        {
                            querystatus = Objquery.insert_underwatchservice(servicename);
                            if (querystatus == 0)
                                result = "" + servicename + " inserted failed.";
                            else
                                result = "" + servicename + "  inserted successfully.";
                        }
                        else
                        {
                            result = "Service already exists.";
                        }
                    }
                    else
                    {
                        result = "Please select service";
                    }
                }
                else
                {
                    result = "Please select service";
                }

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Insert_underwatchservice Exception : " + ex.Message.ToString());

            }

            return Json(result);
        }


        public JsonResult delete_underwatchservicedata(string servicename)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.delete_underwatch_servicedata(servicename);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "delete_underwatchservicedata Exception : " + ex.Message.ToString());

            }
            if (querystatus == 0)
                result = "" + servicename + "  deleted failed.";
            else
                result = "" + servicename + "  deleted successfully.";
            return Json(result);
        }

        public JsonResult GetunderwatchservicedetailsReport(string IP, string Device, string ouid, string FieldSerach, string ValueSerach)
        {

            string Getunderwatchservicedetailsdata = string.Empty;
            try
            {
                string search = "";
                //   string strFieldSearch = "";

                if (IP != null)
                {

                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like'%" + Device + "%'";
                }

                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (FieldSerach != null && ValueSerach != null)
                {
                    search += " and " + FieldSerach + " like '%" + ValueSerach + "%'";


                }

                dt = Objquery.Get_underwatchServiceDetailReport(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetunderwatchservicedetailsReport Exception : " + ex.Message.ToString());
            }
            var data = new { Getunderwatchservicedetailsdata = dt };
            return Json(data);
        }
        public IActionResult report_asset_information()
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
        public IActionResult report_software_information()
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
        public JsonResult get_productkey_data(string ouid, string searching)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (searching != null)
                    search += " and dk.data1 like '%" + searching + "%'";

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_productkey_data Exception : " + ex.Message.ToString());
            }

            var data = new { productkeywise = Objquery.Get_product_key_data(search) };
            return Json(data);
        }
        public JsonResult get_productkey_cnt_data(string ouid, string stype, string searching, string pname, string ip, string system)
        {
            try
            {

                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and T.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (stype != null && searching != null)
                    search += " and " + stype + " like '%" + searching + "%'";

                if (pname != null)
                    search += " where T.data1 ='" + pname + "'";

                if (ip != null && ip != "")
                    search += " and T.ip like '%" + ip + "%'";

                if (system != null && system != "-1")
                    search += " and T.device_name ='" + system + "'";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_productkey_cnt_data Exception : " + ex.Message.ToString());
            }
            var data = new { productkeywise = Objquery.Get_product_key_cnt_data(search) };
            return Json(data);
        }

        public JsonResult get_productkey_Generate_data(string ouid, string stype, string searching, string generatetype, string pname, string ip, string system)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and T.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (pname != null)
                    if (generatetype == "Product")
                        search += " where T.data1 in('" + pname.Substring(1) + "')";
                    else if (generatetype == "System")
                        search += " where T.device_id in(" + pname.Substring(1) + ")";
                    //search += " and device_id in(" + pname.Substring(1) + ")";
                    //search += " where dl.device_id in(" + pname.Substring(1) + ")";
                    else
                        search += " where ns.data1 in('" + pname.Substring(1) + "')";
                if (ip != null && ip != "")
                    search += " and T.ip like '%" + ip + "%'";
                if (system != null && system != "-1")
                    search += " and T.device_name ='" + system + "'";
                if (stype != null && searching != null)
                    search += " and " + stype + " like '%" + searching + "%'";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_productkey_Generate_data Exception : " + ex.Message.ToString());
            }
            var data = new { productkeygenerate = Objquery.Get_product_key_generate_data(search, generatetype) };
            return Json(data);
        }
        public JsonResult get_productkey_system_data(string ouid, string ip, string system)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (ip != null && ip != "-1")
                    search += " and dl.ip like '%" + ip + "%'";

                if (system != null && system != "-1")
                    search += " and dl.device_name ='" + system + "'";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_productkey_system_data Exception : " + ex.Message.ToString());
            }
            var data = new { productkeysystemdata = Objquery.Get_product_key_system_wise(search) };
            return Json(data);
        }
        public JsonResult getou_device(string deviceid)
        {
            string ouid1 = "";
            try
            {
                if (deviceid != null)
                    ouid1 = Objquery.getouid_device(deviceid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "getou_device Exception : " + ex.Message.ToString());
            }
            var data = new { ouid = ouid1 };
            return Json(data);
        }
        public JsonResult get_productkey_system_count_details(string device_id, string serchtype, string value)
        {
            string search = "";
            try
            {
                if (serchtype != null && value != null)
                    search += " and " + serchtype + " like '%" + value + "%'";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_productkey_system_count_details Exception : " + ex.Message.ToString());
            }
            var data = new { productkeysystemdetails = Objquery.Get_product_system_deatils(device_id, search) };
            return Json(data);
        }

        public JsonResult get_productkey_key_data(string ouid, string ip, string system)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (ip != null && ip != "")
                    search += " and dl.ip like '%" + ip + "%'";

                if (system != null && system != "-1")
                    search += " and dl.device_name ='" + system + "'";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_productkey_key_data Exception : " + ex.Message.ToString());
            }
            var data = new { pkeydata = Objquery.Get_product_key_count(search) };
            return Json(data);
        }

        public JsonResult get_productkey_key_count(string keyid, string ouid, string ip, string system)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and T.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (keyid != null)
                    search += " where ns.data1 ='" + keyid + "'";
                if (ip != null && ip != "")
                    search += " and T.ip like '%" + ip + "%'";

                if (system != null && system != "-1")
                    search += " and T.device_name ='" + system + "'";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_productkey_key_count Exception : " + ex.Message.ToString());
            }
            var data = new { keyiddata = Objquery.Get_product_key_cnt_data(search) };

            //var data = new { keyiddata = Objquery.Get_product_key_count_data(keyid, search) };
            return Json(data);
        }
        //public JsonResult get_productkey_key_count_detail(string keyid, string ouid, string ip, string system)
        //{
        //    try
        //    {
        //        if (ouid != null)
        //        {
        //            if (ouid != "-1")
        //                search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
        //        }

        //        if (ip != null && ip != "-1")
        //            search += " and dl.ip ='" + ip + "'";

        //        if (system != null && system != "-1")
        //            search += " and dl.device_name ='" + system + "'";
        //    }
        //    catch (Exception ex)
        //    {
        //        objcommon.WriteLog("csat_reportController", "get_productkey_key_count_detail Exception : " + ex.Message.ToString());
        //    }
        //    var data = new { keyiddatadetails = Objquery.Get_product_key_count_data_details(keyid, search) };
        //    return Json(data);
        //}

        // Software information report

        public JsonResult Getsoftinsatlledcount(string ouid, string softsearch, string authsearch)
        {
            string Getsoftinsatlledcountdata = string.Empty;

            try
            {

                int devicecount = Objquery.Get_DeviceCount(Objquery.Get_ParentOu_id(ouid));

                string search = "";

                if (ouid != null)
                {
                    if (ouid != "-1" && ouid != null)
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (softsearch != null)
                {
                    search += " and software like '%" + softsearch + "%'";
                }
                if (authsearch != "undefined")
                {
                    search += " inner join authorizedata a on datatype=1 and authorizetype=" + authsearch + " and  software=typename";
                }
                dt = Objquery.Get_softinsatlledcount(devicecount, search);
                dt1 = Objquery.Get_softinsatlledcountsyswise(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getsoftinsatlledcount Exception : " + ex.Message.ToString());
            }
            var data = new { Getsoftinsatlledcountdata = dt, Getsoftinsatlledcountsyswisedata = dt1 };
            return Json(data);
        }

        public JsonResult Getsoftinsatlledcountsyswise(string ouid, string Device, string IP)
        {
            string Getsoftinsatlledcountsyswisedata = string.Empty;

            try
            {
                string search = "";
                if (IP != null)
                {

                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like '%" + Device + "%'";
                    //search += " and dl.device_name ='" + Device + "'";
                }



                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }


                dt = Objquery.Get_softinsatlledcountsyswise(search);


            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getsoftinsatlledcountsyswise Exception : " + ex.Message.ToString());
            }
            var data = new { Getsoftinsatlledcountsyswisedata = dt };
            return Json(data);
        }

        public JsonResult Getsoftinsatlleddetails(string IP, string Device, string ouid, string softinstuninstvalue, string software)
        {
            string Getsoftinsatlleddetailsdata = string.Empty;
            try
            {
                string search = "";

                //   string strFieldSearch = "";

                if (IP != null)
                {

                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name like'%" + Device + "%'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")

                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (software != null)
                {
                    search += "and software = '" + software + "'";


                }

                dt = Objquery.Get_softinsatlleddetails(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getsoftinsatlleddetails Exception : " + ex.Message.ToString());
            }
            var data = new { Getsoftinsatlleddetailsdata = dt };
            return Json(data);
        }

        public JsonResult Getsoftuninsatlleddetails(string softinstuninstvalue, string software)
        {
            string Getsoftuninsatlleddetailsdata = string.Empty;
            try
            {
                // string search = "";
                if (software != null)
                    OwnYITConstant.DT_Getuninstalledswdetail = Objquery.Get_softuninstalleddetails(software);
                dt = OwnYITConstant.DT_Getuninstalledswdetail;
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getsoftuninsatlleddetails Exception : " + ex.Message.ToString());
            }
            var data = new { Getsoftuninsatlleddetailsdata = dt };
            return Json(data);
        }

        public JsonResult Showsoftuninsatlleddetails(string IP, string Device, string ouid)
        {
            string showsoftuninsatlleddetailsdata = string.Empty;
            DataTable dttemp = new DataTable();
            try
            {
                //  string search = "";
                OwnYITConstant.DT_Setuninstalledswdetail = OwnYITConstant.DT_Getuninstalledswdetail;
                if (IP != null)
                {

                    if (IP != "")
                    {

                        foreach (DataRow dr in OwnYITConstant.DT_Getuninstalledswdetail.Select("ip like '%" + IP + "%'"))
                        {
                            if (dttemp.Columns.Count == 0)
                            {
                                dttemp = OwnYITConstant.DT_Getuninstalledswdetail.Clone();
                                dttemp.Rows.Clear();
                            }
                            dttemp.Rows.Add(dr.ItemArray);
                        }
                        OwnYITConstant.DT_Setuninstalledswdetail = dttemp;
                    }


                }

                if (Device != null)
                {
                    if (Device != "-1")
                        foreach (DataRow dr in OwnYITConstant.DT_Getuninstalledswdetail.Select("device_name='" + Device + "'"))
                        {
                            if (dttemp.Columns.Count == 0)
                            {
                                dttemp = OwnYITConstant.DT_Getuninstalledswdetail.Clone();
                                dttemp.Rows.Clear();
                            }
                            dttemp.Rows.Add(dr.ItemArray);
                        }
                    OwnYITConstant.DT_Setuninstalledswdetail = dttemp;
                }




            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Showsoftuninsatlleddetails Exception : " + ex.Message.ToString());
            }


            var data = new { showsoftuninsatlleddetailsdata = OwnYITConstant.DT_Setuninstalledswdetail };
            return Json(data);
        }


        // Show software details sysem wise report

        public JsonResult Showinstalledsoftdetailsyswise(string startdate, string enddate, string ouid, string devicename, string ip, string FieldSerach, string ValueSerach, string devicecheck, string device_id)
        {
            string showinstalledsoftdetailsyswisedata = string.Empty;
            try
            {
                string Sdate = "";
                string Edate = "";
                if (startdate != null && enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                string search = "";
                string serachdate = "";

                if (ouid != null)
                {
                    if (ouid != "-1")

                        search += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }


                if (ip != null)
                {

                    if (ip != "")
                        search += " and dl.ip like '%" + ip + "%'";
                }

                if (devicename != null)
                {
                    if (devicename != "-1")
                        search += " and dl.device_name ='" + devicename + "'";
                }

                //if (devicename != null)
                //{
                //    search += "and (virtual_name in('" + devicename + "') or dl.device_name in('" + devicename + "')) ";


                //}

                if (device_id != null)
                {
                    search += "and dl.device_id in(" + device_id + ")";


                }

                if (devicecheck != null)
                {
                    search += "and dl.device_id in(" + devicecheck.Substring(1) + ")";

                }

                if (startdate != null && enddate != null)
                {
                    serachdate = " where installationdate >= '" + Sdate + "' and installationdate <= '" + Edate + "'";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    search += " and " + FieldSerach + " like '%" + ValueSerach + "%'";


                }

                dt = Objquery.Get_softinsatlleddetailsyswise(search, serachdate);
            }

            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Showinstalledsoftdetailsyswise Exception : " + ex.Message.ToString());
            }


            var data = new { showinstalledsoftdetailsyswisedata = dt };
            return Json(data);
        }

        // Generate Soft installed report
        public JsonResult generateinstalledsoft(string software, string ouid, string startdate, string enddate, string devicename, string ip, string FieldSerach, string ValueSerach)
        {
            //   string jj = software.Substring(1);
            string generatesoftinstalleddata = string.Empty;
            try
            {
                string Sdate = "";
                string Edate = "";
                string search = "";
                string serachdate = "";
                if (startdate != null && enddate != null)
                {
                    //Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/mm/yyyy", null).ToString("dd.mm.yyyy");
                    //Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/mm/yyyy", null).ToString("dd.mm.yyyy");
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                if (startdate != null && enddate != null)
                {
                    serachdate = " where  installationdate >= '" + Sdate + "' and installationdate <= '" + Edate + "'";
                }
                if (ip != null)
                {

                    if (ip != "")
                        search += " and dl.ip like '%" + ip + "%'";
                }

                if (devicename != null)
                {
                    if (devicename != "-1")
                        search += " and dl.device_name like'%" + devicename + "%'";
                }


                if (ouid != null)
                {
                    if (ouid.Trim() != "-1")
                        search += " and ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (software != null)
                {
                    search += " and software in ('" + software.Substring(1) + "') ";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    search += " and " + FieldSerach + " like '%" + ValueSerach + "%'";


                }
                dt = Objquery.Get_generatesoftinsatlled(search, serachdate);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "generateinstalledsoft Exception : " + ex.Message.ToString());
            }


            var data = new { generatesoftinstalleddata = dt };
            return Json(data);
        }

        public JsonResult generatenotinstalledsoft(string ouid, string ip, string devicename, string software, string softsearch)
        {

            string generatesoftnotinstalleddata = string.Empty;
            string softnotinstall = software.Substring(1).Replace("\'", "");
            DataTable dttemp = new DataTable();
            try
            {
                dt = Objquery.Get_softuninstalleddetails(softnotinstall);
                if (ip != null)
                {

                    if (ip != "")
                    {

                        foreach (DataRow dr in dt.Select("ip like '%" + ip + "%'"))
                        {
                            if (dttemp.Columns.Count == 0)
                            {
                                dttemp = dt.Clone();
                                dttemp.Rows.Clear();
                            }
                            dttemp.Rows.Add(dr.ItemArray);
                        }
                        dt = dttemp;
                    }


                }
                if (devicename != null)
                {

                    if (devicename != "-1")
                    {

                        foreach (DataRow dr in dt.Select("device_name='" + devicename + "'"))
                        {
                            if (dttemp.Columns.Count == 0)
                            {
                                dttemp = dt.Clone();
                                dttemp.Rows.Clear();
                            }
                            dttemp.Rows.Add(dr.ItemArray);
                        }
                        dt = dttemp;
                    }
                    //dt = dttemp;

                }
                if (softsearch != null)
                {

                    foreach (DataRow dr in dt.Select("software like '%" + softsearch + "%'"))
                    {
                        if (dttemp.Columns.Count == 0)
                        {
                            dttemp = dt.Clone();
                            dttemp.Rows.Clear();
                        }
                        dttemp.Rows.Add(dr.ItemArray);
                    }
                    dt = dttemp;
                }

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "generatenotinstalledsoft Exception : " + ex.Message.ToString());
            }
            var data = new { generatesoftnotinstalleddata = dt };
            return Json(data);
        }
        public IActionResult report_password_change()
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
        public IActionResult report_dcm()
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
        public JsonResult getDCMdata(string serch, string startdate, string enddate, string ouid, string ip, string device)
        {

            try
            {
                string Sdate = "";
                string Edate = "";
                string serachdate = "";
                if (startdate != null && enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                if (startdate != null && enddate != null)
                {
                    serachdate = " and starttime between '" + Sdate + "' and  '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        serachdate += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (ip != null && ip != "")
                {
                    serachdate += " and dl.ip like '%" + ip + "%'";
                }

                if (device != null && device != "-1")
                {
                    serachdate += " and dl.device_name ='" + device + "'";
                }


                if (serch != null)
                {
                    serachdate += " and ds.device_type=" + serch;
                }
                dt = Objquery.Get_DCMdata(serachdate);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "getDCMdata Exception : " + ex.Message.ToString());
            }


            var data = new { getdcm = dt };
            return Json(data);
        }

        public JsonResult GetpasswordchangeReport(string startdate, string enddate, string IP, string Device, string ouid, string usertype)
        {
            try
            {
                string search = "";
                //string searchdate = "";
                //if (startdate != null && enddate != null)
                //{
                //    string Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                //    string Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                //    searchdate = " and e107lastpwddate between '" + Sdate + "' and '" + Edate + "'";
                //}
                if (IP != null)
                {
                    if (IP != "")
                    {
                        search += " and dl.ip like '%" + IP + "%'";
                    }
                }
                if (Device != null)
                {
                    if (Device != "-1")
                    {
                        //search += " and dl.device_name ='" + Device + "'";
                        search += " and (dl.device_id like '%" + Device + "%' or dl.device_name like '%" + Device + "%')";
                        //search += " and dl.device_id ='" + Device + "'";
                    }
                }
                //if (usertype != null)
                //{
                //    if (usertype != "-1")
                //        search += "and upper(e107username) = '" + usertype + "'";
                //}
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                dt = Objquery.Get_passwordchangedetail(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetpasswordchangeReport Exception : " + ex.Message.ToString());
            }
            var data = new { userpasswordchangedata = dt };

            return Json(data);

        }
        public IActionResult report_network_statistics()
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
        // Network Statistics Details Report
        public JsonResult getprotocol()
        {
            try
            {
                dt = Objquery.Get_Protocol();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "getprotocol Exception : " + ex.Message.ToString());
            }


            var data = new { protocol = dt };
            return Json(data);

        }
        public JsonResult getnetwrokdetailsdata(string ouid, string ip, string device, string sdate, string edate, string localip, string remoteip, string packetvalue)
        {
            string network = "";
            try
            {
                string Sdate = "";
                string Edate = "";
                string serachdate = "";
                //var rowhead = "";
                //var chartlabel = "";
                if (sdate != null && edate != null)
                {
                    Sdate = System.DateTime.ParseExact(sdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(edate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                if (sdate != null && edate != null)
                {
                    serachdate = " between '" + Sdate + "' and '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        serachdate += "and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (ip != null && ip != "")
                {
                    serachdate += " and dl.ip like '%" + ip + "%'";
                }

                if (device != null && device != "-1")
                {
                    serachdate += " and dl.device_name like '%" + device + "%'";
                    //serachdate += " and dl.device_name ='" + device + "'";
                }

                if (remoteip != null && remoteip != "-1")
                {
                    serachdate += " and nnt.remote_ip='" + remoteip + "'";
                }
                if (localip != null && localip != "-1")
                {
                    serachdate += " and nnt.local_ip='" + localip + "'";
                }


                if (packetvalue == "1")
                {
                    dt = Objquery.Get_Netwrokdetails_send_data("sum(nnt.packets)", serachdate);
                    dt1 = Objquery.Get_Netwrokdetails_send_Chart_data("sum(nnt.packets)", serachdate);
                }
                else if (packetvalue == "2")
                {
                    dt = Objquery.Get_Netwrokdetails_send_data("sum(nnt.packets)", serachdate);
                    dt1 = Objquery.Get_Netwrokdetails_send_Chart_data("sum(nnt.packets)", serachdate);
                    //dt1 = Objquery.Get_Netwrokdetails_send_Chart_data("sum(nt.received_packets)", serachdate);
                }
                else if (packetvalue == "3")
                {
                    dt = Objquery.Get_Netwrokdetails_send_data("(sum(nnt.bytes)/1024)", serachdate);
                    dt1 = Objquery.Get_Netwrokdetails_send_Chart_data("(sum(nnt.bytes)/1024)", serachdate);
                    //dt = Objquery.Get_Netwrokdetails_send_data("sum(nt.send_size)", serachdate);
                    //dt1 = Objquery.Get_Netwrokdetails_send_Chart_data("sum(nt.send_size)", serachdate);
                }
                else if (packetvalue == "4")
                {
                    dt = Objquery.Get_Netwrokdetails_send_data("(sum(nnt.bytes)/1024)", serachdate);
                    dt1 = Objquery.Get_Netwrokdetails_send_Chart_data("(sum(nnt.bytes)/1024)", serachdate);
                    //dt = Objquery.Get_Netwrokdetails_send_data("sum(nt.received_size)", serachdate);
                    //dt1 = Objquery.Get_Netwrokdetails_send_Chart_data("sum(nt.received_size)", serachdate);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "getnetwrokdetailsdata Exception : " + ex.Message.ToString());
            }


            try
            {

                List<BarChart> Nchart = objcommon.ConvertDataTable<BarChart>(dt1);
                network = JsonConvert.SerializeObject(Nchart);
            }
            catch (Exception ex)
            {
                List<Barchart1> Nchart = objcommon.ConvertDataTable<Barchart1>(dt1);
                network = JsonConvert.SerializeObject(Nchart);
                objcommon.WriteLog("csat_asset_mgmtController", "getnetwrokdetailsdata Chart Exception : " + ex.Message.ToString());

            }

            var data = new { netwrokdetails = dt, networkchartdata = network };
            return Json(data);

        }
        // Network traffic statitics
        public JsonResult getnetwroktrafficstatistics(string ouid, string ip, string device, string sdate, string edate, string packettype)
        {
            string networkstatisticschart1 = "";
            try
            {
                string packettypes = "";
                string remote_ip = "";
                string local_ip = "";
                string Sdate = "";
                string Edate = "";
                string serachdate = "";

                if (sdate != null && edate != null)
                {
                    Sdate = System.DateTime.ParseExact(sdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(edate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    serachdate = " t.startdate between '" + Sdate + "' and  '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        serachdate += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (ip != null && ip != "-1")
                {
                    serachdate += " and dl.ip like '%" + ip + "%'";
                    local_ip += " where local_ip like '%" + ip + "%'";
                    remote_ip += " where remote_ip like '%" + ip + "%'";
                }
                if (device != null && device != "-1")
                {
                    serachdate += " and dl.device_name like'%" + device + "%'";
                }
                if (packettype == "1")
                {
                    packettypes = "sum(packets)";
                    dt = Objquery.Get_network_traffic_Statitic_Chart(local_ip, remote_ip, packettypes, serachdate);
                }
                else
                {
                    packettypes = "sum(bytes/1024)";
                    dt = Objquery.Get_network_traffic_Statitic_Chart(local_ip, remote_ip, packettypes, serachdate);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "getnetwroktrafficstatistics Exception : " + ex.Message.ToString());
            }

            try
            {
                this.database = new DatabaseHandler(OwnYITConstant.db_settings);
                this.DBServer_Type = this.database.DB_SERVER_TYPE;
                switch (this.database.DB_SERVER_TYPE)
                {
                    case 0:
                        dbtype = OwnYITConstant.DatabaseTypes.MSSQL_SERVER;
                        List<assetnwtraficchart> trafficdetails = objcommon.ConvertDataTable<assetnwtraficchart>(dt);
                        networkstatisticschart1 = JsonConvert.SerializeObject(trafficdetails);
                        break;
                    case 1:
                        dbtype = OwnYITConstant.DatabaseTypes.MYSQL_SERVER;
                        List<assetnwtraficchart_decimal> trafficdetails1 = objcommon.ConvertDataTable<assetnwtraficchart_decimal>(dt);
                        networkstatisticschart1 = JsonConvert.SerializeObject(trafficdetails1);
                        break;
                }
            }
            catch (Exception ex)
            {
                //List<assetnwtraficchart> trafficdetails = objcommon.ConvertDataTable<assetnwtraficchart>(dt);
                //networkstatisticschart1 = JsonConvert.SerializeObject(trafficdetails);
                objcommon.WriteLog("csat_reportController", "getnetwroktrafficstatistics Exception : " + ex.Message.ToString());
            }

            var data = new { netwrokstatisticschart = networkstatisticschart1, netwrokstatistics = dt };
            return Json(data);
        }

        public IActionResult report_enumerate_files()
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

        // Enumarated files
        public JsonResult report_fileextension_date(string ouid, string ip, string device, string sdate, string edate)
        {

            try
            {

                string Sdate = "";
                string Edate = "";
                string serachdate = "";
                if (sdate.ToUpper() == "UNDEFINED")
                {
                    sdate = System.DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                    edate = System.DateTime.Now.ToString("dd/MM/yyyy");
                }
                if (sdate != null && edate != null)
                {
                    Sdate = System.DateTime.ParseExact(sdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(edate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                if (sdate != null && edate != null)
                {
                    serachdate = " and e111epochtime between '" + Sdate + "' and  '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        serachdate += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (ip != null && ip != "-1")
                {
                    serachdate += " and ip ='" + ip + "'";
                }

                if (device != null && device != "-1")
                {
                    serachdate += " and device_name ='" + device + "'";
                }

                dt = Objquery.Get_FileExtention_data(serachdate);


            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "report_fileextension_date Exception : " + ex.Message.ToString());
            }

            var data = new { fileextensiondata = dt };
            return Json(data);

        }
        public JsonResult add_fileextension_data(string extentionname)
        {

            try
            {
                if (extentionname.Trim().Length > 0)
                {
                    if (Objquery.check_FileExtention_data(extentionname))

                        Objquery.add_FileExtention_data(extentionname);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "add_fileextension_data Exception : " + ex.Message.ToString());
            }
            DataTable dt = new DataTable();
            dt = Objquery.Get_FileExtention();
            var data = new { fileextensiondata = dt };
            return Json(data);
        }
        public JsonResult remove_fileextension_data(string extentionname)
        {

            try
            {
                Objquery.delete_FileExtention_data(extentionname);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "getnetwrokdetailsdata Exception : " + ex.Message.ToString());
            }
            DataTable dt = new DataTable();
            dt = Objquery.Get_FileExtention();
            var data = new { fileextensiondata = dt };
            return Json(data);
        }
        public JsonResult apply_fileextension_data(string extentionname, string deviceidlist, string extentionnamecount, string extentionname1)
        {
            try
            {
                string MSG = "";
                //string extensionmsg = "";
                //string[] extentionnameArr = extentionname1.Split(',');
                //for (int i = 0; i < extentionnameArr.Length; i++)
                //{
                //    extensionmsg = "!" + extentionnameArr[i];
                //}
                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                int secondsSinceEpoch = (int)t.TotalSeconds;
                Random random = new Random();
                int rdm = random.Next(100000, 999999);
                string uniqueID = secondsSinceEpoch.ToString() + rdm.ToString();
                MSG = "#6062@" + uniqueID + "!" + extentionnamecount + "!" + extentionname1.Substring(1).Replace(",", "!") + "!@6062#";
                //MSG = "#505@" + extentionnamecount + "!" + extentionname1.Substring(1).Replace(",", "!") + "!@505#";
                extentionname = extentionname.Substring(1).Replace("\'", "");
                deviceidlist = deviceidlist.Substring(1);
                string[] strdeviceIDArr = deviceidlist.Split(',');
                for (int i = 0; i < strdeviceIDArr.Length; i++)
                {
                    Objquery.apply_FileExtention_data(extentionname, strdeviceIDArr[i], uniqueID);
                    Objquery.InsertQueryLog(strdeviceIDArr[i], MSG, Objquery.Get_Location_ID(strdeviceIDArr[i]));
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "apply_fileextension_data Exception : " + ex.Message.ToString());
            }
            var data = "Apply successfully";
            return Json(data);
        }

        public JsonResult report_fileextension_scanresult(string ouid, string ip, string device, string sdate, string edate, string fileex)
        {

            try
            {

                string Sdate = "";
                string Edate = "";
                string serachdate = "";
                if (sdate.ToUpper() == "UNDEFINED")
                {
                    sdate = System.DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                    edate = System.DateTime.Now.ToString("dd/MM/yyyy");
                }
                if (sdate != null && edate != null)
                {
                    Sdate = System.DateTime.ParseExact(sdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(edate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                if (sdate != null && edate != null)
                {
                    serachdate = " and file_enum_time between '" + Sdate + "' and  '" + Edate + "'";
                }
                if (ouid != null && ouid.ToUpper() != "UNDEFINED")
                {
                    if (ouid != "-1")
                        serachdate += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (ip != null && ip != "-1" && ip.ToUpper() != "UNDEFINED")
                {
                    serachdate += " and ip like '%" + ip + "%'";
                }

                if (device != null && device != "-1" && device.ToUpper() != "UNDEFINED")
                {
                    serachdate += " and device_name like '%" + device + "%'";
                    //serachdate += " and device_name ='" + device + "'";
                }
                if (fileex != null && fileex != "-1" && fileex.ToUpper() != "UNDEFINED")
                {
                    serachdate += " and file_extention like '%" + fileex + "%'";
                }
                dt = Objquery.Get_FileExtention_scanresult(serachdate);


            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "report_fileextension_scanresult Exception : " + ex.Message.ToString());
            }

            var data = new { fileextensionscanresult = dt };
            return Json(data);

        }
        public JsonResult report_fileextension_scanresult_data(string file_enum_id, string deviceid)
        {
            DataTable dt_device = new DataTable();
            try
            {
                dt = Objquery.Get_FileExtention_scanresult_data(file_enum_id, deviceid);
                dt_device = Objquery.Get_FileExtention_scanresult_device(file_enum_id, deviceid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "report_fileextension_scanresult_data Exception : " + ex.Message.ToString());
            }
            var data = new { fileextensionscanresultdata = dt, fileextensionscanresultdevice = dt_device };
            return Json(data);
        }

        public IActionResult report_autopower_off_status()
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

        public JsonResult Get_autopowerstatus_Data(string startdate, string enddate, string IP, string Device, string ouid)
        {
            try
            {
                string Sdate = "";
                string Edate = "";
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }

                string search = "";
                if (IP != null)
                {
                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and dl.device_name ='" + Device + "'";
                }

                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                dt = Objquery.Get_autopowerstatus_Data(Sdate, Edate, search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Get_autopowerstatus_Data Exception : " + ex.Message.ToString());
            }
            var data = new { autopower_data = dt };

            return Json(data);
        }
        public IActionResult report_policy_apply()
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
        public JsonResult Getpolicytype()
        {

            var data = new { policytype = Objquery.Get_PolicyType() };
            return Json(data);
        }
        public JsonResult Getpolicydata(string IP, string Device, string ouid, string PolicyType, string startdate, string enddate)
        {
            string search = "";
            string Sdate = "";
            string Edate = "";
            if (startdate != null && enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 23:59:59";
                search = " where plm.start_date >= '" + Sdate + "' and plm.start_date <= '" + Edate + "'";
            }
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and device_name like '%" + Device + "%'";
                    //search += " and device_name ='" + Device + "'";
                }
                if (IP != null)
                {
                    if (IP != "-1")
                        search += " and ip like '%" + IP + "%'";
                }
                //if (PolicyType != null)
                //{
                //    if (PolicyType != "-1")
                //    {
                //        if (search == "")
                //            search += " where policy_type ='" + PolicyType + "'";
                //        else
                //            search += " and policy_type ='" + PolicyType + "'";
                //    }
                //}
                dt = Objquery.Get_policy_data_report(search, Sdate, Edate);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getpolicydata Exception : " + ex.Message.ToString());
            }
            var data = new { policydata = dt };
            return Json(data);
        }

        public IActionResult report_offnetwork_activity()
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
        public JsonResult offnetwork_activity(string IP, string Device, string ouid, string startdate, string enddate)
        {
            string Sdate = "";
            string Edate = "";
            if (startdate != null && enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }

            try
            {

                string search = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and device_name ='" + Device + "'";
                }
                if (IP != null)
                {
                    if (IP != "-1")
                        search += " and ip like '%" + IP + "%'";
                }

                dt = Objquery.Get_network_on_off_report(search, Sdate, Edate);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "offnetwork_activity Exception : " + ex.Message.ToString());
            }
            var data = new { nwoff = dt };
            return Json(data);
        }

        public JsonResult offnetwork_activity_select_data(string DeviceId)
        {

            try
            {
                dt = Objquery.Get_network_on_off_selected_data(DeviceId);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "offnetwork_activity_select_data Exception : " + ex.Message.ToString());
            }
            var data = new { nwoffselectdata = dt };
            return Json(data);
        }

        public JsonResult offnetwork_activity_report_details(string DeviceId, string strstartdate, string strenddate)
        {
            string Sdate = "";
            string Edate = "";
            if (strstartdate != null && strenddate != null)
            {
                Sdate = System.DateTime.ParseExact(strstartdate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(strenddate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                dt = Objquery.Get_network_on_off_report_details(DeviceId, Sdate, Edate);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "offnetwork_activity_report_details Exception : " + ex.Message.ToString());
            }
            var data = new { nwoffdetails = dt };
            return Json(data);
        }

        public JsonResult offnetwork_activity_dataleakage(string DeviceId, string strstartdate, string strenddate)
        {
            string Sdate = "";
            string Edate = "";
            if (strstartdate != null && strenddate != null)
            {
                Sdate = System.DateTime.ParseExact(strstartdate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(strenddate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                dt = Objquery.Get_network_on_off_dataleakage_report(DeviceId, Sdate, Edate);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "offnetwork_activity_dataleakage Exception : " + ex.Message.ToString());
            }
            var data = new { nwoffdataleakage = dt };
            return Json(data);
        }

        public JsonResult offnetwork_activity_printer(string DeviceId, string strstartdate, string strenddate)
        {
            string Sdate = "";
            string Edate = "";
            if (strstartdate != null && strenddate != null)
            {
                Sdate = System.DateTime.ParseExact(strstartdate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(strenddate.ToString(), "dd/MM/yyyy hh:mm:ss", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                dt = Objquery.Get_network_on_off_printer_report(DeviceId, Sdate, Edate);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "offnetwork_activity_printer Exception : " + ex.Message.ToString());
            }
            var data = new { nwoffprinter = dt };
            return Json(data);
        }
        public IActionResult report_antivirus_update()
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
        public JsonResult Getantivirusdata(string IP, string Device, string ouid, string startdate, string enddate, string FieldSerach, string ValueSerach)
        {
            LoadData loaddata = new LoadData();
            string Sdate = "";
            string Edate = "";
            if (startdate != null && enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd");
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd");

            }

            try
            {
                string strFieldSearch = "";
                string ou_id = "";
                string search = "";
                if (ouid != null)
                {
                    if (ouid.Length >= 5)
                        ou_id += " and ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                    else
                    {

                    }
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and device_name ='" + Device + "'";
                }
                if (IP != null)
                {
                    if (IP != "-1")
                        search += " and ip like '%" + IP + "%'";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";


                }
                dt = Objquery.Get_antivirus_data_report(Sdate, Edate, ou_id, search, strFieldSearch);
                loaddata = objcommon.DatatableToObject(dt);

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getantivirusdata Exception : " + ex.Message.ToString());
            }
            //var data = new { avdata = dt };
            return Json(loaddata);
        }
        // Port Authentication
        public JsonResult get_port_authentication_report(string IP, string Device, string ouid, string searching, string value, string startdate, string enddate)
        {
            try
            {
                string search = "";
                string Sdate = "";
                string Edate = "";
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    //search = " and  dl.datetime >= '" + Sdate + "' and dl.datetime <= '" + Edate + "'";
                    search = "and ((nc.start_time >= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Sdate + "')) and nc.start_time <= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Edate + "'))) or (nc.end_time >= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Sdate + "')) and nc.end_time <= (select DATEDIFF(s, '1970-01-01 00:00:00', '" + Edate + "')))) ";
                }
                if (IP != null)
                {
                    if (IP != "-1")
                        search += " dl.ip like '%" + IP + "%'";
                }
                if (Device != null && Device != "-1")
                {
                    if (Device != "-1")
                        search += " and dl.device_name like'%" + Device + "%'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (searching != "-1" && value != null)
                {
                    search += " and " + searching + " like '%" + value + "%'";
                    //if (search != "")
                    //    search += " and " + Objquery.portinfo_serching_list(searching, value);
                    //else
                    //    search += Objquery.portinfo_serching_list(searching, value);
                }
                dt = Objquery.port_authentication_report(Sdate, Edate, search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "get_port_authentication_report Exception : " + ex.Message.ToString());
            }
            var data = new { portauthreport = dt };
            return Json(data);
        }
        public JsonResult port_authentication_setting()
        {

            try
            {
                dt = Objquery.port_authentication_setting_data();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "port_authentication_setting Exception : " + ex.Message.ToString());
            }
            var data = new { portauth = dt };
            return Json(data);
        }
        public JsonResult port_authorized_add(string authport, string portnumber, string portname)
        {
            string strreturn = "Port not submited";
            int cnt = 0;
            try
            {
                cnt = Objquery.port_authorized_add_data(authport, portnumber, portname);
                if (cnt > 0)
                {
                    strreturn = "Port authorize successfully";
                    Objquery.port_authorized_local_authorize(authport, portnumber);
                    Objquery.port_authorized_remote_authorize(authport, portnumber);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "port_authorized_add Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        public JsonResult deleteAuthport(string portid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.Delete_auth_port(portid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deleteAuthport Exception : " + ex.Message.ToString());

            }
            if (querystatus == 1)
                result = "port deleted successfully";
            else
            {
                result = "port not deleted.";
            }

            return Json(result);
        }

        public IActionResult report_process_log()
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
        public IActionResult report_unauthorise_ip_log()
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
        // Process Log
        public JsonResult ShowProcesslog(string IP, string Device, string ouid, string startdate, string enddate, string FieldSerach, string ValueSerach, string viewby)
        {
            LoadData loaddata = new LoadData();
            string Sdate = "";
            string Edate = "";
            if (startdate != null && enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                string strFieldSearch = "";
                string search = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        // search += "(" + Objquery.Get_ParentOu_id(ouid) + ")";
                        search += " and ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and (dl.device_name like '%" + Device + "%' or dl.device_id like '%" + Device + "%')";
                    //search += " and device_name ='" + Device + "'";
                }
                if (IP != null)
                {
                    if (IP != "")
                        search += " and ip like '%" + IP + "%'";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";
                }
                if (viewby != null)
                {
                    if (viewby == "Unauthorise")
                        search += " and pad.authorizetype = 0";
                    else
                        search += " and (pad.authorizetype = 1 or pad.authorizetype is null)";
                }
                dt = Objquery.Get_ProcessLog(Sdate, Edate, search, strFieldSearch);
                loaddata = objcommon.DatatableToObject(dt);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "ShowProcesslog Exception : " + ex.Message.ToString());
            }
            //var data = new { processlogdata = dt };
            return Json(loaddata);
        }
        // Delete Process Log
        public JsonResult Delete_Processlog(string IP, string Device, string startdate, string enddate, string FieldSerach, string ValueSerach)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                string Sdate = "";
                string Edate = "";
                if (startdate != null && enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                string strFieldSearch = "";
                string search = "";

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and device_id ='" + Device + "'";
                }
                if (IP != null)
                {
                    if (IP != "")
                        search += " and device_id like '%" + IP + "%'";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";


                }
                querystatus = Objquery.Delete_ProcessLog(Sdate, Edate, search, strFieldSearch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Delete_Processlog Exception : " + ex.Message.ToString());

            }
            if (querystatus == 0)
                result = "Record deleted failed.";
            else
                result = "Record deleted successfully";
            return Json(result);
        }

        // Unauthorised IP Log
        public JsonResult Delete_unauthiplog(string IP, string Device, string startdate, string enddate, string FieldSerach, string ValueSerach)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                string Sdate = "";
                string Edate = "";
                if (startdate != null && enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                string strFieldSearch = "";
                string search = "";

                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and device_id ='" + Device + "'";
                }
                if (IP != null)
                {
                    if (IP != "")
                        search += " and device_id like '%" + IP + "%'";
                }
                if (FieldSerach == "T.local_ip")
                    FieldSerach = "local_ip";
                if (FieldSerach == "T.local_port")
                    FieldSerach = "local_port";
                if (FieldSerach == "T.remote_ip")
                    FieldSerach = "remote_ip";
                if (FieldSerach == "T.remote_port")
                    FieldSerach = "remote_port";
                if (FieldSerach != "-1" && ValueSerach != null)
                {
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";
                }
                querystatus = Objquery.Delete_unauthipLog(Sdate, Edate, search, strFieldSearch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Delete_unauthiplog Exception : " + ex.Message.ToString());

            }
            if (querystatus == 0)
                result = "Record deleted failed.";
            else
                result = "Record deleted successfully";
            return Json(result);
        }

        // Manage Agent Registry
        public IActionResult report_manage_agent_registry()
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
        public JsonResult GetManageRegData(string ouid, string device, string ip, string startdate, string enddate, string fieldserach, string valuesearch)
        {
            string Sdate = "";
            string Edate = "";
            try
            {
                string strFieldSearch = "";
                string search = "";
                if (startdate != null && enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    search += " and c057timestamp between '" + Sdate + "' and  '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (device != null)
                {
                    if (device != "-1")
                        search += " and device_name like '%" + device + "%'";
                    //search += " and device_name ='" + device + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        search += " and ip like '%" + ip + "%'";
                }
                if (fieldserach != null && valuesearch != null)
                {
                    if (fieldserach == "c057keytype")
                        fieldserach = "keytype";
                    if (fieldserach == "c057action")
                        fieldserach = "action";
                    strFieldSearch += " and " + fieldserach + " like '%" + valuesearch + "%'";
                }
                dt = Objquery.Show_DeviceRegistry(search, strFieldSearch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetManageRegData Exception : " + ex.Message.ToString());
            }
            var data = new { agentregdata = dt };
            return Json(data);
        }

        // Asset Information Report
        public JsonResult GetAssetInfoData(string ouid, string device, string ip, string startdate, string enddate, string periodduration, string fieldserach, string valuesearch)
        {
            // string strmotherboard = "";
            string Sdate = "";
            string Edate = "";
            DataTable dtnew = new DataTable();
            if (startdate != null && enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("dd-MM-yyyy");
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("dd-MM-yyyy");
            }
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                    {
                        ouid = Objquery.get_ou_nodelinkage_allchild(ouid);
                        if (search == "" || search == null)
                            search += " ou_id in (" + ouid + ")";
                        else
                            search += "and ou_id in (" + ouid + ")";
                    }
                    //search += " where ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (device != null)
                {
                    if (device != "-1")
                    {
                        if (search == "" || search == null)
                            search += " device_name like '%" + device + "%'";
                        else
                            search += " and device_name like '%" + device + "%'";
                        //if (search == "" || search == null)
                        //    search += " device_name ='" + device + "'";
                        //else
                        //    search += " and device_name ='" + device + "'";
                    }
                }
                if (ip != null)
                {
                    if (ip != "")
                    {
                        if (search == "" || search == null)
                            search += " ip like '%" + ip + "%'";
                        else
                            search += " and ip like '%" + ip + "%'";
                    }
                }
                if (periodduration != null)
                {
                    if (periodduration != "-1")
                    {
                        if (search == "" || search == null)
                        {
                            if (periodduration == "warranty_from_date")
                                search += " warranty_from_date >= '" + Sdate + "' and warranty_to_date <= '" + Edate + "'";
                            else if (periodduration == "amc_from_date")
                                search += " amc_from_date >= '" + Sdate + "' and amc_to_date <= '" + Edate + "'";
                            else if (periodduration == "insurance_from_date")
                                search += " insurance_from_date >= '" + Sdate + "' and insurance_to_date <= '" + Edate + "'";
                        }
                        else
                        {
                            if (periodduration == "warranty_from_date")
                                search += " and warranty_from_date >= '" + Sdate + "' and warranty_to_date <= '" + Edate + "'";
                            else if (periodduration == "amc_from_date")
                                search += " and amc_from_date >= '" + Sdate + "' and amc_to_date <= '" + Edate + "'";
                            else if (periodduration == "insurance_from_date")
                                search += " and insurance_from_date >= '" + Sdate + "' and insurance_to_date <= '" + Edate + "'";
                        }
                    }
                }
                if (fieldserach != null && valuesearch != null)
                {
                    if (search == "" || search == null)
                        search += "" + fieldserach + " like '%" + valuesearch + "%'";
                    else
                        search += " and " + fieldserach + " like '%" + valuesearch + "%'";
                }
                //DataTable dtAsset = Objquery.Get_AssetInfo(search);
                //DataRow[] result = dtAsset.Select(search);
                //dtnew = objcommon.ConvertToDataTable(result, dtAsset);

                dtnew= Objquery.Get_AssetInfo_new(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetAssetInfoData Exception : " + ex.Message.ToString());
            }
            var data = new { getsysinfo = dtnew };
            return Json(data);
        }

        public IActionResult report_agent_connectivity()
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

        // Agent Connectivity Report
        public JsonResult GetAgentConnData(string ouid, string ip, string startdate, string enddate, string category)
        {
            string str = "";
            string Sdate = "";
            string Edate = "";

            if (startdate != null && enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
            }
            try
            {
                if (startdate != null && enddate != null && category != null && category != "-1")
                {
                    search += " and " + category + " between '" + Sdate + "' and  '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (ip != null)
                {
                    if (ip != "")
                        search += " and ip like '%" + ip + "%'";
                }
                if (category != null)
                {
                    if (category != "-1")
                    {
                        str = category;
                    }
                }

                dt = Objquery.Get_AgentConnectivity(str, search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetAgentConnData Exception : " + ex.Message.ToString());

            }
            var data = new { getsysinfo = dt };
            return Json(data);
        }

        public JsonResult GetAgentConnsyswiseData(string ouid, string system, string ip)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        search += " and device_name like '%" + system + "%'";
                    //search += " and device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        search += " and ip like '%" + ip + "%'";
                }
                dt = Objquery.Get_AgentConnectivitySyswise(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetAgentConnsyswiseData Exception : " + ex.Message.ToString());

            }
            var data = new { getsyscatinfo = dt };
            return Json(data);
        }

        //hardware info report
        public IActionResult report_hardware_information()
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


        public JsonResult Gethrdjsondata()
        {

            try
            {
                dt = Objquery.Get_Hardware_type1();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Gethrdjsondata Exception : " + ex.Message.ToString());
            }
            var data = new { hardwaretype = dt };
            return Json(data);

        }

        public JsonResult GetHWJsonDetailData(string hwclass, string ip, string device, string ouid)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                    {
                        if (search.Length > 0)
                            search += " and ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                        else
                            search += " where ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                    }
                }
                if (device != null)
                {
                    if (device != "-1")
                    {
                        if (search.Length > 0)
                            search += " and device_name like '%" + device + "%'";
                        else
                            search += " where device_name like '%" + device + "%'";
                        //if (search.Length > 0)
                        //    search += " and device_name ='" + device + "'";
                        //else
                        //    search += " where device_name ='" + device + "'";
                    }
                }
                if (ip != null)
                {
                    if (ip != "")
                    {
                        if (search.Length > 0)
                            search += " and ip like '%" + ip + "%'";
                        else
                            search += " where ip like '%" + ip + "%'";
                    }
                }
                if (hwclass != null)
                {
                    if (hwclass != "-1")
                    {
                        if (search.Length > 0)
                            search += " and hardware_class in (" + hwclass.Substring(1).ToString() + ")";
                        else
                            search += " where hardware_class in (" + hwclass.Substring(1).ToString() + ")";
                    }
                }
                dt = Objquery.Get_Hardware_detaildata(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetHWJsonDetailData Exception : " + ex.Message.ToString());
            }
            var data = new { hardwaredetaildata = dt };
            return Json(data);
        }

        public JsonResult ShowHWJsonDetailData(string hwclass, string detail, string ip, string device, string ouid)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (device != null)
                {
                    if (device != "-1")
                        search += " and device_name ='" + device + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        search += " and ip ='" + ip + "'";
                }
                dt = Objquery.Show_Hardware_detaildata(hwclass, detail, search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "ShowHWJsonDetailData Exception : " + ex.Message.ToString());
            }
            var data = new { showhardwaredetaildata = dt };
            return Json(data);

        }
        public JsonResult HWwiseJsonDetailData(string hwclass, string detail, string ip, string device, string ouid)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (device != null)
                {
                    if (device != "-1")
                        search += " and device_name ='" + device + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        search += " and ip like '%" + ip + "%'";
                }
                dt = Objquery.Show_HardwareWise_detaildata(hwclass.Substring(1), detail.Substring(1), search);
                foreach (DataRow dr in dt.Rows)
                {
                    dr[6] = dr[6].ToString().Replace("manufacturer", "Manufacturer").Replace("DeviceID", "Deviceid").Replace("description", "Description").Replace("MonitorManufacturer", "Manufacturer").Replace("PNPDeviceid", "PNPDeviceID").Replace("name", "Name").Replace("SystemName", "Manufacturer").Replace("MaxClockSpeed", "Speed").Replace("AdapterCompatibility", "Manufacturer").Replace("DriverVersion", "Version");
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "HWwiseJsonDetailData Exception : " + ex.Message.ToString());
            }
            var data = new { showhardwarewisedetaildata = dt };
            return Json(data);

        }

        public IActionResult report_event_monitoring()
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
        public JsonResult report_event_monitoring_data(string ouid, string startdate, string enddate, string fieldserach, string valuesearch, string devicename, string ipaddress)
        {
            string Sdate = "";
            string Edate = "";
            string search = "";
            try
            {
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    search += " and nem.event_time >= '" + Sdate + "' and nem.event_time <= '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (devicename != null && devicename.Trim() != "")
                {
                    search += " and dl.device_name like '%" + devicename + "%'";
                }
                if (ipaddress != null && ipaddress.Trim() != "")
                {
                    search += " and dl.ip like '%" + ipaddress + "%'";
                }
                if (fieldserach != null && valuesearch != null)
                {
                    search += " and nem.event_type ='" + fieldserach + "' and nem.event_id = '" + valuesearch + "'";
                    //search += " and " + fieldserach + " like '%" + valuesearch + "%'";
                }

                dt = Objquery.report_event_monitoring_data(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "report_event_monitoring_data Exception : " + ex.Message.ToString());
            }
            var data = new { event_data = dt };
            return Json(data);
        }

        public JsonResult apply_event_count_data(string etype, string eid, string edescription)
        {
            try
            {
                dt = Objquery.apply_event_count_data();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "apply_event_count_data Exception : " + ex.Message.ToString());
            }
            var data = new { apply_event_data = dt };
            return Json(data);
        }

        public JsonResult report_event_apply_show(string ouid, string system, string ip)
        {
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null && system != "-1")
                {
                    if (system != "-1")
                        search += " and dl.device_name like'%" + system + "%'";
                }
                if (ip != null)
                {
                    if (ip != "")
                        search += " and dl.ip like '%" + ip + "%'";
                }
                dt = Objquery.event_apply_show(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "report_event_apply_show Exception : " + ex.Message.ToString());
            }
            var data = new { applyshow = dt };
            return Json(data);
        }
        public JsonResult reportlogo()
        {
            try
            {
                dtreortlogo = Objquery.reportlogo();
                for (int i = 0; i < dtreortlogo.Rows.Count; i++)
                {
                    if (dtreortlogo.Rows[i][0].ToString() == "ProductName")
                    {
                        Configuration.productname = dtreortlogo.Rows[i][1].ToString();
                    }
                    else
                    if (dtreortlogo.Rows[i][0].ToString() == "ProductLogo")
                    {
                        Configuration.productname = dtreortlogo.Rows[i][1].ToString();
                    }

                }

                //Configuration.logo = dtreortlogo.Rows[0]["propertyvalue"].ToString();                
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "reportlogo Exception : " + ex.Message.ToString());
            }
            var data = new { getproduct = dtreortlogo };
            return Json(data);
        }

        public IActionResult report_usb_history()
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
        public JsonResult USBHistoryReport(string ip, string system, string ouid, string FieldSerach, string ValueSerach)
        {
            //string Sdate = "";
            //string Edate = "";
            string search = "";
            //string serachdate = "";
            string strFieldSearch = "";

            try
            {
                //if (startdate != null & enddate != null)
                //{
                //    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                //    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                //    serachdate = " and uh.created_date >= '" + Sdate + "' and uh.created_date <= '" + Edate + "'";
                //}
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        search += " and dl.device_name like '%" + system + "%'";
                    //search += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        search += " and dl.ip like '%" + ip + "%'";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";
                }

                dt = Objquery.USBHistoryReport(search, strFieldSearch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "USBHistoryReport Exception : " + ex.Message.ToString());
            }
            var data = new { usbhistorydata = dt };
            return Json(data);
        }
        public IActionResult report_gui_log()
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
        public JsonResult GetGuilogReport(string ip, string system, string ouid, string startdate, string enddate, string FieldSerach, string ValueSerach)
        {
            string Sdate = "";
            string Edate = "";
            string search = "";
            string serachdate = "";
            string strFieldSearch = "";

            try
            {
                if (startdate != null & enddate != null)
                {
                    Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 00:00:00";
                    Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + " 23:59:59";
                    serachdate = " and gl.action_date >= '" + Sdate + "' and gl.action_date <= '" + Edate + "'";
                }
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in (" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        search += " and dl.device_name like '%" + system + "%'";
                    //search += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "")
                        search += " and dl.ip like '%" + ip + "%'";
                }
                if (FieldSerach != null && ValueSerach != null)
                {
                    strFieldSearch = " and " + FieldSerach + " like '%" + ValueSerach + "%'";
                }

                dt = Objquery.Get_Gui_log(search, serachdate, strFieldSearch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetGuilogReport Exception : " + ex.Message.ToString());
            }
            var data = new { guilogdata = dt };
            return Json(data);
        }
        public JsonResult AddAntivirus(string antivirus_name, string service_name)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.AddAntivirus(antivirus_name, service_name);
                if (querystatus > 0)
                    result = "" + antivirus_name + " add successfull";
                else
                    result = "Failed to add " + antivirus_name + "";
            }
            catch (Exception ex)
            {

            }
            return Json(result);

        }
        public JsonResult Getaddantivirus()
        {
            LoadData loaddata = new LoadData();
            try
            {
                dt = Objquery.Getaddantivirus();
                loaddata = objcommon.DatatableToObject(dt);

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getaddantivirus Exception : " + ex.Message.ToString());
            }
            //var data = new { avdata = dt };
            return Json(loaddata);
        }
        public JsonResult DeleteAntivirus(string servicename)
        {
            string result = "";
            int querystatus = 0;
            try
            {
                if (servicename != null)
                {
                    querystatus = Objquery.DeleteAntivirus(servicename);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "DeleteAntivirus Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0)
                result = "Record not deleted";
            else
                result = "Record deleted successfully";
            return Json(result);

        }

        #region Employee Details

        public IActionResult report_employee_details()
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
        public JsonResult GetEmployee()
        {
            var data = new { emp = Objquery.GetEmployee() };
            return Json(data);
        }
        public JsonResult GetEmployeeDetailsReport(string Employee, string IPAddress)
        {
            DataTable dt = new DataTable();
            try
            {
                if (Employee != null)
                {
                    if (Employee != "-1")
                    {
                        if (search == "" || search == null)
                            search += " where employee_no = '" + Employee + "' ";
                        else
                            search += " and employee_no = '" + Employee + "' ";
                    }
                }
                if (IPAddress != null)
                {
                    if (IPAddress != "-1")
                    {
                        if (search == "" || search == null)
                            search += " where dhcpd_ip = '" + IPAddress + "' ";
                        else
                            search += " and dhcpd_ip = '" + IPAddress + "' ";
                    }
                }
                dt = Objquery.GetEmployeeDetailsReport(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetEmployeeDetailsReport Exception : " + ex.Message.ToString());
            }
            var data = new { emp_details = dt };
            return Json(data);
        }
        public JsonResult DeleteEmployee(string mac_address)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                if (mac_address.Length > 0)
                {
                    mac_address = mac_address.Substring(3);
                    querystatus = Objquery.DeleteEmployee(mac_address);
                }
                if (querystatus == 0)
                    result = "Employee details not deleted";
                else
                    result = "Employee details deleted successfully";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "DeleteEmployee Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = System.IO.File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                objcommon.WriteLog("csat_reportController", "GetDataTableFromExcel Total Column Count : " + ws.Dimension.End.Column);
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                        //objcommon.WriteLog("csat_reportController", "GetDataTableFromExcel Coloum Name : " + cell.Text);
                    }
                }
                return tbl;
            }
        }
        public IActionResult Employee_file_upload(IFormFile formFile)
        {
            DataTable dt = new DataTable();
            int querystatus = 0;
            string result = "";
            try
            {
                if (formFile == null || formFile.Length == 0)
                    return Json("File not selected");
                string fileExtension = Path.GetExtension(formFile.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                    return Json("File is not supported only excel file support");
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Upload");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string rootFolder = Path.Combine(uploadsFolder, Path.GetFileName(formFile.FileName));
                using (var fileStream = new FileStream(rootFolder, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
                dt = GetDataTableFromExcel(rootFolder, true);
                objcommon.WriteLog("csat_reportController", "Employee_file_upload datatable count : " + dt.Rows.Count);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() != "")
                    {
                        int cnt = 0;
                        cnt = Objquery.get_Employee_details_count(dt.Rows[i][0].ToString().Trim());
                        if (cnt == 0)
                        {
                            querystatus = Objquery.Add_Employee_details(dt.Rows[i][0].ToString().Trim(), dt.Rows[i][1].ToString().Trim(),
                              dt.Rows[i][2].ToString().Trim(), dt.Rows[i][3].ToString().Trim(), dt.Rows[i][4].ToString().Trim(),
                              dt.Rows[i][5].ToString().Trim(), dt.Rows[i][6].ToString().Trim(), dt.Rows[i][7].ToString().Trim(),
                              dt.Rows[i][8].ToString().Trim(), dt.Rows[i][9].ToString().Trim(), dt.Rows[i][10].ToString().Trim(),
                              dt.Rows[i][11].ToString().Trim(), dt.Rows[i][12].ToString().Trim(), dt.Rows[i][13].ToString().Trim(),
                              dt.Rows[i][14].ToString().Trim(), dt.Rows[i][15].ToString().Trim(), dt.Rows[i][16].ToString().Trim(),
                              dt.Rows[i][17].ToString().Trim(), dt.Rows[i][18].ToString().Trim(), dt.Rows[i][19].ToString().Trim(),
                              dt.Rows[i][20].ToString().Trim(), dt.Rows[i][21].ToString().Trim(), dt.Rows[i][22].ToString().Trim(),
                              dt.Rows[i][23].ToString().Trim(), dt.Rows[i][24].ToString().Trim(), dt.Rows[i][25].ToString().Trim(),
                              dt.Rows[i][26].ToString().Trim());
                        }
                    }
                }
                if (querystatus > 0)
                    result = "File uploaded successfully";
                else
                    result = "File uploaded fail";
                System.IO.File.Delete(rootFolder);
            }
            catch (Exception ex)
            {
                //return Json(new { status = "error " + ex.Message });
                objcommon.WriteLog("csat_reportController", "Employee_file_upload Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }

        #endregion

        #region Summary of Compliance Management
        public IActionResult report_summary_compliance_management()
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
        public JsonResult GetCompliance_Management_Summary_report(string ip, string systemname, string ouid)
        {
            string strsearch = "";
            string deviceid = "";

            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        strsearch += " where ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (systemname != null)
                {
                    if (systemname != "-1")
                    {
                        deviceid = Objquery.Get_device_idby_systemname(systemname);
                        strsearch += " and (device_id ='" + deviceid + "' or device_name like '%" + systemname + "%')";
                        //strsearch += " and device_id ='" + deviceid + "'";
                    }
                }
                if (ip != null)
                {
                    if (ip != "-1")
                    {
                        //deviceid = Objquery.Get_device_id("ip='" + ip + "'");
                        strsearch += " and ip_address ='" + ip + "'";
                    }
                }
                dt = Objquery.GetCompliance_Management_Summary_report(strsearch);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "GetCompliance_Management_Summary_report Exception : " + ex.Message.ToString());
            }
            var data = new { cm_data = dt };
            return Json(data);
        }

        #endregion

        #region Active Directory Data 
        public IActionResult report_active_directory_data()
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
        public JsonResult getaddata(string adtype)
        {
            try
            {
                if (adtype == "1")
                    dt = Objquery.getaddata_ou();
                if (adtype == "2")
                    dt = Objquery.getaddata_user();
                if (adtype == "3")
                    dt = Objquery.getaddata_device();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "getaddata Exception : " + ex.Message.ToString());
            }
            var data = new { ad_data = dt };
            return Json(data);
        }

        #endregion
    }
}