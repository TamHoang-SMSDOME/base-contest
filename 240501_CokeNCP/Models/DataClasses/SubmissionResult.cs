using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    public class SubmissionResult
    {
        public BC_240501_COKENCP Entry = new BC_240501_COKENCP();

        public BC_240501_COKENCP_Winners Winner = new BC_240501_COKENCP_Winners();

        public Parameters param;
        
        public bool IsSendSMS = true;

        public bool IsSavable;

        public List<string> ListOfReasonsForPossibleFailures = new List<string>();

        public List<string> ListOfResponsesForPossibleFailures = new List<string>();


    }
}