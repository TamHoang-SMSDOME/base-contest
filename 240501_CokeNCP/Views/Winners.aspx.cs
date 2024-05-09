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
    public partial class Winners : System.Web.UI.Page
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
                PurgeDiv.Visible = false;
                ExportDiv.Visible = false;
                PagingDiv.Visible = false;
                LoadedDiv.Visible = false;
                PurgeSel.Visible = false;

                var Result = repo.WinnerGroups();
                Result.Insert(0, "Select All");
                ddlGroupName.DataSource = Result;
                ddlGroupName.DataBind();


            }
        }

        protected void Filter_Click(object sender, EventArgs e)
        {
            //Validate Input
            int integer;

            //Needs to Test
            if (Int32.TryParse(CurrentPage.Text, out integer) == false)
            {
                lblModal.Text = "Please select a proper page!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }

            if (lblTotal.Text != "" &&
                (Int32.TryParse(CurrentPage.Text, out integer) == false ||
                Convert.ToInt32(CurrentPage.Text) < 1 ||
                Convert.ToInt32(CurrentPage.Text) > repo.calculateLastPage(Convert.ToInt32(lblTotal.Text), PageSize)))
            {
                lblModal.Text = "Please select a proper page!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }


            //Based on Options?

            /*User submits a date assuming a Local Date, Therefore gotta convert that into UTC*/
            /*Also means that all the dates coming in from DB will be in UTC and therefore need to be converted to Local Time*/


            #region Dates not applicable for Winners

            //string dateString = startDate.Text;
            //
            //  DateTime StartDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out StartDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            //      return;

            //  }

            //  dateString = endDate.Text;
            //  DateTime EndDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out EndDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            //      return;

            //  }


            //dateString = wstartDate.Text;
            //
            //  DateTime StartDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out StartDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
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
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            //      return;

            //  }

            #endregion


            var Options = new Options()
            {
                WinnerGroupName = ddlGroupName.SelectedValue,
                Page = Convert.ToInt32(CurrentPage.Text),
                PageSize = PageSize,
            };

            logger.Info("Winner Page.  Start Filter Method.");

            var Result = repo.GetWinners(Options);
            if (Result.Valid)
            {
                logger.Info("Winner Page.  Filter Successfully!  Finish Filter Method.");
            }
            else
            {
                logger.Error("Winner Page.  Filter Failed!  " + Result.message + "  Finish Filter Method.", Result.exception);
            }

            if (!Result.Valid)
            {
                lblModal.Text = Result.message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }

            //json = SARepository.SerializerHelper(new Dictionary<string, object>() {
            //        { "Count" , Query.Count() },
            //        { "Entries" , retblock},
            //        { "EntriesHeader" , headers}
            //    });


            lblTotal.Text = Result.TotalCount.ToString();
            lblTotalPages.Text = repo.calculateLastPage(Convert.ToInt32(lblTotal.Text), PageSize).ToString();

            var dt = repo.ListOfDictionaryToDataTable(Result.DataAsDictionary);


            WinnersGV.DataSource = dt; // Paging?
            WinnersGV.DataBind();

            if (User.Identity.IsAuthenticated && User.IsInRole("Superusers"))
            {
                PurgeDiv.Visible = true;
                PurgeSel.Visible = true;
            }
            else
            {
                PurgeDiv.Visible = false;
                PurgeSel.Visible = false;
            }

            PagingDiv.Visible = true;
            LoadedDiv.Visible = true;
            ExportDiv.Visible = true;

        }

        public void ExportToCsv_click(object sender, EventArgs e)
        {
            //Validate Input
            int integer;

            //Needs to Test
            if (Int32.TryParse(CurrentPage.Text, out integer) == false)
            {
                lblModal.Text = "Please select a proper page!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }

            if (lblTotal.Text != "" &&
                (Int32.TryParse(CurrentPage.Text, out integer) == false ||
                Convert.ToInt32(CurrentPage.Text) < 1 ||
                Convert.ToInt32(CurrentPage.Text) > repo.calculateLastPage(Convert.ToInt32(lblTotal.Text), PageSize)))
            {
                lblModal.Text = "Please select a proper page!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
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
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            //      return;

            //  }

            //  dateString = endDate.Text;
            //  DateTime EndDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out EndDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
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
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            //      return;

            //  }


            //  dateString = wendDate.Text;
            //  DateTime EndDate;
            //  if (DateTime.TryParseExact(dateString, repo.DateTiimeFormat, CultureInfo.InvariantCulture,
            //DateTimeStyles.None, out EndDate))
            //  {
            //      //Console.WriteLine(dateTime);
            //  }
            //  else
            //  {
            //      lblModal.Text = "Please enter a proper date!";
            //      ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            //      return;

            //  }

            #endregion

            var Options = new Options()
            {
                WinnerGroupName = ddlGroupName.SelectedValue,
            };

            logger.Info("Winner Page.  Start Export Winners To CSV Method.");

            var Result = repo.GetWinnersCSV(Options);
            if (Result.Valid)
            {
                logger.Info("Winner Page.  Export Winners To CSV Successfully!  Finish Export Winner To CSV Method.");
            }
            else
            {
                logger.Error("Winner Page.  Export Winners To CSV Failed!  " + Result.message + "  Finish Export Winner To CSV Method.", Result.exception);
            }

            if (!Result.Valid)
            {
                lblModal.Text = Result.message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }

            //json = SARepository.SerializerHelper(new Dictionary<string, object>() {
            //        { "Count" , Query.Count() },
            //        { "Entries" , retblock},
            //        { "EntriesHeader" , headers}
            //    });

            var dt = repo.ListOfDictionaryToDataTable(Result.DataAsDictionary);

            string headers = string.Join(",", Result.DataHeaders.ToArray());

            GeneralFunctions.ExportToCsv(dt, headers, "winners");

        }

        protected void FirstPage_Click(object sender, EventArgs e)
        {
            CurrentPage.Text = (1).ToString();
            Filter_Click(sender, e);
        }

        protected void PreviousPage_Click(object sender, EventArgs e)
        {
            int integer;
            if (Int32.TryParse(CurrentPage.Text, out integer) == true && Convert.ToInt32(CurrentPage.Text) > 1)
            {
                CurrentPage.Text = (Convert.ToInt32(CurrentPage.Text) - 1).ToString();
                Filter_Click(sender, e);
            }
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            int integer;
            if (Int32.TryParse(CurrentPage.Text, out integer) == true && Convert.ToInt32(CurrentPage.Text) < repo.calculateLastPage(Convert.ToInt32(lblTotal.Text), PageSize))
            {
                CurrentPage.Text = (Convert.ToInt32(CurrentPage.Text) + 1).ToString();
                Filter_Click(sender, e);
            }
        }

        protected void LastPage_Click(object sender, EventArgs e)
        {
            CurrentPage.Text = repo.calculateLastPage(Convert.ToInt32(lblTotal.Text), PageSize).ToString();
            Filter_Click(sender, e);
        }

        protected void PurgeSelected_Click(object sender, EventArgs e)
        {
            var DelIDs = new List<int>();
            foreach (GridViewRow grow in WinnersGV.Rows)
            {
                //Searching CheckBox("chkDel") in an individual row of Grid  
                CheckBox chkdel = (CheckBox)grow.FindControl("Tick");
                //If CheckBox is checked than delete the record with particular empid  
                if (chkdel.Checked)
                {

                    int empid = Convert.ToInt32(grow.Cells[1].Text);
                    DelIDs.Add(empid);
                }
            }

            logger.Info("Winner Page.  Start Purge Selected Entries Method.");

            var result = repo.PurgeSelectedWinners(DelIDs);
            if (result.Valid)
            {
                logger.Info("Winner Page.  Purge Selected Entries Successfully!  Finish Purge Selected Entries Method.");
            }
            else
            {
                logger.Error("Winner Page.  Purge Selected Entries Failed!  " + result.message + "  Finish Purge Selected Entries Method.", result.exception);
            }


            Response.Redirect(Request.RawUrl);
        }

        protected void Purge_Click(object sender, EventArgs e)
        {
            logger.Info("Winner Page.  Start Purge All Winners Method.");

            var result = repo.PurgeWinner();
            if (result.Valid)
            {
                logger.Info("Winner Page.  Purge Alls Winners Successfully!  Finish Purge All Winners Method.");
            }
            else
            {
                logger.Error("Winner Page.  Purge All Winners Failed!  " + result.message + "  Finish Purge All Winners Method.", result.exception);
            }

            Response.Redirect(Request.RawUrl);
        }


    }
}