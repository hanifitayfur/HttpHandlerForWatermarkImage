using Microsoft.Owin;
using Owin;
using WatermarkingImage;

[assembly: OwinStartupAttribute(typeof(WebSampleProject.Startup))]
namespace WebSampleProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

    public class SampleImageHelper : ImageHandler
    {
        public SampleImageHelper()
            : base("My watermark!")
        {
        }
    }
}
