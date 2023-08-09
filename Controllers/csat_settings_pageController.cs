using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using OwnYITCSAT.DataAccessLayer;
using System.IO;
using OwnYITCSAT.Models;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace OwnYITCSAT.Controllers
{
    public class csat_settings_pageController : Controller
    {
        DBQueryHandler Objquery = new DBQueryHandler();
        OwnYITCommon objcommon = new OwnYITCommon();
        DataTable dtsubtosubmenu = new DataTable();
        //  Jsondata abc = new Jsondata();        
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        int querystatus = 0;
        string result = "";
        int cnt = 0;
        string strquery;
        string searchdata = "";
        string hdnram;
        string hdnhdd;
        string hdncd;
        string hdnlan;
        string hdnwifi;
        string hdnav;
        public IActionResult csat_settings_page(int id)
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
                    TempData["notselect"] = "";
                    if (OwnYITConstant.DT_SETTINGS_MENU == null)
                    {
                        OwnYITConstant.DT_SETTINGS_MENU = Objquery.Get_SubMenu(id);
                    }
                    if (id.ToString() == null)
                        return RedirectToAction("csat_login", "csat_login");
                    if (OwnYITConstant.DT_SETTINGS_MENU == null)
                        return RedirectToAction("csat_login", "csat_login");
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatsettingController", "csat_settings_page Exception : " + ex.Message.ToString());
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
                    OwnYITConstant.DT_SETTINGS_SUB_MENU = GetCssData(dtsubtosubmenu);
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatsettingController", "get_Submenu Exception : " + ex.Message.ToString());
                }
            return View("csatsetting");

        }
        public DataTable GetCssData(DataTable dtcss)
        {
            try
            {
                for (int i = 0; i < dtcss.Rows.Count; i++)
                {

                    if ("Scann IP Range" == dtcss.Rows[i]["menu_name"].ToString())//Agent Management Menu start
                        dtcss.Rows[i]["class"] = "scan-ip-range";
                    else if ("Agent Installation" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-installation";
                    else if ("Agent Installation Status" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-installatin-status";
                    else if ("Agent Uninstall" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-uninstall";
                    else if ("Authorize IP Range" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "authorize-ip-range";
                    else if ("Agent Release License" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "agent-realease-license";
                    else if ("Branch/Unit Level Creation" == dtcss.Rows[i]["menu_name"].ToString())//Organization Structure Management Menu start
                        dtcss.Rows[i]["class"] = "branch-level-creation-1";
                    else if ("Manage Branch/Unit" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "change-user-password";
                    else if ("Unlink Agent From Branch/Unit" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "admin-access-rights";
                    else if ("Branch/Unit IP Range Binding" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "unlink-agent";
                    else if ("Manage Admin User" == dtcss.Rows[i]["menu_name"].ToString())//Admin User Management Menu start
                        dtcss.Rows[i]["class"] = "manage-admin-user";
                    else if ("Change User Password" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "change-user-password";
                    else if ("Admin User Access Rights" == dtcss.Rows[i]["menu_name"].ToString())
                        dtcss.Rows[i]["class"] = "admin-access-rights";
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
        public IActionResult setting_flag_onoff()
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
                return View();
        }
        public IActionResult authorizeip_access_gui()
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
                return View();
        }
        public IActionResult setting_device_onoff()
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
                return View();
        }
        public JsonResult GetAuthIPAddress()
        {
            DataTable dtauthips = new DataTable();
            dtauthips = Objquery.GetAuthIPAddress();
            var data = new { dtauthip = dtauthips };
            return Json(data);

        }
        public JsonResult AddAuthIPAddress(string ipaddress)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.AddAuthIPAddress(ipaddress);
                if (querystatus > 0)
                    result = "Allow to access GUI on " + ipaddress + "";
                else
                    result = "Failed to access GUI on " + ipaddress + "";
            }
            catch (Exception ex)
            {

            }
            return Json(result);

        }
        public JsonResult DeleteAuthGUIIP(string ipaddress)
        {
            int querystatus = 0;
            try
            {
                if (ipaddress != null)
                {
                    querystatus = Objquery.Delete_Authipgui(ipaddress);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "DeleteAuthGUIIP Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0)
                result = "Record not deleted";
            else
                result = "Record deleted successfully";
            return Json(result);

        }
        //public JsonResult Bindflagconfiguration()
        //{
        //    DataTable dtflagconfigs= new DataTable();
        //    dtflagconfigs = Objquery.Bindflagconfiguration();
        //    var data = new { dtflagconfig = dtflagconfigs };
        //    return Json(data);

        //}
        public JsonResult Bindflagconfiguration(string ouid, string devicename, string ipaddress)
        {
            DataTable dtflagconfigs = new DataTable();
            string strCond = "";
            if (ipaddress != null && ipaddress != "")
            {
                strCond += " and dl.ip like '%" + ipaddress + "%'";
            }

            if (devicename != null && devicename != "")
            {

                strCond += " and dl.device_name like '%" + devicename + "%'";
            }

            if (ouid != null)
            {
                if (ouid != "-1")
                    strCond += " and dl.ou_id  in(" + Objquery.Get_ParentOu_id(ouid) + ")";
            }
            dtflagconfigs = Objquery.Bindflagconfiguration(strCond);
            var data = new { dtflagconfig = dtflagconfigs };
            return Json(data);

        }
        public JsonResult Bindfilenamelist()
        {
            DataTable dtfilenamelist = new DataTable();
            dtfilenamelist = Objquery.Bindfilenamelist();
            var data = new { filenamelist = dtfilenamelist };
            return Json(data);

        }
        public JsonResult Bindsectionlist(string filename)
        {
            DataTable dtsectionlist = new DataTable();
            dtsectionlist = Objquery.Bindsectionlist(filename);
            var data = new { sectionlist = dtsectionlist };
            return Json(data);

        }
        public JsonResult Bindpropertynamelist(string filename, string sectionname)
        {
            DataTable dtpropertylist = new DataTable();
            dtpropertylist = Objquery.Bindpropertynamelist(filename, sectionname);
            var data = new { propertylist = dtpropertylist };
            return Json(data);

        }
        public JsonResult Getdevicelist(string ouid, string sysname, string ipadd)
        {
            string search = "";
            try
            {
                if (sysname != null && sysname != "")
                {
                    search += " and dl.device_name like '%" + sysname + "%'";
                }
                if (ipadd != null && ipadd != "")
                {
                    search += " and dl.ip like '%" + ipadd + "%'";
                }
                if (ouid != null && ouid != "-1")
                {
                    search += " and dl.ou_id in(" + ouid + ")";
                }
                dt = Objquery.getdevicelist(search);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_setting_pageController", "Getdevicelist Exception : " + ex.Message.ToString());
            }
            var data = new { devicedata = dt };
            return Json(data);


        }
        public JsonResult SetFlag(string filename, string sectionname, string flagname, string flagvalue, string deviceidall, string flag_action)
        {
            int queryresult = 0;
            //int chkcount = 0;
            string strmsg = "";
            string strtype = "String";
            string straction = "";
            string result = "";
            string deviceall = deviceidall.Substring(1);
            string[] deiveces = deviceall.Split(',');
            for (int i = 0; i < deiveces.Length; i++)
            {
                //chkcount = Objquery.Get_flag_count(deiveces[i].ToString());
                //if(chkcount > 0)
                // {
                //int type1 = Int16.Parse(type);
                //if (type1 == 0)
                //    strtype = "String";
                //else
                //    strtype = "Integer";

                //int action1 = Int16.Parse(flag_action);
                //if (action1 == 1)
                //    straction = "Get";
                //else
                //    straction = "Set";
                //if(flagvalue == null || flagvalue == "")
                //{
                //    flagvalue = "-1";
                //}
                //queryresult = Objquery.updateFlag(filename, sectionname, flagname, flagvalue, deiveces[i].ToString(), strtype, straction);
                //strmsg = "#2012@" + deiveces[i].ToString() + "!" + filename + "!" + sectionname + "!" + flagname + "!" + flagvalue + "!" + type + "!" + flag_action + "!@2012#";
                //querystatus = Objquery.InsertQueryLog(deiveces[i].ToString(), strmsg, Objquery.Get_Location_ID(deiveces[i].ToString()));
                //}
                //else
                //{
                //int type1 = Int16.Parse(type);
                //if (type1 == 0)
                //    strtype = "String";
                //else
                //    strtype = "Integer";

                int action1 = Int16.Parse(flag_action);
                if (action1 == 1)
                    straction = "Get";
                else
                    straction = "Set";
                if (flagvalue == null || flagvalue == "")
                {
                    flagvalue = "-1";
                }
                queryresult = Objquery.SetFlag(filename, sectionname, flagname, flagvalue, deiveces[i].ToString(), strtype, straction);
                strmsg = "#2012@" + deiveces[i].ToString() + "!" + filename + "!" + sectionname + "!" + flagname + "!" + flagvalue + "!0!" + flag_action + "!@2012#";
                querystatus = Objquery.InsertQueryLog(deiveces[i].ToString(), strmsg, Objquery.Get_Location_ID(deiveces[i].ToString()));
                //}
            }
            if (queryresult > 0)
            {
                result = "Flag Set Successfully";
            }
            else
            {
                result = "Flag Set Failed";
            }
            return Json(result);
        }
        public JsonResult edit_SetFlag(string filename, string sectionname, string flagname, string flagvalue, string deviceid)
        {
            int queryresult = 0;
            string result = "";
            //queryresult = Objquery.updateFlag(filename, sectionname, flagname, flagvalue, deviceid);
            if (queryresult > 0)
            {
                result = "Flag Update Successfully";
            }
            else
            {
                result = "Flag Update Failed";
            }
            return Json(result);
        }
        public JsonResult DeleteFlag(string filename, string section, string propertyname, string propertyvalue, string deviceid)
        {
            int queryresult = 0;
            string result = "";
            queryresult = Objquery.deleteFlag(filename, section, propertyname, propertyvalue, deviceid);
            if (queryresult > 0)
            {
                result = "Delete Flag Successfully";
            }
            else
            {
                result = "Delete Flag Failed";
            }
            return Json(result);

        }
        public IActionResult setting_organization_structure()
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
                GetTreeDetails1();
            return View();
        }
        public void GetTreeDetails1()
        {

            var treedata = "";
            var childrendata = "";
            var endchardata = "";
            var childdata = " children: [ { ";
            var jsonData11 = "";
            try
            {
                dt = Objquery.Get_treedata();

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ou_nodelinkage_levelid"].ToString() == "1")
                        treedata += " id: '1', text: '" + dt.Rows[i]["ou_nodelinkage_nodename"].ToString() + "' , a_attr: { href: '#' }, state: {  selected: false },";
                    else if (dt.Rows[i]["ou_nodelinkage_levelid"].ToString() == "2")
                        childrendata += childdata + " id: '" + dt.Rows[i]["ou_nodelinkage_levelid"].ToString() + "' , text: '" + dt.Rows[i]["ou_nodelinkage_nodename"].ToString() + "', state: { selected: false },";
                    else
                        childrendata += childdata + " id: '" + dt.Rows[i]["ou_nodelinkage_levelid"].ToString() + "' , text: '" + dt.Rows[i]["ou_nodelinkage_nodename"].ToString() + "', state: { selected: false },},]";




                }

                endchardata = treedata + childrendata + " }, ] }, ] ; ";
                jsonData11 = "[ { " + endchardata;
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetTreeDetails1 Exception : " + ex.Message.ToString());
            }
            ViewBag.treedata = jsonData11;

        }
        public JsonResult Getleveldata()
        {
            try
            {
                dt = Objquery.Get_Level();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Getleveldata Exception : " + ex.Message.ToString());
            }
            var data = new { level = dt };
            return Json(data);
        }
        public JsonResult Getlevelid()
        {
            try
            {
                dt = Objquery.Get_Level_id();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Getlevelid Exception : " + ex.Message.ToString());
            }
            var data = new { level_id = dt };
            return Json(data);
        }
        public JsonResult Addlevel(string id, string level)
        {

            try
            {
                if (id != null && level != null)
                {
                    cnt = Objquery.Get_LevelCount(level);
                    if (cnt == 0)
                    {
                        querystatus = Objquery.insert_oulevel(id, level);
                        if (querystatus == 0)
                            result = "Level inserted failed.";
                        else
                            result = "Level inserted successfully.";
                    }
                    else
                        result = "Level alredy exists.";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Addlevel Exception : " + ex.Message.ToString());
            }
            return Json(result);

        }
        public JsonResult BindlevelProperty(string id)
        {

            try
            {
                dt = Objquery.Get_level_rights(id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "BindlevelProperty Exception : " + ex.Message.ToString());
            }
            var data = new { levelproperty = dt };
            return Json(data);

        }

        public JsonResult BindlevelPropertytoSetTree(string id)
        {
            if (OwnYITConstant.TreeactionProperty != null)
                OwnYITConstant.TreeactionProperty = "";

            // DataTable dts = new DataTable();
            objcommon.WriteLog("csatsettingController", "BindlevelProperty 1 : " + OwnYITConstant.TreeactionProperty);
            var TreeActionvalue = "";
            objcommon.WriteLog("csatsettingController", "BindlevelProperty 2 : " + TreeActionvalue);
            try
            {
                dt.Clear();
                dt = Objquery.Get_level_rights(id);
                for (var i = 0; i < dt.Rows.Count; i++)
                {


                    if (dt.Rows[i]["status"].ToString() == "1" && dt.Rows[i]["propertyname"].ToString() == "1")
                    {
                        TreeActionvalue += "  link_admin: " +
                                             "{" +
                                            "separator_before: false," +
                                "separator_after: true," +
                                "label: 'Link Admin'," +
                                "action: function(obj) {" +
                                " $('#admin_user').modal('show');" +
                                "}" +
                                " },";
                    }
                    else if (dt.Rows[i]["status"].ToString() == "1" && dt.Rows[i]["propertyname"].ToString() == "2")
                    {
                        TreeActionvalue += "  link_engineer: " +
                                             "{" +
                                            "separator_before: false," +
                                "separator_after: true," +
                                "label: 'Link Engineer'," +
                                "action: function(obj) {" +
                                " $('#engineer_user').modal('show');" +
                                "}" +
                                " },";
                    }
                    else if (dt.Rows[i]["status"].ToString() == "1" && dt.Rows[i]["propertyname"].ToString() == "3")
                    {
                        TreeActionvalue += "  link_user: " +
                                             "{" +
                                            "separator_before: false," +
                                "separator_after: true," +
                                "label: 'Link User'," +
                                "action: function(obj) {" +
                                " $('#simple_user').modal('show');" +
                                "}" +
                                " },";
                    }
                    else if (dt.Rows[i]["status"].ToString() == "1" && dt.Rows[i]["propertyname"].ToString() == "4")
                    {
                        // dtsystem = Objquery.Get_device_list();
                        //string strjquery = "var Setgriddata=$('#div_system');  jQuery('#div_system').html('');";
                        //string strcreatetable = "";
                        //strcreatetable += "<table class='table table-striped table-bordered system_datatable' style='width:100%'>";
                        //strcreatetable += " <thead><tr><th class='select noExport' data-orderable='false'><input type='checkbox' class='parent' data-group='.group4' id='chkdhead' /></th>";
                        //strcreatetable += "<th class='col-sm-10'>System Name</th></tr></thead><tbody>";
                        //string strrawdata = "";

                        //for (int k = 0; k < dtsystem.Rows.Count; k++)
                        //{
                        //    strrawdata += "<tr> <td class='select'><input type='checkbox' class='group4' name='check[]' value='" + dtsystem.Rows[i]["device_id"].ToString() + "' id='gridrawcheckp1'  /></td>";
                        //    strrawdata += "<td class='col-sm-10'>"+ dtsystem.Rows[i]["devicename"].ToString() + "</td></tr>";
                        //}
                        //string stralldata= strjquery + "Setgriddata.append(" +strcreatetable + strrawdata + " </tbody></table>);";
                        TreeActionvalue += "  link_system: " +
                                             "{" +
                                            "separator_before: false," +
                                "separator_after: true," +
                                "label: 'Link System'," +
                                "action: function(obj) {" +
                                " $('#system_user').modal('show');" +
                                "}" +
                                " },";
                    }
                    else if (dt.Rows[i]["status"].ToString() == "1" && dt.Rows[i]["propertyname"].ToString() == "5")
                    {
                        TreeActionvalue += " Add_node: " +
                                             "{" +
                                            "separator_before: false," +
                                "separator_after: true," +
                                "label: 'Add Node'," +
                                "action: function(obj) {" +
                                " $('#add_node').modal('show');" +
                                "}" +
                                " },";
                    }
                    else if (dt.Rows[i]["status"].ToString() == "1" && dt.Rows[i]["propertyname"].ToString() == "6")
                    {
                        DataTable dtrenameou = Objquery.Get_ou_Details(id);
                        string ouid = dtrenameou.Rows[0]["ou_nodelinkage_ouid"].ToString();
                        string ounodename = dtrenameou.Rows[0]["ou_nodelinkage_nodename"].ToString();

                        TreeActionvalue += "  Rename: " +
                                             "{" +
                                            "separator_before: false," +
                                "separator_after: true," +
                                "label: 'Rename Branch/ Unit'," +
                                "action: function(obj) {" +
                                " document.getElementById('ouname').value='" + ounodename + "'; document.getElementById('hiddenouid').value='" + ouid + "'; $('#rename').modal('show');" +
                                "}" +
                                " },";
                    }

                }
                TreeActionvalue += "  Delete: " +
                                           "{" +
                                          "separator_before: false," +
                              "separator_after: true," +
                              "label: 'Delete Branch/ Unit'," +
                              "action: function(obj) {" +
                              " $('#delete_node').modal('show');" +
                              "}" +
                              " },";





            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "BindlevelPropertytoSetTree Exception : " + ex.Message.ToString());
            }
            OwnYITConstant.TreeactionProperty = "";
            objcommon.WriteLog("csatsettingController", "BindlevelProperty 3 : " + OwnYITConstant.TreeactionProperty);
            objcommon.WriteLog("csatsettingController", "BindlevelProperty 4 : " + TreeActionvalue);
            OwnYITConstant.TreeactionProperty = " return {" + TreeActionvalue + "};";
            objcommon.WriteLog("csatsettingController", "BindlevelProperty 5 : " + OwnYITConstant.TreeactionProperty);
            objcommon.WriteLog("csatsettingController", "BindlevelProperty 6 : " + TreeActionvalue);
            var data = new { treeactionoption = "" };
            return Json(data);

        }
        //public JsonResult binddevicelist()
        //{

        //    var data = new { devicelist = Objquery.Get_device_list() };
        //    return Json(data);

        //}
        public JsonResult Updateoudetails(string ouid, string ouname)
        {
            try
            {
                if (ouid != null && ouid != null)
                {
                    querystatus = Objquery.Update_OUname(ouid, ouname);
                    querystatus = Objquery.call_Proc_InsLogName();
                    querystatus = Objquery.call_Proc_insallchild();
                    DataTable dtrenameou = Objquery.Get_ou_Details_nodelinkage(ouid);
                    string ounodename = dtrenameou.Rows[0]["ou_nodelinkage_nodename"].ToString();
                    string oulongname = dtrenameou.Rows[0]["ou_nodelinkage_longname"].ToString();
                    Objquery.Update_OUname_devicelinkage(ouid, ounodename, oulongname);
                    if (querystatus == 0)
                        result = "Branch/OU Updated failed.";
                    else
                        result = "Branch/OU Updated successfully.";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Updateoudetails Exception : " + ex.Message.ToString());
            }
            return Json(result);

        }
        //public JsonResult Insert_device_linkagedata(string deviceid, string levelid)
        //{

        //    try
        //    {
        //        if (deviceid != null && levelid != null)
        //        {
        //            dt = Objquery.Get_ou_Details1(levelid);
        //            string strouid = dt.Rows[0]["ou_nodelinkage_ouid"].ToString();
        //            string strnodename = dt.Rows[0]["ou_nodelinkage_nodename"].ToString();
        //            string strlongname = dt.Rows[0]["ou_nodelinkage_longname"].ToString();
        //            querystatus = Objquery.Update_OU_device(deviceid.Substring(1));
        //            querystatus = Objquery.Insert_OU_device(strouid, deviceid.Substring(1));
        //            querystatus = Objquery.Insert_device_linkage(strouid, strnodename, strlongname, deviceid.Substring(1));
        //            if (querystatus == 0)
        //                result = "Device added failed.";
        //            else
        //                result = "Device added successfully.";

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objcommon.WriteLog("csatsettingController", "Insert_device_linkagedata Exception : " + ex.Message.ToString());
        //    }
        //    return Json(result);

        //}
        public JsonResult clearaction()
        {
            OwnYITConstant.TreeactionProperty = "";

            return Json("");
        }
        public JsonResult Updatelevelrights(string levelid, string propertyname)
        {

            try
            {
                if (levelid != null)
                {
                    cnt = Objquery.Get_Level_RightsCount(levelid);
                    if (cnt == 0)
                        querystatus = Objquery.insert_level_rights(levelid, propertyname.Substring(1));
                    else
                    {
                        querystatus = Objquery.Delete_level_rights(levelid);
                        if (propertyname != null)
                            querystatus = Objquery.insert_level_rights(levelid, propertyname.Substring(1));
                    }
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Updatelevelrights Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0)
                result = "Property saved failed.";
            else
                result = "Property saved successfully.";
            return Json(result);

        }
        public JsonResult Editlevel(string levelid, string propertyname)
        {

            try
            {
                if (levelid != null && propertyname != null)
                {
                    querystatus = Objquery.Update_level(levelid, propertyname);

                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Editlevel Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0)
                result = "Level updated failed.";
            else
                result = "Level updated successfully.";
            return Json(result);

        }

        public JsonResult BindLinkDevices(string ouid, string ip, string device)
        {

            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                        searchdata += " and ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }

                if (ip != null && ip != "")
                {
                    searchdata += " and ip like '%" + ip + "%'";
                }

                if (device != null && device != "-1")
                {
                    searchdata += " and device_name ='" + device + "'";
                }



                dt = Objquery.Get_Linkage_Device(searchdata);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "BindLinkDevices Exception : " + ex.Message.ToString());
            }
            var data = new { linkagedata = dt };
            return Json(data);

        }
        public JsonResult Unlinksystem(string deviceid, string ouid)
        {

            try
            {
                if (deviceid != null && ouid != null)
                {
                    querystatus = Objquery.Unlinksystem(deviceid.Substring(1), ouid.Substring(1));

                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Unlinksystem Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0)
                result = "Unlink system failed.";
            else
                result = "Unlink system successfully.";
            return Json(result);

        }

        public JsonResult DeleteOU(string ouid)
        {
            int querystatus1 = 0;
            try
            {
                if (ouid != null)
                {
                    if (ouid != "-1")
                    {
                        querystatus = Objquery.Delete_OU1(ouid);
                        dt = Objquery.Getallchild_OU(ouid);
                        ouid = dt.Rows[0]["ou_nodelinkage_allchild"].ToString();
                        querystatus1 = Objquery.Delete_OU(ouid);
                    }
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "DeleteOU Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0 && querystatus1 == 0)
                result = "Branch/ Unit not deleted";
            else
                result = "Branch/ Unit remove successfully";
            return Json(result);

        }




        /// <summary>
        /// //
        /// </summary>
        /// <returns></returns>

        //public JsonResult GetTreeDetails()
        //{
        //    var treedata = "";
        //    var childrendata = "";
        //    var endchardata = "";
        //    var childdata = " children: [ { ";
        //    var jsonData11 = "";
        //    try
        //    {
        //        dt = Objquery.Get_treedata();

        //        for (var i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (dt.Rows[i]["ou_nodelinkage_levelid"].ToString() == "1")
        //                treedata += " id: '1', text: '" + dt.Rows[i]["ou_nodelinkage_nodename"].ToString() + "' , a_attr: { href: '#' }, state: {  selected: false },";
        //            else if (dt.Rows[i]["ou_nodelinkage_levelid"].ToString() == "2")
        //                childrendata += childdata + " id: '" + dt.Rows[i]["ou_nodelinkage_levelid"].ToString() + "' , text: '" + dt.Rows[i]["ou_nodelinkage_nodename"].ToString() + "', state: { selected: false },";
        //            else
        //                childrendata += childdata + " id: '" + dt.Rows[i]["ou_nodelinkage_levelid"].ToString() + "' , text: '" + dt.Rows[i]["ou_nodelinkage_nodename"].ToString() + "', state: { selected: false },},]";




        //        }

        //        endchardata = treedata + childrendata + " }, ] }, ] ; ";
        //        jsonData11 = "[ { " + endchardata;


        //    }
        //    catch (Exception ex)
        //    {
        //        objcommon.WriteLog("csatsettingController", "Unlinksystem Exception : " + ex.Message.ToString());
        //    }

        //    var data = new { treeou = jsonData11 };
        //    return Json(data);


        //}
        //public JsonResult clearaction()
        //{
        //    OwnYITConstant.TreeactionProperty = null;

        //    return Json("");
        //}


        public IActionResult setting_scan_ip_range()
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
        // Scan IP Range
        public JsonResult GetScanIPdata()
        {
            try
            {
                dt = Objquery.Get_ScanIPdata();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetScanIPdata Exception : " + ex.Message.ToString());
            }
            var data = new { scanipdetail = dt };
            return Json(data);
        }
        //  Device OnOff Status
        public JsonResult GetDeviceOnOffStatus()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Objquery.GetDeviceOnOffStatus();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetDeviceOnOffStatus Exception : " + ex.Message.ToString());
            }
            var data = new { dtdeviceonoff = dt };
            return Json(data);
        }
        public JsonResult AddDeviceOnOff(string ip_address)
        {
            string result = "";
            try
            {
                string fileName = @"C:\programdata\DTM\icmp_iplist.txt";
                if (!Directory.Exists(@"C:\programdata\DTM"))
                {
                    Directory.CreateDirectory(@"C:\programdata\DTM");
                }
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.AppendAllText(fileName, ip_address + Environment.NewLine);
                    result = "IP Address add successfull";
                }
                else
                {
                    var myFile = System.IO.File.CreateText(fileName);
                    myFile.Close();
                    System.IO.File.AppendAllText(fileName, ip_address + Environment.NewLine);
                    result = "IP Address add successfull";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "AddDeviceOnOff Exception : " + ex.Message.ToString());

            }
            return Json(result);
        }
        public JsonResult AddScanIPdata(string iprange, string netmask)
        {
            int cnt = 0;
            string parts1 = "";
            try
            {
                // if (iprange != null && netmask != null)
                if (iprange != null)
                {
                    if (netmask != null)
                    {
                        string[] parts = iprange.Split('.');
                        //if (parts.Length == 4)
                        //{
                        parts1 = parts[3];
                        if (parts1 != "")
                        {
                            dt = Objquery.Get_ScanIPdata();
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                if ((dt.Rows[i][0].ToString() == iprange | dt.Rows[i][0].ToString() == netmask) & (dt.Rows[i][1].ToString() == iprange | dt.Rows[i][1].ToString() == netmask))
                                {
                                    if (dt.Rows[i][0].ToString() == iprange & dt.Rows[i][1].ToString() == netmask)
                                    {
                                        cnt = 1;

                                    }
                                }
                            }
                            if (cnt == 0)
                            {
                                querystatus = Objquery.Add_ScanIPdata(iprange, netmask);
                                if (querystatus == 0)
                                    result = "IP Range added failed.";
                                else
                                    result = "IP Range added successfully.";
                            }
                            else
                            {
                                result = "IP range already exist";
                            }
                            //}
                            //else
                            //    result = "Please provide Proper IP Range !";
                        }
                        else
                            result = "Please provide Proper IP Range !";

                    }
                    else
                        result = "Please provide netmask!";
                }
                else
                    result = "Please provide IP Range!";
            }
            catch (Exception ex)
            {

                objcommon.WriteLog("csatsettingController", "AddScanIPdata Exception : " + ex.Message.ToString());
            }

            return Json(result);
        }
        public JsonResult ReScanIPdata(string iprange, string netmask)
        {
            int querystatus = 0;
            try
            {
                querystatus = Objquery.Add_ScanIPdata(iprange, netmask);
                if (querystatus == 0)
                    result = "IP Re-scan failed.";
                else
                    result = "IP Re-scan successfully.";
            }
            catch (Exception ex)
            {

                objcommon.WriteLog("csatsettingController", "ReScanIPdata Exception : " + ex.Message.ToString());
            }

            return Json(result);
        }
        public JsonResult GetScanIPPCDetail(string iprange)
        {
            string iprange1 = "";
            try
            {
                iprange1 = iprange.Substring(iprange.LastIndexOf('.') + 1);
            }
            catch (Exception)
            {
            }
            try
            {
                if (Convert.ToInt32(iprange1) > 0)
                    searchdata = " where IP_Address like '%" + iprange + "%' ";
                //else
                //    searchdata = " where t.ip like '" + iprange.Substring(0, iprange.LastIndexOf('.') + 1) + "%' ";
                dt = Objquery.Get_ScanIPPCDetails(searchdata);

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetScanIPPCDetail Exception : " + ex.Message.ToString());
            }
            var data = new { scanippcdetail = dt };
            return Json(data);
        }

        public JsonResult DeleteScanIP(string iprange, string subnet)
        {

            try
            {
                if (iprange != null && subnet != null)
                {
                    querystatus = Objquery.Delete_Scaniprange(iprange, subnet);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "DeleteScanIP Exception : " + ex.Message.ToString());
            }
            if (querystatus == 0)
                result = "IP range not deleted";
            else
                result = "IP range deleted successfully";
            return Json(result);

        }

        // Authorized IP data
        public JsonResult GetAuthorizedIPdata()
        {
            try
            {
                dt = Objquery.Get_AuthorizedIPdata();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetAuthorizedIPdata Exception : " + ex.Message.ToString());
            }
            var data = new { authorizedipdetail = dt };
            return Json(data);
        }
        public JsonResult AddAuthorizedIPdata(int type, string start_ip, string end_ip, string remoteport, string localport)
        {
            try
            {
                if (remoteport == null)
                    remoteport = "0";

                if (localport == null)
                    localport = "0";
                if (start_ip != null && end_ip != null)
                {
                    string[] parts = start_ip.Split('.');
                    string startipend = parts[3];
                    string[] parts1 = end_ip.Split('.');
                    string endipend = parts1[3];
                    if (startipend != "" && endipend != "")
                    {
                        querystatus = Objquery.Add_AuthorizedIPdata(type, start_ip, end_ip, start_ip.Substring(0, start_ip.LastIndexOf(".")), startipend, endipend, remoteport, localport);
                        if (type == 0)
                        {
                            if (querystatus == 0)
                                result = "Authorized IP Range added failed.";
                            else
                                result = "Authorized IP Range added successfully.";
                        }
                        else
                        {
                            if (querystatus == 0)
                                result = "Unauthorized IP Range added failed.";
                            else
                                result = "Unauthorized IP Range added successfully.";
                        }
                    }
                    else
                        result = "Please provide Proper IP Range !";
                }
                else
                {
                    result = "Please provide IP Range!";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "AddAuthorizedIPdata Exception : " + ex.Message.ToString());
            }

            return Json(result);
        }
        public JsonResult deleteAuthorizedIPdata(string authipid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                //querystatus = Objquery.Delete_AuthorizedIPdata(authipid);
                //if (querystatus == 0)
                //    result = "Authorized IP Range Deleted failed.";
                //else
                //    result = "Authorized IP Range Deleted successfully.";

                string type = Objquery.Get_Authorized_Unauthorized_Type(authipid);
                querystatus = Objquery.Delete_AuthorizedIPdata(authipid);
                if (Convert.ToInt32(type) == 0)
                {
                    if (querystatus == 0)
                        result = "Authorized IP Range Deleted failed.";
                    else
                        result = "Authorized IP Range Deleted successfully.";
                }
                else
                {
                    if (querystatus == 0)
                        result = "Unauthorized IP Range Deleted failed.";
                    else
                        result = "Unauthorized IP Range Deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deleteAuthorizedIPdata Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult GetAuthorizedIPdetail(string authipid)
        {
            var data = new { authipdata = Objquery.Get_AuthorizedIP_details(authipid) };
            return Json(data);
        }
        public JsonResult UpdateAuthorizedIPdetail(string ipid, string startip, string endip, string localport, string remoteport, string type)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.Update_AuthorizedIP_details(ipid, startip, endip, localport, remoteport, type);
                if (Convert.ToInt32(type) == 0)
                {
                    if (querystatus == 0)
                        result = "Authorized IP Range updated failed.";
                    else
                        result = "Authorized IP Range updated successfully.";
                }
                else
                {
                    if (querystatus == 0)
                        result = "Unauthorized IP Range updated failed.";
                    else
                        result = "Unauthorized IP Range updated successfully.";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "UpdateAuthorizedIPdetail Exception : " + ex.Message.ToString());

            }

            return Json(result);
        }

        // Database maintenance
        public IActionResult setting_database_maintenance()
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
        // Show database backup

        public JsonResult Showdatabasebackup()
        {
            string backuptype = "";
            string schedule = "";
            try
            {
                dt = Objquery.Get_databasebackup();
                if (dt.Rows.Count > 0)
                {
                    switch (Convert.ToInt32(dt.Rows[0][0]))
                    {
                        case 0:
                            backuptype = "Daily";
                            schedule = "";
                            break;
                        case 1:
                            backuptype = "Weekly";
                            string strdays;
                            strdays = dt.Rows[0][1].ToString();
                            string strWeekly = "";
                            if (Convert.ToInt32(strdays.ToString().Substring(0, 1)) == 1)
                            {
                                strWeekly += ", Sunday";
                            }
                            if (Convert.ToInt32(strdays.ToString().Substring(1, 1)) == 1)
                            {
                                strWeekly += ", Monday";
                            }

                            if (Convert.ToInt32(strdays.ToString().Substring(2, 1)) == 1)
                            {
                                strWeekly += ", Tuesday";
                            }
                            if (Convert.ToInt32(strdays.ToString().Substring(3, 1)) == 1)
                            {
                                strWeekly += ", Wednesday";
                            }
                            if (Convert.ToInt32(strdays.ToString().Substring(4, 1)) == 1)
                            {
                                strWeekly += ", Thursday";
                            }
                            if (Convert.ToInt32(strdays.ToString().Substring(5, 1)) == 1)
                            {
                                strWeekly += ", Friday";
                            }
                            if (Convert.ToInt32(strdays.ToString().Substring(6, 1)) == 1)
                            {
                                strWeekly += ", Saturday";
                            }
                            if (strWeekly.Trim().Length > 0)
                                strWeekly = strWeekly.Substring(1);
                            schedule = strWeekly;
                            break;
                        case 2:
                            backuptype = "Monthly";
                            schedule = dt.Rows[0][1].ToString();
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Showdatabasebackup Exception : " + ex.Message.ToString());
            }
            var data = new { databasebackup = dt, schedule = schedule, backuptype = backuptype };
            return Json(data);
        }
        // Get History removal data 
        public JsonResult gethistoryremoval()
        {
            try
            {
                dt = Objquery.Get_databasehistoryremoval();
            }

            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "gethistoryremoval Exception : " + ex.Message.ToString());
            }
            var data = new { datahistory = dt };
            return Json(data);
        }

        // Update History removal data 
        public JsonResult updatehistoryremoval(string dispname, string days)
        {
            try
            {
                cnt = Objquery.Update_databasehistoryremoval(dispname, days);
            }

            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "updatehistoryremoval Exception : " + ex.Message.ToString());
            }
            return Json(cnt);
        }
        // Store database backup
        public JsonResult Adddatabasebackup(string backuptype, string backupdetails, string backuptime, string backuppath)
        {
            try
            {

                if (backuppath != null && backuptime != null)
                {
                    if (backupdetails == "0000000")
                    {
                        result = "Please select days";
                    }
                    else
                    {
                        //if (!backuppath.Contains("\\"))
                        //    backuppath.Replace(@"\", "\\\\");
                        backuppath = @"C:\\Program Files (x86)\\OwnyITEE\\OwnYitServer\\DBBackup\\" + backuppath;
                        cnt = Objquery.Update_databaseschedule(backuptype, backupdetails, backuptime, backuppath);
                        if (cnt == 0)
                            result = "Database scheduling updated has been failed...";
                        else
                            result = "Database scheduling has been updated...";
                    }
                }
                else
                    result = "Please enter details..";

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Adddatabasebackup Exception : " + ex.Message.ToString());
            }


            return Json(result);
        }

        // Admin User management

        public IActionResult setting_admin_user_mgmt()
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
        public JsonResult Showadminuser()
        {

            try
            {
                dt = Objquery.Get_adminuserinfo();

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Showadminuser Exception : " + ex.Message.ToString());
            }
            var data = new { adminuserinfo = dt };
            return Json(data);
        }
        // Add Admin user 
        public JsonResult Addadminuser(string userrole, string loginuser, string loginid, string passwd, string emailid, string contact)
        {
            int getlogincnt;
            try
            {

                if (passwd == null || loginid == null)
                {
                    result = "Please enter details";
                }
                else
                {
                    getlogincnt = Objquery.Get_logincount(loginid);
                    if (getlogincnt == 0)
                    {
                        querystatus = Objquery.Add_adminuserinfo(userrole, loginuser, loginid, passwd, emailid, contact);
                        if (querystatus == 1)
                            result = "User added successfully";
                        else
                            result = "User added failed";
                    }
                    else
                    {
                        result = "Login ID already exist";
                    }

                }

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Addadminuser Exception : " + ex.Message.ToString());
            }


            return Json(result);
        }
        // Delete Admin user
        public JsonResult deleteAdminuser(string userid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.Delete_adminuser(userid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deleteAdminuser Exception : " + ex.Message.ToString());

            }
            if (querystatus == 1)
                result = "User deleted successfully";

            return Json(result);
        }

        public JsonResult GetAdminuserdetail(string admineditid)
        {
            var data = new { adminuserdata = Objquery.Get_Adminuserdetails(admineditid) };
            return Json(data);
        }

        public JsonResult UpdateAdminuserdetail(string userid, string usertype, string loginuser, string pwd, int chngepwd, string emailid, string contact)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                if (chngepwd == 0 && loginuser != null)
                {
                    querystatus = Objquery.Update_Adminuserdetails(userid, usertype, loginuser, emailid, contact);
                    if (querystatus == 1)
                        result = "Admin user updated successfully.";
                    else
                        result = "Admin user updated failed.";
                }
                else if (chngepwd == 1 && pwd != null && loginuser != null)
                {
                    querystatus = Objquery.Update_Adminuserdetails1(userid, usertype, loginuser, pwd, emailid, contact);
                    if (querystatus == 1)
                        result = "Admin user updated successfully.";
                    else
                        result = "Admin user updated failed.";
                }
                else
                {
                    result = "Please enter Details.";
                }

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "UpdateAdminuserdetail Exception : " + ex.Message.ToString());

            }

            return Json(result);
        }
        // Show Menu list in user rights

        public JsonResult ShowMenuList(string userid)
        {
            var data = new { menulist = Objquery.Get_MenuList(userid) };
            return Json(data);
        }
        public JsonResult AddUserRights(string userid, string menuid, string menuuncheckid)
        {

            try
            {

                if (menuid != null)
                {
                    string checkid = menuid.Substring(1);
                    cnt = Objquery.update_UserRights(userid, checkid, 1);
                }

                if (menuuncheckid != null)
                {
                    string uncheckid = menuuncheckid.Substring(1);
                    cnt = Objquery.update_UserRights(userid, uncheckid, 0);
                }
                if (cnt >= 1)
                    result = "Data Updated..!";


            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "AddUserRights Exception : " + ex.Message.ToString());
            }
            return Json(result);

        }


        // Notification Handler
        public IActionResult setting_notification_handler()
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
        public JsonResult GetNotificationTemp()
        {
            try
            {
                dt = Objquery.Get_NotificationTemp();

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetNotificationTemp Exception : " + ex.Message.ToString());
            }
            var data = new { notificationtemp = dt };
            return Json(data);
        }
        public JsonResult bindusercategory()
        {
            try
            {
                dt = Objquery.Get_User();
                dt1 = Objquery.Get_Category();

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "bindusercategory Exception : " + ex.Message.ToString());
            }


            var data = new { binduser = dt, bindcategory = dt1 };
            return Json(data);
        }

        // Add Template Data
        public JsonResult AddTemplatedata(string Temname, bool syssec, string syssecid, int drpselectdevice, int drpselectdevice1, int drpselectdevice2, int drpselectdevice3, int drpselectdevice4, int drpselectdevice5, int drpselectdevice6, int drpselectdevice7, int drpselectdevice8, int drpselectdevice9, string emailto, string emailcc, string chkuserfinal)
        {
            int cnt = 0;
            string strtemplateid;
            if (Temname != null && chkuserfinal != null && syssecid != null)
            {
                cnt = Objquery.Get_TempCount(Temname);
                if (cnt == 0)
                {
                    int cnt1 = Objquery.Insert_Template(Temname);
                    strtemplateid = Objquery.Get_TempID(Temname);
                    if (strtemplateid != null)
                    {
                        string jj = syssecid.Substring(1);
                        string[] values = jj.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i].ToString() == "change")
                                Objquery.Insert_TemplateData(strtemplateid, 1, 101, 12, drpselectdevice, 0, 0);
                            if (values[i].ToString() == "Running")
                                Objquery.Insert_TemplateData(strtemplateid, 1, 0, 2, drpselectdevice, 0, 0);
                            if (values[i].ToString() == "Down")
                                Objquery.Insert_TemplateData(strtemplateid, 1, 0, 0, drpselectdevice, 0, 0);
                            if (values[i].ToString() == "Disable")
                                Objquery.Insert_TemplateData(strtemplateid, 1, 104, 8, drpselectdevice, 0, 0);
                            if (values[i].ToString() == "AccessCom")
                                Objquery.Insert_TemplateData(strtemplateid, 1, 108, 5, drpselectdevice, 0, 0);

                            // Hardware
                            if (values[i].ToString() == "HWAdded")
                                Objquery.Insert_TemplateData(strtemplateid, 11, 0, 1, drpselectdevice1, 0, 0);
                            if (values[i].ToString() == "HWRemoved")
                                Objquery.Insert_TemplateData(strtemplateid, 11, 0, -1, drpselectdevice1, 0, 0);

                            // Software
                            if (values[i].ToString() == "SWAdded")
                                Objquery.Insert_TemplateData(strtemplateid, 12, 0, 1, drpselectdevice2, 0, 0);
                            if (values[i].ToString() == "SWRemoved")
                                Objquery.Insert_TemplateData(strtemplateid, 12, 0, -1, drpselectdevice2, 0, 0);

                            // Share
                            if (values[i].ToString() == "ShareAdded")
                                Objquery.Insert_TemplateData(strtemplateid, 20, 0, 1, drpselectdevice3, 0, 0);
                            if (values[i].ToString() == "ShareRemoved")
                                Objquery.Insert_TemplateData(strtemplateid, 20, 0, -1, drpselectdevice3, 0, 0);

                            // Performance Monitoring
                            if (values[i].ToString() == "PerformanceNotification")
                            {
                                //CPU
                                Objquery.Insert_TemplateData(strtemplateid, 13, 111, 5, drpselectdevice4, 0, 0);
                                // RAM
                                Objquery.Insert_TemplateData(strtemplateid, 13, 112, 5, drpselectdevice4, 0, 0);
                                //Disk
                                Objquery.Insert_TemplateData(strtemplateid, 14, 122, 5, drpselectdevice4, 0, 0);
                            }
                            if (values[i].ToString() == "ApplicationMonitoring")
                                Objquery.Insert_TemplateData(strtemplateid, 18, 0, 5, drpselectdevice4, 0, 0);

                            // System Tools
                            if (values[i].ToString() == "Systemfol")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 131, 10, drpselectdevice5, 0, 0);
                            if (values[i].ToString() == "ControlPanel")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 132, 10, drpselectdevice5, 0, 0);
                            if (values[i].ToString() == "Registry")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 133, 10, drpselectdevice5, 0, 0);
                            if (values[i].ToString() == "Services")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 135, 10, drpselectdevice5, 0, 0);

                            // User
                            if (values[i].ToString() == "unsuclogin")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 134, 11, drpselectdevice6, 0, 0);
                            if (values[i].ToString() == "User_add")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 0, 1, drpselectdevice6, 0, 0);
                            if (values[i].ToString() == "User_remove")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 0, -1, drpselectdevice6, 0, 0);
                            if (values[i].ToString() == "User_change")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 0, 2, drpselectdevice6, 0, 0);
                            if (values[i].ToString() == "user_enabled")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 0, 3, drpselectdevice6, 0, 0);
                            if (values[i].ToString() == "user_Disabled")
                                Objquery.Insert_TemplateData(strtemplateid, 15, 0, 4, drpselectdevice6, 0, 0);


                            // Policy Information

                            if (values[i].ToString() == "policyApply")
                                Objquery.Insert_TemplateData(strtemplateid, 2, 11, 9, drpselectdevice7, 0, 0);
                            if (values[i].ToString() == "PolicyRemoved")
                                Objquery.Insert_TemplateData(strtemplateid, 2, 11, 10, drpselectdevice7, 0, 0);
                            if (values[i].ToString() == "policyVioletion")
                                Objquery.Insert_TemplateData(strtemplateid, 2, 11, 11, drpselectdevice7, 0, 0);

                            // Remote Notification
                            if (values[i].ToString() == "RemoteNotification")
                                Objquery.Insert_TemplateData(strtemplateid, 10, 0, 10, drpselectdevice8, 0, 0);

                            // OwnyitCSAT / Ownyit Agent
                            if (values[i].ToString() == "csatNotification")
                                Objquery.Insert_TemplateData(strtemplateid, 9, 0, -1, drpselectdevice9, 0, 0);

                        }
                    }
                    try
                    {
                        if (strtemplateid != null)
                            Objquery.Insert_UserNotificationLevel(strtemplateid, "0", 0);
                    }
                    catch (Exception ex1)
                    {
                        objcommon.WriteLog("csatsettingController", "AddTemplatedata Exception : " + ex1.Message.ToString());
                    }
                    // User
                    if (chkuserfinal != null && strtemplateid != null)
                    {
                        if (chkuserfinal.Length > 1)
                            chkuserfinal = " where user_id in(" + chkuserfinal.Substring(1) + ")";
                        if (chkuserfinal.Length > 0)
                        {
                            Objquery.Insert_UserNotification(strtemplateid, chkuserfinal);
                        }
                    }
                    // Email 
                    if ((emailto != null || emailcc != null) && strtemplateid != null)
                    {
                        if (emailto != null)
                        {
                            Objquery.Insert_EmailNotification(strtemplateid, emailto.ToString(), 1);
                        }
                        if (emailcc != null)
                        {

                            Objquery.Insert_EmailNotification(strtemplateid, emailcc.ToString(), 2);
                        }
                    }

                    result = "Template Added.";

                }
                else
                {
                    result = "Template already exist.";
                }
            }
            else
            {
                result = "Enter Details";
            }
            return Json(result);
        }
        // Edit Template Data
        public JsonResult bindedituser(string template_id)
        {
            try
            {
                dt = Objquery.Get_EditUser(template_id);

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "bindedituser Exception : " + ex.Message.ToString());
            }


            var data = new { bindedituser = dt };
            return Json(data);
        }
        public JsonResult GetTemplatedetail(string edittempid)
        {
            dt1 = Objquery.Get_TemplateEmaildetails(edittempid);
            var data = new { templatedetail = Objquery.Get_Templatedetails(edittempid), emailtemplatedetail = dt1 };
            return Json(data);
        }
        // Update Template Data
        public JsonResult UpdateTemplatedata(string editTemname, bool syseditsec, string syseditsecid, int drpeditselectdevice, int drpeditselectdevice1, int drpeditselectdevice2, int drpeditselectdevice3, int drpeditselectdevice4, int drpeditselectdevice5, int drpeditselectdevice6, int drpeditselectdevice7, int drpeditselectdevice8, int drpeditselectdevice9A, string editemailto, string editemailcc, string chkedituserfinal)
        {

            string strtemplateid;
            //if (editTemname != null && chkedituserfinal != null  && syseditsecid != null)
            if (editTemname != null)
            {
                //int cnt1 = Objquery.Insert_Template(Temname);
                strtemplateid = Objquery.Get_TempID(editTemname);
                if (syseditsecid != null)
                {
                    Objquery.Update_DeleteTemplateeventdetails(strtemplateid);
                    string jj = syseditsecid.Substring(1);
                    string[] values = jj.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (values[i].ToString() == "change1")
                            Objquery.Insert_TemplateData(strtemplateid, 1, 101, 12, drpeditselectdevice, 0, 0);
                        if (values[i].ToString() == "Running1")
                            Objquery.Insert_TemplateData(strtemplateid, 1, 0, 2, drpeditselectdevice, 0, 0);
                        if (values[i].ToString() == "Down1")
                            Objquery.Insert_TemplateData(strtemplateid, 1, 0, 0, drpeditselectdevice, 0, 0);
                        if (values[i].ToString() == "Disable1")
                            Objquery.Insert_TemplateData(strtemplateid, 1, 104, 8, drpeditselectdevice, 0, 0);
                        if (values[i].ToString() == "AccessCom1")
                            Objquery.Insert_TemplateData(strtemplateid, 1, 108, 5, drpeditselectdevice, 0, 0);

                        // Hardware
                        if (values[i].ToString() == "HWAdded1")
                            Objquery.Insert_TemplateData(strtemplateid, 11, 0, 1, drpeditselectdevice1, 0, 0);
                        if (values[i].ToString() == "HWRemoved1")
                            Objquery.Insert_TemplateData(strtemplateid, 11, 0, -1, drpeditselectdevice1, 0, 0);

                        // Software
                        if (values[i].ToString() == "SWAdded1")
                            Objquery.Insert_TemplateData(strtemplateid, 12, 0, 1, drpeditselectdevice2, 0, 0);
                        if (values[i].ToString() == "SWRemoved1")
                            Objquery.Insert_TemplateData(strtemplateid, 12, 0, -1, drpeditselectdevice2, 0, 0);

                        // Share
                        if (values[i].ToString() == "ShareAdded1")
                            Objquery.Insert_TemplateData(strtemplateid, 20, 0, 1, drpeditselectdevice3, 0, 0);
                        if (values[i].ToString() == "ShareRemoved1")
                            Objquery.Insert_TemplateData(strtemplateid, 20, 0, -1, drpeditselectdevice3, 0, 0);

                        // Performance Monitoring
                        if (values[i].ToString() == "PerformanceNotification1")
                        {
                            //CPU
                            Objquery.Insert_TemplateData(strtemplateid, 13, 111, 5, drpeditselectdevice4, 0, 0);
                            // RAM
                            Objquery.Insert_TemplateData(strtemplateid, 13, 112, 5, drpeditselectdevice4, 0, 0);
                            //Disk
                            Objquery.Insert_TemplateData(strtemplateid, 14, 122, 5, drpeditselectdevice4, 0, 0);
                        }
                        if (values[i].ToString() == "ApplicationMonitoring1")
                            Objquery.Insert_TemplateData(strtemplateid, 18, 0, 5, drpeditselectdevice4, 0, 0);

                        // System Tools
                        if (values[i].ToString() == "Systemfol1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 131, 10, drpeditselectdevice5, 0, 0);
                        if (values[i].ToString() == "ControlPanel1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 132, 10, drpeditselectdevice5, 0, 0);
                        if (values[i].ToString() == "Registry1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 133, 10, drpeditselectdevice5, 0, 0);
                        if (values[i].ToString() == "Services1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 135, 10, drpeditselectdevice5, 0, 0);

                        // User
                        if (values[i].ToString() == "unsuclogin1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 134, 11, drpeditselectdevice6, 0, 0);
                        if (values[i].ToString() == "User_add1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 0, 1, drpeditselectdevice6, 0, 0);
                        if (values[i].ToString() == "User_remove1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 0, -1, drpeditselectdevice6, 0, 0);
                        if (values[i].ToString() == "User_change1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 0, 2, drpeditselectdevice6, 0, 0);
                        if (values[i].ToString() == "user_enabled1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 0, 3, drpeditselectdevice6, 0, 0);
                        if (values[i].ToString() == "user_Disabled1")
                            Objquery.Insert_TemplateData(strtemplateid, 15, 0, 4, drpeditselectdevice6, 0, 0);


                        // Policy Information

                        if (values[i].ToString() == "policyApply1")
                            Objquery.Insert_TemplateData(strtemplateid, 2, 11, 9, drpeditselectdevice7, 0, 0);
                        if (values[i].ToString() == "PolicyRemoved1")
                            Objquery.Insert_TemplateData(strtemplateid, 2, 11, 10, drpeditselectdevice7, 0, 0);
                        if (values[i].ToString() == "policyVioletion1")
                            Objquery.Insert_TemplateData(strtemplateid, 2, 11, 11, drpeditselectdevice7, 0, 0);

                        // Remote Notification
                        if (values[i].ToString() == "RemoteNotification1")
                            Objquery.Insert_TemplateData(strtemplateid, 10, 0, 10, drpeditselectdevice8, 0, 0);

                        // OwnyitCSAT / Ownyit Agent
                        if (values[i].ToString() == "csatNotification1")
                            Objquery.Insert_TemplateData(strtemplateid, 9, 0, -1, drpeditselectdevice9A, 0, 0);

                    }
                }
                try
                {
                    Objquery.Update_DeleteTemplatelevelsdetails(strtemplateid);
                    Objquery.Insert_UserNotificationLevel(strtemplateid, "0", 0);
                }
                catch (Exception ex1)
                {
                    objcommon.WriteLog("csatsettingController", "UpdateTemplatedata Exception : " + ex1.Message.ToString());
                }
                // User
                if (chkedituserfinal != null)
                {
                    Objquery.Update_DeleteTemplateuserdetails(strtemplateid, "");
                    if (chkedituserfinal.Length > 1)
                        chkedituserfinal = " where user_id in(" + chkedituserfinal.Substring(1) + ")";
                    if (chkedituserfinal.Length > 0)
                    {
                        Objquery.Insert_UserNotification(strtemplateid, chkedituserfinal);
                    }
                }
                // Email 
                if (editemailto != null || editemailcc != null)
                {
                    string strcond = "";
                    if (editemailto != null)
                    {
                        strcond = "and c052notificationcomtype=1";
                        Objquery.Update_DeleteTemplateuserdetails(strtemplateid, strcond);
                        Objquery.Insert_EmailNotification(strtemplateid, editemailto.ToString(), 1);
                    }
                    if (editemailcc != null)
                    {
                        strcond = "and c052notificationcomtype=2";
                        Objquery.Update_DeleteTemplateuserdetails(strtemplateid, strcond);
                        Objquery.Insert_EmailNotification(strtemplateid, editemailcc.ToString(), 2);
                    }
                }

                result = "Template Updated..!";
            }
            else
            {
                result = "Enter Details";
            }
            return Json(result);
        }

        // Delete Template
        public JsonResult deleteTemplate(string Temid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.Delete_Template(Temid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deleteTemplate Exception : " + ex.Message.ToString());

            }
            if (querystatus > 0)
                result = "Template deleted successfully";

            return Json(result);
        }

        // Get OU device detail

        public JsonResult GetOUDeviceFortemplate(string name, string tempid)
        {
            string type = "";
            if (name == "01")
            {
                dt = Objquery.Get_Templateou(tempid);
                type = "OU";
            }
            else
            {
                dt = Objquery.Get_Templatesystemnameip(tempid);
                type = "System";
            }

            var data = new { getoudevicetempdata = dt, gridtype = type };
            return Json(data);
        }

        // Apply Template on OU/Device
        public JsonResult ApplyTemplateOUDevice(string fieldname, string tempid, string checkedid)
        {
            int querystatus = 0;
            try
            {
                Objquery.delete_TemplateOUDevice(fieldname, tempid);
            }
            catch (Exception e)
            {
                objcommon.WriteLog("csatsettingController", "Delete TemplateOUDevice Exception : " + e.Message.ToString());
            }
            string strQuery = "";
            int intdevnotapplid = 0;
            string strid = Objquery.get_devnottempid();
            if (strid != "")
                intdevnotapplid = Convert.ToInt16(strid);
            else
                intdevnotapplid = 0;
            if (checkedid != null)
            {
                string jj = checkedid.Substring(1);
                string[] values = jj.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    strQuery += Objquery.GETInsert_TemplateOUDevice(fieldname, values[i].ToString(), tempid, intdevnotapplid);
                }
                querystatus = Objquery.Execute_TemplateOUDevice(strQuery);
            }
            if (querystatus > 0)
            {
                result = "Notification set successfully";
            }
            return Json(result);
        }

        public JsonResult get_root_ouid()
        {
            string root_ouid = "";
            try
            {
                root_ouid = Objquery.get_root_ouid();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "get_root_ouid Exception : " + ex.Message.ToString());
            }
            return Json(root_ouid);
        }
        public JsonResult get_OU_Data_child(string ou_id)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dtparentou = new DataTable();
            DataTable dttemp = new DataTable();
            dtparentou.Columns.Add("ouidouname");
            try
            {
                dt = Objquery.Get_OU_Child_Data(ou_id);
                if (ou_id == "0")
                    ou_id = Objquery.get_root_ouid();
                //dt = Objquery.Get_OU_Child_Data(ou_id);
                dt1 = Objquery.Get_OU_self_Data(ou_id);
                Int64 varOUID = 1;

                while (true)
                {
                    dttemp = Objquery.getParentOUUID(ou_id);
                    if (dttemp.Rows.Count > 0)
                    {
                        varOUID = Convert.ToInt64(dttemp.Rows[0]["ou_nodelinkage_parentouid"].ToString());
                        DataRow dr = dtparentou.NewRow();
                        dr["ouidouname"] = dttemp.Rows[0]["ouidouname"].ToString();
                        dtparentou.Rows.Add(dr);
                    }
                    ou_id = varOUID.ToString();
                    if (varOUID <= 0)
                        break;
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "get_OU_Data_child Exception : " + ex.Message.ToString());
            }
            var data = new { ou_data_child = dt, ou_data_self = dt1, ou_data_parent = dtparentou };
            return Json(data);
        }
        public JsonResult get_OU_linked_devices(string ou_id)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {
                dt = Objquery.Get_OU_linked_device_Data(ou_id);
                dt1 = Objquery.Get_OU_self_Data(ou_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "get_OU_linked_devices Exception : " + ex.Message.ToString());
            }
            var data = new { ou_data_linked_device = dt, ou_data_linked_self = dt1 };
            return Json(data);
        }
        public JsonResult ou_unlink_devices(string device_id)
        {
            try
            {
                device_id = device_id.Substring(1);
                Objquery.unlink_devices_from_ou(device_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "ou_unlink_devices Exception : " + ex.Message.ToString());
            }
            return Json("Unlink devices successfully");
        }
        public JsonResult Add_Branch_Unit_ou_data(string ou_id)
        {
            DataTable dt1 = new DataTable();
            try
            {
                if (ou_id == "0")
                    ou_id = Objquery.get_root_ouid();
                dt1 = Objquery.Get_OU_self_Data(ou_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Add_Branch_Unit_ou_data Exception : " + ex.Message.ToString());
            }
            var data = new { ou_data_linked_self_add = dt1 };
            return Json(data);
        }
        public JsonResult add_new_ou_branch_unit(string ou_id, string ou_name)
        {
            int querystatus = 0;
            DataTable dt = new DataTable();
            long maxouid;
            int levelid;
            int entity_levelid;
            try
            {
                maxouid = Objquery.get_max_ouid();
                levelid = Objquery.get_levelid_of_ouid(ou_id);
                entity_levelid = Objquery.get_entity_levelid();
                if (entity_levelid > levelid)
                {
                    querystatus = Objquery.insert_new_ou_branch_unit(maxouid.ToString(), ou_id, Convert.ToString(levelid + 1), levelid.ToString(), ou_name);
                    Objquery.insert_ouuserroll(maxouid.ToString());
                    Objquery.execute_insallchild();
                    Objquery.execute_inslongname();
                    if (querystatus == 1)
                        result = "OU saved successfully.";
                    else
                        result = "OU saved failed.";
                }
                else
                {
                    result = "No more level available.";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "add_new_ou_branch_unit Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult get_OU_unlinked_devices(string ou_id)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {
                dt = Objquery.Get_OU_unlinked_device_Data(ou_id);
                dt1 = Objquery.Get_OU_self_Data(ou_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "get_OU_unlinked_devices Exception : " + ex.Message.ToString());
            }
            var data = new { ou_data_unlinked_device = dt, ou_data_unlinked_self = dt1 };
            return Json(data);
        }
        public JsonResult link_ou_branch_unit(string ou_id, string device_ids)
        {
            DataTable dt = new DataTable();
            Objquery.insert_update_device_linkage(device_ids, ou_id);
            return Json("OU/Branch Unit linked devices successfully");
        }
        public JsonResult get_OU_unlinked_users(string ou_id)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {
                dt = Objquery.Get_OU_unlinked_user_Data(ou_id);
                dt1 = Objquery.Get_OU_self_Data(ou_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "get_OU_unlinked_users Exception : " + ex.Message.ToString());
            }
            var data = new { ou_data_unlinked_user = dt, ou_data_ou_self = dt1 };
            return Json(data);
        }
        public JsonResult link_user_with_ou_branch_unit(string ou_id, string user_ids)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                user_ids = user_ids.Substring(1);
                querystatus = Objquery.link_user_with_ou_branch_unit(ou_id, user_ids);
                if (querystatus <= 0)
                    result = "User already linked";
                else
                    result = "Users linked with OU successfully";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "link_user_with_ou_branch_unit Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }

        // Settings Agent release license
        // Agent release licence
        public IActionResult setting_agent_release_licence()
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

        // Get Data Uninstall and agent release lincence 
        public JsonResult GetAgentReleaseLicence(string IP, string Device, string ouid, string startdate, string enddate)
        {
            string Sdate = "";
            string Edate = "";
            string searchdate = "";

            if (startdate != null & enddate != null)
            {
                Sdate = System.DateTime.ParseExact(startdate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 00:00:00";
                Edate = System.DateTime.ParseExact(enddate.ToString(), "dd/MM/yyyy hh:mm tt", null).ToString("yyyy-MM-dd") + " 23:59:59";
                searchdate = " and nu.last_poll_time > '" + Sdate + "' and nu.last_poll_time < '" + Edate + "' ";
            }

            try
            {
                string search = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (Device != null && Device != "-1")
                {
                    if (Device != "-1")
                        search += " and dl.device_name like'%" + Device + "%'";
                }
                if (IP != null)
                {
                    if (IP != "")
                        search += " and dl.ip like '%" + IP + "%'";
                }
                dt = Objquery.Get_Agent_release_licence(search, searchdate);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_setting_pageController", "GetAgentReleaseLicence Exception : " + ex.Message.ToString());
            }
            var data = new { userdata = dt };
            return Json(data);
        }

        // Uninstall and agent release lincence 
        public JsonResult deleteAgentReleaseLicence(string agentdelete, string deviceidlist)
        {
            string strquerymsg = "";
            string strreturn = "";
            try
            {
                if (agentdelete == null || deviceidlist == null)
                {
                    strreturn = "Please select atleast one device";
                }
                else
                {
                    if (deviceidlist.Trim().Length > 0)
                    {
                        if (agentdelete == "0")
                        {
                            deviceidlist = deviceidlist.Substring(1);
                            string[] arragentdelete = deviceidlist.Split(',');
                            for (int i = 0; i < arragentdelete.Length; i++)
                            {
                                if (arragentdelete[i].ToString().Trim().Length > 1)
                                    strquerymsg = "#2222@" + arragentdelete[i] + "!@2222#";
                                querystatus = Objquery.delete_Agent_release_licence(arragentdelete[i], strquerymsg);
                            }
                            strreturn = "Uninstall action successfully applied";
                        }
                        else
                        {
                            string[] strdeviceidarr = deviceidlist.Split(',');
                            for (int i = 0; i < strdeviceidarr.Length; i++)
                            {
                                if (strdeviceidarr[i].ToString().Trim().Length > 1)
                                    Objquery.Release_Agent_release_licence(strquerymsg, strdeviceidarr[i]);
                            }
                            strreturn = "Release license action successfully applied";
                        }
                    }
                    else
                    {
                        strreturn = "Please select atleast one device";
                    }
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_setting_pageController", "deleteAgentReleaseLicence Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        #region CSAT Updater
        public IActionResult setting_csatupdater()
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
        public JsonResult csat_updater_all_task_data()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Objquery.csat_updater_all_task_data();

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_all_task_data Exception : " + ex.Message.ToString());
            }
            var data = new { csat_task_data = dt };
            return Json(data);
        }
        public JsonResult csat_updater_add_new_task(string taskname, string taskdesc)
        {
            string strreturn = "";
            try
            {
                if (taskname.Trim().Length > 0)
                {
                    if (Objquery.csat_updater_check_task_exist(taskname) > 0)
                        strreturn = "Task " + taskname + " already exist";
                    else
                    {
                        if (Objquery.csat_updater_add_new_task(taskname, taskdesc) > 0)
                            strreturn = "Task " + taskname + " submitted successfully";
                    }
                }
                else
                    strreturn = "Please enter task name";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_add_new_task Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        public JsonResult csat_updater_open_activity_add(string taskid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Objquery.csat_updater_open_activity_new(taskid);

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_open_activity_add Exception : " + ex.Message.ToString());
            }
            var data = new { csat_task_data_open_activity = dt };
            return Json(data);
        }
        public JsonResult csat_updater_add_new_activity(string task_id, string activity_type, string dw_file_32, string dw_file_64, string command_32, string command_64, string destination_path_32, string destination_path_64, string working_directory, string source_folder_path, string source_filename, string server_folder_path, string server_filename)
        {
            objcommon.WriteLog("csatsettingController", "task_id:" + task_id + " , activity_type : " + activity_type + " , dw_file_32 : " + dw_file_32 + " , dw_file_64 : " + dw_file_64 + " , command_32 : " + command_32 + " , command_64 : " + command_64 + " , destination_path_32 : " + destination_path_32 + " , destination_path_64 : " + destination_path_64 + " , working_directory : " + working_directory + " , source_folder_path : " + source_folder_path + " , source_filename : " + source_filename + " , server_folder_path : " + server_folder_path + " , server_filename : " + server_filename);
            string strreturn = "";
            string tasktype = activity_type;
            string DownloadURL_32 = "";
            string DownloadURL_64 = "";
            try
            {
                if (tasktype == "2")
                {
                    if (command_32.Trim().Length <= 0 || command_64.Trim().Length <= 0)
                    {
                        strreturn = "Please do not allow blank command";
                    }
                }
                else if (tasktype == "1")
                {
                    if (destination_path_32.Trim().Length <= 0 || destination_path_64.Trim().Length <= 0)
                    {
                        strreturn = "Please do not allow blank destination path";
                    }
                }
                else if (tasktype == "3")
                {
                    if (source_folder_path.Trim().Length <= 0 || source_filename.Trim().Length <= 0 || server_filename.Trim().Length <= 0)
                    {
                        strreturn = "Please do not allow blank path and filename";
                    }
                }
                if (strreturn == "")
                {
                    OwnYITCommon objComm = new OwnYITCommon();
                    //string DownloadPathLoad = objComm.GetGUIconfig("DownLoadPath");
                    //string DownloadPathLoadURL = objComm.GetGUIconfig("DownLoadURL");
                    string DownloadPathLoad = GetGUIconfig("DownLoadPath");
                    string DownloadPathLoadURL = GetGUIconfig("DownLoadURL");
                    objcommon.WriteLog("csatsettingController", "DownloadPathLoad : " + DownloadPathLoad + " , DownloadPathLoadURL : " + DownloadPathLoadURL);

                    long intFileSize_32 = 0;
                    string LocalFilePath_32 = "";
                    long intFileSize_64 = 0;
                    string LocalFilePath_64 = "";
                    if (tasktype == "1")
                    {
                        try
                        {
                            int lindex = 0;
                            string fileName_32 = "";
                            lindex = dw_file_32.LastIndexOf('\\');
                            fileName_32 = dw_file_32.Substring(lindex + 1);
                            //System.IO.File.Copy(dw_file_32, DownloadPathLoad + "\\" + fileName_32, true);

                            FileInfo ff = new FileInfo(DownloadPathLoad + "\\" + fileName_32);
                            intFileSize_32 = (long)ff.Length;

                            DownloadURL_32 = DownloadPathLoadURL + "//" + fileName_32;
                            LocalFilePath_32 = DownloadPathLoad + "\\" + fileName_32;
                            objcommon.WriteLog("csatsettingController", "DownloadURL_32 : " + DownloadURL_32 + " , LocalFilePath_32 : " + LocalFilePath_32 + " , intFileSize_32 : " + intFileSize_32.ToString());
                        }
                        catch (Exception ex)
                        {
                            objcommon.WriteLog("csatsettingController", "csat_updater_add_new_activity 32 Exception : " + ex.Message.ToString());
                        }
                        try
                        {
                            int lindex = 0;
                            string fileName_64 = "";
                            lindex = dw_file_64.LastIndexOf('\\');
                            fileName_64 = dw_file_64.Substring(lindex + 1);
                            //System.IO.File.Copy(dw_file_32, DownloadPathLoad + "\\" + fileName_64, true);

                            FileInfo ff = new FileInfo(DownloadPathLoad + "\\" + fileName_64);
                            intFileSize_64 = (long)ff.Length;

                            DownloadURL_64 = DownloadPathLoadURL + "//" + fileName_64;
                            LocalFilePath_64 = DownloadPathLoad + "\\" + fileName_64;
                            objcommon.WriteLog("csatsettingController", "DownloadURL_64 : " + DownloadURL_64 + " , LocalFilePath_64 : " + LocalFilePath_64 + " , intFileSize_64 : " + intFileSize_64.ToString());

                        }
                        catch (Exception ex1)
                        {
                            objcommon.WriteLog("csatsettingController", "csat_updater_add_new_activity 64 Exception : " + ex1.Message.ToString());
                        }
                    }

                    //string strQuery = "insert into update_task_details(task_id,task_type,download_url_32,download_url_64,download_size_32,download_size_64,task_param_32,task_param_64,update_date,destination_path_32,destination_path_64,localpath_32,localpath_64)" +
                    //    " values(" + task_id + "," + tasktype + ",'" + DownloadURL_32 + "','" + DownloadURL_64 + "','" + intFileSize_32 + "','" + intFileSize_64 + "','" + command_32 + "','" + command_64 + "',getdate(),'" + destination_path_32 + "','" + destination_path_64 + "','" + LocalFilePath_32 + "','" + LocalFilePath_64 + "')";
                    if (Objquery.csat_updater_add_activity_new(task_id, tasktype, DownloadURL_32, DownloadURL_64, intFileSize_32.ToString(), intFileSize_64.ToString(), command_32, command_64, destination_path_32, destination_path_64, LocalFilePath_32, LocalFilePath_64, working_directory, source_folder_path, source_filename, server_folder_path, server_filename) > 0)
                        strreturn = "Task submitted successfully";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_add_new_activity Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        public JsonResult csat_updater_open_activity_show(string taskid, string devicename, string ipaddress)
        {
            DataTable dt = new DataTable();
            DataTable dtdevice = new DataTable();
            DataTable dtdevicesearch = new DataTable();
            try
            {
                string strsearchcond = "";
                if (devicename == null || devicename.ToUpper() == "SELECT SYSTEM")
                    devicename = "";
                if (ipaddress == null || ipaddress.ToUpper() == "SELECT IP ADDRESS")
                    ipaddress = "";

                if (devicename.Trim().Length > 0 || ipaddress.Trim().Length > 0)
                {
                    if (devicename.Trim().Length > 0)
                        strsearchcond = " where device_name = '" + devicename + "'";
                    if (ipaddress.Trim().Length > 0 && strsearchcond.Trim().Length > 0)
                        strsearchcond += " and ip='" + ipaddress + "'";
                    else if (ipaddress.Trim().Length > 0 && strsearchcond.Trim().Length <= 0)
                        strsearchcond = " where ip='" + ipaddress + "'";
                }
                dt = Objquery.csat_updater_open_activity_show(taskid);
                dtdevice = Objquery.csat_updater_open_activity_devices();
                dtdevicesearch = Objquery.csat_updater_open_activity_devices(strsearchcond);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_open_activity_show Exception : " + ex.Message.ToString());
            }
            var data = new { csat_task_data_open_activity = dt, csat_task_apply_devices = dtdevice, csat_task_apply_devices_search = dtdevicesearch };
            return Json(data);
        }
        public JsonResult csat_updater_open_activity_show_only(string taskid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Objquery.csat_updater_open_activity_show(taskid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_open_activity_show_only Exception : " + ex.Message.ToString());
            }
            var data = new { csat_task_data_open_activity = dt };
            return Json(data);
        }
        public JsonResult csat_updater_apply_task_on_devices(string taskid, string deviceids)
        {
            string returndata = "";
            try
            {
                deviceids = deviceids.Substring(1);
                returndata = Objquery.csat_updater_task_apply_on_devices(taskid, deviceids);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_apply_task_on_devices Exception : " + ex.Message.ToString());
            }

            return Json(returndata);
        }
        public JsonResult csat_updater_open_task_apply_on_devices(string taskid)
        {
            DataTable dtdevice = new DataTable();
            try
            {
                dtdevice = Objquery.csat_updater_apply_on_device(taskid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_open_task_apply_on_devices Exception : " + ex.Message.ToString());
            }
            var data = new { csat_task_apply_on_devices = dtdevice };
            return Json(data);
        }
        public JsonResult csat_updater_open_task_apply_success_devices(string taskid)
        {
            DataTable dtdevice = new DataTable();
            try
            {
                dtdevice = Objquery.csat_updater_apply_success_device(taskid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_open_task_apply_success_devices Exception : " + ex.Message.ToString());
            }
            var data = new { csat_task_apply_success_devices = dtdevice };
            return Json(data);
        }
        public JsonResult csat_updater_task_delete(string taskid)
        {
            string strreturn = "";
            try
            {
                if (Objquery.csat_updater_delete_task(taskid) > 0)
                    strreturn = "Delete task successfully";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_updater_task_delete Exception : " + ex.Message.ToString());
            }

            return Json(strreturn);
        }
        #endregion
        // DCM 
        public IActionResult setting_dcm()
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

        public JsonResult get_dcm_policy()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Objquery.Get_DCM_Policy();

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "get_dcm_policy Exception : " + ex.Message.ToString());
            }
            var data = new { dcm_policy = dt };
            return Json(data);
        }

        public JsonResult Add_parameter_master(string software)
        {
            DataTable dtprocessor = new DataTable();
            DataTable dtsoftware = new DataTable();
            DataTable dtos = new DataTable();
            string search = "";
            Objquery.Add_ParameterMasterData();
            dtprocessor = Objquery.Get_ParameterProcessorData();
            dtos = Objquery.Get_ParameterOSData();
            if (software != null)
                search += "  where software  '%" + software + "%'";
            dtsoftware = Objquery.Get_ParameterSoftwareData(search);
            var data = new { dcm_processordata = dtprocessor, dcm_osdata = dtos, dcm_softdata = dtsoftware };
            return Json(data);

        }

        // Insert DCM Template
        public JsonResult Add_DCM_Template(string policyname, string editpolicyid, string chekboxram, string ramsize, string chekboxhdd, string hddsize, string chekboxprocessor, string chkprocessorid, string chkprocessor, string chekboxos, string chkos, string chekboxsoftware, string chksoft, string lan, string cd, string wifi, string antivirus, string dcmaction)
        {
            var first = true;
            var first1 = true;
            var first2 = true;
            int Pid = 0;
            int Rcount = 0;
            int result1 = 0;
            string result = "";
            string strquerymsg = "";
            string strremovemsg = "";
            string dcm = "16";
            if (Objquery.get_policy_byid(policyname) > 0)

            {
                result = "Policy Alredy Exits";
            }
            else
            {
                if (chekboxsoftware != null && chekboxsoftware != "false" && chekboxsoftware != "undefined")
                {
                    if (chksoft != null && chksoft != "false" && chksoft != "undefined" && chksoft != "-1")
                    {
                        string soft = "1";
                        if (dcmaction == "undefined")
                        {
                            dcmaction = "1";
                        }

                        string chksoft1 = chksoft.Substring(1);
                        string[] valuessoft = chksoft1.Split('!');
                        //string chksoftid1 = chksoftid.Substring(1);
                        //string[] valuessoftid = chksoftid1.Split(',');
                        //string chksoftparamid1 = chksoftparamid.Substring(1);
                        //string[] valuessoftparamid = chksoftparamid1.Split(',');
                        string remark = chksoft1.Replace('!', ',');
                        for (int i = 0; i < valuessoft.Length; i++)
                        {
                            if (first == true)
                            {
                                Rcount = Objquery.checkdcmrulescount(policyname, dcm, soft);
                                if (Rcount == 0)
                                {
                                    Rcount = Objquery.Get_Rules_last_Count();
                                    strquerymsg = "#6061@2!" + Rcount + "!16!1!" + dcmaction + "!" + valuessoft.Length + "" + chksoft + "!@6061#";
                                    strremovemsg = "#6061@0!" + Rcount + "!16!1!" + dcmaction + "!" + valuessoft.Length + "" + chksoft + "!@6061#";
                                    objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Software DCM Rules Create msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                                    querystatus = Objquery.insert_rules_master(Rcount.ToString(), policyname, dcm, soft, "1", "1", dcmaction, strquerymsg, strremovemsg, remark);
                                }
                                else
                                {
                                    result = "Policy Alredy Exits";
                                }
                                first = false;
                            }
                        }

                    }
                }
                if (chekboxhdd != null && chekboxhdd != "false" && chekboxhdd != "undefined")
                {
                    string hdd = "5";
                    if (dcmaction == "undefined")
                    {
                        dcmaction = "1";
                    }
                    if (hddsize != null)
                    {
                        Rcount = Objquery.checkdcmrulescount(policyname, dcm, hdd);
                        Int64 size = 0;
                        Int64 str_size = 0;
                        if (hddsize == "1" || hddsize == "2")
                        {
                            str_size = Int64.Parse(hddsize);
                            size = str_size * 1024 * 1024 * 1024 * 1024;
                        }
                        else
                        {
                            str_size = Int64.Parse(hddsize);
                            size = str_size * 1024 * 1024 * 1024;
                        }
                        if (Rcount == 0)
                        {
                            Rcount = Objquery.Get_Rules_last_Count();
                            strquerymsg = "#6061@2!" + Rcount + "!16!5!" + dcmaction + "!1!" + hddsize + "!@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!16!5!" + dcmaction + "!1!" + hddsize + "!@6061#";
                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : HDD DCM Rules create msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master(Rcount.ToString(), policyname, dcm, hdd, "1", "1", dcmaction, strquerymsg, strremovemsg, hddsize);
                        }
                        else
                        {
                            result = "Policy Alredy Exits";
                        }
                    }

                }
                if (chekboxram != null && chekboxram != "false" && chekboxram != "undefined")
                {
                    string ram = "4";
                    if (dcmaction == "undefined")
                    {
                        dcmaction = "1";
                    }
                    if (ramsize != null)
                    {
                        Rcount = Objquery.checkdcmrulescount(policyname, dcm, ram);
                        if (Rcount == 0)
                        {
                            Int64 size = 0;
                            Int64 str_size = 0;
                            str_size = Int64.Parse(ramsize);
                            size = str_size * 1024 * 1024 * 1024;
                            Rcount = Objquery.Get_Rules_last_Count();
                            strquerymsg = "#6061@2!" + Rcount + "!16!4!" + dcmaction + "!1!" + ramsize + "!@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!16!4!" + dcmaction + "!1!" + ramsize + "!@6061#";
                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : RAM DCM Rules create msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master(Rcount.ToString(), policyname, dcm, ram, "1", "1", dcmaction, strquerymsg, strremovemsg, ramsize);
                        }
                        else
                        {
                            result = "Policy Alredy Exits";
                        }
                    }

                }

                if (cd != null && cd != "-1" && cd != "undefined")
                {
                    string cd_msg_no = "7";
                    if (dcmaction == "undefined")
                    {
                        dcmaction = "1";
                    }
                    Rcount = Objquery.checkdcmrulescount(policyname, dcm, cd_msg_no);
                    if (Rcount == 0)
                    {
                        Rcount = Objquery.Get_Rules_last_Count();
                        strquerymsg = "#6061@2!" + Rcount + "!16!7!" + dcmaction + "!1!1!@6061#";
                        strremovemsg = "#6061@0!" + Rcount + "!16!7!" + dcmaction + "!1!1!@6061#";
                        objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : CDDrive DCM Rules create msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        querystatus = Objquery.insert_rules_master(Rcount.ToString(), policyname, dcm, cd_msg_no, "1", "1", dcmaction, strquerymsg, strremovemsg, cd);
                    }
                    else
                    {
                        result = "Policy Alredy Exits";
                    }
                }
                if (lan != null && lan != "-1" && lan != "undefined")
                {
                    string lan_msg_no = "6";
                    if (dcmaction == "undefined")
                    {
                        dcmaction = "1";
                    }
                    Rcount = Objquery.checkdcmrulescount(policyname, dcm, lan_msg_no);
                    if (Rcount == 0)
                    {
                        Rcount = Objquery.Get_Rules_last_Count();
                        strquerymsg = "#6061@2!" + Rcount + "!16!6!" + dcmaction + "!1!1!@6061#";
                        strremovemsg = "#6061@0!" + Rcount + "!16!6!" + dcmaction + "!1!1!@6061#";
                        objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : LAN DCM Rules create msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        querystatus = Objquery.insert_rules_master(Rcount.ToString(), policyname, dcm, lan_msg_no, "1", "1", dcmaction, strquerymsg, strremovemsg, lan);
                    }
                    else
                    {
                        result = "Policy Alredy Exits";
                    }
                }
                if (wifi != null && wifi != "-1" && wifi != "undefined")
                {
                    string wifi_msg_no = "8";
                    if (dcmaction == "undefined")
                    {
                        dcmaction = "1";
                    }
                    Rcount = Objquery.checkdcmrulescount(policyname, dcm, wifi_msg_no);
                    if (Rcount == 0)
                    {
                        Rcount = Objquery.Get_Rules_last_Count();
                        strquerymsg = "#6061@2!" + Rcount + "!16!8!" + dcmaction + "!1!1!@6061#";
                        strremovemsg = "#6061@0!" + Rcount + "!16!8!" + dcmaction + "!1!1!@6061#";
                        objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : WIFI DCM Rules create msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        querystatus = Objquery.insert_rules_master(Rcount.ToString(), policyname, dcm, wifi_msg_no, "1", "1", dcmaction, strquerymsg, strremovemsg, wifi);
                    }
                    else
                    {
                        result = "Policy Alredy Exits";
                    }
                }
                if (chekboxprocessor != null && chekboxprocessor != "false" && chekboxprocessor != "undefined")
                {
                    string processor = "3";
                    if (dcmaction == "undefined")
                    {
                        dcmaction = "1";
                    }
                    if (chkprocessor != null)
                    {
                        string chkprocessor1 = chkprocessor.Substring(1);
                        string[] valuesprocessor = chkprocessor1.Split('!');
                        string remark = chkprocessor1.Replace('!', ',');
                        for (int i = 0; i < valuesprocessor.Length; i++)
                        {
                            if (first1 == true)
                            {
                                Rcount = Objquery.checkdcmrulescount(policyname, dcm, processor);
                                if (Rcount == 0)
                                {
                                    Rcount = Objquery.Get_Rules_last_Count();
                                    strquerymsg = "#6061@2!" + Rcount + "!16!3!" + dcmaction + "!" + valuesprocessor.Length + "" + chkprocessor + "!@6061#";
                                    strremovemsg = "#6061@0!" + Rcount + "!16!3!" + dcmaction + "!" + valuesprocessor.Length + "" + chkprocessor + "!@6061#";
                                    objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Processor DCM Rules Create msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                                    querystatus = Objquery.insert_rules_master(Rcount.ToString(), policyname, dcm, processor, "1", "1", dcmaction, strquerymsg, strremovemsg, remark);
                                }
                                else
                                {
                                    result = "Policy Alredy Exits";
                                }
                                first1 = false;
                            }
                        }

                    }
                }
                if (chekboxos != null && chekboxos != "false" && chekboxos != "undefined")
                {
                    string stros = "2";
                    if (dcmaction == "undefined")
                    {
                        dcmaction = "1";
                    }
                    if (chkos != null)
                    {
                        string chkos1 = chkos.Substring(1);
                        string[] valueschkos = chkos1.Split('!');
                        string remark = chkos1.Replace('!', ',');
                        for (int i = 0; i < valueschkos.Length; i++)
                        {
                            if (first2 == true)
                            {
                                Rcount = Objquery.checkdcmrulescount(policyname, dcm, stros);
                                if (Rcount == 0)
                                {
                                    Rcount = Objquery.Get_Rules_last_Count();
                                    strquerymsg = "#6061@2!" + Rcount + "!16!2!" + dcmaction + "!" + valueschkos.Length + "" + chkos + "!@6061#";
                                    strremovemsg = "#6061@0!" + Rcount + "!16!2!" + dcmaction + "!" + valueschkos.Length + "" + chkos + "!@6061#";
                                    objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Processor DCM Rules Create msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                                    querystatus = Objquery.insert_rules_master(Rcount.ToString(), policyname, dcm, stros, "1", "1", dcmaction, strquerymsg, strremovemsg, remark);
                                }
                                else
                                {
                                    result = "Policy Alredy Exits";
                                }
                                first2 = false;
                            }
                        }

                    }
                }
                if (policyname != null)
                {
                    string strpolicyname = policyname;
                    string DCMpolicyname = strpolicyname += "_Policy";
                    string DCMtype = "16";
                    querystatus = Objquery.insert_policy_type(DCMpolicyname, DCMtype);
                    Pid = Objquery.get_policy_byid(DCMpolicyname);
                    DataTable rulelist = new DataTable();
                    rulelist = Objquery.Get_Rule_Data(policyname);
                    for (int k = 0; k < rulelist.Rows.Count; k++)
                    {
                        result1 = Objquery.insert_policy_linkage(Pid, rulelist.Rows[k]["rules_id"].ToString());
                    }
                    result = "Policy Added Successfully";
                }
            }



            //-----------------------------------------
            //if (editpolicyid != null)
            // strquery += Objquery.Delete_DCMParameterData(editpolicyid);
            //if (Objquery.checkdcmpolicycount(policyname) > 0 && editpolicyid == null)
            //{
            //    result = "Policy already exist";
            //}
            //else
            //{
            //    dt = Objquery.Get_Parametervaluesforadd();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        switch (dt.Rows[i][2].ToString().ToUpper())
            //        {
            //            case "RAM":
            //                hdnram = dt.Rows[i][0].ToString();
            //                break;
            //            case "HARDDISK":
            //                hdnhdd = dt.Rows[i][0].ToString();
            //                break;
            //            case "CDDRIVE":
            //                hdncd = dt.Rows[i][0].ToString();
            //                break;
            //            case "LAN":
            //                hdnlan = dt.Rows[i][0].ToString();
            //                break;
            //            case "WIFI":
            //                hdnwifi = dt.Rows[i][0].ToString();
            //                break;
            //            case "ANTIVIRUS":
            //                hdnav = dt.Rows[i][0].ToString();
            //                break;
            //            case "PROCESSOR":
            //                break;
            //        }

            //    }
            //    // Add Ram Policy
            //    if (chekboxram == "true")
            //    {
            //        if (editpolicyid != null)
            //            strquery += Objquery.edit_dcmrampolicy(editpolicyid, hdnram, "11", "RAM", ramsize, "MB", null, dcmaction);
            //        else
            //            strquery += Objquery.add_dcmrampolicy(hdnram, "11", "RAM", ramsize, "MB", null, dcmaction);
            //    }
            //    // Add HDD Policy
            //    if (chekboxhdd == "true")
            //    {
            //        string sizestring = "";
            //        if (hddsize == "1" || hddsize == "2")
            //        {
            //            sizestring = "TB";
            //        }
            //        else
            //        {
            //            sizestring = "GB";
            //        }
            //        if (editpolicyid != null)
            //        {

            //            strquery += Objquery.edit_dcmrampolicy(editpolicyid, hdnhdd, "11", "HARDDISK", hddsize, sizestring, null, dcmaction);
            //        }
            //        else
            //        {
            //            strquery += Objquery.add_dcmrampolicy(hdnhdd, "11", "HARDDISK", hddsize, sizestring, null, dcmaction);
            //        }

            //    }
            //    if (lan != null)
            //    {
            //        if (lan != "undefined")
            //        {
            //            if (editpolicyid != null)
            //                strquery += Objquery.edit_dcmrampolicy(editpolicyid, hdnlan, "11", "LAN", lan, null, null, dcmaction);
            //            else
            //                strquery += Objquery.add_dcmrampolicy(hdnlan, "11", "LAN", lan, null, null, dcmaction);
            //        }
            //    }
            //    if (cd != null)
            //    {
            //        if (cd != "undefined")
            //        {
            //            if (editpolicyid != null)
            //                strquery += Objquery.edit_dcmrampolicy(editpolicyid, hdncd, "11", "CDDrive", cd, null, null, dcmaction);
            //            else
            //                strquery += Objquery.add_dcmrampolicy(hdncd, "11", "CDDrive", cd, null, null, dcmaction);
            //        }
            //    }
            //    if (antivirus == "1")
            //    {
            //        if (editpolicyid != null)
            //            strquery += Objquery.edit_dcmrampolicy(editpolicyid, hdnav, "11", "Antivirus", antivirus, null, null, dcmaction);
            //        else
            //            strquery += Objquery.add_dcmrampolicy(hdnav, "11", "Antivirus", antivirus, null, null, dcmaction);
            //    }
            //    if (wifi != null)
            //    {
            //        if (wifi != "undefined")
            //        {
            //            if (editpolicyid != null)
            //                strquery += Objquery.edit_dcmrampolicy(editpolicyid, hdnwifi, "11", "WIFI", wifi, null, null, dcmaction);
            //            else
            //                strquery += Objquery.add_dcmrampolicy(hdnwifi, "11", "WIFI", wifi, null, null, dcmaction);
            //        }
            //    }

            //    if (chekboxprocessor == "true")
            //    {
            //        string chkprocessor1 = chkprocessor.Substring(1);
            //        string[] values = chkprocessor1.Split(',');
            //        string chkprocessorid1 = chkprocessorid.Substring(1);
            //        string[] valuesid = chkprocessorid1.Split(',');
            //        for (int i = 0; i < values.Length; i++)
            //        {
            //            if (editpolicyid != null)
            //                strquery += Objquery.edit_dcmrampolicy(editpolicyid, valuesid[i].ToString(), "11", "PROCESSOR", "0", null, values[i].ToString(), dcmaction);
            //            else
            //                strquery += Objquery.add_dcmrampolicy(valuesid[i].ToString(), "11", "PROCESSOR", "0", null, values[i].ToString(), dcmaction);
            //        }
            //    }

            //    if (chksoft != null)
            //    {
            //        if (chksoftid != null)
            //        {
            //            string chksoft1 = chksoft.Substring(1);
            //            string[] valuessoft = chksoft1.Split(',');
            //            string chksoftid1 = chksoftid.Substring(1);
            //            string[] valuessoftid = chksoftid1.Split(',');
            //            string chksoftparamid1 = chksoftparamid.Substring(1);
            //            string[] valuessoftparamid = chksoftparamid1.Split(',');

            //            for (int i = 0; i < valuessoft.Length; i++)
            //            {
            //                if (editpolicyid != null)
            //                    strquery += Objquery.edit_dcmrampolicy(editpolicyid, valuessoftparamid[i].ToString(), "12", "SOFTWARE", valuessoftid[i].ToString(), null, valuessoft[i].ToString(), dcmaction);
            //                else
            //                    strquery += Objquery.add_dcmrampolicy(valuessoftparamid[i].ToString(), "12", "SOFTWARE", valuessoftid[i].ToString(), null, valuessoft[i].ToString(), dcmaction);
            //            }

            //        }
            //    }


            //    if (chekboxos == "true")
            //    {
            //        string chkos1 = chkos.Substring(1);
            //        string[] valuesos = chkos1.Split(',');
            //        string chkosid1 = chkosid.Substring(1);
            //        string[] valuesosid = chkosid1.Split(',');
            //        for (int i = 0; i < valuesos.Length; i++)
            //        {
            //            if (editpolicyid != null)
            //                strquery += Objquery.edit_dcmrampolicy(editpolicyid, valuesosid[i].ToString(), "13", "OS", "0", null, valuesos[i].ToString(), dcmaction);
            //            else
            //                strquery += Objquery.add_dcmrampolicy(valuesosid[i].ToString(), "13", "OS", "0", null, valuesos[i].ToString(), dcmaction);
            //        }
            //    }

            //    if (editpolicyid != null)
            //        Objquery.execute_editdcmpolicy(strquery);
            //    else
            //        Objquery.execute_dcmpolicy(policyname, strquery);

            //    result = "Policy Added Successfully";
            //}

            return Json(result);
        }

        // Edit DCM Template Detail
        public JsonResult GetDCMTemplatedetail(string edittempid)
        {
            dt = Objquery.Get_DCMParameterProcessorData(edittempid);
            var data = new { templatedetail = Objquery.Get_DCMTemplatedetails(edittempid), templateprocessordata = dt, templateosdata = Objquery.Get_DCMParameterOSData(edittempid) };
            return Json(data);
        }
        public JsonResult GetDCMTemplateSoftdetail(string edittempid, string software)
        {
            string search = "";
            if (software != null)
                search += " and pm.value like '%" + software + "%'";
            var data = new { templatesoftdata = Objquery.Get_DCMParameterSoftData(edittempid, software) };
            return Json(data);
        }

        // Apply DCM Template
        public JsonResult Apply_DCMTemplate(string ouid, string policyid, string device_name, string ip)
        {
            var data = new { getou = Objquery.Get_OUList("", "1"), getdevice = Objquery.Get_DCMsyatemname_ip(ouid, policyid, device_name, ip) };
            return Json(data);
        }

        public JsonResult DCM_Apply_Submit(string applypolicyid, string deviceids, string ouid)
        {
            string devices = deviceids.Substring(1);
            string[] values = devices.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                cnt = Objquery.policy_linkage_master_deviceid_count(applypolicyid, values[i].ToString());
                if (cnt == 0)
                {
                    querystatus = Objquery.insert_policy_linkage_master(applypolicyid, "0", values[i].ToString(), null);
                    querystatus = Objquery.insert_policy_deployment_master("0", values[i].ToString(), null, applypolicyid);
                    dt = Objquery.Get_device_policy_info(" where dl.device_id in(" + values[i].ToString() + ")");
                    querystatus = Objquery.insert_ou_deployment_linkage_master(dt.Rows[0]["ou_id"].ToString(), dt.Rows[0]["device_id"].ToString(), dt.Rows[0]["login_user"].ToString(), applypolicyid);
                    querystatus = Objquery.insert_Policy_Querylog(dt.Rows[0]["device_id"].ToString(), applypolicyid, Objquery.Get_Location_ID(dt.Rows[0]["device_id"].ToString()));

                    if (querystatus == 0)
                        result = "Policy applied failed.";
                    else
                        result = "Policy applied successfully.";
                }
                else
                {
                    result = "Policy already applied.";
                }
            }

            return Json(result);
        }


        // Delete DCM Template

        public JsonResult DCMDeleteTemplate(string policyid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.Delete_DCMTemplate(policyid);
                if (querystatus > 1)
                    result = "Policy deleted successfully";
                else
                    result = "Policy does not deleted successfully";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "DCMDeleteTemplate Exception : " + ex.Message.ToString());

            }
            return Json(result);
        }

        // Show DCM Apply Count
        public JsonResult Show_DCMCount(string policyid)
        {

            try
            {
                dt = Objquery.ShowDCMCountdata(policyid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Show_DCMCount Exception : " + ex.Message.ToString());

            }
            var data = new { showcountdata = dt };
            return Json(data);
        }

        // Add Software Compliance
        public JsonResult Add_Soft_Compliance(string softaction)
        {
            int querystatus = 0;
            string strPB = "";
            string result = "";
            try
            {

                if (softaction == "1")
                    strPB = "10";
                if (softaction == "2")
                    strPB = "01";
                if (softaction == "3")
                    strPB = "11";

                querystatus = Objquery.Add_SoftCompliance(strPB);
                if (querystatus > 0)
                    result = "Compliance setting changed successfully";

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Add_Soft_Compliance Exception : " + ex.Message.ToString());

            }
            return Json(result);
        }

        public JsonResult GetGroupData()
        {
            try
            {
                dt = Objquery.GetGroupData();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetGroupData Exception : " + ex.Message.ToString());
            }
            var data = new { groupdata = dt };
            return Json(data);
        }
        [HttpPost]
        public JsonResult addgroup(string group_type, string group_name, string item_type)
        {
            int group_id = 0;
            try
            {
                if (group_type != "" && item_type != "" && group_name != "")
                {
                    group_id = Objquery.get_max_groupid();
                    cnt = Objquery.Get_group_item_count(group_type, item_type);
                    if (cnt == 0)
                    {
                        querystatus = Objquery.addgroup(group_type, group_name, item_type, group_id);
                        if (querystatus == 1)
                            result = "Group added successfully";
                        else
                            result = "Group added failed";
                    }
                    else
                        result = "Group already exists.";
                }
                else
                    result = "Please enter group name.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "addgroup Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult deletegroup(string groupid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.deletegroup(groupid);
                if (querystatus == 0)
                    result = "Group deleted failed.";
                else
                    result = "Group deleted successfully.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deletegroup Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult GetItemData(string group_id)
        {
            try
            {
                dt = Objquery.GetItemData(group_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetItemData Exception : " + ex.Message.ToString());
            }
            var data = new { itemdata = dt };
            return Json(data);
        }
        public JsonResult additem(string group_id, string group_type, string group_name, string item_type, string item_name)
        {
            int item_id = 0;
            try
            {
                if (item_name != "")
                {
                    item_id = Objquery.get_max_itemid(group_id);
                    cnt = Objquery.Get_group_item_count_check(group_type, item_type, item_name);
                    if (cnt == 0)
                    {
                        if (item_id == 1)
                            querystatus = Objquery.updateitem(group_id, group_type, group_name, item_type, item_id, item_name);
                        else
                            querystatus = Objquery.additem(group_id, group_type, group_name, item_type, item_id, item_name);

                        if (querystatus == 1)
                            result = "Item added successfully";
                        else
                            result = "Item added failed";
                    }
                    else
                        result = "Item already exists.";
                }
                else
                    result = "Please enter item name.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "additem Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult deleteitem(string groupid, string itemid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.deleteitem(groupid, itemid);
                if (querystatus == 0)
                    result = "Item deleted failed.";
                else
                    result = "Item deleted successfully.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deleteitem Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public IActionResult setting_software_authentication()
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

        // Process Authentication
        public IActionResult setting_process_authentication()
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
        // Event Monitoring
        public IActionResult setting_event_monitoring()
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

        #region Process Authentication

        public JsonResult ProcessAction(string process, string ActionType)
        {
            int querystatus = 0;
            string type = "";
            bool result = false;
            try
            {
                switch (ActionType.ToUpper())
                {
                    case "WHITELIST":
                        type = Objquery.Check_Authorized_Type(2, process);
                        if (type == null)
                            querystatus = Objquery.Insert_Un_Authorized(2, process, Objquery.Get_ProcessID(process), 1);
                        else if (type == "0")
                            querystatus = Objquery.Update_Un_Authorized(1, 2, process);
                        break;
                    case "BLACKLIST":
                        type = Objquery.Check_Authorized_Type(2, process);
                        if (type == null)
                            querystatus = Objquery.Insert_Un_Authorized(2, process, Objquery.Get_ProcessID(process), 0);
                        else if (type == "1")
                            querystatus = Objquery.Update_Un_Authorized(0, 2, process);
                        break;
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "ProcessAction Exception : " + ex.Message.ToString());
                result = false;
            }

            if (querystatus == 0)
                result = false;
            else
                result = true;
            return Json(result);
        }
        public JsonResult csat_process_authentication_data(string strsearch)
        {
            DataTable dt = new DataTable();
            try
            {
                if (strsearch == null)
                    strsearch = "";
                dt = Objquery.csat_process_data(strsearch);

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_process_authentication_data Exception : " + ex.Message.ToString());
            }
            var data = new { csat_process_auth_data = dt };
            return Json(data);
        }
        public JsonResult csat_process_authentication_apply(string process_selected_value, string process_not_selected)
        {
            string strreturn = "";
            int cnt = 0;
            try
            {
                cnt = Objquery.csat_process_auth_apply(process_not_selected, process_selected_value);
                if (cnt > 0)
                    strreturn = "Process authentication successfully apply";
                else
                    strreturn = "Some problem applying Process authentication";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_process_authentication_apply Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        #endregion
        #region Software Authentication

        public JsonResult SoftwareAction(string software, string ActionType, string guid)
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
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "SoftwareAction Exception : " + ex.Message.ToString());
                result = false;
            }

            if (querystatus == 0)
                result = false;
            else
                result = true;
            return Json(result);
        }
        public JsonResult csat_software_authentication_data(string strsearch)
        {
            DataTable dt = new DataTable();
            try
            {
                if (strsearch == null)
                    strsearch = "";
                dt = Objquery.csat_software_data(strsearch);

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_software_authentication_data Exception : " + ex.Message.ToString());
            }
            var data = new { csat_software_auth_data = dt };
            return Json(data);
        }
        //public JsonResult csat_software_authentication_apply(string software_selected_value, string software_not_selected)


        public JsonResult csat_software_authentication_apply(string commentText)
        {
            string strreturn = "";
            int cnt = 0;
            try
            {
                //cnt = Objquery.csat_software_auth_apply(software_not_selected, software_selected_value);
                cnt = Objquery.csat_software_auth_apply(commentText, "");
                if (cnt > 0)
                    strreturn = "Software authentication successfully apply";
                else
                    strreturn = "Some problem applying Software authentication";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_software_authentication_apply Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        #endregion

        #region Event Monitoring
        public JsonResult csat_event_monitoring_data()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Objquery.csat_event_data();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_monitoring_data Exception : " + ex.Message.ToString());
            }
            var data = new { csat_event_data = dt };
            return Json(data);
        }
        public JsonResult csat_event_monitoring_new(string event_type, string event_id, string event_desc)
        {
            string strreturn = "";
            try
            {
                if (Objquery.csat_event_new_create(event_type, event_id, event_desc) == 1)
                {
                    strreturn = "Event submitted successfully";
                }
                else
                {
                    strreturn = "Event not submitted";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_monitoring_new Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        public JsonResult csat_event_edit_event_data(string rule_id)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Objquery.csat_event_edit_data(rule_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_edit_event_data Exception : " + ex.Message.ToString());
            }
            var data = new { csat_event_edit_data = dt };
            return Json(data);
        }
        public JsonResult csat_event_delete_event(string rule_id)
        {
            string strreturn = "";
            try
            {
                if (Objquery.csat_event_delete_event_id(rule_id) > 0)
                {
                    strreturn = "Event deleted successfully";
                }
                else
                {
                    strreturn = "Some problem when Event delete";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_delete_event Exception : " + ex.Message.ToString());
            }

            return Json(strreturn);
        }
        public JsonResult csat_event_save_event(string rule_id, string event_desc)
        {
            string strreturn = "";
            try
            {
                if (Objquery.csat_event_save_event(rule_id, event_desc) >= 0)
                {
                    strreturn = "Event submitted successfully";
                }
                else
                {
                    strreturn = "Event not submitted";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_save_event Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        public JsonResult csat_event_single_apply_data(string rule_id)
        {
            DataTable dt = new DataTable();
            DataTable dtou = new DataTable();
            DataTable dtdevice = new DataTable();
            try
            {
                dt = Objquery.csat_event_single_data(rule_id);
                dtou = Objquery.Get_OUList("", "1");
                dtdevice = Objquery.csat_event_single_apply_device(rule_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_single_apply_data Exception : " + ex.Message.ToString());
            }
            var data = new { csat_event_single_data = dt, csat_event_single_oudata = dtou, csat_event_single_device_data = dtdevice };
            return Json(data);
        }
        public JsonResult csat_event_single_apply_data_search(string rule_id, string ou_id, string device_name, string ip)
        {
            DataTable dtdevice = new DataTable();
            try
            {
                dtdevice = Objquery.csat_event_single_apply_device(rule_id, ou_id, device_name, ip);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_single_apply_data_search Exception : " + ex.Message.ToString());
            }
            var data = new { csat_event_single_device_data = dtdevice };
            return Json(data);
        }
        public JsonResult csat_event_single_apply_submit(string rule_id, string deviceids)
        {
            string strreturn = "";
            DataTable dtdevice = new DataTable();
            try
            {
                deviceids = deviceids.Substring(1);
                if (Objquery.csat_event_single_apply_submit(rule_id, deviceids) > 0)
                    strreturn = "Event apply on devices successfully";
                else
                    strreturn = "Some problem when Event apply";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_single_apply_submit Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        public JsonResult csat_event_multiply_apply_data(string rule_id)
        {
            DataTable dt = new DataTable();
            DataTable dtou = new DataTable();
            DataTable dtdevice = new DataTable();
            try
            {
                //rule_id = rule_id.Substring(1);
                dt = Objquery.csat_event_multiple_data(rule_id);
                dtou = Objquery.Get_OUList("", "1");
                dtdevice = Objquery.csat_event_multiple_apply_device();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_multiply_apply_data Exception : " + ex.Message.ToString());
            }
            var data = new { csat_event_multiple_data = dt, csat_event_multiple_oudata = dtou, csat_event_multiple_device_data = dtdevice };
            return Json(data);
        }
        public JsonResult csat_event_multiple_apply_data_search(string ou_id, string device_name, string ip)
        {
            DataTable dtdevice = new DataTable();
            try
            {
                dtdevice = Objquery.csat_event_multiple_apply_device(ou_id, device_name, ip);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_multiple_apply_data_search Exception : " + ex.Message.ToString());
            }
            var data = new { csat_event_multiple_device_data = dtdevice };
            return Json(data);
        }
        public JsonResult csat_event_multiple_apply_submit(string rule_ids, string deviceids)
        {
            string strreturn = "";
            DataTable dtdevice = new DataTable();
            try
            {
                deviceids = deviceids.Substring(1);
                rule_ids = rule_ids.Substring(1);
                string[] strruleidarr = rule_ids.Split(',');
                for (int i = 0; i < strruleidarr.Length; i++)
                {
                    Objquery.csat_event_single_apply_submit(strruleidarr[i].ToString(), deviceids);
                }
                strreturn = "Event apply on devices successfully";
            }
            catch (Exception ex)
            {
                strreturn = "Some problem when Event apply";
                objcommon.WriteLog("csatsettingController", "csat_event_multiple_apply_submit Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }
        public JsonResult csat_event_apply_popup(string rule_id, string apply_type)
        {
            //string strreturn = "";
            DataTable dtdevice = new DataTable();
            try
            {
                if (apply_type == "0")
                    dtdevice = Objquery.csat_event_apply_on(rule_id);
                else if (apply_type == "1")
                    dtdevice = Objquery.csat_event_apply_success(rule_id);

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "csat_event_apply_popup Exception : " + ex.Message.ToString());
            }
            var data = new { csat_event_apply_device_data = dtdevice };
            return Json(data);
        }
        #endregion

        public IActionResult setting_managedisk_cleanup()
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
        public IActionResult setting_service()
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
        public JsonResult Getservice()
        {
            var data = new { sname = Objquery.Get_servicename(), dname = Objquery.Get_displayname() };
            return Json(data);
        }
        public JsonResult Getservice_settings_data(string IP, string Device, string ouid, string servicename, string displayname)
        {
            LoadData loaddata = new LoadData();
            try
            {

                string search = "";
                if (ouid != null)
                {
                    if (ouid != "-1")
                        search += " and dl.ou_id in(" + Objquery.Get_ParentOu_id(ouid) + ")";
                }
                if (Device != null)
                {
                    if (Device != "-1")
                        search += " and device_name ='" + Device + "'";
                }
                if (IP != null)
                {
                    if (IP != "")
                        search += " and ip like '%" + IP + "%'";
                }
                if (servicename != null && servicename != "")
                {
                    search += " and nis.service_name  like '%" + servicename + "%'";
                }
                if (displayname != null && displayname != "")
                {
                    search += " and nis.display_name  like '%" + displayname + "%'";
                }

                dt = Objquery.service_setting_data(search);
                loaddata = objcommon.DatatableToObject(dt);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csat_reportController", "Getservice_settings_data Exception : " + ex.Message.ToString());
            }
            //var data = new { servicedata = dt };
            return Json(loaddata);
        }

        public JsonResult service_settings_apply(string IP, string Deviceid, string ouid, string servicename, string displayname, string servicestatus, string servicetype)
        {
            string strmsg = "";
            string strmsg1 = "";
            string strreturn = "";
            try
            {

                if (servicetype == null)
                    servicetype = "-1";
                if (servicestatus == null)
                    servicestatus = "-1";
                Deviceid = Deviceid.Substring(1);
                strmsg = "#77@" + servicename + "!" + servicetype + "!" + servicestatus + "!@77#";
                strmsg1 = "#17@2!@17#";
                Objquery.service_setting_appy(Deviceid, strmsg);
                Objquery.service_setting_appy(Deviceid, strmsg1);
                strreturn = "Service setting apply successfully";
            }
            catch (Exception ex)
            {
                strreturn = "Service setting not apply some reason";
                objcommon.WriteLog("csat_reportController", "service_settings_apply Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }


        // Manage Disk Cleanup
        // Show database backup
        public JsonResult Showdiskcleanup()
        {
            try
            {
                dt = Objquery.Get_diskcleanup();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Showdiskcleanup Exception : " + ex.Message.ToString());
            }
            var data = new { diskcleaup = dt };
            return Json(data);
        }

        public JsonResult GetDeviceData(string ouid, string device, string ip)
        {
            var data = new { getdevice = Objquery.Get_device(ouid, device, ip) };
            return Json(data);
        }
        // Add Disk Cleanup Schedule
        public JsonResult Adddiskcleanupschedule(string deviceid, string backuptype, string backupdetails, string backuptime)
        {
            try
            {
                //int Rcount = 0;
                //string strquerymsg = "";
                //int querystatus = 0;

                if (backupdetails == "0000000")
                    result = "Please select days";
                else if (backuptype == "-1")
                    result = "Please select schedule";
                else
                {
                    //Rcount = Objquery.Get_Rules_last_Count();
                    ////#6061@Action!RuleID!13!1!Datails!time!@6061#
                    //strquerymsg = "#6061@2!" + Rcount + "!" + "13!" + backuptype + "!" + backupdetails + "!" + backuptime + "!@6061#";
                    //querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, "13", backuptype, policy_type, verification, status, strquerymsg, strremovemsg, desc);
                    //objcommon.WriteLog("csatsettingController", "Create_Rules : Manage_Disk_Cleanup Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                    string deviceid1 = deviceid.Substring(1);
                    string[] deviceidvalues = deviceid1.Split(',');

                    for (int i = 0; i < deviceidvalues.Length; i++)
                    {
                        if (Objquery.Check_DiskDeviceSchedulercount(deviceidvalues[i].ToString()) == 0)
                            cnt = Objquery.Add_diskschedule(deviceidvalues[i].ToString(), backuptype, backupdetails, backuptime);
                        else
                            cnt = Objquery.Update_diskschedule(deviceidvalues[i].ToString(), backuptype, backupdetails, backuptime);
                    }
                    if (cnt == 0)
                        result = "Devicewise schedule not set successfully";
                    else
                        result = "Devicewise schedule set successfully";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "Adddiskcleanupschedule Exception : " + ex.Message.ToString());
            }


            return Json(result);

        }

        public IActionResult setting_configuration()
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
        public JsonResult GetConfigurationdata(string sectionname)
        {
            try
            {
                dt = Objquery.GetConfiguration(sectionname);
            }

            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "GetConfigurationdata Exception : " + ex.Message.ToString());
            }
            var data = new { configdata = dt, sectionname = Objquery.GetSection() };
            return Json(data);
        }

        public JsonResult AddConfiguration(string sec, string prop, string propval)
        {
            string result = "";
            int querystatus = 0;
            try
            {
                string jj = prop.Substring(1);
                string[] prop1 = jj.Split(',');
                string jj1 = propval.Substring(1);
                string[] propval1 = jj1.Split('!');
                for (int i = 0; i < prop1.Length; i++)
                {
                    querystatus = Objquery.Update_configuration(sec, prop1[i].ToString(), propval1[i].ToString());
                }
                if (querystatus == 0)
                    result = "Configuration Updated failed.";
                else
                    result = "Configuration Updated successfully.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "AddConfiguration Exception : " + ex.Message.ToString());
            }
            return Json(result);

        }

        public IActionResult setting_makesetup()
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

        public JsonResult create_setup(string ServerIP, string FolderPath)
        {
            string value = "";
            OperatingSystem os_info = System.Environment.OSVersion;

            if (os_info.VersionString.ToString().Contains("Windows"))
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(FolderPath + "\\OwnYITEEUpdater.xml");
                    ds.Tables[0].Rows[0]["ServerIP"] = ServerIP;
                    ds.WriteXml(FolderPath + "\\OwnYITEEUpdater.xml");
                    StringBuilder strMMFile = new StringBuilder();
                    strMMFile.Append("#include \"OwnItUpdater.MMH\"\n");
                    strMMFile.Append("<$Property \"REBOOT\" VALUE=\"ReallySuppress\">\n");
                    strMMFile.Append("<$Property \"Manufacturer\"   Value=\"Tectona SoftSolutions(P) Ltd\">\n");
                    string[] filePaths = Directory.GetFiles(FolderPath);
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        string onlyFileName = filePaths[i].Trim().Substring(filePaths[i].ToString().LastIndexOf("\\")).Replace("\\", "");

                        if (onlyFileName.Trim().ToUpper() != "CREATESETUP.MM" && onlyFileName.Trim().ToUpper() != "OWNITUPDATER.MMH" && onlyFileName.Trim().ToUpper() != "OWNITUPDATER.RTF" && onlyFileName.Trim().ToUpper() != "OWNITUPDATER.VER")
                        {
                            strMMFile.AppendFormat("<$Files '{0}\\" + onlyFileName + "' DestDir='INSTALLDIR'>\n", FolderPath);
                        }
                    }
                    //strMMFile.AppendFormat("<$Files '{0}\\CSATHandler.exe' DestDir='INSTALLDIR'>\n", FolderPath);
                    //strMMFile.AppendFormat("<$Files '{0}\\OwnYITEEUpdater.xml' DestDir='INSTALLDIR'>\n", FolderPath);
                    //strMMFile.AppendFormat("<$Files '{0}\\Newtonsoft.Json.dll' DestDir='INSTALLDIR'>\n", FolderPath);

                    strMMFile.Append("#define MyServiceAlias         OwnYITHandler\n");
                    strMMFile.Append("#define MyServiceDesc          OwnYIT Handler Service\n");
                    strMMFile.Append("#define RecoveryResetInSeconds <?=6*60*60>  ;;6 hours\n");
                    strMMFile.Append("#( '/'\n");
                    strMMFile.Append("   #define RecoveryActions\n");
                    strMMFile.Append("   restart/5000                ;;1.  Restart after 5 seconds\n");
                    strMMFile.Append("   restart/60000               ;;2.  Restart after 1 minute\n");
                    strMMFile.Append("   restart/600000              ;;3+. Restart after 10 minutes\n");
                    strMMFile.Append("#)\n");
                    strMMFile.Append("<$DirectoryTree Key=\"INSTALLDIR\" Dir=\"[ProgramFilesFolder]\\OwnYit\\MyServiceDirectory\" CHANGE=\"\">\n");
                    strMMFile.Append("<$Component \"<$MyServiceAlias>\" Create=\"Y\" Directory_=\"INSTALLDIR\">\n");
                    strMMFile.Append("   <$Files \".\\OwnyITHandler.exe\" KeyFile=\"*\">\n");
                    strMMFile.Append("   #(\n");
                    strMMFile.Append("       <$ServiceInstall\n");
                    strMMFile.Append("                  Name=\"<$MyServiceAlias>\"\n");
                    strMMFile.Append("           DisplayName=\"<$MyServiceDesc>\"\n");
                    strMMFile.Append("           Description=\"<$MyServiceDesc>\"\n");
                    strMMFile.Append("               Process=\"Own Interactive\"\n");
                    strMMFile.Append("       >\n");
                    strMMFile.Append("   #)\n");
                    strMMFile.Append("   <$ServiceControl Name=\"<$MyServiceAlias>\" AtInstall=\"start stop\" AtUninstall=\"stop delete\">\n");
                    strMMFile.Append("<$/Component>");
                    string strFileName = "CreateSetup.mm";
                    System.IO.File.Delete(FolderPath + "\\" + strFileName);
                    System.IO.File.WriteAllText(FolderPath + "\\" + strFileName, strMMFile.ToString());
                    objcommon.ExecuteProcess(FolderPath, strFileName); // Windows

                    value = "Create setup successfully";
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csat_asset_mgmtController", "create_setup Exception : " + ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    objcommon.ExecuteProcessLinux();
                    value = "Create setup successfully";
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csat_asset_mgmtController", "create_setup Exception : " + ex.Message.ToString());
                }
            }
            return Json(value);
        }

        public IActionResult setting_user_mgmt()
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
                //ViewBag.Json = JsonConvert.SerializeObject(nodes, Formatting.Indented);
                //ViewBag.Json = (new JavaScriptSerializer()).Serialize(nodes);
                return View();
        }

        #region User Management
        public JsonResult ShowGroupmgmt()
        {
            try
            {
                dt = Objquery.Get_Groupmgmt();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "ShowGroupmgmt Exception : " + ex.Message.ToString());
            }
            var data = new { showgroupmgmt = dt };
            return Json(data);
        }
        public JsonResult userlinkup(string groupid)
        {
            string groupname = "";
            try
            {
                groupname = Objquery.get_groupname(groupid);
                dt = Objquery.userlinkup(groupid);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "userlinkup Exception : " + ex.Message.ToString());
            }
            var data = new { user_linkup = dt, groupnameheader = groupname };
            return Json(data);
        }
        public JsonResult deleteuserpopup(string userid, string groupid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.deleteuserpopup(userid, groupid);
                if (querystatus == 0)
                    //  result = "User deleted failed.";
                    result = "User Unlink failed.";
                else
                    //  result = "User deleted successfully.";
                    result = "User Unlink successfully.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deleteuserpopup Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult linkuser()
        {
            try
            {
                dt = Objquery.linkuser();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "linkuser Exception : " + ex.Message.ToString());
            }
            var data = new { link_user = dt };
            return Json(data);
        }
        public JsonResult apply_link_user(string user_ids, string groupid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                user_ids = user_ids.Substring(1);
                string[] userid = user_ids.Split(',');
                string old_groupid = "";
                string groupname = "";
                string username = "";
                for (int i = 0; i < userid.Length; i++)
                {
                    old_groupid = Objquery.get_groupid(userid[i].ToString());
                    groupname = Objquery.get_groupname(old_groupid);
                    username = Objquery.get_username(userid[i].ToString());
                    if (groupname.ToUpper() == "DEFAULT")
                    {
                        querystatus = Objquery.apply_link_user(userid[i].ToString(), groupid);
                        if (querystatus == 0)
                            result = "User link apply failed.";
                        else
                            result = "User link apply successfully.";
                    }
                    else if (groupname != "" && groupname.ToUpper() != "DEFAULT")
                    {
                        result = "User " + username + " is allready exists in " + groupname + " group";
                    }
                    else
                    {
                        querystatus = Objquery.apply_link_user_insert(userid[i].ToString(), groupid);
                        if (querystatus == 0)
                            result = "User link apply failed.";
                        else
                            result = "User link apply successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "apply_link_user Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult add_group(string groupname, string groupdescription, string grouptype, string chkuser, string menuid)
        {
            try
            {
                if (chkuser != null)
                    chkuser = chkuser.Substring(1);
                if (menuid != null)
                    menuid = menuid.Substring(1);
                cnt = Objquery.Get_group_count(groupname);
                if (cnt == 0)
                {
                    querystatus = Objquery.add_group(groupname, groupdescription, grouptype, chkuser, menuid);
                    if (querystatus == 1)
                        result = "Group added successfully";
                    else
                        result = "Group added failed";
                }
                else
                    result = "Group already exists.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "add_group Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult get_group_data(string groupid)
        {
            //DataTable dt2 = new DataTable();
            //DataTable dt3 = new DataTable();
            //DataTable dt4 = new DataTable();
            //DataTable dt5 = new DataTable();
            //var nodeslist = "";

            //try
            //{
            //    List<TreeViewNode> nodes = new List<TreeViewNode>();
            //    int parents = 0;
            //    int parents1, parents2, parents3, parents4 = 0;
            //    int menus, menus1, menus2, menus3, menus4 = 0;
            //    dt = Objquery.show_group_rights_data();
            //    List<menu_master> tree = objcommon.ConvertDataTable<menu_master>(dt);
            //    for (int i = 0; i < tree.Count; i++)
            //    {
            //        if (tree[i].menu_parent_id == parents)
            //        {
            //            nodes.Add(new TreeViewNode { id = tree[i].menu_id.ToString(), text = tree[i].menu_name.ToString(), parent = tree[i].menu_parent_id.ToString(), active = tree[i].active.ToString() });
            //            parents = tree[i].menu_parent_id;
            //            menus = tree[i].menu_id;

            //            dt1 = Objquery.child_data(menus);
            //            List<child_menu_master> tree1 = objcommon.ConvertDataTable<child_menu_master>(dt1);

            //            for (int j = 0; j < tree1.Count; j++)
            //            {
            //                nodes.Add(new TreeViewNode { id = tree1[j].menu_id.ToString(), text = tree1[j].menu_name.ToString(), parent = tree1[j].menu_parent_id.ToString(), active = tree1[j].active.ToString() });
            //                parents1 = tree1[j].menu_parent_id;
            //                menus1 = tree1[j].menu_id;

            //                dt2 = Objquery.sub_child_data(menus1);
            //                List<child_menu_master> tree2 = objcommon.ConvertDataTable<child_menu_master>(dt2);

            //                for (int k = 0; k < tree2.Count; k++)
            //                {
            //                    nodes.Add(new TreeViewNode { id = tree2[k].menu_id.ToString(), text = tree2[k].menu_name.ToString(), parent = tree2[k].menu_parent_id.ToString(), active = tree2[k].active.ToString() });
            //                    parents2 = tree2[k].menu_parent_id;
            //                    menus2 = tree2[k].menu_id;

            //                    dt3 = Objquery.sub_sub_child_data(menus2);
            //                    List<child_menu_master> tree3 = objcommon.ConvertDataTable<child_menu_master>(dt3);
            //                    for (int l = 0; l < tree3.Count; l++)
            //                    {
            //                        nodes.Add(new TreeViewNode { id = tree3[l].menu_id.ToString(), text = tree3[l].menu_name.ToString(), parent = tree3[l].menu_parent_id.ToString(), active = tree3[l].active.ToString() });
            //                        parents3 = tree3[l].menu_parent_id;
            //                        menus3 = tree3[l].menu_id;

            //                        dt4 = Objquery.sub_sub_sub_child_data(menus3);
            //                        List<child_menu_master> tree4 = objcommon.ConvertDataTable<child_menu_master>(dt4);
            //                        for (int m = 0; m < tree4.Count; m++)
            //                        {
            //                            nodes.Add(new TreeViewNode { id = tree4[m].menu_id.ToString(), text = tree4[m].menu_name.ToString(), parent = tree4[m].menu_parent_id.ToString(), active = tree4[m].active.ToString() });
            //                            parents4 = tree4[m].menu_parent_id;
            //                            menus4 = tree4[m].menu_id;

            //                            dt5 = Objquery.sub_sub_sub_sub_child_data(menus4);
            //                            List<child_menu_master> tree5 = objcommon.ConvertDataTable<child_menu_master>(dt5);
            //                            for (int n = 0; n < tree5.Count; n++)
            //                            {
            //                                nodes.Add(new TreeViewNode { id = tree5[n].menu_id.ToString(), text = tree5[n].menu_name.ToString(), parent = tree5[n].menu_parent_id.ToString(), active = tree5[n].active.ToString() });
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    nodeslist = JsonConvert.SerializeObject(nodes);
            //}
            //catch (Exception ex)
            //{
            //    objcommon.WriteLog("csatsettingController", "get_group_data Exception : " + ex.Message.ToString());
            //}

            var data = new { getgroupdata = Objquery.get_group_data(groupid), getbinduserdata = Objquery.Get_Link_User(groupid) };
            return Json(data);
        }
        public JsonResult editgroup(string groupname, string groupdescription, string grouptype, string chkuser, string groupid)
        {
            try
            {
                if (chkuser != null)
                {
                    chkuser = chkuser.Substring(1);
                    string[] userid = chkuser.Split(',');
                    string old_groupid = "";
                    string group_name = "";
                    string user_name = "";
                    int cnt1, cnt2 = 0;
                    for (int i = 0; i < userid.Length; i++)
                    {
                        old_groupid = Objquery.get_groupid(userid[i].ToString());
                        group_name = Objquery.get_groupname(old_groupid);
                        user_name = Objquery.get_username(userid[i].ToString());
                        if (group_name.ToUpper() == "DEFAULT")
                        {
                            cnt2 = Objquery.apply_link_user(userid[i].ToString(), groupid);
                        }
                        else if (group_name != "" && group_name.ToUpper() != "DEFAULT")
                        {
                            cnt = Objquery.Get_user_group_relation_count(userid[i].ToString(), groupid);
                            if (cnt == 0)
                            {
                                result = "User " + user_name + " is allready exists in " + group_name + " group";
                                return Json(result);
                            }
                        }
                        else
                        {
                            cnt1 = Objquery.apply_link_user_insert(userid[i].ToString(), groupid);
                        }
                    }
                }
                querystatus = Objquery.editgroup(groupname, groupdescription, grouptype, chkuser, groupid);
                if (querystatus == 1)
                    result = "Group information updated successfully";
                else
                    result = "Group information updated failed";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "editgroup Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult deletegroupdata(string groupid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.deletegroupdata(groupid);
                if (querystatus == 0)
                    result = "Group deleted failed.";
                else
                    result = "Group deleted successfully.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deletegroupdata Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult show_group_rights_data(string groupid)
        {
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            var nodeslist = "";
            string groupname = "";
            try
            {
                List<TreeViewNode> nodes = new List<TreeViewNode>();
                int parents = 0;
                int parents1, parents2, parents3, parents4 = 0;
                int menus, menus1, menus2, menus3, menus4 = 0;
                groupname = Objquery.get_groupname(groupid);
                dt = Objquery.show_group_rights_data(groupid);
                List<menu_master> tree = objcommon.ConvertDataTable<menu_master>(dt);
                for (int i = 0; i < tree.Count; i++)
                {
                    if (tree[i].menu_parent_id == parents)
                    {
                        nodes.Add(new TreeViewNode { id = tree[i].menu_id.ToString(), text = tree[i].menu_name.ToString(), parent = tree[i].menu_parent_id.ToString(), active = tree[i].active.ToString() });
                        parents = tree[i].menu_parent_id;
                        menus = tree[i].menu_id;

                        dt1 = Objquery.child_data(menus);
                        List<child_menu_master> tree1 = objcommon.ConvertDataTable<child_menu_master>(dt1);

                        for (int j = 0; j < tree1.Count; j++)
                        {
                            nodes.Add(new TreeViewNode { id = tree1[j].menu_id.ToString(), text = tree1[j].menu_name.ToString(), parent = tree1[j].menu_parent_id.ToString(), active = tree1[j].active.ToString() });
                            parents1 = tree1[j].menu_parent_id;
                            menus1 = tree1[j].menu_id;

                            dt2 = Objquery.sub_child_data(menus1);
                            List<child_menu_master> tree2 = objcommon.ConvertDataTable<child_menu_master>(dt2);

                            for (int k = 0; k < tree2.Count; k++)
                            {
                                nodes.Add(new TreeViewNode { id = tree2[k].menu_id.ToString(), text = tree2[k].menu_name.ToString(), parent = tree2[k].menu_parent_id.ToString(), active = tree2[k].active.ToString() });
                                parents2 = tree2[k].menu_parent_id;
                                menus2 = tree2[k].menu_id;

                                dt3 = Objquery.sub_sub_child_data(menus2);
                                List<child_menu_master> tree3 = objcommon.ConvertDataTable<child_menu_master>(dt3);
                                for (int l = 0; l < tree3.Count; l++)
                                {
                                    nodes.Add(new TreeViewNode { id = tree3[l].menu_id.ToString(), text = tree3[l].menu_name.ToString(), parent = tree3[l].menu_parent_id.ToString(), active = tree3[l].active.ToString() });
                                    parents3 = tree3[l].menu_parent_id;
                                    menus3 = tree3[l].menu_id;

                                    dt4 = Objquery.sub_sub_sub_child_data(menus3);
                                    List<child_menu_master> tree4 = objcommon.ConvertDataTable<child_menu_master>(dt4);
                                    for (int m = 0; m < tree4.Count; m++)
                                    {
                                        nodes.Add(new TreeViewNode { id = tree4[m].menu_id.ToString(), text = tree4[m].menu_name.ToString(), parent = tree4[m].menu_parent_id.ToString(), active = tree4[m].active.ToString() });
                                        parents4 = tree4[m].menu_parent_id;
                                        menus4 = tree4[m].menu_id;

                                        dt5 = Objquery.sub_sub_sub_sub_child_data(menus4);
                                        List<child_menu_master> tree5 = objcommon.ConvertDataTable<child_menu_master>(dt5);
                                        for (int n = 0; n < tree5.Count; n++)
                                        {
                                            nodes.Add(new TreeViewNode { id = tree5[n].menu_id.ToString(), text = tree5[n].menu_name.ToString(), parent = tree5[n].menu_parent_id.ToString(), active = tree5[n].active.ToString() });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                nodeslist = JsonConvert.SerializeObject(nodes);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "show_group_rights_data Exception : " + ex.Message.ToString());
            }

            var data = new { rights_group_name = groupname, node = nodeslist };
            return Json(data);
        }
        public JsonResult apply_user_rights(string groupid, string menuid)
        {
            try
            {
                if (menuid != null)
                {
                    menuid = menuid.Substring(1);
                    querystatus = Objquery.apply_user_rights(groupid, menuid);
                    if (querystatus == 1)
                        result = "Group rights updated successfully";
                    else
                        result = "Group rights updated failed";
                }
                //    chkuser = chkuser.Substring(1);
                //    string[] select_item = selectitem.Split(',');
                //    string old_groupid = "";
                //    string group_name = "";
                //    string user_name = "";
                //    int cnt1, cnt2 = 0;
                //    for (int i = 0; i < userid.Length; i++)
                //    {
                //        old_groupid = Objquery.get_groupid(userid[i].ToString());
                //        group_name = Objquery.get_groupname(old_groupid);
                //        user_name = Objquery.get_username(userid[i].ToString());
                //        if (group_name.ToUpper() == "DEFAULT")
                //        {
                //            cnt2 = Objquery.apply_link_user(userid[i].ToString(), groupid);
                //        }
                //        else if (group_name != "" && group_name.ToUpper() != "DEFAULT")
                //        {
                //            cnt = Objquery.Get_user_group_relation_count(userid[i].ToString(), groupid);
                //            if (cnt == 0)
                //            {
                //                result = "User " + user_name + " is allready exists in " + group_name + " group";
                //                return Json(result);
                //            }
                //        }
                //        else
                //        {
                //            cnt1 = Objquery.apply_link_user_insert(userid[i].ToString(), groupid);
                //        }
                //    }
                //}
                //querystatus = Objquery.editgroup(groupname, groupdescription, grouptype, chkuser, groupid);
                //if (querystatus == 1)
                //    result = "Group information updated successfully";
                //else
                //    result = "Group information updated failed";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "apply_user_rights Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }


























        public JsonResult ShowUsermgmt()
        {
            string user_name = "";
            try
            {
                user_name = Configuration.username;
                dt = Objquery.Get_Usermgmt();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "ShowUsermgmt Exception : " + ex.Message.ToString());
            }
            var data = new { showusermgmt = dt, get_user_name = user_name };
            return Json(data);
        }
        public JsonResult binduserdata()
        {
            DataTable dt_user = new DataTable();
            var nodeslist = "";

            try
            {
                dt_user = Objquery.Get_Unlink_User();
                dt = Objquery.group_rights_data();
                List<TreeViewNode> nodes = new List<TreeViewNode>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        nodes.Add(new TreeViewNode { id = row["menu_id"].ToString(), text = row["menu_name"].ToString(), parent = row["menu_parent_id"].ToString(), active = row["active"].ToString() });
                    }
                    nodeslist = JsonConvert.SerializeObject(nodes);
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "binduserdata Exception : " + ex.Message.ToString());
            }
            var data = new { binduserdata = dt_user, node = nodeslist };
            return Json(data);
        }

        //public JsonResult binduserdata()
        //{
        //    DataTable dt_user = new DataTable();
        //    DataTable dt2 = new DataTable();
        //    DataTable dt3 = new DataTable();
        //    DataTable dt4 = new DataTable();
        //    DataTable dt5 = new DataTable();
        //    var nodeslist = "";

        //    try
        //    {
        //        dt_user = Objquery.Get_Unlink_User();
        //        List<TreeViewNode> nodes = new List<TreeViewNode>();
        //        int parents = 0;
        //        int parents1, parents2, parents3, parents4 = 0;
        //        int menus, menus1, menus2, menus3, menus4 = 0;
        //        dt = Objquery.group_rights_data();
        //        List<menu_master> tree = objcommon.ConvertDataTable<menu_master>(dt);
        //        for (int i = 0; i < tree.Count; i++)
        //        {
        //            if (tree[i].menu_parent_id == parents)
        //            {
        //                nodes.Add(new TreeViewNode { id = tree[i].menu_id.ToString(), text = tree[i].menu_name.ToString(), parent = tree[i].menu_parent_id.ToString(), active = tree[i].active.ToString() });
        //                parents = tree[i].menu_parent_id;
        //                menus = tree[i].menu_id;

        //                dt1 = Objquery.child_data(menus);
        //                List<child_menu_master> tree1 = objcommon.ConvertDataTable<child_menu_master>(dt1);

        //                for (int j = 0; j < tree1.Count; j++)
        //                {
        //                    nodes.Add(new TreeViewNode { id = tree1[j].menu_id.ToString(), text = tree1[j].menu_name.ToString(), parent = tree1[j].menu_parent_id.ToString(), active = tree1[j].active.ToString() });
        //                    parents1 = tree1[j].menu_parent_id;
        //                    menus1 = tree1[j].menu_id;

        //                    dt2 = Objquery.sub_child_data(menus1);
        //                    List<child_menu_master> tree2 = objcommon.ConvertDataTable<child_menu_master>(dt2);

        //                    for (int k = 0; k < tree2.Count; k++)
        //                    {
        //                        nodes.Add(new TreeViewNode { id = tree2[k].menu_id.ToString(), text = tree2[k].menu_name.ToString(), parent = tree2[k].menu_parent_id.ToString(), active = tree2[k].active.ToString() });
        //                        parents2 = tree2[k].menu_parent_id;
        //                        menus2 = tree2[k].menu_id;

        //                        dt3 = Objquery.sub_sub_child_data(menus2);
        //                        List<child_menu_master> tree3 = objcommon.ConvertDataTable<child_menu_master>(dt3);
        //                        for (int l = 0; l < tree3.Count; l++)
        //                        {
        //                            nodes.Add(new TreeViewNode { id = tree3[l].menu_id.ToString(), text = tree3[l].menu_name.ToString(), parent = tree3[l].menu_parent_id.ToString(), active = tree3[l].active.ToString() });
        //                            parents3 = tree3[l].menu_parent_id;
        //                            menus3 = tree3[l].menu_id;

        //                            dt4 = Objquery.sub_sub_sub_child_data(menus3);
        //                            List<child_menu_master> tree4 = objcommon.ConvertDataTable<child_menu_master>(dt4);
        //                            for (int m = 0; m < tree4.Count; m++)
        //                            {
        //                                nodes.Add(new TreeViewNode { id = tree4[m].menu_id.ToString(), text = tree4[m].menu_name.ToString(), parent = tree4[m].menu_parent_id.ToString(), active = tree4[m].active.ToString() });
        //                                parents4 = tree4[m].menu_parent_id;
        //                                menus4 = tree4[m].menu_id;

        //                                dt5 = Objquery.sub_sub_sub_sub_child_data(menus4);
        //                                List<child_menu_master> tree5 = objcommon.ConvertDataTable<child_menu_master>(dt5);
        //                                for (int n = 0; n < tree5.Count; n++)
        //                                {
        //                                    nodes.Add(new TreeViewNode { id = tree5[n].menu_id.ToString(), text = tree5[n].menu_name.ToString(), parent = tree5[n].menu_parent_id.ToString(), active = tree5[n].active.ToString() });
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        nodeslist = JsonConvert.SerializeObject(nodes);
        //    }
        //    catch (Exception ex)
        //    {
        //        objcommon.WriteLog("csatsettingController", "binduserdata Exception : " + ex.Message.ToString());
        //    }
        //    var data = new { binduserdata = dt_user, node = nodeslist };
        //    return Json(data);
        //}
        public JsonResult bindgroupdata()
        {
            try
            {
                dt = Objquery.Get_Group();
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "bindgroupdata Exception : " + ex.Message.ToString());
            }
            var data = new { bindgroupdata = dt };
            return Json(data);
        }
        public JsonResult adduser(string username, string usertype, string groupname, string emailid, string userpassword, string contactno, string securityquestion, string securityanswer)
        {
            string encryptpassword = "";
            string encryptsequrityanswer = "";
            username = username.ToLower();
            if ((!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(username)) == true)
            {
                try
                {
                    encryptpassword = Encrypt.EncryptText(userpassword);
                    encryptsequrityanswer = Encrypt.EncryptText(securityanswer);

                    //encryptpassword = Encrypt.EncryptString(username, userpassword);
                    //encryptsequrityanswer = Encrypt.EncryptString(securityanswer, securityanswer);
                }
                catch (Exception) { }
            }
            try
            {
                if (username != "" && groupname != "")
                {
                    cnt = Objquery.Get_user_group_count(username, groupname);
                    if (cnt == 0)
                    {
                        contactno = contactno.Replace("(", "(+");
                        querystatus = Objquery.adduser(username, usertype, groupname, emailid, encryptpassword, contactno, securityquestion, encryptsequrityanswer);
                        if (querystatus == 1)
                            result = "User added successfully";
                        else
                            result = "User added failed";
                    }
                    else
                        result = "User already exists.";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "adduser Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        public JsonResult get_user_data(string userid)
        {
            string decryptpassword = "";
            string decryptsequrityanswer = "";
            string userpassword = Objquery.get_userpassword(userid);
            string secretanswer = Objquery.get_secretanswer(userid);
            try
            {
                decryptpassword = Encrypt.DecryptText(userpassword);
                decryptsequrityanswer = Encrypt.DecryptText(secretanswer);
            }
            catch (Exception) { }

            var data = new { getuserdata = Objquery.get_user_data(userid), bindgroupdata = Objquery.Get_Group(), getpassword = decryptpassword, getsecretanswer = decryptsequrityanswer };
            return Json(data);
        }
        public JsonResult edituser(string username, string usertype, string groupname, string emailid, string userpassword, string contactno, string userid, string securityquestion, string securityanswer)
        {
            string encryptpassword = "";
            string encryptsequrityanswer = "";
            //string encryptsequrityanswer1 = "";
            if ((!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(username)) == true)
            {
                try
                {
                    encryptpassword = Encrypt.EncryptText(userpassword);
                    encryptsequrityanswer = Encrypt.EncryptText(securityanswer);

                    //encryptpassword = Encrypt.EncryptString(username, userpassword);
                    //encryptsequrityanswer = Encrypt.EncryptString(securityanswer, securityanswer);
                    //encryptsequrityanswer1 = Encrypt.DecryptString(securityanswer, securityanswer);
                }
                catch (Exception) { }
            }
            try
            {
                contactno = contactno.Replace("(", "(+");
                querystatus = Objquery.edituser(username, usertype, groupname, emailid, encryptpassword, contactno, userid, securityquestion, encryptsequrityanswer);
                if (querystatus == 0)
                    result = "User information updated failed.";
                else
                    result = "User information updated successfully.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "edituser Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }

        public JsonResult old_password_match(string oldpassword, string user_id)
        {
            string encryptpasswoed = "";
            string strreturn = "";
            string passwordencrypt = "";

            try
            {
                encryptpasswoed = Objquery.get_password(user_id);
                passwordencrypt = Encrypt.DecryptText(encryptpasswoed);
                if (oldpassword != passwordencrypt)
                {
                    strreturn = "Please enter valid old password.";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("settingsController", "old_password_match Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }

        public JsonResult set_reset_password(string oldpassword, string newpassword, string userid)
        {
            string encryptpasswoed = "";
            string strreturn = "";
            string passwordencrypt = "";
            string username11 = "";
            try
            {
                int cnt1 = 0;
                int cnt = 0;

                passwordencrypt = Objquery.get_password(userid);
                //encryptpasswoed = Encrypt.EncryptString(username11, oldpassword);
                cnt = Objquery.set_reset_check_old_password(passwordencrypt, userid);
                if (cnt == -1)
                {
                    strreturn = "Old password is incorrect";
                }
                else if (cnt == -2)
                {
                    strreturn = "Some problem while set reset password";
                }
                else if (cnt == 1)
                {
                    username11 = Objquery.get_username(userid);
                    encryptpasswoed = Encrypt.EncryptText(newpassword);
                    cnt1 = Objquery.set_reset_password(oldpassword, encryptpasswoed, userid);
                    strreturn = "Password set successfully";
                }
                else
                {
                    strreturn = "Please enter password";
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "set_reset_password Exception : " + ex.Message.ToString());
            }
            return Json(strreturn);
        }

        public JsonResult deleteuserdata(string userid)
        {
            int querystatus = 0;
            string result = "";
            try
            {
                querystatus = Objquery.deleteuserdata(userid);
                if (querystatus == 0)
                    result = "User deleted failed.";
                else
                    result = "User deleted successfully.";
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatsettingController", "deleteuserdata Exception : " + ex.Message.ToString());

            }
            return Json(result);
        }
        #endregion
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
