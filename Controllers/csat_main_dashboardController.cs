using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OwnYITCSAT.DataAccessLayer;
using OwnYITCSAT.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace OwnYITCSAT.Controllers
{
    public class csat_main_dashboardController : Controller
    {
        DBQueryHandler Objquery = new DBQueryHandler();
        OwnYITCommon Objcom = new OwnYITCommon();
        DataTable DtAlerts = new DataTable();
        DataTable DtPolicyAlerts = new DataTable();
        DataTable DtLastUser = new DataTable();
        DataTable DtDeviceStatus = new DataTable();
        DataTable DtPCOU = new DataTable();
        string PCdata = "";
        string Auditdata = "";
        string Policydata = "";
        string lastpolldata = "";
        string stackeddomain = "";
        string stackedalerttype = "";
        string startdate = "";
        string enddate = "";
        string strtime = "";
        DataTable dt = new DataTable();
        string startdate1 = OwnYITConstant.CommanStartDate;
        string enddate1 = OwnYITConstant.CommanEndDate;
        string Strtime = OwnYITConstant.CommanStrtime;
        string groupid = Configuration.groupid;
        string uploadpath = "";
        public IHostingEnvironment hostingEnvironment;
        public csat_main_dashboardController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult csat_main_dashboard(IFormFile file)
        {
            //Setting TLS 1.2 protocol
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
            {
                if (file == null)
                {
                    ViewBag.Message = string.Format("");
                    return View();
                }

                if (file.Length < 1)
                {
                    ViewBag.Message = string.Format("");
                    return View();
                }
                string[] allowedImageTypes = new string[] { "application/octet-stream", "text/plain" };
                if (!allowedImageTypes.Contains(file.ContentType.ToLower()))
                {
                    ViewBag.Message = string.Format("Unsupported Upload File.");
                    return View();
                }
                string FilePath = System.IO.Path.Combine(hostingEnvironment.WebRootPath + "\\xml");
                string FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + ".dat";
                string FullPath = System.IO.Path.Combine(FilePath, FileName);
                using (FileStream FileStream2 = new FileStream(FullPath, FileMode.Create))
                {
                    file.CopyTo(FileStream2);
                    ViewBag.Message = string.Format("Data Upload Successfully.");
                }
                uploadpath = FullPath;
                Objcom.WriteLog("License", "uploadpath : " + uploadpath);
                DataSet ds = new DataSet();
                try
                {
                    byte[] key_value = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x08, 0x03, 0x04, 0x01, 0x01, 0x09, 0x02, 0x06, 0x04, 0x01, 0x03, 0x04, 0x09, 0x07, 0x08, 0x00 };
                    byte[] IV_value = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

                    try
                    {
                        string strDestPath = OwnYITCommon.GetSystemPath() + "\\Assertyit\\Configuration\\reg.dat";
                        System.IO.File.Copy(uploadpath, strDestPath, true);
                    }
                    catch (Exception ex)
                    {
                        Objcom.WriteLog("License", "uploadpath before : " + ex.Message.ToString());
                    }

                    byte[] EncryptedData = System.IO.File.ReadAllBytes(uploadpath);
                    string strDecryptedData = "";
                    using (Aes myAes = Aes.Create())
                    {
                        strDecryptedData = DecryptStringFromBytes_Aes(EncryptedData, key_value, IV_value);
                        //System.IO.File.WriteAllText("D:\\originalDecrypt.dat", strDecryptedData);
                    }
                    Objcom.WriteLog("License", "Decrypt data : " + strDecryptedData);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(strDecryptedData);
                    XmlNodeReader xmlReader = new XmlNodeReader(doc);
                    ds.ReadXml(xmlReader);

                    DataTable customerdetails = ds.Tables["Customer"];
                    DataTable productdetails = ds.Tables["Products"];
                    string SecretInstallID = customerdetails.Rows[0]["SecretInstallID"].ToString();
                    string VisibleInstallID = customerdetails.Rows[0]["VisibleInstallID"].ToString();
                    string CustomerID = customerdetails.Rows[0]["CustomerID"].ToString();
                    string LocID = customerdetails.Rows[0]["LocID"].ToString();
                    string SIID = customerdetails.Rows[0]["SIID"].ToString();
                    string PatnerID = customerdetails.Rows[0]["PatnerID"].ToString();
                    string CustomerName = customerdetails.Rows[0]["CustomerName"].ToString();
                    string LocationName = customerdetails.Rows[0]["LocationName"].ToString();
                    int recordCount = 0;
                    Objcom.WriteLog("License", "After data to convert into datatable");
                    recordCount = Objquery.get_Entity_Entry_Count(CustomerID, CustomerName, "CustomerID");
                    if (recordCount <= 0)
                        Objquery.insert_Entity_Data(CustomerID, CustomerName, "CustomerID");
                    recordCount = 0;

                    recordCount = Objquery.get_Entity_Entry_Count(LocID, LocationName, "LocationID");
                    if (recordCount <= 0)
                        Objquery.insert_Entity_Data(LocID, LocationName, "LocationID");

                    recordCount = 0;
                    recordCount = Objquery.get_Entity_Entry_Count(SecretInstallID, "", "SecretInstallID");
                    if (recordCount <= 0)
                        Objquery.insert_Entity_Data(SecretInstallID, "", "SecretInstallID");

                    recordCount = 0;
                    recordCount = Objquery.get_Entity_Entry_Count(VisibleInstallID, "", "VisibleInstallID");
                    if (recordCount <= 0)
                        Objquery.insert_Entity_Data(VisibleInstallID, "", "VisibleInstallID");

                    recordCount = 0;
                    recordCount = Objquery.get_Entity_Entry_Count(SIID, "", "SIID");
                    if (recordCount <= 0)
                        Objquery.insert_Entity_Data(SIID, "", "SIID");

                    recordCount = 0;
                    recordCount = Objquery.get_Entity_Entry_Count(PatnerID, "", "PatnerID");
                    if (recordCount <= 0)
                        Objquery.insert_Entity_Data(PatnerID, "", "PatnerID");
                    Objcom.WriteLog("License", "Before enter license data");
                    for (int i = 0; i < productdetails.Rows.Count; i++)
                    {
                        int intCount = 0;
                        intCount = Objquery.Get_component_count(productdetails.Rows[i]["ProductID"].ToString(), productdetails.Rows[i]["componentID"].ToString());
                        if (intCount <= 0)
                            Objquery.insert_license_product(productdetails.Rows[i]["ProductID"].ToString(), productdetails.Rows[i]["Quantity"].ToString(), productdetails.Rows[i]["componentID"].ToString(), productdetails.Rows[i]["StartDate"].ToString(), productdetails.Rows[i]["EndDate"].ToString(), productdetails.Rows[i]["Valid"].ToString());
                        else
                            Objquery.update_License_date(productdetails.Rows[i]["ProductID"].ToString(), productdetails.Rows[i]["componentID"].ToString(), productdetails.Rows[i]["Quantity"].ToString(), productdetails.Rows[i]["StartDate"].ToString(), productdetails.Rows[i]["EndDate"].ToString(), productdetails.Rows[i]["Valid"].ToString());
                    }
                    //TempData["Message"] = "Upload File Successfully.";                
                    Objcom.WriteLog("License", "After enter license data");
                    return this.PartialView("License_verification");

                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("License", "Exception : " + ex.Message.ToString());
                }
                return View();
            }
        }
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {

            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // strReturn = Encoding.UTF8.GetString(encrypted, 0, encrypted.Length);
            return encrypted;
        }
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
        //public IActionResult csat_main_dashboard()
        //{
        //    ViewBag.username = HttpContext.Session.GetString("username");
        //    ViewBag.password = HttpContext.Session.GetString("password");
        //    if (ViewBag.username == null && ViewBag.password == null)
        //    {
        //        return RedirectToAction("session_time_out", "csat_login");
        //    }
        //    else if (ViewBag.username == "" && ViewBag.password == "")
        //    {
        //        return RedirectToAction("csat_login", "csat_login");
        //    }
        //    else
        //        return View();
        //}
        public string viewusername()
        {
            string str = Configuration.username;
            return str;
        }
        public JsonResult GetDashboardCharts(string NoOfDays)
        {
            try
            {
                //enddate = HttpContext.Session.GetString("LoginDate");
                enddate = OwnYITConstant.CommanEndDate;
                if (enddate == "" || enddate == null)
                {
                    enddate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ");
                    OwnYITConstant.CommanEndDate = enddate;
                }
                if (NoOfDays == "1")
                {
                    startdate = System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                    strtime = " and timestamp between '" + startdate + "' and '" + enddate + "'";
                    //HttpContext.Session.SetString("Charttimestamp", " between '" + startdate + "' and '" + enddate + "'");
                }
                else if (NoOfDays == "2")
                {
                    startdate = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 00:00:00";
                    strtime = " and timestamp between '" + startdate + "' and '" + enddate + "'";
                    //HttpContext.Session.SetString("Charttimestamp", " between '" + startdate + "' and '" + enddate + "'");
                }
                else if (NoOfDays == "30")
                {
                    startdate = System.DateTime.Now.AddDays(-31).ToString("yyyy-MM-dd") + " 00:00:00";
                    strtime = " and timestamp between '" + startdate + "' and '" + enddate + "'";
                    //HttpContext.Session.SetString("Charttimestamp", " between '" + startdate + "' and '" + enddate + "'");
                }
                OwnYITConstant.CommanStartDate = startdate;
                // Policy Violation
                try
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Policy Violation Chart Start");
                    //DtPolicyAlerts = Objquery.BindPolicyAlert(strtime);
                    DtPolicyAlerts = Objquery.BindComplianceChart();
                    if (DtPolicyAlerts.Rows.Count != 0)
                    {
                        List<piechart> Policylistdata = Objcom.ConvertDataTable<piechart>(DtPolicyAlerts);
                        Policydata = JsonConvert.SerializeObject(Policylistdata);
                    }
                    else
                    {
                        List<piechart> Policylistdata = Objcom.ConvertDataTable<piechart>(DtPolicyAlerts);
                        Policydata = JsonConvert.SerializeObject(Policylistdata);
                    }

                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Policy Violation Chart Alert Count : " + Policydata);
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Policy Violation Chart End");
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Policy Violation Chart Exception : " + ex.Message.ToString());
                }

                // Audit Trail
                try
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Audit Trail Chart Start");
                    DtAlerts = Objquery.BindAssetAlert(strtime);
                    dt = Objquery.GroupBindAssetAlert(groupid);
                    if (DtAlerts.Rows.Count != 0)
                    {
                        DataTable dtAudit = (from dtalert in DtAlerts.AsEnumerable()
                                             join dtgroup in dt.AsEnumerable()
                                             on dtalert["Label"].ToString() equals dtgroup["menu_name"].ToString()
                                             where dtgroup.Field<string>("menu_name") == dtalert.Field<string>("Label")
                                             select dtalert
                                              ).CopyToDataTable();
                        List<piechart> Audtlistdata = Objcom.ConvertDataTable<piechart>(dtAudit);
                        Auditdata = JsonConvert.SerializeObject(Audtlistdata);
                    }
                    else
                    {
                        List<piechart> Audtlistdata = Objcom.ConvertDataTable<piechart>(DtAlerts);
                        Auditdata = JsonConvert.SerializeObject(Audtlistdata);
                    }

                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Audit Trail Chart Alert Count : " + Auditdata);
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Audit Trail Chart End");
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Audit Trail Chart Exception : " + ex.Message.ToString());
                }

                // Most affected Domain
                try
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Most affected Domain Chart Start");
                    try
                    {
                        enddate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ");
                        dt = Objquery.BindMostAffectedDomain(startdate, enddate);
                        List<StackedBarchart> stackedlistdaomain = Objcom.ConvertDataTable<StackedBarchart>(dt);
                        stackeddomain = JsonConvert.SerializeObject(stackedlistdaomain);
                    }
                    catch (Exception ex)
                    {
                        Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Most affected Domain Chart GetData Exception : " + ex.Message.ToString());
                    }
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Most affected Domain Data :" + stackeddomain);
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Most affected Domain Chart End");
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Most affected Domain Chart Exception : " + ex.Message.ToString());
                }
                //  Last Poll Time

                try
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Last Poll Time Chart Start");
                    DtLastUser = Objquery.Get_LastUserLogin();
                    List<piechart> lastpolllistdata = Objcom.ConvertDataTable<piechart>(DtLastUser);
                    lastpolldata = JsonConvert.SerializeObject(lastpolllistdata);
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Last Poll Time Data :" + lastpolldata);
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Last Poll Time Chart End");
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Last Poll Time Chart Exception : " + ex.Message.ToString());
                }

                // PC Connectivity
                try
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard PC Connectivity Chart Start");
                    DtDeviceStatus = Objquery.Get_DeviceStatus();
                    List<piechart> PClistdata = Objcom.ConvertDataTable<piechart>(DtDeviceStatus);
                    PCdata = JsonConvert.SerializeObject(PClistdata);
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard PC Connectivity Chart Data :" + PCdata);
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard PC Connectivity Chart End");
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard PC Connectivity Chart Exception : " + ex.Message.ToString());
                }
                // CSAT Audit Trail

                try
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard CSAT Audit Trail Chart Start");
                    dt = Objquery.BindCSATAuditTrail(startdate, enddate);
                    DataTable dt_group = Objquery.GroupBindCSATAuditTrail(groupid);
                    if (dt.Rows.Count != 0)
                    {
                        DataTable dtCSAT = (from dtaudit in dt.AsEnumerable()
                                            join dtgroup in dt_group.AsEnumerable()
                                            on dtaudit["Alert_type"].ToString() equals dtgroup["menu_name"].ToString()
                                            where dtgroup.Field<string>("menu_name") == dtaudit.Field<string>("Alert_type")
                                            select dtaudit
                                              ).CopyToDataTable();
                        List<SatckedBarchart1> stackedlistaudit = Objcom.ConvertDataTable<SatckedBarchart1>(dtCSAT);
                        stackedalerttype = JsonConvert.SerializeObject(stackedlistaudit);
                    }
                    else
                    {
                        List<SatckedBarchart1> stackedlistaudit = Objcom.ConvertDataTable<SatckedBarchart1>(dt);
                        stackedalerttype = JsonConvert.SerializeObject(stackedlistaudit);
                    }
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard CSAT Audit Trail Data" + stackedalerttype);
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard CSAT Audit Trail Chart End");
                }
                catch (Exception ex)
                {
                    Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard CSAT Audit Trail Chart Exception : " + ex.Message.ToString());
                }

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "csat_main_dashboard Exception : " + ex.Message.ToString());
            }
            OwnYITConstant.CommanStrtime = strtime;
            //HttpContext.Session.SetString("Strtime", strtime);
            var data = new { policydata1 = Policydata, pcdata1 = PCdata, auditdata1 = Auditdata, lastpolldata1 = lastpolldata, stacked1 = stackeddomain, stackedalert = stackedalerttype };
            return Json(data);
        }
        // policy alert
        public JsonResult Getpolicychartpopup(string ouid, string system, string ip, string categorytype)
        {
            try
            {
                // string search = "";
                string strFieldSearch = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        strFieldSearch += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                //strFieldSearch += "and device_timestamp between '" + startdate1 + "' and '" + enddate1 + "' ";
                //dt = Objquery.Getpolicychartpopup(strFieldSearch, categorytype);
                if (categorytype == "10001")
                    dt = Objquery.GetCompliancechartpopup(strFieldSearch, "win_password='NO'");
                else if (categorytype == "10002")
                    dt = Objquery.GetCompliancechartpopup(strFieldSearch, "screen_saver_psw='NO'");
                else if (categorytype == "10003")
                    dt = Objquery.GetCompliancechartpopup(strFieldSearch, "av_installed not like 'YES%'");
                else if (categorytype == "10004")
                    dt = Objquery.GetCompliancechartpopup(strFieldSearch, "pirated_unactivated_os ='YES'");
                else if (categorytype == "10005")
                    dt = Objquery.GetCompliancechartpopup(strFieldSearch, "usb_port_enabled ='YES'");
                else if (categorytype == "10006")
                    dt = Objquery.GetCompliancechartpopup(strFieldSearch, "firewall_installed_enabled <> 'Enabled'");
                else if (categorytype == "10007")
                    dt = Objquery.GetCompliancechartpopup(strFieldSearch, "encryption_tool_installed_sdesk = 'NO' and encryption_tool_installed_vcrypt='NO'");
                else if (categorytype == "10008")
                    dt = Objquery.GetCompliancechartpopupDomain(strFieldSearch);
                else if (categorytype == "10009")
                    dt = Objquery.GetCompliancechartpopupShare(strFieldSearch);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "Getpolicychartpopup Exception : " + ex.Message.ToString());
            }
            var data = new { getpolicydata = dt };
            return Json(data);
        }
        // hardware alert
        public JsonResult GetHWaudittrail(string ouid, string system, string ip, string categorytype)
        {
            try
            {
                // string search = "";
                string strFieldSearch = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        strFieldSearch += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                //strFieldSearch += "and device_timestamp between '" + startdate1 + "' and '" + enddate1 + "' ";
                strFieldSearch += "and servertime between '" + startdate1 + "' and '" + enddate1 + "' ";
                dt = Objquery.Get_HWaudittrail(strFieldSearch, categorytype);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetHWaudittrail Exception : " + ex.Message.ToString());
            }
            var data = new { gethwaudittraildata = dt };
            return Json(data);

            //DataTable dt1 = new DataTable();
            //dt1.Columns.Add("message");
            //DataRow dr = dt1.NewRow();
            //try
            //{
            //    // string search = "";
            //    if (HttpContext.Session.GetString("Charttimestamp") != null)
            //    {
            //        string strFieldSearch = "";
            //        if (ouid != null)
            //        {
            //            if (ouid != "-1")
            //                strFieldSearch += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
            //        }
            //        if (system != null)
            //        {
            //            if (system != "-1")
            //                strFieldSearch += " and dl.device_name ='" + system + "'";
            //        }
            //        if (ip != null)
            //        {
            //            if (ip != "-1")
            //                strFieldSearch += " and dl.ip ='" + ip + "'";
            //        }
            //        strFieldSearch += "and device_timestamp " + HttpContext.Session.GetString("Charttimestamp");
            //        dt = Objquery.Get_HWaudittrail(strFieldSearch, categorytype);
            //        dr["message"] = "";
            //    }
            //    else
            //    {
            //        dr["message"] = "Session Expired";   
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Objcom.WriteLog("csat_main_dashboardController", "GetHWaudittrail Exception : " + ex.Message.ToString());
            //}
            //dt1.Rows.Add(dr);
            //var data = new { gethwaudittraildata = dt, dtmessage = dt1 };
            //return Json(data);
        }

        public JsonResult GetOUID(string ouname)
        {
            string ouid1 = "";
            try
            {
                if (ouname != null)
                {
                    ouid1 = Objquery.get_ouid(ouname);
                }

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetOUID Exception : " + ex.Message.ToString());
            }
            var data = new { ouid = ouid1 };
            return Json(data);
        }

        public JsonResult GetRawDataLeakage(string ip, string system, string ouid)
        {
            string ou_id = "";
            try
            {

                // string search = "";
                string strFieldSearch = "";
                if (ouid != null && ouid != "[object NodeList]")
                {
                    ou_id = Objquery.get_ouid(ouid);
                    if (ouid != "-1")
                        strFieldSearch += " and ou_id in(" + Objquery.Get_ParentOu_id(ou_id) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                string enddate = DateTime.Now.ToString("yyyy-MM-dd" + " 23:59:59");
                strFieldSearch += "and server_time between '" + startdate1 + "' and '" + enddate + "'";
                //strFieldSearch += "and e108servertime " + HttpContext.Session.GetString("Charttimestamp");
                dt = Objquery.GetRawDataLeakagechartdata(strFieldSearch);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetRawDataLeakage Exception : " + ex.Message.ToString());
            }
            var data = new { getdataleakagedata = dt };
            return Json(data);
        }

        public JsonResult GetPrinterUsage(string ip, string system, string ouid, string printer)
        {
            string ou_id = "";
            try
            {
                string strFieldSearch = "";
                if (ouid != null && ouid != "[object NodeList]")
                {
                    ou_id = Objquery.get_ouid(ouid);
                    if (ouid != "-1")
                        strFieldSearch += " and ou_id in(" + Objquery.Get_ParentOu_id(ou_id) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                if (printer != null)
                {
                    if (printer != "-1")
                        strFieldSearch += " and printer_name ='" + printer + "'";
                }
                string enddate = DateTime.Now.ToString("yyyy-MM-dd" + " 23:59:59");
                strFieldSearch += "and device_timestamp between '" + startdate1 + "' and '" + enddate + "'";
                //strFieldSearch += "and device_timestamp " + HttpContext.Session.GetString("Charttimestamp");
                dt = Objquery.GetPrinterUsagechartdata(strFieldSearch);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetPrinterUsage Exception : " + ex.Message.ToString());
            }
            var data = new { printerusagedata = dt };
            return Json(data);
        }

        public JsonResult GetModemUsage(string ip, string system, string ouid)
        {
            string ou_id = "";
            try
            {
                string strFieldSearch = "";
                if (ouid != null && ouid != "[object NodeList]")
                {
                    ou_id = Objquery.get_ouid(ouid);
                    if (ouid != "-1")
                        strFieldSearch += " and ou_id in(" + Objquery.Get_ParentOu_id(ou_id) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                string enddate = DateTime.Now.ToString("yyyy-MM-dd" + " 23:59:59");
                strFieldSearch += "and device_timestamp between '" + startdate1 + "' and '" + enddate + "'";
                //strFieldSearch += "and device_timestamp " + HttpContext.Session.GetString("Charttimestamp");
                dt = Objquery.GetModemUsagechartdata(strFieldSearch);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetModemUsage Exception : " + ex.Message.ToString());
            }
            var data = new { modemusagedata = dt };
            return Json(data);
        }

        public JsonResult GetRemovableMedia(string ip, string system, string ouid)
        {
            string ou_id = "";
            try
            {
                string strFieldSearch = "";
                if (ouid != null && ouid != "[object NodeList]")
                {
                    ou_id = Objquery.get_ouid(ouid);
                    if (ouid != "-1" && ou_id != null)
                        strFieldSearch += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ou_id) + ")";
                    if (ou_id == null && ouid != null)
                        strFieldSearch += " and dl.ou_node_name='" + ouid + "'";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name like '%" + system + "%'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                string enddate = DateTime.Now.ToString("yyyy-MM-dd" + " 23:59:59");
                strFieldSearch += " and ((e110starttime > '" + startdate1 + "' and e110starttime < '" + enddate + "') or (e110endtime > '" + startdate1 + "' and e110endtime < '" + enddate + "'))";
                //strFieldSearch += "and e110starttime >= '" + startdate1 + "' and e110endtime <= '" + enddate1 + "'";
                //strFieldSearch += "and e110serverstarttime between '" + startdate1 + "' and '" + enddate1 + "'";
                dt = Objquery.GetRemovableMediachartdata(strFieldSearch, startdate1, enddate);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetRemovableMedia Exception : " + ex.Message.ToString());
            }
            var data = new { removabledata = dt };
            return Json(data);
        }

        // PC Connectivity chart data 


        //public JsonResult Get_syatemname_ip1()
        //{
        //    var data = new { systemip = Objquery.Get_syatemname_ip1() };
        //    return Json(data);
        //}
        public JsonResult GetPCConnectivitychartdata(string ip, string system, string ouid, string status)
        {
            try
            {
                // string search = "";
                string strFieldSearch = "";
                if (ouid != null && ouid != "[object NodeList]")
                {
                    if (ouid != "-1")
                        strFieldSearch += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                //else
                //{
                //    if (ouid == null)
                //    {
                //        if (ouid == "-1")
                //            strFieldSearch += "";
                //    }
                //    if (status == "Linked Equipment" || status == "Not Linked Detail")
                //    {
                //        if (ouid != null)
                //        {
                //            if (ouid != "-1")
                //                strFieldSearch += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(" ") + ")";
                //        }
                //    }

                //}
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name like '%" + system + "%'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                //if (status != null)
                //{
                //    if (status != "-1")
                //    {
                if (status == "Linked Equipment")
                {
                    strFieldSearch += " and dm.status=1";
                    dt = Objquery.GetPCConnectivitychartdata(strFieldSearch);
                }
                if (status == "Not Linked Detail")
                {
                    strFieldSearch += " and dm.status in (0,2,3) and dm.device_id not in (select dm.device_id from device_master dm left outer join node_user_info nui on dm.device_id=nui.device_id where device_mode in (1,2,3)   and status not in (-1,90) and status = 0 and device_timestamp > CONVERT(date, getdate(),103)) ";
                    dt = Objquery.GetPCConnectivitychartdata(strFieldSearch);
                }
                if (status == "Not Monitoring Device Detail")
                {
                    //strFieldSearch += "";
                    dt = Objquery.GetPCConnectivitychartdata1(strFieldSearch);
                }
                if (status == "Today Connected Device")
                {
                    strFieldSearch += " and dm.device_id in (select dm.device_id from device_master dm left outer join node_user_info nui on dm.device_id=nui.device_id where device_mode in (1,2,3)   and status not in (-1,90) and status = 0 and device_timestamp > CONVERT(date, getdate(),103))";
                    dt = Objquery.GetPCConnectivitychartdata(strFieldSearch);
                }

            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetPCConnectivitychartdata Exception : " + ex.Message.ToString());
            }
            var data = new { getpcconnectivitydata = dt };
            return Json(data);
        }


        // Last Polltime chart data

        public JsonResult GetLastPollTimechartdata(string ip, string system, string lastpolldays)
        {
            try
            {
                string strFieldSearch = "";
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and T.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        strFieldSearch += " and T.ip like '%" + ip + "%'";
                }
                if (lastpolldays != null && lastpolldays != "-1")
                {
                    strFieldSearch += Objquery.Agent_info_Date(lastpolldays);
                }
                dt = Objquery.Get_AgentInfo("", strFieldSearch);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetLastPollTimechartdata Exception : " + ex.Message.ToString());
            }
            var data = new { getlastpolltimedata = dt };
            return Json(data);
        }

        // CSAT Audit Chart Data

        public JsonResult GetCSATAuditProcesschartdata(string ip, string system, string ouid, string status)
        {
            try
            {
                // string search = "";
                string strFieldSearch = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        strFieldSearch += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                if (status != null)
                {
                    if (status == "2")
                        strFieldSearch += " and process_name not in (select typename from authorizedata )";
                    else
                        strFieldSearch += " and pa.datatype = 2 and pa.authorizetype ='" + status + "'";
                }
                dt = Objquery.GetCSATAuditProcesschartdata1(strFieldSearch);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetCSATAuditProcesschartdata Exception : " + ex.Message.ToString());
            }
            var data = new { getcsatauditprocesschartdata = dt };
            return Json(data);
        }


        public JsonResult GetCSATAuditSoftwarechartdata(string ip, string system, string ouid, string status)
        {
            try
            {
                // string search = "";
                string strFieldSearch = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        strFieldSearch += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                if (status != null)
                {
                    if (status == "2")
                        strFieldSearch += " and software not in (select typename from authorizedata )";
                    else
                        strFieldSearch += " and pa.datatype = 1 and pa.authorizetype ='" + status + "'";
                }
                dt = Objquery.GetCSATAuditSoftwarechartdata1(strFieldSearch);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetCSATAuditSoftwarechartdata Exception : " + ex.Message.ToString());
            }
            var data = new { getcsatauditsoftwarechartdata = dt };
            return Json(data);
        }


        public JsonResult GetCSATAuditUserchartdata(string ip, string system, string ouid, string status)
        {
            try
            {
                // string search = "";
                string strFieldSearch = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        strFieldSearch += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (system != null)
                {
                    if (system != "-1")
                        strFieldSearch += " and dl.device_name ='" + system + "'";
                }
                if (ip != null)
                {
                    if (ip != "-1")
                        strFieldSearch += " and dl.ip like '%" + ip + "%'";
                }
                if (status != null)
                {
                    if (status != "-1")
                    {
                        if (status == "1")
                            strFieldSearch += "and ut.username='Administrator'";
                        else if (status == "0")
                            strFieldSearch += "and ut.username<>'Administrator'";
                        else if (status == "2")
                            strFieldSearch += "and ut.username = null";
                    }
                }
                dt = Objquery.GetCSATAuditUserchartdata1(strFieldSearch);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "GetCSATAuditUserchartdata Exception : " + ex.Message.ToString());
            }
            var data = new { getcsataudituserchartdata = dt };
            return Json(data);
        }

        //public JsonResult Get_systemname_ip()
        //{
        //    var data = new { systemip = Objquery.Get_systemname_ip() };
        //    return Json(data);
        //}

        public JsonResult Get_alert()
        {
            try
            {
                string user_id = Objquery.get_user_id(Configuration.username);
                dt = Objquery.Get_alert(user_id);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "Get_alert Exception : " + ex.Message.ToString());
            }
            var data = new { getalert = dt };
            return Json(data);
        }
        public JsonResult Get_count()
        {
            int count = 0;
            try
            {
                string user_id = Objquery.get_user_id(Configuration.username);
                count = Objquery.Get_AlertCount(user_id);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "AlertCount Exception : " + ex.Message.ToString());
            }

            var data = new { alertcount = count };
            return Json(data);
        }
        public JsonResult Delete_alert()
        {
            int deletealert = 0;
            int delete_alert = 0;
            try
            {
                deletealert = Objquery.deletealert();
                delete_alert = Objquery.delete_alert();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_main_dashboardController", "Delete_alert Exception : " + ex.Message.ToString());
            }

            var data = new { alert_delete = deletealert, alert_delete104 = delete_alert };
            return Json(data);
        }
    }
}