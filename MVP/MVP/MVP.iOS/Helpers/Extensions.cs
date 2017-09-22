using System;
using System.Drawing;
using UIKit;

namespace Microsoft.Mvp.iOS.Helpers
{
    public static class Extensions
    {
        public static UIImage ToUIImage(this UIColor color, int width, int height)
        {
            var imageSize = new SizeF(width, height);
            var imageSizeRectF = new RectangleF(0, 0, width, height);
            UIGraphics.BeginImageContextWithOptions(imageSize, false, 0);
            var context = UIGraphics.GetCurrentContext();

            context.SetFillColor(color.CGColor);
            context.FillRect(imageSizeRectF);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }
    }
}
