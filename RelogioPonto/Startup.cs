using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RelogioPonto.Startup))]
namespace RelogioPonto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
