using BaseContest_WebForms.Models;
using BaseContest_WebForms.Models.Repositories.Utility;
using SmsDome.bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BaseContest_WebForms.Views
{
    public partial class TransactionPage : System.Web.UI.Page
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
                    startDate.Text = repo.FromUTCToLocal(repo.StartDate).ToString(repo.DateTimeFormat, CultureInfo.InvariantCulture);
                    endDate.Text = repo.FromUTCToLocal(repo.EndDate).ToString(repo.DateTimeFormat, CultureInfo.InvariantCulture);
                }
                else
                {
                    Response.Redirect("/Views/Login.aspx");
                }
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


            /*User submits a date assuming a Local Date, Therefore gotta convert that into UTC*/
            /*Also means that all the dates coming in from DB will be in UTC and therefore need to be converted to Local Time*/

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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                    return;

                }
            }
            catch
            {
                lblModal.Text = "Please enter a proper date!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                    return;

                }

            }
            catch
            {
                lblModal.Text = "Please enter a proper date!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;

            }


            var Options = new Options()
            {
                StartDate = StartDate.AddHours(repo.SubtractLocalTimeZone),
                EndDate = EndDate.AddHours(repo.SubtractLocalTimeZone),
                Page = Convert.ToInt32(CurrentPage.Text),
                PageSize = PageSize,
            };

            logger.Info("Transaction Page.  Start Filter Method.");

            var Result = repo.GetTransactions(Options);
            if (Result.Valid)
            {
                logger.Info("Transaction Page.  Filter Successfully!  Finish Filter Method.");
            }
            else
            {
                logger.Error("Transaction Page.  Filter Failed!  " + Result.message + "  Finish Filter Method.", Result.exception);
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
            smsTotal.InnerText = repo.CountNumberTransactions("SMS", Options).ToString();
            emailTotal.InnerText = repo.CountNumberTransactions("Email", Options).ToString();
            whatsappTotal.InnerText = repo.CountNumberTransactions("Whatsapp", Options).ToString();

            lblTotal.Text = Result.TotalCount.ToString();
            lblTotalPages.Text = repo.calculateLastPage(Convert.ToInt32(lblTotal.Text), PageSize).ToString();

            var dt = repo.ListOfDictionaryToDataTable(Result.DataAsDictionary);

            EntriesGV.DataSource = dt; // Paging?
            EntriesGV.DataKeyNames = Result.DataHeaders.ToArray();
            EntriesGV.DataBind();

            ExportDiv.Visible = true;
            PagingDiv.Visible = true;
            LoadedDiv.Visible = true;

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

            string dateString = startDate.Text;

            DateTime StartDate;
            if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
          DateTimeStyles.None, out StartDate))
            {
                //Console.WriteLine(dateTime);
            }
            else
            {
                lblModal.Text = "Please enter a proper date!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;

            }

            dateString = endDate.Text;
            DateTime EndDate;
            if (DateTime.TryParseExact(dateString, repo.DateTimeFormat, CultureInfo.InvariantCulture,
          DateTimeStyles.None, out EndDate))
            {
                //Console.WriteLine(dateTime);
            }
            else
            {
                lblModal.Text = "Please enter a proper date!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;

            }



            var Options = new Options()
            {
                StartDate = StartDate.AddHours(repo.SubtractLocalTimeZone),
                EndDate = EndDate.AddHours(repo.SubtractLocalTimeZone),
            };

            var smsTotal = repo.CountNumberTransactions("SMS", Options).ToString();
            var emailTotal = repo.CountNumberTransactions("Email", Options).ToString();
            var whatsappTotal = repo.CountNumberTransactions("Whatsapp", Options).ToString();

            var dtHead = new DataTable("dtHead");
            dtHead.Columns.Add("SMS", typeof(string));
            dtHead.Columns.Add("Email", typeof(string));
            dtHead.Columns.Add("Whatsapp", typeof(string));
            DataRow row = dtHead.NewRow();
            row["SMS"] = smsTotal.ToString();
            row["Email"] = emailTotal.ToString();
            row["Whatsapp"] = whatsappTotal.ToString();
            dtHead.Rows.Add(row);

            var Result = repo.GetTransactions(Options, true);

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

            string headers = string.Join(",", Result.DataHeaders.Where(s => !s.Contains("LogID")).ToArray());

            var listTrans = new List<Transaction>() {
                new Transaction{
                    Table = dtHead,
                    Header =
                    string.Join(",", new string[]{
                    "No of SMS credits used","No of Email credits used","No of Whatsapp credits used"
                    })
                 },
                new Transaction{
                    Table = dt,
                    Header = headers
                }
            };

            logger.Info("Transaction Page.  Start Export Transaction To CSV Method.");

            GeneralFunctions.ExportToCsvHandler(listTrans, "transactions");
            if (Result.Valid)
            {
                logger.Info("Transaction Page.  Export Transaction To CSV Successfully!  Finish Export Transaction To CSV Method.");
            }
            else
            {
                logger.Error("Transaction Page.  Export Transaction To CSV Failed!  " + Result.message + "  Finish Export Transaction To CSV Method.", Result.exception);
            }

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
    }
}