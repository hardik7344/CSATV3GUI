#pragma checksum "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_report\report_active_directory_data.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "76eec5920b3f8a5d06b07bc88ae0a58db317aea0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_csat_report_report_active_directory_data), @"mvc.1.0.view", @"/Views/csat_report/report_active_directory_data.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/csat_report/report_active_directory_data.cshtml", typeof(AspNetCore.Views_csat_report_report_active_directory_data))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\_ViewImports.cshtml"
using OwnYITCSAT;

#line default
#line hidden
#line 2 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\_ViewImports.cshtml"
using OwnYITCSAT.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"76eec5920b3f8a5d06b07bc88ae0a58db317aea0", @"/Views/csat_report/report_active_directory_data.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbc54f8cb37d807092c8e3ca3f38d3620e0913e8", @"/Views/_ViewImports.cshtml")]
    public class Views_csat_report_report_active_directory_data : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "3", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("frm_report_ad_data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frm_report_ad_data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onload", new global::Microsoft.AspNetCore.Html.HtmlString("getaddata()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_report\report_active_directory_data.cshtml"
  
    ViewData["Title"] = "report_active_directory_data";
    Layout = "~/Views/Shared/ownyit_main_page.cshtml";

#line default
#line hidden
            BeginContext(122, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(124, 3913, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4981aed767844a2a34f7cf769752c97", async() => {
                BeginContext(151, 275, true);
                WriteLiteral(@"
    <div class=""row"">
        <!--side menu-->
        <div class=""ibox float-e-margins "">
            <div class=""ibox-content"" style=""padding:4px;"">
                <ul class=""breadcrumb"" style=""margin-bottom:0px;"">
                    <li>
                        ");
                EndContext();
                BeginContext(427, 73, false);
#line 14 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_report\report_active_directory_data.cshtml"
                   Write(Html.ActionLink("Reports", "report_active_directory_data", "csat_report"));

#line default
#line hidden
                EndContext();
                BeginContext(500, 233, true);
                WriteLiteral("\r\n                        <i class=\"icon-angle-right\"></i>\r\n                    </li>\r\n                    <li> Active Directory Data  </li>\r\n                </ul>\r\n            </div>\r\n        </div>\r\n\r\n        <!--side menu end-->\r\n");
                EndContext();
                BeginContext(764, 165, true);
                WriteLiteral("\r\n        <div class=\"col-sm-2 col-md-2 col-lg-2 no_padding\">\r\n            <div class=\"ibox float-e-margins \">\r\n                <div class=\"ibox-content sidemenu\">\r\n");
                EndContext();
                BeginContext(978, 32, false);
#line 29 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_report\report_active_directory_data.cshtml"
                   Write(Html.Partial("csat_report_menu"));

#line default
#line hidden
                EndContext();
#line 29 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_report\report_active_directory_data.cshtml"
                                                         ;
                    

#line default
#line hidden
                BeginContext(1036, 484, true);
                WriteLiteral(@"                </div>
            </div>
        </div>
        <div class=""col-sm-10 col-md-10 col-lg-10"">
            <div class=""ibox float-e-margins"" style=""margin-bottom: 10px;"">
                <div class=""ibox-content"">
                    <div class=""row"">
                        <div class=""col-sm-12 col-md-12 col-lg-12"">

                            <div class=""row"">
                                <div class=""col-xs-12"">
                                    ");
                EndContext();
                BeginContext(1520, 563, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9acd8650755646ed815e1273342e79c9", async() => {
                    BeginContext(1564, 512, true);
                    WriteLiteral(@"
                                        <div class=""form-group"">
                                            <label for=""nameField"" style=""margin-top: -5px;background-color: #fff;text-align: center;color: #3D93C8;width: 120px;"" class=""col-xs-2 form_label"">&nbsp;&nbsp;Advance Search&nbsp;&nbsp;</label>
                                            <div class=""col-xs-10"">
                                            </div>
                                        </div>
                                    ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2083, 256, true);
                WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=""row"">
                        <div class=""col-xs-12"">
                            ");
                EndContext();
                BeginContext(2339, 1108, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3724c2ea7ad44afda2d63f054da1ad9c", async() => {
                    BeginContext(2433, 388, true);
                    WriteLiteral(@"
                                <div class=""form-group"">
                                    <label for=""nameField"" class=""col-xs-1"">AD Data Type</label>
                                    <div class=""col-xs-4"">
                                        <select class=""form-control col-sm-3 col-lg-3 col-md-3"" name=""ad_data"" id=""ad_data"">
                                            ");
                    EndContext();
                    BeginContext(2821, 43, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "65fcbd11601d405288f6c4a85db320ac", async() => {
                        BeginContext(2839, 16, true);
                        WriteLiteral("Active Directory");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(2864, 46, true);
                    WriteLiteral("\r\n                                            ");
                    EndContext();
                    BeginContext(2910, 31, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7abea93eeeb9472bbc027ebed70c739b", async() => {
                        BeginContext(2928, 4, true);
                        WriteLiteral("User");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(2941, 46, true);
                    WriteLiteral("\r\n                                            ");
                    EndContext();
                    BeginContext(2987, 33, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3481ef10613d4cc9b102c9be10773c0f", async() => {
                        BeginContext(3005, 6, true);
                        WriteLiteral("Device");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(3020, 420, true);
                    WriteLiteral(@"
                                        </select>
                                    </div>
                                    <div class=""pull-left col-xs-1"">
                                        <input type=""button"" class=""btn btn-primary"" value=""Show"" id=""btnshowreport"" onclick=""getaddata()"">
                                    </div>
                                </div>
                            ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3447, 583, true);
                WriteLiteral(@"
                        </div>
                    </div>


                </div>
            </div>
            <!--form  content end-->
            <!--agent report form end-->
            <!--agent report Datatable start-->
            <div class=""row ibox2"">
                <div class=""col-sm-12 col-md-12 col-lg-12 ibox2-content"" id=""addata_divclear"">
                    <table id=""tbladdata"" class=""table table-striped table-bordered display nowrap"" width=""100%""></table>
                </div>
            </div>
        </div>
        <br />
    </div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4037, 257, true);
            WriteLiteral(@"

<script>
    var table1 = """";
        function getaddata() {
            //debugger;
            $(""#overlay"").show();
            $(""#overlay"").fadeIn(300);
            var type = document.getElementById(""ad_data"").value;
            var url = '");
            EndContext();
            BeginContext(4295, 46, false);
#line 97 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_report\report_active_directory_data.cshtml"
                  Write(Url.Content("~/csat_report/getaddata?adtype="));

#line default
#line hidden
            EndContext();
            BeginContext(4341, 6756, true);
            WriteLiteral(@"' + type;
            $.ajax({
                type: ""POST"",
                url: url,
                success: function (response) {
                    //debugger;
                    var agent_divclear = document.getElementById('addata_divclear');
                        agent_divclear.innerHTML = '&nbsp;';
                    $('#addata_divclear').append(""<table id='tbladdata' class='table table-striped table-bordered' width='100%'></table>"");
                    otable = document.getElementById('tbladdata');
                    var data1 = [];
                    if (type == 1) {
                        for (var i = 0; i < response.ad_data.length; i++) {
                            data1.push([response.ad_data[i].ouName, response.ad_data[i].longName]);
                        }
                        var otable = $(""#tbladdata"").DataTable({
                            data: data1, scrollX: !0, searching: !1, dom: ""<'top'lB>rt<'bottom'ip><'clear'>"", buttons: [""colvis"", { text: ""PDF"", exte");
            WriteLiteral(@"nd: ""pdfHtml5"", download: ""open"", filename: ""Active Directory_"" + today, orientation: ""portrait"", pageSize: ""A4"", exportOptions: { columns: "":visible"", search: ""applied"", order: ""applied"" }, customize: function (t) { t.content.splice(0, 1); var e = new Date, n = e.getDate() + ""-"" + (e.getMonth() + 1) + ""-"" + e.getFullYear(); t.pageMargins = [20, 60, 20, 30], t.defaultStyle.fontSize = 7, t.styles.tableHeader.fontSize = 7, t.header = function () { return { columns: [{ margin: [0, -5, 30, 15], alignment: ""left"", width: 100, image: ""data:image/png;base64,"" + pdflogo }, { margin: [-10, 10, 10, 0], alignment: ""center"", width: 410, text: ""Active Directory Report"", fontSize: 12 }, { alignment: ""right"", fontSize: 14, text: """" }], margin: 20 } }, t.footer = function (t, e) { return { columns: [{ alignment: ""left"", text: [""Created on: "", { text: n.toString() }] }, { alignment: ""right"", text: [""page "", { text: t.toString() }, "" of "", { text: e.toString() }] }], margin: 20 } }; var a = { hLineWidth: function (t) { return ");
            WriteLiteral(@".5 }, vLineWidth: function (t) { return .5 }, hLineColor: function (t) { return ""#aaa"" }, vLineColor: function (t) { return ""#aaa"" }, paddingLeft: function (t) { return 4 }, paddingRight: function (t) { return 4 } }; t.content[0].layout = a } }, 'excel'],
                            columns: [{ title: ""OU Name"", width: '90px' }, { title: ""OU Long Name"", width: '90px' }]
                        });
                    }
                    else if (type == 2) {
                        for (var i = 0; i < response.ad_data.length; i++) {
                            data1.push([response.ad_data[i].ouName, response.ad_data[i].userName, response.ad_data[i].longName]);
                        }
                        var otable = $(""#tbladdata"").DataTable({
                            data: data1, scrollX: !0, searching: !1, dom: ""<'top'lB>rt<'bottom'ip><'clear'>"", buttons: [""colvis"", { text: ""PDF"", extend: ""pdfHtml5"", download: ""open"", filename: ""Active Directory_"" + today, orientation: ""portrait"", pageSi");
            WriteLiteral(@"ze: ""A4"", exportOptions: { columns: "":visible"", search: ""applied"", order: ""applied"" }, customize: function (t) { t.content.splice(0, 1); var e = new Date, n = e.getDate() + ""-"" + (e.getMonth() + 1) + ""-"" + e.getFullYear(); t.pageMargins = [20, 60, 20, 30], t.defaultStyle.fontSize = 7, t.styles.tableHeader.fontSize = 7, t.header = function () { return { columns: [{ margin: [0, -5, 30, 15], alignment: ""left"", width: 100, image: ""data:image/png;base64,"" + pdflogo }, { margin: [-10, 10, 10, 0], alignment: ""center"", width: 410, text: ""Active Directory Report"", fontSize: 12 }, { alignment: ""right"", fontSize: 14, text: """" }], margin: 20 } }, t.footer = function (t, e) { return { columns: [{ alignment: ""left"", text: [""Created on: "", { text: n.toString() }] }, { alignment: ""right"", text: [""page "", { text: t.toString() }, "" of "", { text: e.toString() }] }], margin: 20 } }; var a = { hLineWidth: function (t) { return .5 }, vLineWidth: function (t) { return .5 }, hLineColor: function (t) { return ""#aaa"" }, vLineColor: fu");
            WriteLiteral(@"nction (t) { return ""#aaa"" }, paddingLeft: function (t) { return 4 }, paddingRight: function (t) { return 4 } }; t.content[0].layout = a } }, 'excel'],
                            columns: [{ title: ""OU Name"", width: '90px' }, { title: ""User Name"", width: '90px' }, { title: ""Long Name"", width: '90px' }]
                        });
                    }
                    else if (type == 3) {
                        for (var i = 0; i < response.ad_data.length; i++) {
                            data1.push([response.ad_data[i].ouName, response.ad_data[i].deviceName]);
                        }
                        var otable = $(""#tbladdata"").DataTable({
                            data: data1, scrollX: !0, searching: !1, dom: ""<'top'lB>rt<'bottom'ip><'clear'>"", buttons: [""colvis"", { text: ""PDF"", extend: ""pdfHtml5"", download: ""open"", filename: ""Active Directory_"" + today, orientation: ""portrait"", pageSize: ""A4"", exportOptions: { columns: "":visible"", search: ""applied"", order: ""applied"" }, customize");
            WriteLiteral(@": function (t) { t.content.splice(0, 1); var e = new Date, n = e.getDate() + ""-"" + (e.getMonth() + 1) + ""-"" + e.getFullYear(); t.pageMargins = [20, 60, 20, 30], t.defaultStyle.fontSize = 7, t.styles.tableHeader.fontSize = 7, t.header = function () { return { columns: [{ margin: [0, -5, 30, 15], alignment: ""left"", width: 100, image: ""data:image/png;base64,"" + pdflogo }, { margin: [-10, 10, 10, 0], alignment: ""center"", width: 410, text: ""Active Directory Report"", fontSize: 12 }, { alignment: ""right"", fontSize: 14, text: """" }], margin: 20 } }, t.footer = function (t, e) { return { columns: [{ alignment: ""left"", text: [""Created on: "", { text: n.toString() }] }, { alignment: ""right"", text: [""page "", { text: t.toString() }, "" of "", { text: e.toString() }] }], margin: 20 } }; var a = { hLineWidth: function (t) { return .5 }, vLineWidth: function (t) { return .5 }, hLineColor: function (t) { return ""#aaa"" }, vLineColor: function (t) { return ""#aaa"" }, paddingLeft: function (t) { return 4 }, paddingRight: function (t)");
            WriteLiteral(@" { return 4 } }; t.content[0].layout = a } }, 'excel'],
                            columns: [{ title: ""OU Name"", width: '90px' }, { title: ""Device Name"", width: '90px' }]
                        });
                    }
                        $("".table"").css({ ""width"": ""100%"" });
                        $("".dataTables_scrollHeadInner "").css({ ""width"": ""100%"" });
                    }
                }).done(function () {
                    setTimeout(function () {
                        $(""#overlay"").fadeOut(300);
                    }, 500);
                });

        }
</script>

");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
