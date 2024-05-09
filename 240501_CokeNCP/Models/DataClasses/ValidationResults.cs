using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    public class ValidationResult
    {
        public ValidationResult(bool valid)
        {
            IsSuccessful = valid;
        }

        public bool IsSuccessful;

        public string ReasonForFailure;


        public string ResponseForFailure;


    }
}