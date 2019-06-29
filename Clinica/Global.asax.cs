using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Clinica.Models;

namespace Clinica
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ClinicaDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ClinicaDbContext>());
        }

        protected void Application_EndRequest()
        {
            var context = new HttpContextWrapper(Context);
            
            if (context.Response.StatusCode == 401) 
            {
                context.Response.Redirect("~/Acesso/Logar");
            }
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("pt-BR");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            // there are a lot of other pattern properties in ci.DateTimeFormat you can set 
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}
