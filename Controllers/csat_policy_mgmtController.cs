using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OwnYITCSAT.DataAccessLayer;
using OwnYITCSAT.Models;

namespace OwnYITCSAT.Controllers
{
    public class csat_policy_mgmtController : Controller
    {
        private DatabaseHandler database = null;
        private int DBServer_Type = 0;
        OwnYITConstant.DatabaseTypes dbtype;
        string groupid = Configuration.groupid;
        DBQueryHandler Objquery = new DBQueryHandler();
        OwnYITCommon objcommon = new OwnYITCommon();
        DataTable dt = new DataTable();
        int querystatus = 0;
        int policyid = 0;
        int cnt = 0;
        string result = "";
        public IActionResult csat_policy_mgmt(int id)
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
                    if (OwnYITConstant.DT_POLICY_MENU == null)
                    {
                        OwnYITConstant.DT_POLICY_MENU = Objquery.Get_SubMenu(id);
                    }
                    //  Session["Policyid"] = id;
                    if (id.ToString() == null)
                        return RedirectToAction("csat_login", "csat_login");
                    // Session["Policysubmenu"] = OwnYITConstant.DT_POLICY_MENU;
                    if (OwnYITConstant.DT_POLICY_MENU == null)
                        return RedirectToAction("csat_login", "csat_login");
                    return View();
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatpolicymgmtController", "csat_policy_mgmt Exception : " + ex.Message.ToString());
                }
            return View();
        }
        public IActionResult csat_policy_create()
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
        //public JsonResult Get_Policy_data(int type)
        public JsonResult Get_Policy_data()
        {

            try
            {

                dt = Objquery.Get_Policy_Data("");
                //if (type == 4)
                //    dt = Objquery.Get_Policy_Data("type=4 and subtype=1");
                //else if (type == 42)
                //    dt = Objquery.Get_Policy_Data("type=4 and subtype=2");
                //else
                //    dt = Objquery.Get_Policy_Data("type=" + type + "");
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_data Exception : " + ex.Message.ToString());
            }
            var data = new { Policydata = dt };
            return Json(data);
        }
        public JsonResult Get_Policy_Save(string rulesid, string policyname, string description)
        {
            string result = "";
            try
            {
                if (policyname != "" && rulesid != "")
                    if (policyname != null && rulesid != null)
                    {
                        //cnt = Objquery.policy_master_count(policyname);
                        //if (cnt == 0)
                        //{
                        querystatus = Objquery.insert_policy_master(policyname, description);
                        policyid = Objquery.Get_Last_policy_Id();
                        querystatus = Objquery.insert_policy_linkage(policyid, rulesid.Substring(1));
                        if (querystatus == 0)
                            result = "Policy created failed.";
                        else
                            result = "Policy created successfully.";
                    }
                    else
                    {
                        result = "Please Enter Policy Details..";
                    }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_Save Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }

        //public JsonResult Get_Policy_Details_data(int type)
        public JsonResult Get_Policy_Details_data()
        {
            try
            {
                dt = Objquery.Get_Policy_Details_data();
                //if (type == 4)
                //    dt = Objquery.Get_Policy_Details_data("and r.type=4 and r.subtype=1");
                //else if (type == 42)
                //    dt = Objquery.Get_Policy_Details_data("and r.type=4 and  r.subtype=2");
                //else
                //    dt = Objquery.Get_Policy_Details_data("and r.type=" + type + "");
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_Details_data Exception : " + ex.Message.ToString());
            }
            var data = new { Policydetailsdata = dt };
            return Json(data);
        }
        public JsonResult GetPolicy(string name)
        {
            string type = "";
            if (name == "01")
            {
                dt = Objquery.Get_OUCount();
                type = "OU";
            }
            else if (name == "02")
            {
                dt = Objquery.Get_systemnameip();
                type = "System";
            }
            else
            {
                dt = Objquery.Get_Loginuser();
                type = "User";
            }
            var data = new { getapppolicydata = dt, gridtype = type };
            return Json(data);
        }
        public JsonResult GetPolicy_ipvalue(string name, string ip,string devicename)
        {
            string type = "";
            if (name == "01")
            {
                dt = Objquery.Get_OUCount();
                type = "OU";
            }
            else if (name == "02")
            {
                string strCond = "";
                if (devicename != null && devicename.Trim() != "")
                    strCond = " and device_name like '%"+devicename+"%'";
                dt = Objquery.Get_systemnameipsearch(ip, strCond);
                type = "System";
            }
            else
            {
                dt = Objquery.Get_Loginuser();
                type = "User";
            }
            var data = new { getapppolicydata = dt, gridtype = type };
            return Json(data);
        }
        public JsonResult Get_Policy(string policyid, string ostype)
        {
            string policyname = "";
            string osname = "";
            if (ostype == "0")
                osname = "Windows";
            else
                osname = "Linux";
            policyname = Objquery.Get_policyname(policyid);
            dt = Objquery.Get_systemnameip_apply(policyid, osname);
            var data = new { getapppolicydata = dt, policy_name = policyname };
            return Json(data);

            //string policyname = "";
            //string type = "";
            //policyname = Objquery.Get_policyname(policyid);
            //if (name == "01")
            //{
            //    dt = Objquery.Get_OUCount_apply();
            //    type = "OU";
            //}
            //else if (name == "02")
            //{
            //    dt = Objquery.Get_systemnameip_apply(policyid);
            //    type = "System";
            //}
            //else
            //{
            //    dt = Objquery.Get_Loginuser();
            //    type = "User";
            //}
            //var data = new { getapppolicydata = dt, gridtype = type, policy_name = policyname };
            //return Json(data);
        }
        public JsonResult Get_Policy_ip(string policyid, string ostype, string ip)
        {
            string policyname = "";
            string osname = "";
            if (ostype == "0")
                osname = "Windows";
            else
                osname = "Linux";
            policyname = Objquery.Get_policyname(policyid);
            if (ip == "null" || ip == "" || ip == null)
                dt = Objquery.Get_systemnameip_apply(policyid, osname);
            else
                dt = Objquery.Get_systemnameip_apply_ip(policyid, osname, ip);
            var data = new { getapppolicydata = dt, policy_name = policyname };
            return Json(data);
        }
        public JsonResult get_apply_policy_devices(string policy_id)
        {
            string policyname = "";
            DataTable dt = new DataTable();

            try
            {
                policyname = Objquery.Get_policyname(policy_id);
                dt = Objquery.Get_apply_policy_devices(policy_id);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "get_apply_policy_devices Exception : " + ex.Message.ToString());
            }
            var data = new { applypolicydevice = dt, policy_name = policyname };
            return Json(data);
        }

        public JsonResult Get_Policy_Apply(string policyid, string applyid, string applytype)
        {
            string result = "";
            int querystatus = 0;


            try
            {


                string jj = applyid.Substring(1);
                string[] values = jj.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    if (applytype == "01")
                    {
                        cnt = Objquery.policy_linkage_master_ou_count(policyid.Substring(1), values[i].ToString());
                        if (cnt == 0)
                        {
                            querystatus = Objquery.insert_policy_linkage_master(policyid.Substring(1), values[i].ToString(), "0", null);
                            querystatus = Objquery.insert_policy_deployment_master(values[i].ToString(), "0", null, policyid.Substring(1));

                            dt = Objquery.Get_device_policy_info(" where ou_id in(" + values[i].ToString() + ")");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                querystatus = Objquery.insert_ou_deployment_linkage_master(dt.Rows[j]["ou_id"].ToString(), dt.Rows[j]["device_id"].ToString(), dt.Rows[j]["login_user"].ToString(), policyid.Substring(1));
                                querystatus = Objquery.insert_Policy_Querylog(dt.Rows[j]["device_id"].ToString(), policyid.Substring(1), Objquery.Get_Location_ID(dt.Rows[j]["device_id"].ToString()));
                            }

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
                    if (applytype == "02")
                    {
                        cnt = Objquery.policy_linkage_master_deviceid_count(policyid.Substring(1), values[i].ToString());
                        if (cnt == 0)
                        {
                            querystatus = Objquery.insert_policy_linkage_master(policyid.Substring(1), "0", values[i].ToString(), null);
                            querystatus = Objquery.insert_policy_deployment_master("0", values[i].ToString(), null, policyid.Substring(1));
                            dt = Objquery.Get_device_policy_info(" where dl.device_id in(" + values[i].ToString() + ")");
                            querystatus = Objquery.insert_ou_deployment_linkage_master(dt.Rows[0]["ou_id"].ToString(), dt.Rows[0]["device_id"].ToString(), dt.Rows[0]["login_user"].ToString(), policyid.Substring(1));
                            querystatus = Objquery.insert_Policy_Querylog(dt.Rows[0]["device_id"].ToString(), policyid.Substring(1), Objquery.Get_Location_ID(dt.Rows[0]["device_id"].ToString()));

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
                    if (applytype == "03")
                    {
                        cnt = Objquery.policy_linkage_master_username_count(policyid.Substring(1), values[i].ToString());
                        if (cnt == 0)
                        {
                            querystatus = Objquery.insert_policy_linkage_master(policyid.Substring(1), "0", "0", values[i].ToString());
                            querystatus = Objquery.insert_policy_deployment_master("0", "0", values[i].ToString(), policyid.Substring(1));
                            dt = Objquery.Get_device_policy_info(" where nd.login_user in('" + values[i].ToString() + "')");
                            querystatus = Objquery.insert_ou_deployment_linkage_master(dt.Rows[0]["ou_id"].ToString(), dt.Rows[0]["device_id"].ToString(), dt.Rows[0]["login_user"].ToString(), policyid.Substring(1));
                            querystatus = Objquery.insert_Policy_Querylog(dt.Rows[0]["device_id"].ToString(), policyid.Substring(1), Objquery.Get_Location_ID(dt.Rows[0]["device_id"].ToString()));
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
                }



            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_Apply Exception : " + ex.Message.ToString());
            }

            return Json(result);
        }
        public JsonResult Get_Policy_Apply_link_popup(string policyid, string applyid)
        {
            string result = "";
            int querystatus = 0;

            try
            {
                string jj = applyid.Substring(1);
                string[] values = jj.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    cnt = Objquery.policy_linkage_master_deviceid_count(policyid, values[i].ToString());
                    if (cnt == 0)
                    {
                        querystatus = Objquery.insert_policy_linkage_master(policyid, "0", values[i].ToString(), null);
                        querystatus = Objquery.insert_policy_deployment_master("0", values[i].ToString(), null, policyid);
                        dt = Objquery.Get_device_policy_info(" where dl.device_id in(" + values[i].ToString() + ")");
                        querystatus = Objquery.insert_ou_deployment_linkage_master(dt.Rows[0]["ou_id"].ToString(), dt.Rows[0]["device_id"].ToString(), dt.Rows[0]["login_user"].ToString(), policyid);
                        querystatus = Objquery.insert_Policy_Querylog(dt.Rows[0]["device_id"].ToString(), policyid, Objquery.Get_Location_ID(dt.Rows[0]["device_id"].ToString()));

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
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_Apply_link_popup Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }

        public JsonResult Get_Policy_Apply_link(string policyid, string applyid, string applytype)
        {
            string result = "";
            int querystatus = 0;


            try
            {


                string jj = applyid.Substring(1);
                string[] values = jj.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    if (applytype == "01")
                    {
                        cnt = Objquery.policy_linkage_master_ou_count(policyid, values[i].ToString());
                        if (cnt == 0)
                        {
                            querystatus = Objquery.insert_policy_linkage_master(policyid, values[i].ToString(), "0", null);
                            querystatus = Objquery.insert_policy_deployment_master(values[i].ToString(), "0", null, policyid);

                            dt = Objquery.Get_device_policy_info(" where ou_id in(" + values[i].ToString() + ")");
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                querystatus = Objquery.insert_ou_deployment_linkage_master(dt.Rows[j]["ou_id"].ToString(), dt.Rows[j]["device_id"].ToString(), dt.Rows[j]["login_user"].ToString(), policyid);
                                querystatus = Objquery.insert_Policy_Querylog(dt.Rows[0]["device_id"].ToString(), policyid, Objquery.Get_Location_ID(dt.Rows[0]["device_id"].ToString()));
                            }

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
                    if (applytype == "02")
                    {
                        cnt = Objquery.policy_linkage_master_deviceid_count(policyid, values[i].ToString());
                        if (cnt == 0)
                        {
                            querystatus = Objquery.insert_policy_linkage_master(policyid, "0", values[i].ToString(), null);
                            querystatus = Objquery.insert_policy_deployment_master("0", values[i].ToString(), null, policyid);
                            dt = Objquery.Get_device_policy_info(" where dl.device_id in(" + values[i].ToString() + ")");
                            querystatus = Objquery.insert_ou_deployment_linkage_master(dt.Rows[0]["ou_id"].ToString(), dt.Rows[0]["device_id"].ToString(), dt.Rows[0]["login_user"].ToString(), policyid);
                            querystatus = Objquery.insert_Policy_Querylog(dt.Rows[0]["device_id"].ToString(), policyid, Objquery.Get_Location_ID(dt.Rows[0]["device_id"].ToString()));

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
                    if (applytype == "03")
                    {
                        cnt = Objquery.policy_linkage_master_username_count(policyid, values[i].ToString());
                        if (cnt == 0)
                        {
                            querystatus = Objquery.insert_policy_linkage_master(policyid, "0", "0", values[i].ToString());
                            querystatus = Objquery.insert_policy_deployment_master("0", "0", values[i].ToString(), policyid);
                            dt = Objquery.Get_device_policy_info(" where nd.login_user in('" + values[i].ToString() + "')");
                            querystatus = Objquery.insert_ou_deployment_linkage_master(dt.Rows[0]["ou_id"].ToString(), dt.Rows[0]["device_id"].ToString(), dt.Rows[0]["login_user"].ToString(), policyid);
                            querystatus = Objquery.insert_Policy_Querylog(dt.Rows[0]["device_id"].ToString(), policyid, Objquery.Get_Location_ID(dt.Rows[0]["device_id"].ToString()));
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
                }



            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_Apply_link Exception : " + ex.Message.ToString());
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult Create_Rules(string rulestype, string rulesname, string hwtype, string status, string desc, string crc, string thersold, string thersoldper, string destport, string eventtype, string eventid, string backuptype, string backupdetails, string backuptime, string firewall_rules, string fw_action, string fw_sourceip, string fw_sourceport, string fw_destinationip, string fw_destinationport, string fw_direction, string fw_perform_action, string fw_protocol_rules, string ostype)
        {
            int Rcount = 0;
            string result = "";
            string strquerymsg = "";
            string strremovemsg = "";

            try
            {
                if (rulestype == "1")
                {
                    Rcount = Objquery.Get_Rules_Count(rulestype, hwtype, status);
                    if (Rcount == 0)
                    {
                        if (hwtype != "" || status != "")
                        {
                            Rcount = Objquery.Get_Rules_last_Count();
                            strquerymsg = "#6061@2!" + Rcount + "!" + rulestype + "!" + hwtype + "!" + status + "@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!" + rulestype + "!" + hwtype + "!" + status + "@6061#";
                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Hardware Policy Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, rulestype, hwtype, "1", "1", status, strquerymsg, strremovemsg, desc);

                            if (querystatus == 1)
                                result = "Rules created successfully.";
                            else
                                result = "Rules created failed.";
                        }
                        else
                            result = "Please enter details";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "2")
                {
                    Rcount = Objquery.Get_Rules_Count(rulestype, hwtype, status);
                    if (Rcount == 0)
                    {
                        if (hwtype != "" || status != "")
                        {
                            Rcount = Objquery.Get_Rules_last_Count();
                            if (hwtype == "1")
                            {
                                strquerymsg = "#6061@2!" + Rcount + "!2!1!SYSCPU%!" + status + "%!POPUP!1@6061#";
                                strremovemsg = "#6061@0!" + Rcount + "!2!1!SYSCPU%!" + status + "%!POPUP!1@6061#";
                                querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, rulestype, hwtype, "1", "1", status, strquerymsg, strremovemsg, desc);
                            }
                            else if (hwtype == "2")
                            {
                                strquerymsg = "#6061@2!" + Rcount + "!2!2!SYSRAM%!" + status + "%!POPUP!1@6061#";
                                strremovemsg = "#6061@0!" + Rcount + "!2!2!SYSRAM%!" + status + "%!POPUP!1@6061#";
                                querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, rulestype, hwtype, "1", "1", status, strquerymsg, strremovemsg, desc);
                            }
                            else if (hwtype == "3")
                            {
                                strquerymsg = "#6061@2!" + Rcount + "!2!3!SYSDISK%!" + status + "%!POPUP!1@6061#";
                                strremovemsg = "#6061@0!" + Rcount + "!2!3!SYSDISK%!" + status + "%!POPUP!1@6061#";
                                querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, rulestype, hwtype, "1", "1", status, strquerymsg, strremovemsg, desc);
                            }
                            else if (hwtype == "4") // Application Monitoring
                            {
                                if (thersoldper == null)
                                    thersoldper = "NA";
                                if (thersold == null)
                                    thersold = "NA";
                                strquerymsg = "#6061@2!" + Rcount + "!2!5!CPUThreshold!" + thersoldper + "!MEMThreshold!" + thersold + "@6061#";
                                strremovemsg = "#6061@0!" + Rcount + "!2!5!CPUThreshold!" + thersoldper + "!MEMThreshold!" + thersold + "@6061#";
                                //if (crc == null)
                                //    crc = "NA";
                                //if (thersold == "0")
                                //{
                                //    //strquerymsg = "#6061@2!" + Rcount + "!2!5!AppName!<" + status + ">!AppCRC!<" + crc.Substring(1) + ">!CPUThreshold!<NA>!MEMThreshold!<" + thersoldper + ">@6061#";
                                //    strquerymsg = "#6061@2!" + Rcount + "!2!5!CPUThreshold!NA!MEMThreshold!" + thersoldper + "@6061#";
                                //    strremovemsg = "#6061@0!" + Rcount + "!2!5!CPUThreshold!NA!MEMThreshold!" + thersoldper + "@6061#";
                                //}
                                //else if (thersold == "1")
                                //{
                                //    strquerymsg = "#6061@2!" + Rcount + "!2!5!CPUThreshold!" + thersoldper + "!MEMThreshold!NA@6061#";
                                //    strremovemsg = "#6061@0!" + Rcount + "!2!5!CPUThreshold!" + thersoldper + "!MEMThreshold!NA@6061#";
                                //}
                                querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, hwtype, "1", "1", "", "", thersold, thersoldper, "", "", strquerymsg, strremovemsg, desc);
                            }
                            else if (hwtype == "5") // Application Kill
                            {
                                if (crc == null)
                                    crc = "NA";
                                //if (thersold == "0")
                                //{
                                strquerymsg = "#6061@2!" + Rcount + "!2!4!AppName!" + status + "!AppCRC!" + crc.ToString() + "!@6061#";
                                strremovemsg = "#6061@0!" + Rcount + "!2!4!AppName!" + status + "!AppCRC!" + crc.ToString() + "!@6061#";
                                //}
                                //else if (thersold == "1")
                                //{
                                //    strquerymsg = "#6061@2!" + Rcount + "!2!4!AppName!<" + status + ">!AppCRC!<" + crc.Substring(1) + ">!CPUThreshold!<" + thersoldper + ">!MEMThreshold!<NA>@6061#";
                                //    strremovemsg = "#6061@0!" + Rcount + "!2!4!AppName!<" + status + ">!AppCRC!<" + crc.Substring(1) + ">!CPUThreshold!<" + thersoldper + ">!MEMThreshold!<NA>@6061#";
                                //}
                                querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, hwtype, "1", "1", status, crc.ToString(), "", "", "", "", strquerymsg, strremovemsg, desc);
                            }

                            if (querystatus == 1)
                                result = "Rules created successfully.";
                            else
                                result = "Rules created failed.";

                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Rules Policy Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        }
                        else
                            result = "Please enter details";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "3")
                {
                    //Rcount = Objquery.Get_Rules_Count(rulestype, "0", hwtype);
                    Rcount = Objquery.Get_Rules_Count(rulestype, "1", hwtype);
                    if (Rcount == 0)
                    {
                        if (hwtype != "" || status != "")
                        {
                            Rcount = Objquery.Get_Rules_last_Count();
                            strquerymsg = "#6061@2!" + Rcount + "!3!1!RemoveShareDays!" + hwtype + "!@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!3!1!RemoveShareDays!" + hwtype + "!@6061#";
                            //strquerymsg = "#6061@2!" + Rcount + "!3!0!ShareName!" + rulesname + "!DaysDisable!" + status + "!DaysDelete!" + hwtype + "@6061#";
                            //strremovemsg = "#6061@0!" + Rcount + "!3!0!ShareName!" + rulesname + "!DaysDisable!" + status + "!DaysDelete!" + hwtype + "@6061#";
                            //if (hwtype == "1")
                            //{
                            //    strquerymsg = "#6061@2!" + Rcount + "!3!0!DaysDisable!<" + status + ">!DaysDelete!<NA>@6061#";
                            //    strremovemsg = "#6061@0!" + Rcount + "!3!0!DaysDisable!<" + status + ">!DaysDelete!<NA>@6061#";
                            //}
                            //else if (hwtype == "2")
                            //{
                            //    strquerymsg = "#6061@2!" + Rcount + "!3!0!DaysDisable!<NA>!DaysDelete!<" + status + ">@6061#";
                            //    strremovemsg = "#6061@0!" + Rcount + "!3!0!DaysDisable!<NA>!DaysDelete!<" + status + ">@6061#";
                            //}

                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Share Policy Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, "1", "1", "1", hwtype, status, null, null, null, null, strquerymsg, strremovemsg, desc);
                            if (querystatus == 1)
                                result = "Rules created successfully.";
                            else
                                result = "Rules created failed.";
                        }
                        else
                            result = "Please enter details";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "4") // User Password Change Policy
                {
                    Rcount = Objquery.Get_Rules_Count(rulestype, hwtype, status);
                    if (Rcount == 0)
                    {
                        if (hwtype != "" && status != "NA")
                        {
                            Rcount = Objquery.Get_Rules_last_Count();
                            if (status != "NA") //user send alert
                            {
                                strquerymsg = "#6061@2!" + Rcount + "!4!" + hwtype + "!" + status + "!@6061#";
                                strremovemsg = "#6061@0!" + Rcount + "!4!" + hwtype + "!" + status + "!@6061#";

                                //    strquerymsg = "#6061@2!" + Rcount + "!11!3!" + thersold + "!" + status + "!@6061#";
                                //    strremovemsg = "#6061@0!" + Rcount + "!11!3!" + thersold + "!" + status + "!@6061#";
                                //}
                                //else if (hwtype != "NA") // user delete
                                //{
                                //    strquerymsg = "#6061@2!" + Rcount + "!11!2!" + thersold + "!" + hwtype + "!@6061#";
                                //    strremovemsg = "#6061@0!" + Rcount + "!11!2!" + thersold + "!" + hwtype + "!@6061#";
                                //}
                                //else if(crc != "NA") //user disable
                                //{

                            }
                            //if (hwtype == "0")
                            //{
                            //    strquerymsg = "#6061@2!" + Rcount + "!4!1!UserName!<" + crc + ">!DaysDisable!<NA>!DaysDelete!<NA>!DaysAlert!<" + status + ">@6061#";
                            //    strremovemsg = "#6061@0!" + Rcount + "!4!1!UserName!<" + crc + ">!DaysDisable!<NA>!DaysDelete!<NA>!DaysAlert!<" + status + ">@6061#";
                            //}
                            //else if (hwtype == "1")
                            //{
                            //    strquerymsg = "#6061@2!" + Rcount + "!4!1!UserName!<" + crc + ">!DaysDisable!<" + status + ">!DaysDelete!<NA>!DaysAlert!<NA>@6061#";
                            //    strremovemsg = "#6061@0!" + Rcount + "!4!1!UserName!<" + crc + ">!DaysDisable!<" + status + ">!DaysDelete!<NA>!DaysAlert!<NA>@6061#";
                            //}
                            //else if (hwtype == "2")
                            //{
                            //    strquerymsg = "#6061@2!" + Rcount + "!4!1!UserName!<" + crc + ">!DaysDisable!<NA>!DaysDelete!<" + status + ">!DaysAlert!<NA>@6061#";
                            //    strremovemsg = "#6061@0!" + Rcount + "!4!1!UserName!<" + crc + ">!DaysDisable!<NA>!DaysDelete!<" + status + ">!DaysAlert!<NA>@6061#";
                            //}

                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : User Policy Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, hwtype, "1", "1", status, null, crc, null, null, null, strquerymsg, strremovemsg, desc);
                            if (querystatus == 1)
                                result = "Rules created successfully.";
                            else
                                result = "Rules created failed.";
                        }
                        else
                            result = "Please enter details";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "6" || rulestype == "7")
                {
                    Rcount = Objquery.Get_Rules_Count(rulestype, "1", status);
                    if (Rcount == 0)
                    {
                        if (hwtype != "" || status != "")
                        {
                            string strpolicyname = "";
                            if (rulestype == "6")
                                strpolicyname = "Remote Desktop Policy";
                            else
                                strpolicyname = "IP Change Restriction Policy";


                            Rcount = Objquery.Get_Rules_last_Count();
                            strquerymsg = "#6061@2!" + Rcount + "!" + rulestype + "!1!" + status + "@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!" + rulestype + "!1!" + status + "@6061#";
                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : " + strpolicyname + " Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, rulestype, "1", "1", "1", status, strquerymsg, strremovemsg, desc);
                            if (querystatus == 1)
                                result = "Rules created successfully.";
                            else
                                result = "Rules created failed.";
                        }
                        else
                            result = "Please enter details";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "42")
                {
                    Rcount = Objquery.Get_Rules_Count("4", "4", status);
                    if (Rcount == 0)
                    {

                        Rcount = Objquery.Get_Rules_last_Count();
                        strquerymsg = "#6061@2!" + Rcount + "!4!2!" + status + "@6061#";
                        strremovemsg = "#6061@0!" + Rcount + "!4!2!" + status + "@6061#";
                        objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Password Complexity policy  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, "4", "2", "1", "1", status, strquerymsg, strremovemsg, desc);
                        if (querystatus == 1)
                            result = "Rules created successfully.";
                        else
                            result = "Rules created failed.";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "10") // Power Saver Policy
                {
                    int Rcount1 = 0;
                    Rcount1 = Objquery.Get_Rules_Count(rulestype, hwtype, status);
                    if (Rcount1 == 0)
                    {
                        result = "Rules created successfully.";
                        Rcount = Objquery.Get_Rules_last_Count();

                        if (hwtype == "1")
                        {
                            //strquerymsg = "#6061@2!" + Rcount + "!4!1!TurnOfMonitor!<" + status + ">!SystemStandBy!<NA>!SystemHibernate!<NA>@6061#";
                            //strremovemsg = "#6061@0!" + Rcount + "!4!1!TurnOfMonitor!<" + status + ">!SystemStandBy!<NA>!SystemHibernate!<NA>@6061#";
                            strquerymsg = "#6061@2!" + Rcount + "!10!" + hwtype + "!TurnOfMonitor!" + status + "!@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!10!" + hwtype + "!TurnOfMonitor!" + status + "!@6061#";
                        }
                        else if (hwtype == "2")
                        {
                            strquerymsg = "#6061@2!" + Rcount + "!10!" + hwtype + "!SystemStandBy!" + status + "!@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!10!" + hwtype + "!SystemStandBy!" + status + "!@6061#";
                        }
                        else if (hwtype == "3")
                        {
                            strquerymsg = "#6061@2!" + Rcount + "!10!" + hwtype + "!SystemHibernate!" + status + "!@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!10!" + hwtype + "!SystemHibernate!" + status + "!@6061#";
                        }
                        objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Power Saver policy  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        //querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, "1", "1", "1", hwtype, status, null, null, null, null, strquerymsg, strremovemsg, desc);
                        querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, hwtype, "1", "1", status, null, null, null, null, null, strquerymsg, strremovemsg, desc);
                        if (querystatus == 1)
                            result = "Rules created successfully.";
                        else
                            result = "Rules created failed.";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "8") // Shut Down Policy
                {
                    Rcount = Objquery.Get_Rules_Count(rulestype, hwtype, status);
                    if (Rcount == 0)
                    {
                        result = "Rules created successfully.";
                        Rcount = Objquery.Get_Rules_last_Count();
                        strquerymsg = "#6061@2!" + Rcount + "!8!1!ShutdownTime!" + status + "!TypeShutDown!" + hwtype + "!@6061#";
                        strremovemsg = "#6061@0!" + Rcount + "!8!1!ShutdownTime!" + status + "!TypeShutDown!" + hwtype + "!@6061#";
                        //if (hwtype == "0")
                        //{
                        //    strquerymsg = "#6061@2!" + Rcount + "!8!0!ShutdownTime!<" + status + ">!gracefully!0!@6061#";
                        //    strremovemsg = "#6061@0!" + Rcount + "!8!0!ShutdownTime!<" + status + ">!gracefully!0!@6061#";
                        //}
                        //else if (hwtype == "1")
                        //{
                        //    strquerymsg = "#6061@2!" + Rcount + "!8!0!ShutdownTime!<" + status + ">!forcefully!1!@6061#";
                        //    strremovemsg = "#6061@0!" + Rcount + "!8!0!ShutdownTime!<" + status + ">!forcefully!1!@6061#";
                        //}
                        objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Shutdown policy  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, "1", "1", "1", status, hwtype, null, null, null, null, strquerymsg, strremovemsg, desc);
                        //querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, "0", "1", "1", hwtype, status, null, null, null, null, strquerymsg, strremovemsg, desc);
                        if (querystatus == 1)
                            result = "Rules created successfully.";
                        else
                            result = "Rules created failed.";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "9") // IP + Port Blocking
                {
                    Rcount = Objquery.Get_Rules_Count(rulestype, "1", hwtype);
                    if (Rcount == 0)
                    {
                        result = "Rules created successfully.";
                        Rcount = Objquery.Get_Rules_last_Count();

                        if (hwtype == "1")
                        {
                            strquerymsg = "#6061@2!" + Rcount + "!9!" + hwtype + "!Protocol!TCP!SIP!" + crc + "!Sport!" + thersold + "!DIP!" + thersoldper + "!DPort!" + destport + "!" + status + "@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!9!" + hwtype + "!Protocol!TCP!SIP!" + crc + "!Sport!" + thersold + "!DIP!" + thersoldper + "!DPort!" + destport + "!" + status + "@6061#";
                        }
                        else if (hwtype == "2")
                        {
                            strquerymsg = "#6061@2!" + Rcount + "!9!" + hwtype + "!Protocol!UDP!SIP!" + crc + "!Sport!" + thersold + "!DIP!" + thersoldper + "!DPort!" + destport + "!" + status + "@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!9!" + hwtype + "!Protocol!UDP!SIP!" + crc + "!Sport!" + thersold + "!DIP!" + thersoldper + "!DPort!" + destport + "!" + status + "@6061#";
                        }
                        else if (hwtype == "3")
                        {
                            strquerymsg = "#6061@2!" + Rcount + "!9!" + hwtype + "!Protocol!ICMP!SIP!" + crc + "!Sport!" + thersold + "!DIP!" + thersoldper + "!DPort!" + destport + "!" + status + "@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!9!" + hwtype + "!Protocol!ICMP!SIP!" + crc + "!Sport!" + thersold + "!DIP!" + thersoldper + "!DPort!" + destport + "!" + status + "@6061#";
                        }
                        objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : IP + Port Blocking  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, "1", "1", "1", hwtype, status, crc, thersold, thersoldper, destport, strquerymsg, strremovemsg, desc);
                        if (querystatus == 1)
                            result = "Rules created successfully.";
                        else
                            result = "Rules created failed.";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "11") // Login Restriction Policy
                {
                    if (ostype == "0")
                    {
                        Rcount = Objquery.Get_Rules_Count(rulestype, ostype, hwtype);
                        if (Rcount == 0)
                        {
                            //result = "Rules created successfully.";
                            Rcount = Objquery.Get_Rules_last_Count();
                            strquerymsg = "#10@1040!0!0!net user " + crc + " /time:" + hwtype + status + "-" + destport + "!" + crc + "!@10#";
                            strremovemsg = "#10@1040!0!0!net user " + crc + " /time:ALL!@10#";

                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules :  Login Restriction policy  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, ostype, "1", "1", hwtype, status, destport, null, null, null, strquerymsg, strremovemsg, desc);
                            if (querystatus == 1)
                                result = "Rules created successfully.";
                            else
                                result = "Rules created failed.";
                        }
                        else
                        {
                            result = "Rules already exists.";
                        }
                    }
                    else
                    {
                        string[] str_action = hwtype.Split(',');
                        hwtype = "";
                        for (int i = 0; i < str_action.Length; i++)
                        {
                            if (str_action[i].ToString() == "Su")
                                hwtype += "Su";
                            if (str_action[i].ToString() == "M")
                                hwtype += "Mo";
                            if (str_action[i].ToString() == "T")
                                hwtype += "Tu";
                            if (str_action[i].ToString() == "W")
                                hwtype += "We";
                            if (str_action[i].ToString() == "Th")
                                hwtype += "Th";
                            if (str_action[i].ToString() == "F")
                                hwtype += "Fr";
                            if (str_action[i].ToString() == "Sa")
                                hwtype += "Sa";
                        }
                        //hwtype = hwtype.Substring(1);
                        //hwtype += ",";

                        Rcount = Objquery.Get_Rules_Count(rulestype, ostype, hwtype);
                        if (Rcount == 0)
                        {
                            //result = "Rules created successfully.";
                            Rcount = Objquery.Get_Rules_last_Count();

                            strquerymsg = "#10@1040!0!0!*;*;" + crc + ";" + hwtype + status + "00-" + destport + "00!" + crc + "!@10#";
                            strremovemsg = "#10@1040!0!0!NA;*;*;" + crc + ";" + hwtype + status + "00-" + destport + "00!" + crc + "!@10#";

                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules :  Login Restriction policy  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, ostype, "1", "1", hwtype, status, destport, null, null, null, strquerymsg, strremovemsg, desc);
                            if (querystatus == 1)
                                result = "Rules created successfully.";
                            else
                                result = "Rules created failed.";
                        }
                        else
                        {
                            result = "Rules already exists.";
                        }
                    }


                    //this.database = new DatabaseHandler(OwnYITConstant.db_settings);
                    //this.DBServer_Type = this.database.DB_SERVER_TYPE;
                    //switch (this.database.DB_SERVER_TYPE)
                    //{
                    //    case 0:
                    //        dbtype = OwnYITConstant.DatabaseTypes.MSSQL_SERVER;
                    //        break;
                    //    case 1:
                    //        dbtype = OwnYITConstant.DatabaseTypes.MYSQL_SERVER;
                    //        string[] str_action = hwtype.Split(',');
                    //        hwtype = "";
                    //        for (int i = 0; i < str_action.Length; i++)
                    //        {
                    //            if (str_action[i].ToString() == "Su")
                    //                hwtype += ",Su";
                    //            if (str_action[i].ToString() == "M")
                    //                hwtype += ",Mo";
                    //            if (str_action[i].ToString() == "T")
                    //                hwtype += ",Tu";
                    //            if (str_action[i].ToString() == "W")
                    //                hwtype += ",We";
                    //            if (str_action[i].ToString() == "Th")
                    //                hwtype += ",Th";
                    //            if (str_action[i].ToString() == "F")
                    //                hwtype += ",Fr";
                    //            if (str_action[i].ToString() == "Sa")
                    //                hwtype += ",Sa";
                    //        }
                    //        hwtype = hwtype.Substring(1);
                    //        hwtype += ",";
                    //        break;
                    //}

                    //Rcount = Objquery.Get_Rules_Count(rulestype, "0", hwtype);
                    //if (Rcount == 0)
                    //{
                    //    result = "Rules created successfully.";
                    //    Rcount = Objquery.Get_Rules_last_Count();
                    //    string osname = Objquery.get_osname_policy();
                    //    strquerymsg = "#10@1040!0!0!net user " + crc + " /time:" + hwtype + status + "-" + destport + "!" + crc + "!@10#";
                    //    strremovemsg = "#10@1040!0!0!net user " + crc + " /time:ALL!@10#";

                    //    objcommon.WriteLog("csatpolicymgmtController", "Create_Rules :  Login Restriction policy  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                    //    querystatus = Objquery.insert_rules_master_multipleparam(Rcount.ToString(), rulesname, rulestype, "0", "1", "1", hwtype, status, destport, null, null, null, strquerymsg, strremovemsg, desc);
                    //    if (querystatus == 1)
                    //        result = "Rules created successfully.";
                    //    else
                    //        result = "Rules created failed.";
                    //}
                    //else
                    //{
                    //    result = "Rules already exists.";
                    //}
                }
                else if (rulestype == "12")
                {
                    Rcount = Objquery.Get_Rules_Count(rulestype, eventtype, eventid);
                    if (Rcount == 0)
                    {
                        if (eventtype != "" || eventid != "")
                        {
                            Rcount = Objquery.Get_Rules_last_Count();
                            strquerymsg = "#6061@2!" + Rcount + "!" + rulestype + "!" + eventtype + "!EventID!" + eventid + "!" + "@6061#";
                            strremovemsg = "#6061@0!" + Rcount + "!" + rulestype + "!" + eventtype + "!EventID!" + eventid + "!" + "@6061#";
                            objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Event Monitoring Policy Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                            querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, rulestype, eventtype, "1", eventid, "1", strquerymsg, strremovemsg, desc);
                            if (querystatus == 1)
                                result = "Rules created successfully.";
                            else
                                result = "Rules created failed.";
                        }
                        else
                            result = "Please enter details";
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
                else if (rulestype == "13")
                {
                    if (backuptype == "2" && backupdetails == "-1")
                    {
                        result = "Please select days";
                    }
                    else if (backuptype == "3" && backupdetails == "-1")
                    {
                        result = "Please select date";
                    }
                    else
                    {
                        Rcount = Objquery.Get_Rules_Count(rulestype, backuptype, backupdetails);
                        if (Rcount == 0)
                        {
                            if (backuptype != "" || backupdetails != "")
                            {
                                Rcount = Objquery.Get_Rules_last_Count();
                                strquerymsg = "#6061@2!" + Rcount + "!" + rulestype + "!" + backuptype + "!" + backupdetails + "!" + backuptime + "!@6061#";
                                strremovemsg = "#6061@0!" + Rcount + "!" + rulestype + "!" + backuptype + "!" + backupdetails + "!" + backuptime + "!@6061#";
                                objcommon.WriteLog("csatpolicymgmtController", "Create_Rules : Disk Cleanup Policy Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                                querystatus = Objquery.insert_rules_master(Rcount.ToString(), rulesname, rulestype, backuptype, "1", "1", "1", strquerymsg, strremovemsg, desc);
                                if (querystatus == 1)
                                    result = "Rules created successfully.";
                                else
                                    result = "Rules created failed.";
                            }
                            else
                                result = "Please enter details";
                        }
                        else
                        {
                            result = "Rules already exists.";
                        }
                    }
                }
                else if (rulestype == "15")
                {
                    Rcount = Objquery.Get_Rules_Count_fw(rulestype, firewall_rules, fw_sourceip, fw_sourceport, fw_destinationip, fw_destinationport, fw_direction, fw_perform_action, fw_protocol_rules);
                    if (Rcount == 0)
                    {
                        Rcount = Objquery.Get_Rules_last_Count();
                        if (fw_sourceip == "" || fw_sourceip == null)
                            fw_sourceip = "NA";
                        if (fw_sourceport == "" || fw_sourceport == null)
                            fw_sourceport = "NA";
                        if (fw_destinationip == "" || fw_destinationip == null)
                            fw_destinationip = "NA";
                        if (fw_destinationport == "" || fw_destinationport == null)
                            fw_destinationport = "NA";
                        strquerymsg = "#6061@2!" + Rcount + "!" + rulestype + "!" + firewall_rules + "!" + fw_sourceip + "!" + fw_sourceport + "!" + fw_destinationip + "!" + fw_destinationport + "!" + fw_direction + "!" + fw_perform_action + "!" + fw_protocol_rules + "!" + "@6061#";
                        strremovemsg = "#6061@0!" + Rcount + "!" + rulestype + "!" + firewall_rules + "!" + fw_sourceip + "!" + fw_sourceport + "!" + fw_destinationip + "!" + fw_destinationport + "!" + fw_direction + "!" + fw_perform_action + "!" + fw_protocol_rules + "!" + "@6061#";
                        objcommon.WriteLog("csatpolicymgmtController", "Create_Rules :Firewall  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        querystatus = Objquery.insert_rules_master_fw_multipleparam(Rcount.ToString(), rulesname, rulestype, firewall_rules, "1", "1", fw_sourceip, fw_sourceport, fw_destinationip, fw_destinationport, fw_direction, fw_perform_action, fw_protocol_rules, strquerymsg, strremovemsg, desc);
                        if (querystatus == 1)
                            result = "Rules created successfully.";
                        else
                            result = "Rules created failed.";

                        #region destinationip is mendatory (20211229)
                        //if (fw_destinationip != "")
                        //{
                        //    Rcount = Objquery.Get_Rules_last_Count();
                        //    if (fw_sourceip == "" || fw_sourceip == null)
                        //        fw_sourceip = "NA";
                        //    if (fw_sourceport == "" || fw_sourceport == null)
                        //        fw_sourceport = "NA";
                        //    if (fw_destinationport == "" || fw_destinationport == null)
                        //        fw_destinationport = "NA";
                        //    strquerymsg = "#6061@2!" + Rcount + "!" + rulestype + "!" + firewall_rules + "!" + fw_sourceip + "!" + fw_sourceport + "!" + fw_destinationip + "!" + fw_destinationport + "!" + fw_direction + "!" + fw_perform_action + "!" + fw_protocol_rules + "!" + "@6061#";
                        //    strremovemsg = "#6061@0!" + Rcount + "!" + rulestype + "!" + firewall_rules + "!" + fw_sourceip + "!" + fw_sourceport + "!" + fw_destinationip + "!" + fw_destinationport + "!" + fw_direction + "!" + fw_perform_action + "!" + fw_protocol_rules + "!" + "@6061#";
                        //    objcommon.WriteLog("csatpolicymgmtController", "Create_Rules :Firewall  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        //    querystatus = Objquery.insert_rules_master_fw_multipleparam(Rcount.ToString(), rulesname, rulestype, firewall_rules, "1", "1", fw_sourceip, fw_sourceport, fw_destinationip, fw_destinationport, fw_direction, fw_perform_action, fw_protocol_rules, strquerymsg, strremovemsg, desc);
                        //    if (querystatus == 1)
                        //        result = "Rules created successfully.";
                        //    else
                        //        result = "Rules created failed.";
                        //}
                        #endregion

                        //if (firewall_rules == "5")
                        //{
                        //    Rcount = Objquery.Get_Rules_last_Count();
                        //    strquerymsg = "#6061@2!" + Rcount + "!" + rulestype + "!" + firewall_rules + "!NA" + "!NA" + "!NA" + "!NA" + "!NA" + "!" + fw_perform_action + "!NA" + "!" + "@6061#";
                        //    strremovemsg = "#6061@0!" + Rcount + "!" + rulestype + "!" + firewall_rules + "!NA" + "!NA" + "!NA" + "!NA" + "!NA" + "!" + fw_perform_action + "!NA" + "!" + "@6061#";
                        //    objcommon.WriteLog("csatpolicymgmtController", "Create_Rules :DCM  Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                        //    querystatus = Objquery.insert_rules_master_fw_multipleparam(Rcount.ToString(), rulesname, rulestype, firewall_rules, "1", "1", fw_sourceip, fw_sourceport, fw_destinationip, fw_destinationport, fw_direction, fw_perform_action, fw_protocol_rules, strquerymsg, strremovemsg, desc);
                        //    if (querystatus == 1)
                        //        result = "Rules created successfully.";
                        //    else
                        //        result = "Rules created failed.";
                        //}
                    }
                    else
                    {
                        result = "Rules already exists.";
                    }
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Create_Rules Exception : " + ex.Message.ToString());
            }
            //if (querystatus == 0)get_apply_policy_devices
            //    result = "Rules created faild.";
            //else
            //    result = "Rules created successfully.";
            return Json(result);
        }
        public JsonResult Create_NACPolicy(string rulestype, string firewall_rules, string nac_str, string policyname, string policydesc)
        {
            int Rcount = 0;
            string result = "";
            string strquerymsg = "";
            string strremovemsg = "";
            string rulesname = "";
            string desc = "";
            string fw_sourceip = "";
            string fw_sourceport = "";
            string fw_destinationip = "";
            string fw_destinationport = "";
            string fw_direction = "";
            string fw_perform_action = "";
            string fw_protocol_rules = "";
            DataTable dtnac_str = new DataTable();
            try
            {
                string rulesid = "";
                dtnac_str = (DataTable)JsonConvert.DeserializeObject(nac_str, (typeof(DataTable)));
                if (dtnac_str.Rows.Count > 0)
                {
                    for (int i = 0; i < dtnac_str.Rows.Count; i++)
                    {
                        fw_destinationip = dtnac_str.Rows[i][1].ToString();
                        rulesname = fw_destinationip;
                        desc = fw_destinationip;
                        fw_direction = dtnac_str.Rows[i][2].ToString();
                        fw_perform_action = dtnac_str.Rows[i][3].ToString();
                        fw_protocol_rules = dtnac_str.Rows[i][4].ToString();

                        Rcount = Objquery.Get_Rules_Count_fw(rulestype, firewall_rules, fw_sourceip, fw_sourceport, fw_destinationip, fw_destinationport, fw_direction, fw_perform_action, fw_protocol_rules);
                        if (Rcount == 0)
                        {
                            if (fw_destinationip != "")
                            {
                                Rcount = Objquery.Get_Rules_last_Count();
                                if (fw_sourceip == "" || fw_sourceip == null)
                                    fw_sourceip = "NA";
                                if (fw_sourceport == "" || fw_sourceport == null)
                                    fw_sourceport = "NA";
                                if (fw_destinationport == "" || fw_destinationport == null)
                                    fw_destinationport = "NA";
                                strquerymsg = "#6061@2!" + Rcount + "!" + rulestype + "!" + firewall_rules + "!" + fw_sourceip + "!" + fw_sourceport + "!" + fw_destinationip + "!" + fw_destinationport + "!" + fw_direction + "!" + fw_perform_action + "!" + fw_protocol_rules + "!" + "@6061#";
                                strremovemsg = "#6061@0!" + Rcount + "!" + rulestype + "!" + firewall_rules + "!" + fw_sourceip + "!" + fw_sourceport + "!" + fw_destinationip + "!" + fw_destinationport + "!" + fw_direction + "!" + fw_perform_action + "!" + fw_protocol_rules + "!" + "@6061#";
                                objcommon.WriteLog("csatpolicymgmtController", "Create_Rules :Firewall Apply msg : " + strquerymsg + " || Remove msg : " + strremovemsg);
                                querystatus = Objquery.insert_rules_master_fw_multipleparam(Rcount.ToString(), rulesname, rulestype, firewall_rules, "1", "1", fw_sourceip, fw_sourceport, fw_destinationip, fw_destinationport, fw_direction, fw_perform_action, fw_protocol_rules, strquerymsg, strremovemsg, desc);
                            }
                        }
                        string strtmpruleid = getRuleID(rulesname, rulestype, firewall_rules, "1", fw_destinationip);
                        if (strtmpruleid.Length > 0)
                            rulesid += "," + strtmpruleid;
                    }
                    if (rulesid.Length > 0)
                        rulesid = rulesid.Substring(1);
                    if (policyname != "" && rulesid != "")
                    {
                        if (policyname != null && rulesid != null)
                        {
                            querystatus = Objquery.insert_policy_master(policyname, policydesc);
                            policyid = Objquery.Get_Last_policy_Id();
                            querystatus = Objquery.insert_policy_linkage(policyid, rulesid);
                            if (querystatus == 0)
                                result = "Policy created failed.";
                            else
                                result = "Policy created successfully.";
                        }
                        else
                        {
                            result = "Please Enter Policy Details..";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Create_NACPolicy Exception : " + ex.Message.ToString());
            }

            return Json(result);
        }
        private string getRuleID(string ruleName, string RuleType, string RuleSubType, string PolicyType, string Param3)
        {
            string ruleID = "";
            ruleID = Objquery.get_rule_id(ruleName, RuleType, RuleSubType, PolicyType, Param3);
            return ruleID;
        }
        public JsonResult Get_Policy_Unapply(string policyid, string deviceid)
        {
            try
            {
                //try
                //{
                //    dt = Objquery.get_ouid_policy(deviceid.Substring(1));
                //    if (dt.Rows.Count > 0)
                //    {
                //        for (int j = 0; j < dt.Rows.Count; j++)
                //        {
                //            Objquery.unapply_policy_linkage_master_OU(policyid, dt.Rows[j]["ou_id"].ToString());
                //            Objquery.unapply_policy_linkage_master_OU(policyid, dt.Rows[j]["ou_id"].ToString());
                //        }
                //    }
                //}
                //catch
                //{

                //}

                string jj = deviceid.Substring(1);
                string[] values = jj.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    try
                    {
                        querystatus = Objquery.unapply_policy_linkage_master(policyid, values[i].ToString());
                        if (querystatus == 0)
                            querystatus = Objquery.unapply_policy_linkage_master1(policyid);
                    }
                    catch (Exception)
                    {
                        //  querystatus = Objquery.unapply_policy_linkage_master1(policyid);
                    }
                    try
                    {
                        querystatus = Objquery.unapply_policy_deployment_master(policyid, values[i].ToString());
                        if (querystatus == 0)
                            querystatus = Objquery.unapply_policy_deployment_master1(policyid);
                    }
                    catch (Exception)
                    {
                        //querystatus = Objquery.unapply_policy_deployment_master1(policyid);
                    }
                    querystatus = Objquery.unapply_Policy_Querylog(values[i].ToString(), policyid, Objquery.Get_Location_ID(values[i].ToString()));

                    querystatus = Objquery.unapply_ou_deployment_linkage_master(policyid, values[i].ToString());


                }

                if (querystatus == 0)
                    result = "Policy unapplied failed.";
                else
                    result = "Policy unapplied successfully.";

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_Unapply Exception : " + ex.Message.ToString());
            }
            return Json(result);

        }

        // Policy Remove
        public JsonResult Get_Policy_remove(string policyid)
        {
            try
            {
                try
                {
                    querystatus = Objquery.remove_policy_linkage_master(policyid);
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_remove policy linkage Exception : " + ex.Message.ToString());
                }
                try
                {
                    querystatus = Objquery.remove_policy_deployment_master(policyid);
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_remove deployment master Exception : " + ex.Message.ToString());
                }

                try
                {
                    dt = Objquery.Get_device_policy_info_unapply(policyid);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            querystatus = Objquery.unapply_Policy_Querylog(dt.Rows[j]["device_id"].ToString(), policyid, Objquery.Get_Location_ID(dt.Rows[j]["device_id"].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_remove Querylog Exception : " + ex.Message.ToString());
                }
                try
                {
                    querystatus = Objquery.remove_ou_deployment_linkage_master(policyid);
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_remove deployment linkage master Exception : " + ex.Message.ToString());
                }
                try
                {
                    querystatus = Objquery.remove_policy_master(policyid);
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_remove policy master Exception : " + ex.Message.ToString());
                }
                try
                {
                    querystatus = Objquery.remove_policy_linkage(policyid);
                }
                catch (Exception ex)
                {
                    objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_remove policy linkage Exception : " + ex.Message.ToString());
                }

                if (querystatus == 0)
                    result = "Policy removed failed.";
                else
                    result = "Policy removed successfully.";

            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Get_Policy_remove Exception : " + ex.Message.ToString());
            }

            return Json(result);
        }
        public JsonResult bindprocess()
        {
            dt = Objquery.Get_Process_list();
            return Json(dt);
        }
        public JsonResult bindprocess_crc(string process)
        {
            dt = Objquery.Get_Process_CRC(process);
            return Json(dt);
        }
        public JsonResult binduser(string ostype)
        {
            string osname = "";
            if (ostype == "0")
                osname = "Windows";
            else
                osname = "Linux";
            dt = Objquery.Get_user_details(osname);
            return Json(dt);
        }
        public JsonResult GetPolicyCharts(string device)
        {
            DataTable dt_group = new DataTable();
            string strdeppolicy = "";
            string stravlpolicy = "";
            string strdeppolicysys = "";
            string strdeppolicyou = "";
            // Deployed Policy chart
            dt_group = Objquery.GroupBindpolicyAlert(groupid);
            try
            {

                dt = Objquery.DeployedPolicyChartData();
                DataTable deploypolicys = (from dtpolicy in dt.AsEnumerable()
                                           join dtgroup in dt_group.AsEnumerable()
                                           on dtpolicy["policy_type"].ToString() equals dtgroup["menu_name"].ToString()
                                           where dtgroup.Field<string>("menu_name") == dtpolicy.Field<string>("policy_type")
                                           select dtpolicy
                                         ).CopyToDataTable();
                List<Multibarchart> deppolicy = objcommon.ConvertDataTable<Multibarchart>(deploypolicys);
                strdeppolicy = JsonConvert.SerializeObject(deppolicy);
                objcommon.WriteLog("csatpolicymgmtController", "Getdeployedpolicy Data : " + strdeppolicy);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Getdeployedpolicy Exception : " + ex.Message.ToString());
            }
            // Available Policy chart
            try
            {
                dt = Objquery.AvailablePolicyChartData();
                DataTable availablepolicys = (from dtpolicy in dt.AsEnumerable()
                                              join dtgroup in dt_group.AsEnumerable()
                                              on dtpolicy["policy_type"].ToString() equals dtgroup["menu_name"].ToString()
                                              where dtgroup.Field<string>("menu_name") == dtpolicy.Field<string>("policy_type")
                                              select dtpolicy
                                       ).CopyToDataTable();
                List<Multibarchart> avlpolicy = objcommon.ConvertDataTable<Multibarchart>(availablepolicys);
                stravlpolicy = JsonConvert.SerializeObject(avlpolicy);
                objcommon.WriteLog("csatpolicymgmtController", "Getavailablepolicy Data : " + stravlpolicy);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Getavailablepolicy Exception : " + ex.Message.ToString());
            }
            // Deployed Policy System wise chart
            try
            {
                dt = Objquery.SystemDeployedPolicyChartData();
                List<Multibarchart> depsyspolicy = objcommon.ConvertDataTable<Multibarchart>(dt);
                strdeppolicysys = JsonConvert.SerializeObject(depsyspolicy);
                objcommon.WriteLog("csatpolicymgmtController", "Getdeployedpolicysystem Data : " + strdeppolicysys);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Getdeployedpolicysystem Exception : " + ex.Message.ToString());
            }
            // Deployed Policy OU wise chart
            try
            {
                string OUChild = Objquery.Get_ParentOu_id(device);
                dt = Objquery.OUDeployedPolicyChartData(OUChild);
                DataTable dpoupolicys = (from dtpolicy in dt.AsEnumerable()
                                         join dtgroup in dt_group.AsEnumerable()
                                         on dtpolicy["policy_type"].ToString() equals dtgroup["menu_name"].ToString()
                                         where dtgroup.Field<string>("menu_name") == dtpolicy.Field<string>("policy_type")
                                         select dtpolicy
                                     ).CopyToDataTable();
                List<Multibarchart> depoupolicy = objcommon.ConvertDataTable<Multibarchart>(dpoupolicys);
                strdeppolicyou = JsonConvert.SerializeObject(depoupolicy);
                objcommon.WriteLog("csatpolicymgmtController", "Getdeployedpolicou Data : " + strdeppolicyou);
            }
            catch (Exception ex)
            {
                objcommon.WriteLog("csatpolicymgmtController", "Getdeployedpolicou Exception : " + ex.Message.ToString());
            }



            var data = new { chartstrdeppolicy = strdeppolicy, chartstravlpolicy = stravlpolicy, sysip = Objquery.Get_systemnameip(), chartstrdepsyspolicy = strdeppolicysys, chartstrdepoupolicy = strdeppolicyou };
            return Json(data);


        }
        public IActionResult policy_hardware_policy()
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
        public IActionResult policy_rules_mgmt()
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
        public IActionResult policy_share_policy()
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
        public IActionResult policy_user_policy()
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
        public IActionResult policy_password_complexity()
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
        public IActionResult policy_remotedesktop_policy()
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
        public IActionResult policy_ipchange_restriction()
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
        public IActionResult policy_shutdown_policy()
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
        public IActionResult policy_powersaver_policy()
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
        public IActionResult policy_login_restriction()
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
    }
}