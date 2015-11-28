using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryCSW.Startup))]
namespace LibraryCSW
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
