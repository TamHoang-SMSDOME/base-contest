using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    [Serializable]
    public class FunctionResult : ViewResult
    {
        public Exception exception;
        public string message;
        public List<string> files;


        public bool Valid;

        public FunctionResult(bool valid)
        {
            Valid = valid;
        }
        
    }
}