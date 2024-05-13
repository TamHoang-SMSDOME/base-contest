using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseContest_WebForms.Models;
using BaseContest_WebForms.Models.Repositories.Utility;

namespace BaseContest_WebForms.Views
{
    public partial class OnlineCompletion : System.Web.UI.Page
    {
        Repository repo = Repository.Instance;
        CustomLogger Logger = Repository.Logger;

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
                if (Request.QueryString["i"] == null || Request.QueryString["i"] == "") {

                    Response.Redirect("~/Views/ErrorPage.aspx");
                }
                else {
                    var VerificationCode = Request.QueryString["i"];

                    using (var db = new BaseEntities())
                    {
                        var ListOfEntries = db.BC_240501_COKENCP.Where(s => s.VerificationCode == VerificationCode && s.IsValid).ToList();
                        if (ListOfEntries.Count == 0)
                        {
                            Response.Redirect("~/Views/ErrorPage.aspx");
                        }
                        EntryID.Value = ListOfEntries[0].EntryID.ToString();
                        ReceiptNo.Text = ListOfEntries[0].ReceiptNo.ToString();
                        ImageLink.Src = repo.ImageLink;
                    }
                    btnOk.Visible = false;
                }

            
            }
        }

        //protected void Upload_Click(object sender, EventArgs e)
        //{

        //}

        protected void Submit_Click(object sender, EventArgs e)
        {
            var Entry = new OnlineEntry();
            #region Validation

            //Validation

            //if (Name.Text == "")
            //{
            //    lblModal.Text = "Please enter a name!";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //    return;
            //}

            //Entry.Name", Name.Text);

            //if (NRIC.Text == "")
            //{
            //    lblModal.Text = "Please enter a NRIC!";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //    return;
            //}

            //Entry.NRIC", NRIC.Text);

            var EID = Convert.ToInt32(EntryID.Value);
            Entry.EntryID = EID;


            if (MobileNo.Text == "")
            {
                lblModal.Text = "Mobile Number is a required field.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }

            var regex = new Regex("^65[89][0-9]{7}$", RegexOptions.IgnoreCase);
            var mobileNo = Mob.SelectedValue + MobileNo.Text;
            var matchResult = regex.Match(mobileNo);

            if (!matchResult.Success)
            {
                lblModal.Text = "Please enter valid mobile number.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }

            Entry.MobileNo = Mob.SelectedValue + MobileNo.Text;

            //string dateString = DOB.Text;

            //DateTime StartDate;

            //try
            //{
            //    if (DateTime.TryParseExact(dateString,  repo.DateTimeFormat, CultureInfo.InvariantCulture,
            //        DateTimeStyles.None, out StartDate))
            //    {
            //        //Console.WriteLine(dateTime);
            //    }
            //    else
            //    {
            //        lblModal.Text = "Please enter a proper date!";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //        return;

            //    }
            //}
            //catch
            //{
            //    lblModal.Text = "Please enter a proper date!";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //    return;
            //}


            //Entry.DOB", StartDate);

            if (Email.Text == "")
            {
                lblModal.Text = "Email is a required field.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }

            if (!IsValidEmail(Email.Text))
            {
                lblModal.Text = "Please enter valid email.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }
            Entry.Email = Email.Text;

            //if (ReceiptNo.Text == "")
            //{
            //    lblModal.Text = "Please key in a Receipt Number !";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //    return;
            //}

            //Entry.ReceiptNo", ReceiptNo.Text);

            //var AmountCleaned = Amount.Text.Replace("SGD", "").Replace("$", "");

            //try
            //{
            //    if (Decimal.TryParse(AmountCleaned, out Amt))
            //    {
            //        Console.WriteLine(dateTime);
            //    }
            //    else
            //    {
            //        lblModal.Text = "Please enter a proper Amount!";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //        return;

            //    }
            //}
            //catch
            //{
            //    lblModal.Text = "Please enter a proper Amount!";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //    return;
            //}

            //Entry.Amount = Amt;

            if (Upload.HasFile)
                try
                {
                    if (Upload.FileBytes.Length > 10000000)
                    {
                        lblModal.Text = "File size cannot be larger than 10MB";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                        return;
                    }


                    //Upload.SaveAs("C:\\Uploads\\" + Upload.FileName);
                    FileSpecs.InnerHtml = "File name: " +
                          Upload.PostedFile.FileName + "<br>" +
                          Upload.PostedFile.ContentLength + " kb<br>" +
                          "Content type: " +
                          Upload.PostedFile.ContentType;

                    Entry.FileName = Upload.PostedFile.FileName;

                }
                catch (Exception ex)
                {
                    lblModal.Text = "ERROR: " + ex.Message.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                    return;
                }
            else
            {
                lblModal.Text = "You have not specified a file.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return; ;
            }

            //if (!chkTnC.Checked)
            //{
            //    lblModal.Text = "Please indicate that you have read and agree to the Terms and Conditions.";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //    return; ;
            //}

            //End of Validation

            #endregion

            Logger.Info("Online Completion Page.  Start Submitting Method.");
            string rtnstring;
            var rtn = repo.CompleteEntry(Entry, new HttpPostedFileWrapper(Upload.PostedFile), out rtnstring);

            if (rtn.Valid)
            {
                Logger.Info("Online Completion Page.  Updated Entry Successfully!  Finish Submitting Method.");
            }
            else
            {
                Logger.Error("Online Completion Page.  Updated Entry Failed!  " + rtn.message + "  Finish Submitting Method.", rtn.exception);
            }
            lblModal.Text = rtn.exception == null ? rtn.message : rtn.exception.Message;
            if (rtn.Valid)
            {
                btnOk.Visible = true;
                btnCancel.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            return;

        }
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                var hostParts = m.Host.Split('.');
                if (hostParts.Length == 1)
                    return false; // No dot.
                if (hostParts.Any(p => p == string.Empty))
                    return false; // Double dot.
                if (hostParts[1].Length < 2)
                    return false; // TLD only one letter.

                if (m.User.Contains(' '))
                    return false;
                if (m.User.Split('.').Any(p => p == string.Empty))
                    return false;
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


    }
}