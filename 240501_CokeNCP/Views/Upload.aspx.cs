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
using BaseContest_WebForms.Models.Repositories.Utility;

namespace BaseContest_WebForms.Views
{
    public partial class Upload : System.Web.UI.Page
    {
        Repository repo = Repository.Instance;
        CustomLogger logger = Repository.Logger;
        //Becoz u donno if some pages will need to have a different PageSize
        static readonly int PageSize = 50;
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

                }
                else
                {
                    Response.Redirect("/Views/Login.aspx");
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            var JsonInfo = new Dictionary<string, object>();

            #region Validation

            //Validation

            if (UploadF.HasFile)
                try
                {
                    //Upload.SaveAs("C:\\Uploads\\" + Upload.FileName);
                    FileSpecs.InnerHtml = "File name: " +
                          UploadF.PostedFile.FileName + "<br>" +
                          UploadF.PostedFile.ContentLength + " kb<br>" +
                          "Content type: " +
                          UploadF.PostedFile.ContentType;

                    JsonInfo.Add("File", new { filename = UploadF.PostedFile.FileName });
                }
                catch (Exception ex)
                {
                    lblModal.Text = "ERROR: " + ex.Message.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                    return;
                }
            else
            {
                lblModal.Text = "You have not specified a file.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return; ;
            }


            //End of Validation

            #endregion

            logger.Info("Upload Page.  Start Upload Entries Method.");

            var rtn = repo.UploadEntries(new HttpPostedFileWrapper(UploadF.PostedFile));
            if (rtn.Valid)
            {
                lblModal.Text = rtn.message;
                logger.Info("Upload Page.  Upload Entries Successfully!  Finish Upload Entries Method.");
            }
            else
            {
                lblModal.Text = rtn.message;
                logger.Error("Upload Page.  Upload Entries Failed!  " + rtn.message + "  Finish Upload Entries Method.", rtn.exception);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            return;

        }

    }
}