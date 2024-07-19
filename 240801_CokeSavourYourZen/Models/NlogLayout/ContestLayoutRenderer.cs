using NLog;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BaseContest_WebForms.Models.NlogLayout
{
    [LayoutRenderer("contest")]
    public class ContestLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(builder);
        }
    }
}