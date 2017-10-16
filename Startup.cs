using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLsach.Startup))]
namespace QLsach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
