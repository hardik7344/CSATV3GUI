#pragma checksum "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\authorizeip_access_gui.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b70b3474e99bf8e63383a9a2d712561c41a1fa67"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_csat_settings_page_authorizeip_access_gui), @"mvc.1.0.view", @"/Views/csat_settings_page/authorizeip_access_gui.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/csat_settings_page/authorizeip_access_gui.cshtml", typeof(AspNetCore.Views_csat_settings_page_authorizeip_access_gui))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b70b3474e99bf8e63383a9a2d712561c41a1fa67", @"/Views/csat_settings_page/authorizeip_access_gui.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbc54f8cb37d807092c8e3ca3f38d3620e0913e8", @"/Views/_ViewImports.cshtml")]
    public class Views_csat_settings_page_authorizeip_access_gui : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/ipaddress_validation.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("ip_scan_form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onload", new global::Microsoft.AspNetCore.Html.HtmlString("showauthipaddress();"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\authorizeip_access_gui.cshtml"
  
    ViewData["Title"] = "authorizeip_access_gui";
    Layout = "~/Views/Shared/ownyit_main_page.cshtml";

#line default
#line hidden
            BeginContext(116, 52, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07cfd212b64d4c10a4d1ecd260bb3a91", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(168, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(170, 8961, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5bb74180ae14a8c8942a44f2c761be2", async() => {
                BeginContext(206, 266, true);
                WriteLiteral(@"
    <div class=""row"">

        <div class=""ibox float-e-margins "">
            <div class=""ibox-content"" style=""padding:4px;"">               
                <ul class=""breadcrumb"" style=""margin-bottom:0px;"">
                    <li>
                        ");
                EndContext();
                BeginContext(473, 74, false);
#line 14 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\authorizeip_access_gui.cshtml"
                   Write(Html.ActionLink("Settings", "setting_scan_ip_range", "csat_settings_page"));

#line default
#line hidden
                EndContext();
                BeginContext(547, 392, true);
                WriteLiteral(@"
                        <i class=""icon-angle-right""></i>
                    </li>
                    <li>Authorized IP Access GUI</li>
                </ul>
            </div>
        </div>

        <!--side menu-->
        <div class=""col-sm-2 col-md-2 col-lg-2 no_padding"">
            <div class=""ibox float-e-margins "">
                <div class=""ibox-content sidemenu"">
");
                EndContext();
                BeginContext(988, 39, false);
#line 27 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\authorizeip_access_gui.cshtml"
                   Write(Html.Partial("csat_settings_page_menu"));

#line default
#line hidden
                EndContext();
#line 27 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\authorizeip_access_gui.cshtml"
                                                                ;
                    

#line default
#line hidden
                BeginContext(1053, 382, true);
                WriteLiteral(@"                </div>
            </div>
        </div>
        <!--side menu end-->

        <div class=""col-sm-10 col-md-10 col-lg-10"">
            <div class=""ibox float-e-margins"" style=""margin-bottom: 10px;"">
                <div class=""ibox-content"">
                    <div class=""row"">
                        <div class=""col-xs-12"">
                            ");
                EndContext();
                BeginContext(1435, 811, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ab228e2288ce4d5b87b2b4b5b9627a33", async() => {
                    BeginContext(1499, 740, true);
                    WriteLiteral(@"
                                <div class=""form-group"">
                                    <label for=""nameField"" class=""col-xs-1"">IP Address</label>
                                    <div class=""col-xs-2"">
                                        <input type=""text"" name=""accessip"" id=""accessip"" class=""form-control ip_address"" placeholder=""Enter IP"" />
                                    </div>
                                    <div class=""pull-left col-xs-1"">
                                        <input type=""button"" class=""btn btn-primary"" value=""Add IP"" id=""btnsavescanip"" onclick=""authip_accessGUI()"">
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
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2246, 1353, true);
                WriteLiteral(@"
                        </div>
                    </div>
                </div>
            </div>

            <!--form  content end-->

            <div class=""row ibox2"">
                <div class=""col-sm-12 col-md-12 col-lg-12 ibox2-content"" id=""divauthip_div"">
                    <table id=""divauthip"" class=""table table-striped table-bordered "" width=""100%""></table>
                </div>
            </div>
        </div>
        <div class=""space_bottom"">&nbsp;</div>
    </div>
    <!--delete_range device model  content-->
    <div class='modal fade ' id='delete_range' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'>
        <div class='modal-dialog  modal-small'>
            <div class='modal-content '>
                <div class='modal-header'>
                    <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>
                    <h6 class='modal-title'>Conformation </h6>
                </div>
     ");
                WriteLiteral(@"           <div class='modal-body'>
                    <!--form  content-->
                    <div class='row'>
                        <div class='col-sm-12 col-md-12 col-lg-12'>

                            <div class='row'>
                                <div class='col-xs-12'>
                                    ");
                EndContext();
                BeginContext(3599, 1222, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4cb1128d986341df9298447e7683251f", async() => {
                    BeginContext(3643, 1171, true);
                    WriteLiteral(@"
                                        <div class='form-group'>

                                            <label for='nameField' class='col-xs-12'>
                                                <i class='glyphicon glyphicon-exclamation-sign delete_msg'></i>
                                                <span style='font-weight:normal;margin-left:30px;'>Are you sure you want to delete this record?</span>
                                            </label>

                                        </div>
                                        <div class='form-group'>
                                            <label for='nameField' class='col-xs-4'></label>
                                            <div class='col-xs-6'>
                                                <input type='button' class='btn btn-primary' name='yes' value='Yes' id=""btnauthguidelete"" />
                                                <input type='button' class='btn btn-default' data-dismiss=""modal"" name='no' valu");
                    WriteLiteral("e=\'No\' />\r\n                                            </div>\r\n                                        </div>\r\n                                    ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4821, 366, true);
                WriteLiteral(@"
                                </div>

                            </div>

                        </div>
                    </div>  <!--form  content end-->
                </div>

            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->
    <!-- delete_range main node popup model box  Content-->
");
                EndContext();
                BeginContext(5213, 192, true);
                WriteLiteral("    <script>\r\n        function showauthipaddress() {\r\n            $(\"#overlay\").show();\r\n            $(\"#overlay\").fadeIn(300);\r\n        $.ajax({\r\n            type: \"POST\",\r\n            url: \'");
                EndContext();
                BeginContext(5406, 52, false);
#line 116 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\authorizeip_access_gui.cshtml"
             Write(Url.Content("~/csat_settings_page/GetAuthIPAddress"));

#line default
#line hidden
                EndContext();
                BeginContext(5458, 2471, true);
                WriteLiteral(@"',
            success: function (data) {
                //debugger;
                var dataclear = document.getElementById('divauthip_div');
                dataclear.innerHTML = '&nbsp;';
                $('#divauthip_div').append(""<table id='divauthip' class='table table-striped table-bordered' width='100%'></table>"");
                dtable = document.getElementById('divauthip');
                var data1 = [];
                for (var i = 0; i < data.dtauthip.length; i++) {
                    //debugger;
                    var delete_div = ""<i class=' glyphicon glyphicon-trash manage_icon ' type='button' data-toggle='modal'  title='Delete' onclick=deleteauthipgui('"" + data.dtauthip[i].ipaddress + ""')></i>"";
                    data1.push([data.dtauthip[i].ipaddress, delete_div]);
                }
                var dtable = $(""#divauthip"").DataTable({
                    data: data1, scrollX: !0, searching: !1, dom: ""<'top'lB>rt<'bottom'ip><'clear'>"", buttons: [""colvis""],
           ");
                WriteLiteral(@"         columns: [{ title: ""IP Address"" },{ title: ""Delete"", orderable: false,className: 'dt-center', width: '30px' }]
                });
                $("".table"").css({ ""width"": ""100%"" });
                $("".dataTables_scrollHeadInner "").css({ ""width"": ""100%"" });
            }

        }).done(function () {
            setTimeout(function () {
                $(""#overlay"").fadeOut(300);
            }, 500);
        });
    }

        function authip_accessGUI() {          
           // debugger;
                 var varipadd = document.getElementById('accessip').value;
                 var ip = varipadd.split(""."");
                    if (ip.length != 4) {
                        alert(""Please provide Proper IP Range !"");                       
                        return false;
                    }
                    //Check Numbers
                    for (var c = 0; c < 4; c++) {
                        //Perform Test
                        if (!(1 / ip[c] > 0) || ip[c]");
                WriteLiteral(@" > 255 || isNaN(parseFloat(ip[c])) || !isFinite(ip[c]) || ip[c].indexOf("" "") !== -1) {
                            alert(""Please provide Proper IP Range !"");
                            return false;
                        }
                    }  
                     //alert(""Please provide Proper IP Range !"");
                     $.ajax({
                        type: ""POST"",
                         url: '");
                EndContext();
                BeginContext(7930, 63, false);
#line 163 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\authorizeip_access_gui.cshtml"
                          Write(Url.Content("~/csat_settings_page/AddAuthIPAddress?ipaddress="));

#line default
#line hidden
                EndContext();
                BeginContext(7993, 681, true);
                WriteLiteral(@"' + varipadd,
                         success: function (data) {
                          // debugger;
                            alert(data);                          
                             showauthipaddress();
                             document.getElementById('accessip').value = """";
                        }
                     });
          }

    // Delete Scan IP Range
        var ipaddress2 = """";  
        function deleteauthipgui(ipaddress) {
      //  debugger;       
            ipaddress2 = ipaddress
           $('#delete_range').modal('show');
        }
        $('#btnauthguidelete').on('click',function(){
            var url1 = '");
                EndContext();
                BeginContext(8675, 62, false);
#line 181 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\authorizeip_access_gui.cshtml"
                   Write(Url.Content("~/csat_settings_page/DeleteAuthGUIIP?ipaddress="));

#line default
#line hidden
                EndContext();
                BeginContext(8737, 387, true);
                WriteLiteral(@"' + ipaddress2;
        $.ajax({
        type: ""POST"",
        url: url1,
        success: function (result) {
            alert(result);
            $('#delete_range').modal('hide');
            showauthipaddress();
            ipaddress = """";            
		 }
		 });

	});


    </script>
    <script>
        $('.ip_address').mask('099.099.099.099');
    </script>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(9131, 8, true);
            WriteLiteral("\r\n\r\n\r\n\r\n");
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
