using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseContest_WebForms.Models
{
    public class InboundMessage
    {
        public DateTime? CreatedOn { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string FileLink { get; set; }
        public string EntrySource { get; set; }
    }
    public class OutboundMessage
    {
        public int ContestId { get; set; }
        public string MobileNo { get; set; }
        public string MessageText { get; set; }
        public string MessageType { get; set; }
    }
}