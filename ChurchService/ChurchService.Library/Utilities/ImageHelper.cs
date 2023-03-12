using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using ChurchService.Library.Models.Users;

namespace ChurchService.Library.Utilities
{
    public static class ImageHelper
    {










        ///// <summary>
        /////     Creates a bitmap object from the given image data url.
        ///// </summary>
        ///// 
        ///// <returns>
        /////     Returns the bitmap image.
        ///// </returns>
        ///// 
        ///// <param name="dataUrl">The image's data url.</param>
        ///// 
        //public static (byte[] binaryData, string mime) DataUrlToImage(string dataUrl)
        //{
        //    var regdata = Regex.Match(dataUrl, @"data:(?<mime>.+?);base64,(?<data>.+)");
        //    var base64Data = regdata.Groups["data"].Value;
        //    var binData = Convert.FromBase64String(base64Data);

        //    return (binData, regdata.Groups["mime"].Value);
        //}










        ///// <summary>
        /////     Compresses the given image using the Magick image library.
        ///// </summary>
        ///// 
        ///// <returns>
        /////     Returns the image data and mime type.
        ///// </returns>
        ///// 
        ///// <param name="image">The Image to compress.</param>
        ///// <param name="format">The image format.</param>
        ///// <param name="quality">The image quality to preserve. Default 85.</param>
        ///// 
        //public static (byte[] bytes, string mime) MagickCompressImage(Image image, ImageMagick.MagickFormat format, int quality = 85)
        //{
        //    using (var bmp = new Bitmap(image))
        //    using (var imageStream = new MemoryStream())
        //    {
        //        bmp.Save(imageStream, ImageFormat.Bmp);

        //        imageStream.Seek(0, SeekOrigin.Begin);

        //        var mi = new ImageMagick.MagickImage(imageStream);

        //        using (var ms = new MemoryStream())
        //        {
        //            mi.Format = format;
        //            mi.Quality = quality;
        //            mi.Strip();
        //            mi.Write(ms);

        //            ms.Seek(0, SeekOrigin.Begin);

        //            return (ms.ToArray(), mi.FormatInfo.MimeType);
        //        }

        //    }
        //}










        ///// <summary>
        /////     Compresses the given image using the Magick image library.
        ///// </summary>
        ///// 
        ///// <returns>
        /////     Returns the image data and mime type.
        ///// </returns>
        ///// 
        ///// <param name="image">The Image to compress.</param>
        ///// <param name="format">The image format.</param>
        ///// 
        //public static (byte[] bytes, string mime) MagickCompressImage(Image image, ImageFormat format = null)
        //{
        //    var codecInfo = GetEncoder(format ?? image.RawFormat);

        //    if (codecInfo == null)
        //    {
        //        codecInfo = GetEncoder(ImageFormat.Png);
        //        format = ImageFormat.Png;
        //    }
        //    else
        //    {
        //        format = format ?? image.RawFormat;
        //    }

        //    using (var bmp = new Bitmap(image))
        //    using (var ms = new MemoryStream())
        //    {
        //        bmp.Save(ms, format);

        //        ms.Seek(0, SeekOrigin.Begin);

        //        var optimizer = new ImageMagick.ImageOptimizer();
        //        optimizer.OptimalCompression = true;
        //        optimizer.Compress(ms);

        //        ms.Seek(0, SeekOrigin.Begin);

        //        return (ms.ToArray(), codecInfo.MimeType);
        //    }

        //}










        ///// <summary>
        /////     Resizes the given Image.
        ///// </summary>
        ///// 
        ///// <returns>
        /////     Returns the resized Image.
        ///// </returns>
        ///// 
        ///// <remarks>
        /////     If only the width or only the height is provided, then the 
        /////     aspect ratio of the original image is preserved.
        ///// </remarks>
        ///// 
        ///// <param name="image">The Image to resize.</param>
        ///// <param name="width">The width of the resized Image.</param>
        ///// <param name="height">The height of the resized Image.</param>
        ///// 
        //public static Image ResizeImage(Image image, int width, int? height = null)
        //{
        //    var ratio = image.Height / (double)image.Width;
        //    Size targetSize;
        //    if (height.HasValue)
        //        targetSize = new Size(width, height.Value);
        //    else
        //        targetSize = new Size(width, (int)Math.Round(width * ratio));
        //    var target = new Bitmap(targetSize.Width, targetSize.Height);
        //    using (var gfx = Graphics.FromImage(target))
        //    {
        //        gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        gfx.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
        //        gfx.DrawImage(image, 0, 0, targetSize.Width, targetSize.Height);
        //    }

        //    return target;
        //}










        ///// <summary>
        /////     Compresses the given Image using the System.Drawing library.
        ///// </summary>
        ///// 
        ///// <returns>
        /////     Returns the image data and mime type.
        ///// </returns>
        ///// 
        ///// <param name="image">The original Image.</param>
        ///// <param name="quality">The desired quality.</param>
        ///// <param name="format">The Image Format.</param>
        ///// 
        //public static (byte[] bytes, string mime) CompressImage(Image image, int quality, ImageFormat format = null)
        //{
        //    var codecInfo = GetEncoder(format ?? ImageFormat.Jpeg);
        //    var qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
        //    var myEncoderParameters = new EncoderParameters(1);
        //    var myEncoderParameter = new EncoderParameter(qualityEncoder, quality);
        //    myEncoderParameters.Param[0] = myEncoderParameter;

        //    using (var bmp = new Bitmap(image))
        //    using (var ms = new MemoryStream())
        //    {
        //        bmp.Save(ms, codecInfo, myEncoderParameters);

        //        ms.Seek(0, SeekOrigin.Begin);

        //        return (ms.ToArray(), codecInfo.MimeType);
        //    }
        //}










        ///// <summary>
        /////     Returns an image encoder for the specified mime type.
        ///// </summary>
        ///// 
        ///// <param name="MimeType">The mime type of the image encoder.</param>
        ///// 
        //private static ImageCodecInfo GetEncoder(string MimeType)
        //{
        //    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        //    foreach (ImageCodecInfo codec in codecs)
        //    {
        //        if (codec.MimeType == MimeType)
        //        {
        //            return codec;
        //        }
        //    }
        //    return null;
        //}










        ///// <summary>
        /////     Returns an image encoder for the specified ImageFormat.
        ///// </summary>
        ///// 
        ///// <param name="format">The ImageFormat of the image encoder.</param>
        ///// 
        //private static ImageCodecInfo GetEncoder(ImageFormat format)
        //{
        //    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        //    foreach (ImageCodecInfo codec in codecs)
        //    {
        //        if (codec.FormatID == format.Guid)
        //        {
        //            return codec;
        //        }
        //    }
        //    return null;
        //}









       
        }
    }

