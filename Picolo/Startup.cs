using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Picolo.Startup))]
namespace Picolo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
