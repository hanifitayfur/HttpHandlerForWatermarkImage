using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public class ImageHandler : IHttpHandler
{
    private string WatermarkText = "Cronom";
   
    
    Image image;
    Graphics graphics;
    byte[] imageBytes ;

    private byte[] WatermarkImageWithHandler(HttpContext context)
    {
       
        if (!File.Exists(context.Request.PhysicalPath))
        {
            return imageBytes;
        }
        this.image = Image.FromFile(context.Request.PhysicalPath);
        var bmp = new Bitmap(this.image);
        this.graphics = Graphics.FromImage(bmp);

        this.graphics.DrawImage(this.image, 0, 0, this.image.Width, this.image.Height);
        this.image = bmp;

        var font = new Font("Calibri", 15);
        var brush = new SolidBrush(Color.OrangeRed);

        var pointF = new PointF()
                         {
                             X = 160, Y = 60
                         };

        this.graphics.DrawString(WatermarkText, font, brush, pointF);

        imageBytes = this.SaveImageInMemory();
        return imageBytes;
    }

    
    private byte[] SaveImageInMemory()
    {
        using (var memoryStream = new MemoryStream())
        {
            this.image.Save(memoryStream, ImageFormat.Jpeg);
            imageBytes = memoryStream.ToArray();
        }

        return imageBytes;
    }


    public void ProcessRequest(HttpContext context)
    {

        var imageWithWatermark = this.WatermarkImageWithHandler(context);
        if (imageWithWatermark.Length<0)
        {
            //Resim yok
            return;
        }
        context.Response.OutputStream.Write(imageWithWatermark, 0, imageWithWatermark.Length);
    }

    public bool IsReusable { get; private set; }
}
