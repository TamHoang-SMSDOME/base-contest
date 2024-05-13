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

namespace BaseContest_WebForms.Views
{
    public partial class TestAPI : System.Web.UI.Page
    {
        Repository repo = Repository.Instance;


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

                MobileNo.Text = repo.DefaultMobileNo;
                EntryText.Text = repo.Keyword + repo.DefaultTestMessage;
                subDate.Text = System.DateTime.UtcNow.AddHours(repo.AddLocalTimeZone).ToString(repo.DateTimeFormat, CultureInfo.InvariantCulture);

                if (User.Identity.IsAuthenticated && User.IsInRole("Superusers"))
                {

                }
                else
                {
                    Response.Redirect("/Views/Login.aspx");
                }
            }
        }

        protected void Submit_click(object sender, EventArgs e)
        {
            if (MobileNo.Text == "")
            {
                lblModal.Text = "Please enter a proper mobile no!";
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }
            if (EntryText.Text == "")
            {
                lblModal.Text = "Please enter a proper entry text!";
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }
            if (subDate.Text == "")
            {
                lblModal.Text = "Please select a proper date!";
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }


            //Based on Options?

            var json = new Dictionary<string, object>();

            /*User submits a date assuming a Local Date, Therefore gotta convert that into UTC*/
            /*Also means that all the dates coming in from DB will be in UTC and therefore need to be converted to Local Time*/

            string dateString = subDate.Text;
          
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

           var url = new System.Uri(Page.Request.Url, ResolveClientUrl("~")).AbsoluteUri;

            //http://smsdome-contest-base-sea-web-prd-development.azurewebsites.net/api/Rest?CreatedOn=2016-01-01&MobileNo=88690652&Message=BASE%20XXXXX&FIleLink=

            var uri = url + "REST/Add/?CreatedOn=" + Server.UrlEncode((repo.FromLocalToUTC(StartDate)).ToString("yyyy-MM-dd HH:mm:ss")) + "&MobileNo=" + Server.UrlEncode(MobileNo.Text) + "&Message=" + Server.UrlEncode(EntryText.Text) + "&FileLink=";

            using (var client = new HttpClient())
            {
                var response =
                    client.GetAsync(uri).Result;
                var code = response.StatusCode;

                //On success
                lblModal.Text = code.ToString();
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;

            }
            
        }
        
    }
}