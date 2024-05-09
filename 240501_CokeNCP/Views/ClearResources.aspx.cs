using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using BaseContest_WebForms.Models;
using System.Globalization;
using System.Data;
using SmsDome.bll;
using System.Net.Http;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System.Web.Configuration;

namespace BaseContest_WebForms.Views
{
    public partial class ClearResources : System.Web.UI.Page
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
                if (User.Identity.IsAuthenticated && User.IsInRole("Superusers"))
                {
                    LoadPage();
                }
                else
                {
                    Response.Redirect("/Views/Login.aspx");
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            var rtn = repo.RemoveTable(ddlTables.SelectedValue);
            lblModal.Text = rtn.message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            if (rtn.Valid)
            {

                LoadPage();
            }
            return;
        }

        private void LoadPage() 
        {
            var tables = repo.LoadAllNameOfTables();
            if (tables.Count() > 0)
            {
                var lstNameOfTables = new List<DropDownItemData>();
                lstNameOfTables.Add(new DropDownItemData() { Code = "", Text = "- Select All -" });
                lstNameOfTables.AddRange(tables.ToList());
                ddlTables.DataSource = lstNameOfTables;
                ddlTables.DataValueField = "Code";
                ddlTables.DataTextField = "Text";
                ddlTables.DataBind();
                DontHaveTablesDiv.Visible = false;
                HaveTablesDiv.Visible = true;
            }
            else
            {
                HaveTablesDiv.Visible = false;
                DontHaveTablesDiv.Visible = true;
            }
        }
    }
}