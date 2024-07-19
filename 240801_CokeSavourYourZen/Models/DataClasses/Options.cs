using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    public class Options
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public string ValidOnly;
        public int Page;
        public int PageSize;
        public string WinnerGroupName;
        public string Status;



        public string EntryType;
        public bool ExcludePastNRIC;
        public bool ExcludePastMob;
        public int QuotaToSelect;
        public bool UploadStatus;
    }
}