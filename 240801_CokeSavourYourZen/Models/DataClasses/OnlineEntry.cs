using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    public class OnlineEntry : BC_240801_COKESAVOURYOURZEN
    {

        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string FileName { get; set; }

    }

    public class Email 
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MobileNo { get; set; }
    }
    public class Transaction
    {
        public DataTable Table { get; set; }
        public string Header { get; set; }
    }
}