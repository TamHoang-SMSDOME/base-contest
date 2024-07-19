using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    public class SubmissionResult
    {
        public BC_240801_COKESAVOURYOURZEN Entry = new BC_240801_COKESAVOURYOURZEN();

        public BC_240801_COKESAVOURYOURZEN_Winners Winner = new BC_240801_COKESAVOURYOURZEN_Winners();

        public Parameters param;
        
        public bool IsSendSMS = true;

        public bool IsSavable;

        public List<string> ListOfReasonsForPossibleFailures = new List<string>();

        public List<string> ListOfResponsesForPossibleFailures = new List<string>();


    }
}