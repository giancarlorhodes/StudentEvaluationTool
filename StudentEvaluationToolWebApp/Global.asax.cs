using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StudentEvaluationToolWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        // STEP 1 - add this method to the MvcApplication class
        // STEP 2 - you will need to System.Security.Principal namespaace to resolve the below code
        // STEP 3 - comment this code line by line to make it your own and show your understanding of it
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // getting these two objects for session and creating local variables
            // it is possible these could be null
            string UserName = Session["AUTHUserName"] as string;
            string Sessroles = Session["AUTHRoles"] as string;

            // this will end this method if the string in empty or null
            if (string.IsNullOrEmpty(UserName))
            {
                return;
            }

            // An identity object represents the user on whose behalf the code is running.
            GenericIdentity i = new GenericIdentity(UserName, "MyCustomType");

            if (Sessroles == null) { Sessroles = ""; }
            string[] roles = Sessroles.Split(',');

            // This class represents the roles of the current user - https://docs.microsoft.com/en-us/dotnet/api/system.security.principal.genericprincipal?view=netframework-4.8
            // Applications that use Forms authentication will often want to use the GenericPrincipal
            // class (in conjunction with the FormsIdentity class), to create a non-Windows specific 
            // authorization scheme, independent of a Windows domain.
            GenericPrincipal principal = new GenericPrincipal(i, roles);



            // The HttpContext.Current.User.Identity property (in ASP.NET web app) is populated based on the user identity authenticated at IIS layer. 
            // For example, if you configure IIS to use windows authentication, this property contains the authenticated windows user account from client.
            HttpContext.Current.User = principal;
        }



    }
}
