using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseContest_WebForms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using AjaxControlToolkit;

namespace BaseContest_WebForms.Views
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        Repository repo = Repository.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.AppendHeader("pragma", "no-cache");
            Response.Expires = -1441;
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            if (!Page.IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Views/Login.aspx");
                }

                AdminTestAPI.Visible = false;
                AdminUpload.Visible = false;
                Config.Visible = false;
                Roles.Visible = false;
                TransactionPage.Visible = false;
                ClearResources.Visible = false;
                //AdminManageSettings.Visible = false;

                if (HttpContext.Current.User.IsInRole("Superusers"))
                {
                    AdminTestAPI.Visible = true;
                    AdminUpload.Visible = true;
                    Config.Visible = true;
                    Roles.Visible = true;
                    TransactionPage.Visible = true;
                    ClearResources.Visible = true;
                    // AdminManageSettings.Visible = true;
                }
                else
                {
                    if (DateTime.UtcNow > repo.TerminationDate)
                    {
                        var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                        authenticationManager.SignOut();
                        Response.Redirect("~/Views/Login.aspx");
                    }
                }
            }
        }

        protected void SignOut_Click(object sender, EventArgs e)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Response.Redirect("~/Views/Login.aspx");
        }
    }
}