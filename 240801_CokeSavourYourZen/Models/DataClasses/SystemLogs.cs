using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models.DataClasses
{
    public class SystemLogs
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Level { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Exception { get; set; }
        public string Logger { get; set; }
        public string UserName { get; set; }
        public string Contest { get; set; }
        public string Url { get; set; }

    }
}