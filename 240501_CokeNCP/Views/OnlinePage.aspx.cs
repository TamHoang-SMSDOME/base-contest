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
using SmsDome.bll;

namespace BaseContest_WebForms.Views
{
    public partial class OnlinePage : System.Web.UI.Page
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
                btnOk.Visible = false;
                Retailer.DataSource = repo.LoadAllRetailers();
                Retailer.DataValueField = "Code";
                Retailer.DataTextField = "Text";
                Retailer.DataBind();
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

            if (Name.Text == "")
            {
                lblModal.Text = "Full Name (As per NRIC) is a required field.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }

            Entry.Name = Name.Text;

            if (MobileNo.Text == "")
            {
                lblModal.Text = "Mobile number (SG only) is a required field.";
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

            //if (Retailer.SelectedValue == "")
            //{
            //    lblModal.Text = "Retailer is a required field.";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //    return;
            //}

            Entry.Retailer = Retailer.SelectedValue;

            Decimal Amt;
            if (Amount.Text == "")
            {
                lblModal.Text = "Amount is a required field.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }

            var AmountCleaned = Amount.Text.Replace("SGD", "").Replace("$", "");

            try
            {
                if (Decimal.TryParse(AmountCleaned, out Amt))
                {
                    //Console.WriteLine(dateTime);
                }
                else
                {
                    lblModal.Text = "Please enter a proper Amount!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                    return;

                }
            }
            catch
            {
                lblModal.Text = "Please enter a proper Amount!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }

            Entry.Amount = Amt;
            var isRetailerHasMinSpend = repo.CheckMinSpendOfRetailer(Retailer.SelectedValue, Amt);
            if(isRetailerHasMinSpend == false)
            {
                lblModal.Text = "Amount is below 5!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;            
            }
            if (ReceiptNo.Text == "")
            {
                lblModal.Text = "Receipt Number is a required field.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }

            Regex rgx = new Regex("[^a-zA-Z0-9]");
            var str = rgx.Replace(ReceiptNo.Text, "");


            Entry.ReceiptNo = str;

            if (Upload.HasFile)
                try
                {
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
                lblModal.Text = "Receipt Image is a required field.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return; ;
            }

            if (!chkTnC.Checked)
            {
                lblModal.Text = "Please confirm that the information submitted is true and accurate.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return; ;
            }
            if (!chkTnC2.Checked)
            {
                lblModal.Text = "Please indicate that you have read and agree to the Terms and Conditions.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return; ;
            }


            //End of Validation

            #endregion
            Logger.Info("Online Page.  Start Submitting Method.");

            var rtn = repo.InsertEntry(Entry, new HttpPostedFileWrapper(Upload.PostedFile));
            lblModal.Text = rtn.message;
            if (rtn.Valid)
            {
                Logger.Info("Online Page.  Submit Entry Successfully!  Finish Submitting Method.");
                btnOk.Visible = true;
                btnCancel.Visible = false;
                GeneralFunctions.SendSms(Convert.ToInt32(repo.AppID), new Guid(repo.AppSecret), Entry.MobileNo.ToString(),
                                  repo.ValidMessage.Replace("{receiptNo}", Entry.ReceiptNo));
            }
            else
            {
                Logger.Error("Online Page.  Submit Entry Failed!  " + rtn.message + "  Finish Submitting Method.", rtn.exception);
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