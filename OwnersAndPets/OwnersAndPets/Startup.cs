using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OwnersAndPets.Startup))]
namespace OwnersAndPets
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
