using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseContest_WebForms.Models;

namespace BaseContest_WebForms.Views
{
    public partial class Constants : System.Web.UI.Page
    {
        Repository repo = Repository.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                Response.Write(repo.GetConstantValues());
                if (!Page.IsPostBack)
                {
                    if (HttpContext.Current.User.IsInRole("Superusers") == false)
                    {
                        Response.Redirect("~/Views/Login.aspx");
                    }

                }
            }
        }
    }
}