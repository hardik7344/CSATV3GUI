#pragma checksum "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\setting_device_onoff.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17d5f08b911ff9044afd339b0f262e301cdb4bd8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_csat_settings_page_setting_device_onoff), @"mvc.1.0.view", @"/Views/csat_settings_page/setting_device_onoff.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/csat_settings_page/setting_device_onoff.cshtml", typeof(AspNetCore.Views_csat_settings_page_setting_device_onoff))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17d5f08b911ff9044afd339b0f262e301cdb4bd8", @"/Views/csat_settings_page/setting_device_onoff.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbc54f8cb37d807092c8e3ca3f38d3620e0913e8", @"/Views/_ViewImports.cshtml")]
    public class Views_csat_settings_page_setting_device_onoff : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("add_ip_form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onload", new global::Microsoft.AspNetCore.Html.HtmlString("showipaddress()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\setting_device_onoff.cshtml"
  
    ViewData["Title"] = "setting_device_onoff";
    Layout = "~/Views/Shared/ownyit_main_page.cshtml";

#line default
#line hidden
            BeginContext(114, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(116, 13560, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aeb38a33b8f84f1a850134f12497d2f6", async() => {
                BeginContext(147, 502, true);
                WriteLiteral(@"
    <div class=""row"">

        <div class=""ibox float-e-margins "">
            <div class=""ibox-content"" style=""padding:4px;"">
                <div class=""pull-right form_label btn_top_margin"">
                    <button class=""btn btn-primary btn-md day"" data-toggle=""modal"" data-target=""#show_ip_address"" id=""btnipaddress"">Add IP Address</button>
                </div>

                <ul class=""breadcrumb"" style=""margin-bottom:0px;"">
                    <li>
                        ");
                EndContext();
                BeginContext(650, 74, false);
#line 18 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\setting_device_onoff.cshtml"
                   Write(Html.ActionLink("Settings", "setting_scan_ip_range", "csat_settings_page"));

#line default
#line hidden
                EndContext();
                BeginContext(724, 387, true);
                WriteLiteral(@"
                        <i class=""icon-angle-right""></i>
                    </li>
                    <li>Device OnOff Status</li>
                </ul>
            </div>
        </div>

        <!--side menu-->
        <div class=""col-sm-2 col-md-2 col-lg-2 no_padding"">
            <div class=""ibox float-e-margins "">
                <div class=""ibox-content sidemenu"">
");
                EndContext();
                BeginContext(1160, 39, false);
#line 31 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\setting_device_onoff.cshtml"
                   Write(Html.Partial("csat_settings_page_menu"));

#line default
#line hidden
                EndContext();
#line 31 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\setting_device_onoff.cshtml"
                                                                ;
                    

#line default
#line hidden
                BeginContext(1225, 1320, true);
                WriteLiteral(@"                </div>
            </div>
        </div>
        <!--side menu end-->

        <div class=""col-sm-10 col-md-10 col-lg-10"">
            <div class=""row ibox2"">
                <div class=""col-sm-12 col-md-12 col-lg-12 ibox2-content"" id=""divscanip_div"">
                    <table id=""divscanip"" class=""table table-striped table-bordered "" width=""100%""></table>
                </div>
            </div>

        </div>
        <div class=""space_bottom"">&nbsp;</div>
    </div>
    <!-- show_detail_popup model box  Content-->
    <div class=""modal fade"" id=""show_ip_address"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"" aria-hidden=""true"" data-backdrop=""static"" data-keyboard=""false"">
        <div class=""modal-dialog  modal-small"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"" onclick=""cleardata()"">&times;</button>
                    <h6");
                WriteLiteral(@" class="" modal-title"">
                        Add IP Address
                    </h6>
                </div>
                <div class=""modal-body"" style=""padding: 20px;"">
                    <div class=""row"">
                        <div class=""col-xs-12"">
                            ");
                EndContext();
                BeginContext(2545, 1182, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "468bf5939f614c0b92ecdcc394cee80b", async() => {
                    BeginContext(2608, 1112, true);
                    WriteLiteral(@"
                                <div class=""form-group"">
                                    <label for=""nameField"" class=""col-xs-3"">IP Address</label>
                                    <div class=""col-xs-8"">
                                        <input class=""form-control ip_address"" type=""text"" name=""ipaddress"" id=""ipaddress"" autofocus autocomplete=""off"" />
                                    </div>
                                </div>
                                <div class=""form-group"" style=""margin-top: 15px;"">
                                    <label for=""nameField"" class=""col-xs-3""></label>
                                    <div class=""col-xs-6"">
                                        <input type=""button"" class=""btn btn-primary btn-custom"" name=""btnsaveip"" value=""Add IP"" id=""btnsaveip"" onclick=""validate()""/>
                                        <input type=""button"" class=""btn btn-default"" data-dismiss=""modal"" value=""Cancel"" onclick=""cleardata()"" />
                        ");
                    WriteLiteral("            </div>\r\n                                </div>\r\n                            ");
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
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3727, 9942, true);
                WriteLiteral(@"
                        </div>
                    </div>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div>

    <!-- End show_detail_popup model box Content -->

    <script>
        (function (g) {
            ""function"" === typeof define && define.amd ? define([""jquery""], g) : g(window.jQuery || window.Zepto)
        })(function (g) {
            var y = function (a, f, d) {
                var k = this,
                    x;
                a = g(a);
                f = ""function"" === typeof f ? f(a.val(), void 0, a, d) : f;
                k.init = function () {
                    d = d || {};
                    k.byPassKeys = [9, 16, 17, 18, 36, 37, 38, 39, 40, 91];
                    k.translation = {
                        0: {
                            pattern: /\d/
                        },
                        9: {
                            pattern: /\d/,
                            optional: ");
                WriteLiteral(@"!0
                        },
                        ""#"": {
                            pattern: /\d/,
                            recursive: !0
                        },
                        A: {
                            pattern: /[a-zA-Z0-9]/
                        },
                        S: {
                            pattern: /[a-zA-Z]/
                        }
                    };
                    k.translation = g.extend({}, k.translation, d.translation);
                    k = g.extend(!0, {}, k, d);
                    a.each(function () {
                        !1 !==
                            d.maxlength && a.attr(""maxlength"", f.length);
                        d.placeholder && a.attr(""placeholder"", d.placeholder);
                        a.attr(""autocomplete"", ""off"");
                        c.destroyEvents();
                        c.events();
                        var b = c.getCaret();
                        c.val(c.getMasked());
               ");
                WriteLiteral(@"         c.setCaret(b + c.getMaskCharactersBeforeCount(b, !0))
                    })
                };
                var c = {
                    getCaret: function () {
                        var b;
                        b = 0;
                        var e = a.get(0),
                            c = document.selection,
                            e = e.selectionStart;
                        if (c && !~navigator.appVersion.indexOf(""MSIE 10"")) b = c.createRange(), b.moveStart(""character"", a.is(""input"") ? -a.val().length : -a.text().length), b = b.text.length;
                        else if (e ||
                            ""0"" === e) b = e;
                        return b
                    },
                    setCaret: function (b) {
                        if (a.is("":focus"")) {
                            var e;
                            e = a.get(0);
                            e.setSelectionRange ? e.setSelectionRange(b, b) : e.createTextRange && (e = e.createTextRange(");
                WriteLiteral(@"), e.collapse(!0), e.moveEnd(""character"", b), e.moveStart(""character"", b), e.select())
                        }
                    },
                    events: function () {
                        a.on(""keydown.mask"", function () {
                            x = c.val()
                        });
                        a.on(""keyup.mask"", c.behaviour);
                        a.on(""paste.mask drop.mask"", function () {
                            setTimeout(function () {
                                a.keydown().keyup()
                            }, 100)
                        });
                        a.on(""change.mask"", function () {
                            a.data(""changeCalled"", !0)
                        });
                        a.on(""blur.mask"",
                            function (b) {
                                b = g(b.target);
                                b.prop(""defaultValue"") !== b.val() && (b.prop(""defaultValue"", b.val()), b.data(""changeCalled"") || b.");
                WriteLiteral(@"trigger(""change""));
                                b.data(""changeCalled"", !1)
                            });
                        a.on(""focusout.mask"", function () {
                            d.clearIfNotMatch && c.val().length < f.length && c.val("""")
                        })
                    },
                    destroyEvents: function () {
                        a.off(""keydown.mask keyup.mask paste.mask drop.mask change.mask blur.mask focusout.mask"").removeData(""changeCalled"")
                    },
                    val: function (b) {
                        var e = a.is(""input"");
                        return 0 < arguments.length ? e ? a.val(b) : a.text(b) : e ? a.val() : a.text()
                    },
                    getMaskCharactersBeforeCount: function (b,
                        e) {
                        for (var a = 0, c = 0, d = f.length; c < d && c < b; c++) k.translation[f.charAt(c)] || (b = e ? b + 1 : b, a++);
                        return a
       ");
                WriteLiteral(@"             },
                    determineCaretPos: function (b, a, d, h) {
                        return k.translation[f.charAt(Math.min(b - 1, f.length - 1))] ? Math.min(b + d - a - h, d) : c.determineCaretPos(b + 1, a, d, h)
                    },
                    behaviour: function (b) {
                        b = b || window.event;
                        var a = b.keyCode || b.which;
                        if (-1 === g.inArray(a, k.byPassKeys)) {
                            var d = c.getCaret(),
                                f = c.val(),
                                n = f.length,
                                l = d < n,
                                p = c.getMasked(),
                                m = p.length,
                                q = c.getMaskCharactersBeforeCount(m - 1) - c.getMaskCharactersBeforeCount(n - 1);
                            p !== f && c.val(p);
                            !l || 65 === a && b.ctrlKey || (8 !== a && 46 !== a && (d = c.determ");
                WriteLiteral(@"ineCaretPos(d, n, m, q)), c.setCaret(d));
                            return c.callbacks(b)
                        }
                    },
                    getMasked: function (b) {
                        var a = [],
                            g = c.val(),
                            h = 0,
                            n = f.length,
                            l = 0,
                            p = g.length,
                            m = 1,
                            q = ""push"",
                            s = -1,
                            r, u;
                        d.reverse ? (q = ""unshift"", m = -1, r = 0, h = n - 1, l = p - 1, u = function () {
                            return -1 < h && -1 < l
                        }) : (r = n - 1, u = function () {
                            return h < n && l < p
                        });
                        for (; u();) {
                            var v = f.charAt(h),
                                w = g.charAt(l),
     ");
                WriteLiteral(@"                           t = k.translation[v];
                            if (t) w.match(t.pattern) ? (a[q](w), t.recursive && (-1 === s ? s = h : h === r && (h = s - m), r === s && (h -= m)), h += m) : t.optional && (h += m, l -= m), l +=
                                m;
                            else {
                                if (!b) a[q](v);
                                w === v && (l += m);
                                h += m
                            }
                        }
                        b = f.charAt(r);
                        n !== p + 1 || k.translation[b] || a.push(b);
                        return a.join("""")
                    },
                    callbacks: function (b) {
                        var e = c.val(),
                            g = c.val() !== x;
                        if (!0 === g && ""function"" === typeof d.onChange) d.onChange(e, b, a, d);
                        if (!0 === g && ""function"" === typeof d.onKeyPress) d.onKeyPress(");
                WriteLiteral(@"e, b, a, d);
                        if (""function"" === typeof d.onComplete && e.length === f.length) d.onComplete(e, b, a, d)
                    }
                };
                k.remove = function () {
                    var a = c.getCaret(),
                        d = c.getMaskCharactersBeforeCount(a);
                    c.destroyEvents();
                    c.val(k.getCleanVal()).removeAttr(""maxlength"");
                    c.setCaret(a - d)
                };
                k.getCleanVal = function () {
                    return c.getMasked(!0)
                };
                k.init()
            };
            g.fn.mask = function (a, f) {
                //this.unmask();
                return this.each(function () {
                    g(this).data(""mask"", new y(this, a, f))
                })
            };
            //g.fn.unmask = function () {
            //    return this.each(function () {
            //        try {
            //            g(this).data");
                WriteLiteral(@"(""mask"").remove()
            //        } catch (a) { }
            //    })
            //};
            g.fn.cleanVal = function () {
                return g(this).data(""mask"").getCleanVal()
            };
            g(""*[data-mask]"").each(function () {
                var a = g(this),
                    f = {};
                ""true"" === a.attr(""data-mask-reverse"") && (f.reverse = !0);
                ""false"" === a.attr(""data-mask-maxlength"") && (f.maxlength = !1);
                ""true"" === a.attr(""data-mask-clearifnotmatch"") && (f.clearIfNotMatch = !0);
                a.mask(a.attr(""data-mask""), f)
            })
        });

        $('.ip_address').mask('099.099.099.099');
    </script>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(13676, 183, true);
            WriteLiteral("\r\n<script>\r\n     function showipaddress() {\r\n            $(\"#overlay\").show();\r\n            $(\"#overlay\").fadeIn(300);\r\n        $.ajax({\r\n            type: \"POST\",\r\n            url: \'");
            EndContext();
            BeginContext(13860, 56, false);
#line 290 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\setting_device_onoff.cshtml"
             Write(Url.Content("~/csat_settings_page/GetDeviceOnOffStatus"));

#line default
#line hidden
            EndContext();
            BeginContext(13916, 2114, true);
            WriteLiteral(@"',
            success: function (data) {
                var dataclear = document.getElementById('divscanip_div');
                dataclear.innerHTML = '&nbsp;';
                $('#divscanip_div').append(""<table id='divscanip' class='table table-striped table-bordered' width='100%'></table>"");
                dtable = document.getElementById('divscanip');
                var data1 = [];
                for (var i = 0; i < data.dtdeviceonoff.length; i++) { 
                    var status = """";
                    if (data.dtdeviceonoff[i].device_status == 1) {
                        status = ""<span class='badge badge-pill badge-success'>ON</span>"";
                    }
                    else {
                        status = ""<span class='badge badge-pill badge-danger'>OFF</span>""; 
                    }
                    data1.push([data.dtdeviceonoff[i].iP_Address, data.dtdeviceonoff[i].device_timestamp, status ]);
                }
                var dtable = $(""#divscanip"").Data");
            WriteLiteral(@"Table({
                    data: data1, scrollX: !0, searching: !1, dom: ""<'top'lB>rt<'bottom'ip><'clear'>"", buttons: [""colvis""],
                    'columnDefs': [{ orderable: true, targets: 1 }], 'select': { 'style': 'multi' }, 'order': [[1, 'desc']],
                    columns: [{ title: ""IP Address"" }, { title: ""Device Scan Time"" }, { title: ""Status"" }]
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
</script>
<script>
    function validate() {

        if (document.getElementById('ipaddress').value == """") {           
            alert(""Please provide IP Address!"");
            return false;
        }
        else {                     
            Addip();
        }
    }
    function Addip() {
       var ipadd");
            WriteLiteral("= document.getElementById(\'ipaddress\').value;\r\n        var url = \'");
            EndContext();
            BeginContext(16031, 62, false);
#line 336 "C:\Users\USER\Desktop\OwnYITCSAT_V3\Views\csat_settings_page\setting_device_onoff.cshtml"
              Write(Url.Content("~/csat_settings_page/AddDeviceOnOff?ip_address="));

#line default
#line hidden
            EndContext();
            BeginContext(16093, 514, true);
            WriteLiteral(@"' + ipadd;
        $.ajax({
            type: ""POST"",
            url: url,
            success: function (result) {
               // debugger;  
                alert(result);
                $('#show_ip_address').modal('hide');
                document.getElementById('ipaddress').value = """";
                //showipaddress();
                              
            }
            });
    }
    function cleardata() {
        document.getElementById('ipaddress').value = """";
    }
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
