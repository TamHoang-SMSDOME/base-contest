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
using BaseContest_WebForms.Models.Repositories.Utility;

namespace BaseContest_WebForms.Views
{
    public partial class PickWinners : System.Web.UI.Page
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
                startDate.Text = repo.FromUTCToLocal(repo.StartDate).ToString(repo.DateTimeFormat, CultureInfo.InvariantCulture);
                endDate.Text = repo.FromUTCToLocal(repo.EndDate).ToString(repo.DateTimeFormat, CultureInfo.InvariantCulture);
                LoadedDiv.Visible = false;

            }
        }

        protected void Filter_Click(object sender, EventArgs e)
        {

            //Validate Input
            int integer;

            //Needs to Test
            if (Int32.TryParse(NoOfRecords.Text, out integer) == false || Convert.ToInt32(NoOfRecords.Text) < 1)
            {
                lblModal.Text = "Please input the number of winners you wish to select!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }


            if (GroupName.Text == "")
            {
                lblModal.Text = "Please input a valid groupname for the records you wish to select!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }



            //Based on Options?
            
            /*User submits a date assuming a Local Date, Therefore gotta convert that into UTC*/
            /*Also means that all the dates coming in from DB will be in UTC and therefore need to be converted to Local Time*/


            #region Dates not applicable for Winners

            //string dateString = startDate.Text;
            //
            //  DateTime StartDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTiimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out StartDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //      return;

            //  }

            //  dateString = endDate.Text;
            //  DateTime EndDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTiimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out EndDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //      return;

            //  }


            //dateString = wstartDate.Text;
            //
            //  DateTime StartDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTiimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out StartDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //      return;

            //  }


            //  dateString = wendDate.Text;
            //  DateTime EndDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out EndDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
            //      return;

            //  }

            #endregion


            //var sDate = Convert.ToDateTime(Options["StartDate"]);
            //var eDate = Convert.ToDateTime(Options["EndDate"]);
            //var EntryType = Options["EntryType"].ToString().ToUpper();
            //var ResToSel = Convert.ToInt32(Options["NoToSelect"]);

            string dateString = startDate.Text;
          
            DateTime StartDate;
            try
            {
                if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
         DateTimeStyles.None, out StartDate))
                {
                    //Console.WriteLine(dateTime);
                }
                else
                {
                    lblModal.Text = "Please enter a proper date!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                    return;

                }
            }
            catch
            {
                lblModal.Text = "Please enter a proper date!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }


            dateString = endDate.Text;
            DateTime EndDate;

            try
            {

                if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
              DateTimeStyles.None, out EndDate))
                {
                    //Console.WriteLine(dateTime);
                }
                else
                {
                    lblModal.Text = "Please enter a proper date!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                    return;

                }
            }
            catch
            {
                lblModal.Text = "Please enter a proper date!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }


            var Options = new Options()
            {
                StartDate = StartDate.AddHours(repo.SubtractLocalTimeZone),
                EndDate = EndDate.AddHours(repo.SubtractLocalTimeZone),
                //{ "WStartDate",  wStartDate.AddHours(-SARepository.LocalTimeZone)},
                //{ "WEndDate",   wEndDate.AddHours(- SARepository.LocalTimeZone)},
                EntryType =  ddlEntryType.SelectedValue ,
                ExcludePastNRIC = ExcludePastNRIC.Checked ,
                ExcludePastMob = ExcludePastMob.Checked,
                QuotaToSelect = Convert.ToInt32(NoOfRecords.Text) ,
                WinnerGroupName = GroupName.Text ,
                UploadStatus = cbUploadStatus.Checked,
            };

            logger.Info("Pick Winner Page.  Start Pick Winner Method.");

            var Result = repo.PickWinner(Options);
            if (Result.Valid)
            {
                logger.Info("Pick Winner Page.  Pick Winner Successfully!  Finish Pick Winner Method.");
            }
            else
            {
                logger.Error("Pick Winner Page.  Pick Winner Failed!  " + Result.message + "  Finish Pick Winner Method.", Result.exception);
            }

            if (!Result.Valid)
            {
                lblModal.Text = Result.message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }
            
            var dt = repo.ListOfDictionaryToDataTable(Result.DataAsDictionary);

            WinnersGV.DataSource = dt; // Paging?
            WinnersGV.DataBind();

            LoadedDiv.Visible = true;

        }
    }
}