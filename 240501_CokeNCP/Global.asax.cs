using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Management;
using System.Web.Configuration;
using System.Configuration;

namespace BaseContest_WebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e)
        {

            Exception exc = Server.GetLastError();
            // Handle HTTP errors
            if (exc.GetType() == typeof(System.Web.HttpUnhandledException))
            {
                if (/*exc.Message.Contains("Maximum request length exceeded") ||*/
                    exc.InnerException.Message.Contains("Maximum request length exceeded"))
                {

                    int maxRequestLength = 0;
                    HttpRuntimeSection section =
                    ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                    //section should never be null, since its able to exceed a request length.
                    if (section != null)
                    {
                        maxRequestLength = section.MaxRequestLength;
                    }

                    Response.Write("File cannot exceed " + (maxRequestLength / 1000).ToString() + "MB");
                    // Clear the error from the server
                    Server.ClearError();
                }

            }

            //var ex = Server.GetLastError();
            //var httpException = ex as HttpException ?? ex.InnerException as HttpException;
            //if (httpException == null) return;

            //if (httpException.WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge)
            //{
            //    //handle the error
            //    Response.Write("File cannot exceed 10MB");
            //}

        }
    }
}