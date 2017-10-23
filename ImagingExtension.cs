#region

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

#endregion

namespace ToolBox {
    public static class ImagingExtension {
        public static ImageFormat ToImageFormat(this string obj) {
            if (obj.IsNullOrWhiteSpace()) return null;
            if (obj.ToLower() == "bmp") return ImageFormat.Bmp;
            if (obj.ToLower() == "emf") return ImageFormat.Emf;
            if (obj.ToLower() == "exif") return ImageFormat.Exif;
            if (obj.ToLower() == "gif") return ImageFormat.Gif;
            if (obj.ToLower() == "icon") return ImageFormat.Icon;
            if (obj.ToLower() == "jpeg") return ImageFormat.Jpeg;
            if (obj.ToLower() == "jpg") return ImageFormat.Jpeg;
            if (obj.ToLower() == "png") return ImageFormat.Png;
            if (obj.ToLower() == "tiff") return ImageFormat.Tiff;
            if (obj.ToLower() == "wmf") return ImageFormat.Wmf;
            return null;
        }

        public static Size ToDrawingSize(this string obj) {
            var s = new Size(0,0);
            if (obj.IsNullOrWhiteSpace()) return s;
            var p = obj.Split(',');
            if (p.Length != 2 || !p[0].IsNumeric() || !p[1].IsNumeric()) return s;
            return new Size(p[0].ToInteger(), p[1].ToInteger());
        }

        public static Image ResizeImage(this Image image, int width, int height) {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
