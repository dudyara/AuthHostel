using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthHostel.Startup))]
namespace AuthHostel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
