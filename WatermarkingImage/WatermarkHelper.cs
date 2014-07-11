using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WatermarkingImage
{
    public class WatermarkHelper
    {
        private readonly string _watermarkText;

        public WatermarkHelper(string watermarkText)
        {
            _watermarkText = watermarkText;
        }

        public byte[] WatermarkImage(string path)
        {
            var imageBytes = new byte[0];

            if (!File.Exists(path))
            {
                return imageBytes;
            }

            using (Image image = Image.FromFile(path))
            {
                var bmp = new Bitmap(image);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawImage(image, 0, 0, image.Width, image.Height);

                    var font = new Font("Calibri", 15);
                    var brush = new SolidBrush(Color.OrangeRed);

                    var pointF = new PointF
                    {
                        X = 160,
                        Y = 60
                    };

                    graphics.DrawString(_watermarkText, font, brush, pointF);
                }
                imageBytes = SaveImageInMemory(image);
            }
            return imageBytes;
        }

        private byte[] SaveImageInMemory(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Jpeg);
                byte[] imageBytes = memoryStream.ToArray();
                return imageBytes;
            }
        }
    }
}
