using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    public class ViewResult : SubmissionResult
    {
        public int TotalCount;

        public List<Dictionary<string, object>> DataAsDictionary;

        public List<string> DataHeaders;

    }
}