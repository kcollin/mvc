using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Identity_test_1.Startup))]
namespace Identity_test_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
