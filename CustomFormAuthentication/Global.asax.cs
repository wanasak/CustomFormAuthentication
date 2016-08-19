using CustomFormAuthentication.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace CustomFormAuthentication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_PostAuthenticateRequest()
        {
            HttpCookie authCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookies != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookies.Value);
                JavaScriptSerializer js = new JavaScriptSerializer();
                User user = js.Deserialize<User>(ticket.UserData);
                MyIdentity myIdentity = new MyIdentity(user);
                MyPrincipal myPricipal = new MyPrincipal(myIdentity);
                HttpContext.Current.User = myPricipal;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}