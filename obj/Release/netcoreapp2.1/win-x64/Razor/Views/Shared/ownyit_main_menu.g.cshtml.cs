#pragma checksum "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\Shared\ownyit_main_menu.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "540daca61ad75b5fcd752834cd7562f958cafdf4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_ownyit_main_menu), @"mvc.1.0.view", @"/Views/Shared/ownyit_main_menu.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/ownyit_main_menu.cshtml", typeof(AspNetCore.Views_Shared_ownyit_main_menu))]
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
#line 2 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\Shared\ownyit_main_menu.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#line 5 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\Shared\ownyit_main_menu.cshtml"
using System.Data;

#line default
#line hidden
#line 6 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\Shared\ownyit_main_menu.cshtml"
using OwnYITCSAT.DataAccessLayer;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"540daca61ad75b5fcd752834cd7562f958cafdf4", @"/Views/Shared/ownyit_main_menu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbc54f8cb37d807092c8e3ca3f38d3620e0913e8", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_ownyit_main_menu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<System.Data.DataTable>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(125, 103, true);
            WriteLiteral("\r\n<div id=\"wrap\">\r\n    <ul class=\"nav navbar-nav navbar-right\" style=\"background-color:transparent;\">\r\n");
            EndContext();
            BeginContext(800, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1334, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 22 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\Shared\ownyit_main_menu.cshtml"
          
            if (OwnYITConstant.DT_MAIN_MENU != null)
            {
                foreach (DataRow dr in (OwnYITConstant.DT_MAIN_MENU.Rows))
                {

#line default
#line hidden
            BeginContext(1512, 67, true);
            WriteLiteral("                    <li class=\"active\">\r\n                        <a");
            EndContext();
            BeginWriteAttribute("href", " href=\'", 1579, "\'", 1652, 1);
#line 28 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\Shared\ownyit_main_menu.cshtml"
WriteAttributeValue("", 1586, Url.Content("~" + @dr["menu_url"] + "?id=" + @dr["menu_id"] + ""), 1586, 66, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1653, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1655, 15, false);
#line 28 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\Shared\ownyit_main_menu.cshtml"
                                                                                                Write(dr["menu_name"]);

#line default
#line hidden
            EndContext();
            BeginContext(1670, 33, true);
            WriteLiteral("</a>\r\n                    </li>\r\n");
            EndContext();
#line 30 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\Shared\ownyit_main_menu.cshtml"
                }
            }
        

#line default
#line hidden
            BeginContext(1748, 704, true);
            WriteLiteral(@"        <li>
            <div class=""btn-group"" style=""margin:14px;"">
                <button type=""button"" class=""btn btn-xs  btn-default ""><span class=""glyphicon glyphicon-log-out""></span>  Logout</button>
                <button type=""button"" class=""btn btn-xs btn-default dropdown-toggle"" data-toggle=""dropdown"">
                    <span class=""caret""></span>
                </button>
                <ul class=""dropdown-menu"" role=""menu"">
                    <li><a href=""#"">Support</a></li>
                    <li><a href=""#"">Contact Us</a></li>
                    <li><a href=""#"">Help</a></li>
                </ul>
            </div>
        </li>


    </ul>
</div>




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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<System.Data.DataTable> Html { get; private set; }
    }
}
#pragma warning restore 1591
