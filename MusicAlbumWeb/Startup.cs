using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicAlbumWeb.Startup))]
namespace MusicAlbumWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
