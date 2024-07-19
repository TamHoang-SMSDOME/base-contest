using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    public class Parameters
    {
        public string EntrySource { get; set; }
        public bool SendResponse { get; set; }

        //Standard Params for SMS/MMS
        public string CreatedOn { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string FileLink { get; set; }

        //Additional API Parameters


    }
}