using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NerdDinner.Startup))]
namespace NerdDinner
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
