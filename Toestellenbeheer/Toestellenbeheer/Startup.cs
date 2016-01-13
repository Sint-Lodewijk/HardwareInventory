using Microsoft.Owin;
using Owin;
using Toestellenbeheer.Models;

[assembly: OwinStartupAttribute(typeof(Toestellenbeheer.Startup))]
namespace Toestellenbeheer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
