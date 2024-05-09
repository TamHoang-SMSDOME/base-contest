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

namespace BaseContest_WebForms.Views
{
    public partial class Settings : System.Web.UI.Page
    {
        Repository repo = Repository.Instance;
        //Becoz u donno if some pages will need to have a different PageSize
        static readonly int PageSize = 50;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
             
            }
        }

        protected void GetCount_Click(object sender, EventArgs e)
        {
           


            lblModal.Text = "Remaining credits : " + repo.CheckAccountCreditBalance();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
            return;
        }

        protected void ValidEntryResponse_Click(object sender, EventArgs e)
        {

        }

        protected void GenericErrorMessage_Click(object sender, EventArgs e)
        {

        }
    }
}