using System.Web;

namespace WatermarkingImage
{
    public abstract class ImageHandler : IHttpHandler
    {
        private readonly WatermarkHelper _helper;

        public bool IsReusable
        {
            get { return true; }
        }

        protected ImageHandler(string watermarkText)
        {
            _helper = new WatermarkHelper(watermarkText);
        }

        public void ProcessRequest(HttpContext context)
        {

            var imageWithWatermark = _helper.WatermarkImage(context.Request.Path);
            if (imageWithWatermark.Length == 0)
            {
                return;
            }
            context.Response.OutputStream.Write(imageWithWatermark, 0, imageWithWatermark.Length);
        }

    }
}
