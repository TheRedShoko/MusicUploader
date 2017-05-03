using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicUploader.Startup))]
namespace MusicUploader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
