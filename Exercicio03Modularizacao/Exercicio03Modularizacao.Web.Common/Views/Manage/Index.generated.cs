﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Manage/Index.cshtml")]
    public partial class _Views_Manage_Index_cshtml : System.Web.Mvc.WebViewPage<Exercicio03Modularizacao.Common.ViewModels.Manage.IndexViewModel>
    {
        public _Views_Manage_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Manage\Index.cshtml"
  
    ViewBag.Title = "Manage your account";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>");

            
            #line 6 "..\..\Views\Manage\Index.cshtml"
Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(".</h2>\r\n<p");

WriteLiteral(" class=\"text-success\"");

WriteLiteral(">");

            
            #line 7 "..\..\Views\Manage\Index.cshtml"
                   Write(ViewBag.StatusMessage);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-md-8\"");

WriteLiteral(">\r\n        <p>\r\n");

            
            #line 11 "..\..\Views\Manage\Index.cshtml"
            
            
            #line default
            #line hidden
            
            #line 11 "..\..\Views\Manage\Index.cshtml"
             if (Model.HasPassword)
            {
                
            
            #line default
            #line hidden
            
            #line 13 "..\..\Views\Manage\Index.cshtml"
           Write(Html.ActionLink("Change your password", "ChangePassword"));

            
            #line default
            #line hidden
            
            #line 13 "..\..\Views\Manage\Index.cshtml"
                                                                          
            }
            else
            {
                
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\Manage\Index.cshtml"
           Write(Html.ActionLink("Pick a password", "SetPassword"));

            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\Manage\Index.cshtml"
                                                                  
            }

            
            #line default
            #line hidden
WriteLiteral("        </p>\r\n        <p>\r\n            Phone Number: ");

            
            #line 21 "..\..\Views\Manage\Index.cshtml"
                      Write(Model.PhoneNumber ?? "None");

            
            #line default
            #line hidden
WriteLiteral(" [\r\n");

            
            #line 22 "..\..\Views\Manage\Index.cshtml"
            
            
            #line default
            #line hidden
            
            #line 22 "..\..\Views\Manage\Index.cshtml"
             if (Model.PhoneNumber != null)
            {
                
            
            #line default
            #line hidden
            
            #line 24 "..\..\Views\Manage\Index.cshtml"
           Write(Html.ActionLink("Change", "AddPhoneNumber"));

            
            #line default
            #line hidden
            
            #line 24 "..\..\Views\Manage\Index.cshtml"
                                                            

            
            #line default
            #line hidden
WriteLiteral("                ");

WriteLiteral(" &nbsp;|&nbsp;\r\n");

            
            #line 26 "..\..\Views\Manage\Index.cshtml"
                
            
            #line default
            #line hidden
            
            #line 26 "..\..\Views\Manage\Index.cshtml"
           Write(Html.ActionLink("Remove", "RemovePhoneNumber"));

            
            #line default
            #line hidden
            
            #line 26 "..\..\Views\Manage\Index.cshtml"
                                                               
            }
            else
            {
                
            
            #line default
            #line hidden
            
            #line 30 "..\..\Views\Manage\Index.cshtml"
           Write(Html.ActionLink("Add", "AddPhoneNumber"));

            
            #line default
            #line hidden
            
            #line 30 "..\..\Views\Manage\Index.cshtml"
                                                         
            }

            
            #line default
            #line hidden
WriteLiteral("            ]\r\n        </p>\r\n        <p>\r\n            External Logins: ");

            
            #line 35 "..\..\Views\Manage\Index.cshtml"
                        Write(Model.Logins.Count);

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 36 "..\..\Views\Manage\Index.cshtml"
       Write(Html.ActionLink("[Manage]", "ManageLogins"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </p>\r\n");

            
            #line 38 "..\..\Views\Manage\Index.cshtml"
        
            
            #line default
            #line hidden
            
            #line 38 "..\..\Views\Manage\Index.cshtml"
         if (Model.TwoFactor)
        {

            
            #line default
            #line hidden
WriteLiteral("            <form");

WriteLiteral(" method=\"post\"");

WriteLiteral(" action=\"/Manage/DisableTFA\"");

WriteLiteral(">\r\n                <p>\r\n                    Two factor is currently enabled:\r\n   " +
"                 <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Disable\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral("/>\r\n                </p>\r\n            </form>\r\n");

            
            #line 46 "..\..\Views\Manage\Index.cshtml"
        }
        else
        {

            
            #line default
            #line hidden
WriteLiteral("            <form");

WriteLiteral(" method=\"post\"");

WriteLiteral(" action=\"/Manage/EnableTFA\"");

WriteLiteral(">\r\n                <p>\r\n                    Two factor is currently disabled:\r\n  " +
"                  <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Enable\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral("/>\r\n                </p>\r\n            </form>\r\n");

            
            #line 55 "..\..\Views\Manage\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 56 "..\..\Views\Manage\Index.cshtml"
         if (Model.BrowserRemembered)
        {

            
            #line default
            #line hidden
WriteLiteral("            <form");

WriteLiteral(" method=\"post\"");

WriteLiteral(" action=\"/Manage/ForgetBrowser\"");

WriteLiteral(">\r\n                <p>\r\n                    Browser is curently remembered for tw" +
"o factor:\r\n                    <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Forget Browser\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" />\r\n                </p>\r\n            </form>\r\n");

            
            #line 64 "..\..\Views\Manage\Index.cshtml"
        }
        else
        {

            
            #line default
            #line hidden
WriteLiteral("            <form");

WriteLiteral(" method=\"post\"");

WriteLiteral(" action=\"/Manage/RememberBrowser\"");

WriteLiteral(">\r\n                <p>\r\n                    Browser is curently not remembered fo" +
"r two factor:\r\n                    <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Remember Browser\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral("/>\r\n                </p>\r\n            </form>\r\n");

            
            #line 73 "..\..\Views\Manage\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </div>\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591