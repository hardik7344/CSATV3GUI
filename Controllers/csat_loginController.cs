using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OwnYITCSAT.DataAccessLayer;
using OwnYITCSAT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace OwnYITCSAT.Controllers
{

    public class csat_loginController : Controller
    {

        DBQueryHandler Objquery = new DBQueryHandler();
        OwnYITCommon Objcom = new OwnYITCommon();
        DataTable Dtlogin = new DataTable();
        DataTable dt_menu = new DataTable();
        DataTable dt = new DataTable();
        string user_id = "";
        string group_id = "";
        string user_name = "";
        internal string wsURL = "";
        public IHostingEnvironment hostingEnvironment;
        public csat_loginController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        //public IActionResult csat_login()
        //{
        //    return View();
        //}
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        string uploadpath = "";
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }
        public FileResult GetCaptchaImage()
        {
            //string text = Session["CAPTCHA"].ToString();
            string text = HttpContext.Session.GetString("CAPTCHA");
            if (text == null)
            {
                text = GetRandomText();
                HttpContext.Session.SetString("CAPTCHA", text);
            }
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }
        public IActionResult csat_login(login Objlogin)
        {
            DataTable dt = new DataTable();
            int accesscnt = 0;
            string passwordencrypt = "";
            bool bFlag = false;
            try
            {
                string str_cap = "";
                if (Objlogin.txtInput == null)
                {
                    str_cap = GetRandomText();
                    HttpContext.Session.SetString("CAPTCHA", str_cap);
                }
                else
                    str_cap = HttpContext.Session.GetString("CAPTCHA");

                string clientCaptcha = Objlogin.txtInput;
                string serverCaptcha = str_cap;
                try
                {
                    if (!clientCaptcha.Equals(serverCaptcha))
                    {
                        TempData["CaptchaMessage"] = "Please enter valid captcha";
                        str_cap = GetRandomText();
                        HttpContext.Session.SetString("CAPTCHA", str_cap);
                        return View();
                    }
                    str_cap = GetRandomText();
                    HttpContext.Session.SetString("CAPTCHA", str_cap);
                }
                catch (Exception)
                {
                    HttpContext.Session.Clear();
                }
                //decoding the string
                Objlogin.username = Base64Decode(Objlogin.username_enc);
                Objlogin.password = Base64Decode(Objlogin.password);

                Objlogin.username = Objlogin.username.ToLower();
                HttpContext.Session.SetString("username", Objlogin.username);
                HttpContext.Session.SetString("password", Objlogin.password);
                //passwordencrypt = Encrypt.EncryptString(Objlogin.username, Objlogin.password);
                passwordencrypt = Encrypt.EncryptText(Objlogin.password);
                bFlag = Objquery.doUserLogin(Objlogin.username, passwordencrypt);
                if (bFlag == true)
                {
                    Configuration.username = Objlogin.username;
                    user_id = Objquery.get_user_id(Objlogin.username);
                    group_id = Objquery.get_groupid(user_id);
                    Configuration.groupid = group_id;
                    user_name = Objquery.get_username(user_id);
                    int checklicense = Objquery.Check_expiryGui();
                    if (group_id == null)
                    {
                        TempData["GroupMessage"] = user_name + " user does not have any rights. Please Contact to Admin.";
                        return RedirectToAction("csat_login", "csat_login");
                    }
                    loadMenuItem();
                    Objcom.WriteLog("csat_loginController", "csat_login Start ");
                    HttpContext.Session.SetString("user", Objlogin.username);
                    if (OwnYITConstant.DT_MAIN_MENU == null)
                        return RedirectToAction("csat_login", "csat_login");
                    OwnYITConstant.CommanEndDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ");
                    Objcom.WriteLog("csat_loginController", "csat_login End ");
                    int cntLicense = Objquery.check_lecense_set();
                    if (cntLicense == 2)
                        if (checklicense > 0)
                        {
                            string ipaddress = HttpContext.Connection.RemoteIpAddress.ToString();
                            Objcom.WriteLog("csat_loginController", "csat_login IP Address : " + ipaddress.ToString());
                            if (ipaddress == "::1" || ipaddress == "127.0.0.1")
                            {
                                return RedirectToAction("csat_main_dashboard", "csat_main_dashboard");
                            }
                            else
                            {
                                accesscnt = Objquery.Check_useraccess_gui(ipaddress);
                                if (accesscnt > 0)
                                {
                                    return RedirectToAction("csat_main_dashboard", "csat_main_dashboard");
                                }
                                else
                                {
                                    TempData["Message"] = "Your system can't access GUI permission";
                                    return RedirectToAction("csat_login", "csat_login");
                                }
                            }

                        }
                        else
                        {
                            TempData["Message1"] = "Your License is Expired! Please contact to Tectona Softsolution Pvt Ltd";
                            return this.PartialView("License_expire");
                        }
                    else
                        return RedirectToAction("license_information", "csat_login");
                }
                else
                {
                    TempData["Message"] = "Please enter valid username and password!";
                    return RedirectToAction("csat_login", "csat_login");
                    //return RedirectToAction("csat_login", "csat_login", Objlogin);
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_loginController", "csat_login Exception : " + ex.Message.ToString());
            }

            return View();
        }
        public IActionResult license_information(IFormFile file)
        {
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
                //License upload code
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
                if (file.FileName != "reg.dat")
                {
                    return this.PartialView("InvalidLicenseUpload");
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
        public IActionResult logout()
        {
            HttpContext.Session.SetString("username", "");
            HttpContext.Session.SetString("password", "");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.password = HttpContext.Session.GetString("password");
            if (ViewBag.username == "" && ViewBag.password == "")
            {
                return RedirectToAction("csat_login", "csat_login");
            }
            return View();
        }
        public IActionResult session_time_out()
        {
            return View();
        }
        private string SetWSURL()
        {
            string strURL = "";

            string StrUrl = "/xml/APIUrl.xml";
            XMLHandler xml_handler = null;
            xml_handler = new XMLHandler(StrUrl);
            string svrIP = xml_handler.getValue("ServerIP");
            strURL = "https://" + svrIP + "/api/";
            return strURL;
        }

        public void loadMenuItem()
        {
            if (user_name.ToUpper() == "ADMIN")
            {
                OwnYITConstant.DT_MAIN_MENU = null;
                if (OwnYITConstant.DT_MAIN_MENU == null)
                {
                    OwnYITConstant.DT_MAIN_MENU = Objquery.GetMainMenu();
                }
                if (OwnYITConstant.DT_MAIN_MENU.Rows.Count == 0)
                {
                    OwnYITConstant.DT_MAIN_MENU = Objquery.GetMainMenu();
                }
            }
            else
            {
                OwnYITConstant.DT_MAIN_MENU = null;
                dt_menu = Objquery.get_menu_data(group_id);
                List<string[]> myTable = new List<string[]>();
                string menuid = "";
                foreach (DataRow dr in dt_menu.Rows)
                {
                    menuid += "," + dr[0].ToString();
                }
                if (menuid.Trim().Length > 1)
                    menuid = menuid.Substring(1);
                if (OwnYITConstant.DT_MAIN_MENU == null)
                {
                    OwnYITConstant.DT_MAIN_MENU = Objquery.GetMainMenu(menuid);
                }
                if (OwnYITConstant.DT_MAIN_MENU.Rows.Count == 0)
                {
                    OwnYITConstant.DT_MAIN_MENU = Objquery.GetMainMenu(menuid);
                }
            }
        }
        public JsonResult Get_Dashbord_chats()
        {
            try
            {
                user_id = Objquery.get_user_id(Configuration.username);
                group_id = Objquery.get_groupid(user_id);
                dt = Objquery.Get_Dashbord_chats(group_id);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_loginController", "Get_Dashbord_chats Exception : " + ex.Message.ToString());
            }
            var data = new { getdashbordchats = dt };
            return Json(data);
        }

        public JsonResult forgot_password(string username, string securityquestion, string securityanswer)
        {
            var querystatus = 0;
            var encryptsequrityanswer = "";
            username = username.ToLower();
            try
            {
                if (username != "")
                {
                    encryptsequrityanswer = Encrypt.EncryptText(securityanswer);
                    querystatus = Objquery.forgot_password(username, securityquestion, encryptsequrityanswer);
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_loginController", "forgot_password Exception : " + ex.Message.ToString());
            }
            return Json(querystatus);
        }
        public JsonResult create_password(string confirmpassword, string newpassword, string username)
        {
            var encryptpassword = "";
            string strreturn = "";
            try
            {
                int cnt1 = 0;

                //passwordencrypt = Objquery.get_password(userid);
                //encryptpasswoed = Encrypt.EncryptString(username11, oldpassword);

                encryptpassword = Encrypt.EncryptText(newpassword);
                cnt1 = Objquery.change_password(encryptpassword, username);
                if (cnt1 == 0)
                {
                    strreturn = "Password not set";
                }
                else
                {
                    strreturn = "Password set successfully";
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_loginController", "create_password Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        public JsonResult master_user()
        {
            string user_name = "";
            try
            {
                user_name = Objquery.Get_master_user();
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_loginController", "master_user Exception : " + ex.Message.ToString());
            }
            var data = new { getmasteruser = user_name };
            return Json(data);
        }
        public JsonResult master_password(string masterpassword, string masteruser)
        {
            var decryptmasterpsw = "";
            var decryptpsw = "";
            string mpassword = "";
            string userpassword = "";
            string strreturn = "";
            try
            {
                if (masterpassword != "")
                {
                    //var encryptmasterpsw = Encrypt.EncryptText(masterpassword);
                    mpassword = Objquery.masterpassword(masteruser);
                    decryptmasterpsw = Encrypt.DecryptText(mpassword);
                    if (masterpassword == decryptmasterpsw)
                    {
                        userpassword = Objquery.userpassword(masteruser);
                        decryptpsw = Encrypt.DecryptText(userpassword);
                        strreturn = masteruser + " : " + decryptpsw;
                    }
                    else
                    {
                        strreturn = "Incorrect master password.";
                    }
                }
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("csat_loginController", "master_password Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }


        public JsonResult AutoGenerateCustomerid()
        {
            Random rnd = new Random();
            Int64 custrnd1 = rnd.Next(20000000, 99999999);
            Int64 custrnd2 = rnd.Next(10000000, 99999999);
            Int64 custids = Int64.Parse(custrnd1.ToString() + custrnd2.ToString());

            Int64 installrnd1 = rnd.Next(10000000, 99999999);
            Int64 installrnd2 = rnd.Next(20000000, 99999999);
            Int64 installids = Int64.Parse(installrnd1.ToString() + installrnd2.ToString());
            var data = new { custid = custids, installid = installids };
            return Json(data);
        }
        public JsonResult AutoGenerateInstallid()
        {
            Random rnd = new Random();
            Int64 custrnd1 = rnd.Next(20000000, 99999999);
            Int64 custrnd2 = rnd.Next(10000000, 99999999);
            Int64 custids = Int64.Parse(custrnd1.ToString() + custrnd2.ToString());

            Int64 installrnd1 = rnd.Next(10000000, 99999999);
            Int64 installrnd2 = rnd.Next(20000000, 99999999);
            Int64 installids = Int64.Parse(installrnd1.ToString() + installrnd2.ToString());
            var data = new { custid = custids, installid = installids };
            return Json(data);
        }
        public JsonResult AutoGeneratelocationid()
        {
            Random rnd = new Random();
            Int64 locrnd1 = rnd.Next(30000000, 99999999);
            Int64 locrnd2 = rnd.Next(20000000, 99999999);
            Int64 locids = Int64.Parse(locrnd1.ToString() + locrnd2.ToString());

            var data = new { locid = locids };
            return Json(data);
        }
        string Path = null;

        public JsonResult licensing(string custid, string instid, string custname, string addr, string addr1, string addr2, string city, string pincode, string state, string country, string email, string contact, string mobno, string installationID, string locid, string loc, string loc_addr, string loc_addr1, string loc_addr2, string loc_city, string loc_pincode, string loc_state, string loc_country, string loc_email, string loc_contact, string loc_mobno, string partnerid, string siid, string chkproduct)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtCustomer = new DataTable("customer_details");
                dtCustomer.Columns.Add("CustomerID");
                dtCustomer.Columns.Add("InstallationID");
                dtCustomer.Columns.Add("Name");
                dtCustomer.Columns.Add("Address");
                dtCustomer.Columns.Add("Address1");
                dtCustomer.Columns.Add("Address2");
                dtCustomer.Columns.Add("City");
                dtCustomer.Columns.Add("PinCode");
                dtCustomer.Columns.Add("State");
                dtCustomer.Columns.Add("Country");
                dtCustomer.Columns.Add("Email");
                dtCustomer.Columns.Add("ContactNo");
                dtCustomer.Columns.Add("MobileNo");
                DataRow dr = dtCustomer.NewRow();
                dr["CustomerID"] = custid;
                dr["InstallationID"] = instid;
                dr["Name"] = custname;
                dr["Address"] = addr;
                dr["Address1"] = addr1;
                dr["Address2"] = addr2;
                dr["City"] = city;
                dr["PinCode"] = pincode;
                dr["State"] = state;
                dr["Country"] = country;
                dr["Email"] = email;
                dr["ContactNo"] = contact;
                dr["MobileNo"] = mobno;
                dtCustomer.Rows.Add(dr);
                ds.Tables.Add(dtCustomer);
                register_customer(dtCustomer, "CustomerID");
                DataTable dtLocation = new DataTable("location_details");
                dtLocation.Columns.Add("CustomerID");
                dtLocation.Columns.Add("LocationID");
                dtLocation.Columns.Add("InstallationID");
                dtLocation.Columns.Add("SecretInstallationIDProc");
                dtLocation.Columns.Add("Name");
                dtLocation.Columns.Add("Address");
                dtLocation.Columns.Add("Address1");
                dtLocation.Columns.Add("Address2");
                dtLocation.Columns.Add("City");
                dtLocation.Columns.Add("PinCode");
                dtLocation.Columns.Add("State");
                dtLocation.Columns.Add("Country");
                dtLocation.Columns.Add("Email");
                dtLocation.Columns.Add("ContactNo");
                dtLocation.Columns.Add("MobileNo");
                DataRow drLoc = dtLocation.NewRow();
                drLoc["CustomerID"] = custid;
                drLoc["LocationID"] = locid;
                drLoc["InstallationID"] = instid;
                drLoc["SecretInstallationIDProc"] = 1111111111111111;
                drLoc["Name"] = loc;
                drLoc["Address"] = loc_addr;
                drLoc["Address1"] = loc_addr1;
                drLoc["Address2"] = loc_addr2;
                drLoc["City"] = loc_city;
                drLoc["PinCode"] = loc_pincode;
                drLoc["State"] = loc_state;
                drLoc["Country"] = loc_country;
                drLoc["Email"] = loc_email;
                drLoc["ContactNo"] = loc_contact;
                drLoc["MobileNo"] = loc_mobno;
                dtLocation.Rows.Add(drLoc);
                ds.Tables.Add(dtLocation);
                register_customer(dtLocation, "LocationID");
                DataTable dtdistributo = new DataTable("distributor_details");
                dtdistributo.Columns.Add("CustomerID");
                dtdistributo.Columns.Add("LocationID");
                dtdistributo.Columns.Add("PartnerID");
                dtdistributo.Columns.Add("SIID");
                DataRow drdistributo = dtdistributo.NewRow();
                drdistributo["CustomerID"] = custid;
                drdistributo["LocationID"] = locid;
                drdistributo["PartnerID"] = partnerid;
                drdistributo["SIID"] = siid;

                dtdistributo.Rows.Add(drdistributo);
                ds.Tables.Add(dtdistributo);
                Objcom.WriteLog("licensing", "After get all data from screen");
                var allproduct = "";
                allproduct = chkproduct.Substring(1);
                Objcom.WriteLog("licensing", "Products selected : " + allproduct);
                DataTable dtProduct = new DataTable("Product_details");
                dtProduct.Columns.Add("CustomerID");
                dtProduct.Columns.Add("LocationID");
                dtProduct.Columns.Add("Product");
                string[] strproductarr = allproduct.Split(',');

                for (int i = 0; i < strproductarr.Length; i++)
                {
                    DataRow drProduct = dtProduct.NewRow();
                    drProduct["CustomerID"] = custid;
                    drProduct["LocationID"] = locid;
                    drProduct["Product"] = strproductarr[i];
                    dtProduct.Rows.Add(drProduct);
                }
                ds.Tables.Add(dtProduct);
                Objcom.WriteLog("licensing", "before get file path");
                //string strFileName = "/xml/tectonas.dat";
                //Path = OwnYITConstant.LINUX_WWW_PATH + strFileName;
                Path = OwnYITCommon.GetSystemPath() + "\\Assertyit\\Configuration\\reg.dat";
                Objcom.WriteLog("licensing", "File Path : " + Path);
                string strXML = ds.GetXml();
                strXML = "<?xml version=\"1.0\" standalone=\"yes\"?>" + "\n" + strXML;
                //System.IO.File.WriteAllText("D:\\original.xml", strXML);

                byte[] key_value = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x08, 0x03, 0x04, 0x01, 0x01, 0x09, 0x02, 0x06, 0x04, 0x01, 0x03, 0x04, 0x09, 0x07, 0x08, 0x00 };
                byte[] IV_value = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

                //string original = System.IO.File.ReadAllText("D:\\original.xml");
                //string original = System.IO.File.ReadAllText(strXML);
                using (Aes myAes = Aes.Create())
                {
                    Objcom.WriteLog("licensing", "Before encrypt data : " + strXML);
                    byte[] strEncryptedData = EncryptStringToBytes_Aes(strXML, key_value, IV_value);
                    Objcom.WriteLog("licensing", "After encrypt data");
                    System.IO.File.WriteAllBytes(Path, strEncryptedData);
                    Objcom.WriteLog("licensing", "After write file");
                }
                CreatedefaultLicenseFile("", instid, custid, locid, siid, partnerid, custname, loc);
            }
            catch (Exception ex)
            {
                Objcom.WriteLog("licensing", "Exception : " + ex.Message.ToString());
            }

            var str = "License Register Successfully!!";
            return Json(str);
        }
        public void CreatedefaultLicenseFile(string SecretInstallID, string VisibleInstallID, string CustomerID, string LocID, string SIID, string PatnerID, string CustomerName, string LocationName)
        {
            DataSet ds = new DataSet();
            DataTable dtCustomer = new DataTable();
            dtCustomer.Columns.Add("SecretInstallID");
            dtCustomer.Columns.Add("SecretInstallationIDProc");
            dtCustomer.Columns.Add("VisibleInstallID");
            dtCustomer.Columns.Add("CustomerID");
            dtCustomer.Columns.Add("LocID");
            dtCustomer.Columns.Add("SIID");
            dtCustomer.Columns.Add("PatnerID");
            dtCustomer.Columns.Add("CustomerName");
            dtCustomer.Columns.Add("LocationName");
            DataRow dr = dtCustomer.NewRow();
            dr["SecretInstallID"] = SecretInstallID;
            dr["SecretInstallationIDProc"] = 1111111111111111111;
            dr["VisibleInstallID"] = VisibleInstallID;
            dr["CustomerID"] = CustomerID;
            dr["LocID"] = LocID;
            dr["SIID"] = SIID;
            dr["PatnerID"] = PatnerID;
            dr["CustomerName"] = CustomerName;
            dr["LocationName"] = LocationName;
            dtCustomer.Rows.Add(dr);

            DataTable dtProduct = new DataTable();
            dtProduct.Columns.Add("ProductID");
            dtProduct.Columns.Add("ProductName");
            dtProduct.Columns.Add("Quantity");
            dtProduct.Columns.Add("StartDate");
            dtProduct.Columns.Add("EndDate");
            dtProduct.Columns.Add("Valid");
            dtProduct.Columns.Add("componentID");

            DataRow drServer = dtProduct.NewRow();
            drServer["ProductID"] = "1";
            drServer["ProductName"] = "OwnYITCSAT";
            drServer["Quantity"] = "1";
            drServer["StartDate"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            drServer["EndDate"] = System.DateTime.Now.AddDays(15).ToString("yyyy-MM-dd HH:mm:ss");
            drServer["Valid"] = "1";
            drServer["componentID"] = "1";
            dtProduct.Rows.Add(drServer);

            DataRow drClient = dtProduct.NewRow();
            drClient["ProductID"] = "1";
            drClient["ProductName"] = "OwnYITCSAT";
            drClient["Quantity"] = "25";
            drClient["StartDate"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            drClient["EndDate"] = System.DateTime.Now.AddDays(15).ToString("yyyy-MM-dd HH:mm:ss");
            drClient["Valid"] = "1";
            drClient["componentID"] = "2";
            dtProduct.Rows.Add(drClient);

            ds.Tables.AddRange(new DataTable[] { dtCustomer, dtProduct });
            ds.Tables[0].TableName = "Customer";
            ds.Tables[1].TableName = "Products";

            string strXML = ds.GetXml();
            strXML = "<?xml version=\"1.0\" standalone=\"yes\"?>" + "\n" + strXML;

            byte[] key_value = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x08, 0x03, 0x04, 0x01, 0x01, 0x09, 0x02, 0x06, 0x04, 0x01, 0x03, 0x04, 0x09, 0x07, 0x08, 0x00 };
            byte[] IV_value = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            string strConfigPath = OwnYITCommon.GetSystemPath() + "\\Assertyit\\Configuration";
            using (Aes myAes = Aes.Create())
            {
                byte[] strEncryptedData = EncryptStringToBytes_Aes(strXML, key_value, IV_value);
                System.IO.File.WriteAllBytes(strConfigPath + "\\defaultreg.dat", strEncryptedData);
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
        string strruturn = "";
        string strruturn1 = "";
        string str = "";
        public string online_customer_registration(string custid, string instid, string custname, string addr, string addr1, string addr2, string city, string pincode, string state, string country, string email, string contact, string mobno)
        {
            DataSet ds = new DataSet();
            DataTable dtCustomer = new DataTable("customer_details");
            dtCustomer.Columns.Add("CustomerID");
            dtCustomer.Columns.Add("InstallationID");
            dtCustomer.Columns.Add("Name");
            dtCustomer.Columns.Add("Address");
            dtCustomer.Columns.Add("Address1");
            dtCustomer.Columns.Add("Address2");
            dtCustomer.Columns.Add("City");
            dtCustomer.Columns.Add("PinCode");
            dtCustomer.Columns.Add("State");
            dtCustomer.Columns.Add("Country");
            dtCustomer.Columns.Add("Email");
            dtCustomer.Columns.Add("ContactNo");
            dtCustomer.Columns.Add("MobileNo");
            DataRow dr = dtCustomer.NewRow();
            dr["CustomerID"] = custid;
            dr["InstallationID"] = instid;
            dr["Name"] = custname;
            dr["Address"] = addr;
            dr["Address1"] = addr1;
            dr["Address2"] = addr2;
            dr["City"] = city;
            dr["PinCode"] = pincode;
            dr["State"] = state;
            dr["Country"] = country;
            dr["Email"] = email;
            dr["ContactNo"] = contact;
            dr["MobileNo"] = mobno;
            dtCustomer.Rows.Add(dr);
            ds.Tables.Add(dtCustomer);
            register_customer(dtCustomer, "CustomerID");
            string custstr = "";
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            { return true; };
            try
            {
                wsURL = SetWSURL();
                using (var client = new WebClient())
                {
                    DataTable customerdetails = ds.Tables["customer_details"];
                    if (customerdetails != null)
                    {
                        string custdet = JsonConvert.SerializeObject(customerdetails);
                        custstr = custdet.Replace("\"", "'");
                        client.Headers.Add("Content-Type:application/json");
                        strruturn = client.UploadString(wsURL + "RegisterCustomer", JsonConvert.SerializeObject(custstr));
                        str = "Registration Successfully!!";
                    }
                }
            }
            catch (Exception)
            {
                str = "Web API Connection Issue.";
            }
            return str;
        }
        string locstr = "";
        public string online_location_registration(string custid, string instid, string installationID, string locid, string loc, string loc_addr, string loc_addr1, string loc_addr2, string loc_city, string loc_pincode, string loc_state, string loc_country, string loc_email, string loc_contact, string loc_mobno)
        {
            DataSet ds = new DataSet();
            DataTable dtLocation = new DataTable("location_details");
            dtLocation.Columns.Add("CustomerID");
            dtLocation.Columns.Add("LocationID");
            dtLocation.Columns.Add("InstallationID");
            dtLocation.Columns.Add("Name");
            dtLocation.Columns.Add("Address");
            dtLocation.Columns.Add("Address1");
            dtLocation.Columns.Add("Address2");
            dtLocation.Columns.Add("City");
            dtLocation.Columns.Add("PinCode");
            dtLocation.Columns.Add("State");
            dtLocation.Columns.Add("Country");
            dtLocation.Columns.Add("Email");
            dtLocation.Columns.Add("ContactNo");
            dtLocation.Columns.Add("MobileNo");
            DataRow drLoc = dtLocation.NewRow();
            drLoc["CustomerID"] = custid;
            drLoc["LocationID"] = locid;
            drLoc["InstallationID"] = instid;
            drLoc["Name"] = loc;
            drLoc["Address"] = loc_addr;
            drLoc["Address1"] = loc_addr1;
            drLoc["Address2"] = loc_addr2;
            drLoc["City"] = loc_city;
            drLoc["PinCode"] = loc_pincode;
            drLoc["State"] = loc_state;
            drLoc["Country"] = loc_country;
            drLoc["Email"] = loc_email;
            drLoc["ContactNo"] = loc_contact;
            drLoc["MobileNo"] = loc_mobno;
            dtLocation.Rows.Add(drLoc);
            ds.Tables.Add(dtLocation);
            register_customer(dtLocation, "LocationID");
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            { return true; };
            try
            {
                wsURL = SetWSURL();
                using (var client = new WebClient())
                {
                    DataTable locationdetails = ds.Tables["location_details"];
                    if (locationdetails != null)
                    {
                        string locdet = JsonConvert.SerializeObject(locationdetails);
                        locstr = locdet.Replace("\"", "'");
                        client.Headers.Add("Content-Type:application/json");
                        strruturn1 = client.UploadString(wsURL + "RegisterLocation", JsonConvert.SerializeObject(locstr));
                        str = "Location registered successfully";
                    }
                }
            }
            catch (Exception)
            {
                str = "Web API Connection Issue.";
            }
            return str;
        }
        string disstr = "";
        public string online_partner_registration(string custid, string locid, string partnerid, string siid)
        {
            DataSet ds = new DataSet();
            DataTable dtdistributo = new DataTable("distributor_details");
            dtdistributo.Columns.Add("CustomerID");
            dtdistributo.Columns.Add("LocationID");
            dtdistributo.Columns.Add("PartnerID");
            dtdistributo.Columns.Add("SIID");
            DataRow drdistributo = dtdistributo.NewRow();
            drdistributo["CustomerID"] = custid;
            drdistributo["LocationID"] = locid;
            drdistributo["PartnerID"] = partnerid;
            drdistributo["SIID"] = siid;
            dtdistributo.Rows.Add(drdistributo);
            ds.Tables.Add(dtdistributo);

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            { return true; };
            try
            {
                wsURL = SetWSURL();
                using (var client = new WebClient())
                {
                    DataTable distibutordetails = ds.Tables["distributor_details"];
                    if (distibutordetails != null)
                    {
                        string disdet = JsonConvert.SerializeObject(distibutordetails);
                        disstr = disdet.Replace("\"", "'");
                        client.Headers.Add("Content-Type:application/json");
                        str = client.UploadString(wsURL + "SetDistributor", JsonConvert.SerializeObject(disstr));
                        str = "Parner data submitted successfully";
                    }
                }
            }
            catch (Exception)
            {
                str = "Web API Connection Issue.";
            }
            return str;
        }
        string prostr = "";
        string strruturn3 = "";
        public string online_product_registration(string custid, string locid, string chkproduct)
        {
            DataSet ds = new DataSet();
            var allproduct = "";
            allproduct = chkproduct.Substring(1);

            DataTable dtProduct = new DataTable("Product_details");
            dtProduct.Columns.Add("CustomerID");
            dtProduct.Columns.Add("LocationID");
            dtProduct.Columns.Add("Product");
            string[] strproductarr = allproduct.Split(',');

            for (int i = 0; i < strproductarr.Length; i++)
            {
                DataRow drProduct = dtProduct.NewRow();
                drProduct["CustomerID"] = custid;
                drProduct["LocationID"] = locid;
                drProduct["Product"] = strproductarr[i];
                dtProduct.Rows.Add(drProduct);
            }
            ds.Tables.Add(dtProduct);
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            { return true; };
            try
            {
                wsURL = SetWSURL();
                using (var client = new WebClient())
                {
                    DataTable productdetails = ds.Tables["Product_details"];
                    if (productdetails != null)
                    {
                        string proddet = JsonConvert.SerializeObject(productdetails);
                        prostr = proddet.Replace("\"", "'");
                        client.Headers.Add("Content-Type:application/json");
                        strruturn3 = client.UploadString(wsURL + "SetProduct", JsonConvert.SerializeObject(prostr));
                        str = "Product requirement submited successfully";
                    }
                }
            }
            catch (Exception)
            {
                str = "Web API Connection Issue.";
            }
            return str;
        }
        string custidstr = "";
        public string online_get_customerid(string custid)
        {

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            { return true; };
            try
            {
                wsURL = SetWSURL();
                using (var client = new WebClient())
                {
                    string cid = JsonConvert.SerializeObject(custid);
                    custidstr = cid.Replace("\"", "'");
                    client.Headers.Add("Content-Type:application/json");
                    strruturn3 = client.UploadString(wsURL + "GetCustomerID", JsonConvert.SerializeObject(custidstr));
                }
            }
            catch (Exception)
            {
                strruturn3 = "Web API Connection Issue.";
            }
            return strruturn3;
        }
        string locidstr = "";
        public string online_get_locationid(string custid, string locid)
        {
            DataSet ds = new DataSet();
            DataTable dtlocationinfo = new DataTable("Online_location_Details");
            dtlocationinfo.Columns.Add("CustomerID");
            dtlocationinfo.Columns.Add("LocationID");

            DataRow drdistributo = dtlocationinfo.NewRow();
            drdistributo["CustomerID"] = custid;
            drdistributo["LocationID"] = locid;
            dtlocationinfo.Rows.Add(drdistributo);
            ds.Tables.Add(dtlocationinfo);
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            { return true; };
            try
            {
                wsURL = SetWSURL();
                using (var client = new WebClient())
                {
                    DataTable locdetails = ds.Tables["Online_location_Details"];
                    if (locdetails != null)
                    {
                        string lid = JsonConvert.SerializeObject(locdetails);
                        locidstr = lid.Replace("\"", "'");
                        client.Headers.Add("Content-Type:application/json");
                        strruturn3 = client.UploadString(wsURL + "GetLocationID", JsonConvert.SerializeObject(locidstr));
                    }
                }
            }
            catch (Exception)
            {
                str = "Web API Connection Issue.";
            }
            return strruturn3;
        }
        private void register_customer(DataTable dtCustomer, string EntityType)
        {
            int entityCount = 0;
            int EntityTypeID = Objquery.Get_entity_typeid(EntityType);
            string customerid = dtCustomer.Rows[0]["CustomerID"].ToString();
            string installionid = dtCustomer.Rows[0]["InstallationID"].ToString();
            string entityid = "";
            entityCount = Objquery.GetEntity_count_withID(EntityTypeID.ToString());
            string CustomerName = dtCustomer.Rows[0]["Name"].ToString();
            if (entityCount <= 0)
            {
                if (EntityType == "LocationID")
                {
                    string locationid = dtCustomer.Rows[0]["LocationID"].ToString();
                    Objquery.insert_Entity_Data(locationid, CustomerName, EntityType);
                    entityid = locationid;
                }
                else
                {
                    Objquery.insert_Entity_Data(customerid, CustomerName, EntityType);
                    entityid = customerid;
                }
            }
            int columnStart = 3;

            if (EntityType == "LocationID")
                columnStart = 4;

            for (int iCol = columnStart; iCol < dtCustomer.Columns.Count; iCol++)
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    int info_typeID = Objquery.Getinfo_type_id(dtCustomer.Columns[iCol].ToString());
                    string entityValue = dtCustomer.Rows[0][dtCustomer.Columns[iCol].ToString()].ToString();
                    int adhoc_info_count = Objquery.get_count_adhoc_info(entityid, info_typeID.ToString());
                    Objcom.WriteLog("CustomerRegister", "Column : " + dtCustomer.Columns[iCol].ToString() + " , Value : " + entityValue + " , Count : " + adhoc_info_count.ToString());
                    if (adhoc_info_count <= 0)
                        Objquery.insert_adhoc_info(entityid, EntityTypeID.ToString(), info_typeID.ToString(), dtCustomer.Columns[iCol].ToString(), entityValue);
                    else
                        Objquery.update_adhoc_info(entityid, EntityTypeID.ToString(), info_typeID.ToString(), entityValue);
                }
            }
        }
        public JsonResult getcountry()
        {
            string filepath = System.IO.Path.Combine(hostingEnvironment.WebRootPath + "\\xml\\CountryState.csv");
            DataTable results = ConvertCSVtoDataTable(filepath);
            var distinctValues = results.AsEnumerable()
                        .Select(row => new
                        {
                            country = row.Field<string>("Country"),
                        })
                        .Distinct();
            var data = new { getcountry = distinctValues };
            return Json(data);
        }
        public JsonResult getstate(string strcountry)
        {
            string filepath = System.IO.Path.Combine(hostingEnvironment.WebRootPath + "\\xml\\CountryState.csv");
            DataTable results1 = ConvertCSVtoDataTable(filepath);
            var resultstate = (from tblstate in results1.AsEnumerable()
                               where tblstate.Field<string>("Country") == strcountry
                               select new
                               {
                                   state = tblstate.Field<string>("State")
                               }).Distinct().ToList();
            DataTable dt = ToDataTable(resultstate);
            var data = new { getstate = dt };
            return Json(data);
        }
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}