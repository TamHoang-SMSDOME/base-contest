using Microsoft.Owin;
using NLog;
using NLog.Config;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaseContest_WebForms.Startup))]
namespace BaseContest_WebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            
            ConfigureAuth(app);
            GlobalDiagnosticsContext.Set("connectionString", System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("contest", typeof(Models.NlogLayout.ContestLayoutRenderer));
        }
    }
}
