#pragma checksum "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\admin_menu.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95e291742523de91f3a8ff51cf0a62270c348b53"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_csat_settings_page_admin_menu), @"mvc.1.0.view", @"/Views/csat_settings_page/admin_menu.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/csat_settings_page/admin_menu.cshtml", typeof(AspNetCore.Views_csat_settings_page_admin_menu))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95e291742523de91f3a8ff51cf0a62270c348b53", @"/Views/csat_settings_page/admin_menu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbc54f8cb37d807092c8e3ca3f38d3620e0913e8", @"/Views/_ViewImports.cshtml")]
    public class Views_csat_settings_page_admin_menu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(42, 79, true);
            WriteLiteral("\r\n<div class=\"category-tree mb30\">\r\n    <ul class=\"subcategories\">\r\n        <li");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 121, "\"", 257, 4);
            WriteAttributeValue("", 129, "130", 129, 3, true);
            WriteAttributeValue(" ", 132, "form_label", 133, 11, true);
            WriteAttributeValue(" ", 143, "div_none", 144, 9, true);
#line 6 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\admin_menu.cshtml"
WriteAttributeValue(" ", 152, ViewContext.RouteData.Values["Action"].ToString() == "setting_organization_structure" ? "active" : "", 153, 104, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(258, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(260, 97, false);
#line 6 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\admin_menu.cshtml"
                                                                                                                                                Write(Html.ActionLink("Organization Structure", "setting_organization_structure", "csat_settings_page"));

#line default
#line hidden
            EndContext();
            BeginContext(357, 18, true);
            WriteLiteral("</li>\r\n        <li");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 375, "\"", 487, 3);
            WriteAttributeValue("", 383, "134", 383, 3, true);
            WriteAttributeValue(" ", 386, "div_none", 387, 9, true);
#line 7 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\admin_menu.cshtml"
WriteAttributeValue(" ", 395, ViewContext.RouteData.Values["Action"].ToString() == "setting_user_mgmt" ? "active" : "", 396, 91, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(488, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(490, 77, false);
#line 7 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\admin_menu.cshtml"
                                                                                                                        Write(Html.ActionLink("User Management", "setting_user_mgmt", "csat_settings_page"));

#line default
#line hidden
            EndContext();
            BeginContext(567, 543, true);
            WriteLiteral(@"</li>
    </ul>
</div>
<script>
    $(function () {
        var current_page_URL = location.href;
        $(""a"").each(function () {
            if ($(this).attr(""href"") !== ""#"") {
                var target_URL = $(this).prop(""href"");
                if (target_URL == current_page_URL) {
                    $('.category-tree').parents('li, ul').removeClass('active');
                    $(this).parent('li').addClass('active');
                    return false;
                }
            }
        });
    });
</script>");
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
