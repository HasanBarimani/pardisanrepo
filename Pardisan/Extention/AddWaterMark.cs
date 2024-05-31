using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Pardisan.Extention
{
    public static class AddWatermark
    {

        public static void AddWatermarkToImage(IFormFile file, string watermarkText)
        {
            using (var image = Image.FromStream(file.OpenReadStream()))
            using (var graphics = Graphics.FromImage(image))
            {
                // Define the font and brush for the watermark text 
                var font = new Font("Arial", 12, FontStyle.Bold);
                var brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255)); // Semi-transparent white 

                // Measure the size of the watermark text 
                var textSize = graphics.MeasureString(watermarkText, font);

                // Calculate the position to place the watermark (bottom-right corner with a 10px margin) 
                var x = image.Width - (int)textSize.Width - 10;
                var y = image.Height - (int)textSize.Height - 10;

                // Draw the watermark text on the image 
                graphics.DrawString(watermarkText, font, brush, x, y);

                // Save the modified image to the same stream 
                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, ImageFormat.Jpeg);
                    outputStream.Seek(0, SeekOrigin.Begin);

                    // Copy the modified image back to the original stream 
                    file.OpenReadStream().SetLength(0);
                    outputStream.CopyTo(file.OpenReadStream());
                }
            }
        }
        public static void Compressimage(string targetPath, String filename, Byte[] byteArrayIn)
        {
            try
            {
                System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                using (MemoryStream memstr = new MemoryStream(byteArrayIn))
                {
                    using (var image = Image.FromStream(memstr))
                    {
                        float maxHeight = 900.0f;
                        float maxWidth = 900.0f;
                        int newWidth;
                        int newHeight;
                        string extension;
                        Bitmap originalBMP = new Bitmap(memstr);
                        int originalWidth = originalBMP.Width;
                        int originalHeight = originalBMP.Height;

                        if (originalWidth > maxWidth || originalHeight > maxHeight)
                        {

                            // To preserve the aspect ratio  
                            float ratioX = (float)maxWidth / (float)originalWidth;
                            float ratioY = (float)maxHeight / (float)originalHeight;
                            float ratio = Math.Min(ratioX, ratioY);
                            newWidth = (int)(originalWidth * ratio);
                            newHeight = (int)(originalHeight * ratio);
                        }
                        else
                        {
                            newWidth = (int)originalWidth;
                            newHeight = (int)originalHeight;

                        }
                        Bitmap bitMAP1 = new Bitmap(originalBMP, newWidth, newHeight);
                        Graphics imgGraph = Graphics.FromImage(bitMAP1);
                        extension = Path.GetExtension(targetPath);
                        if (extension.ToLower() == ".png" || extension.ToLower() == ".gif" || extension.ToLower() == ".jpeg")
                        {
                            imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                            imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                            bitMAP1.Save(targetPath, image.RawFormat);
                            bitMAP1.Dispose();
                            imgGraph.Dispose();
                            originalBMP.Dispose();
                        }
                        else if (extension.ToLower() == ".jpg")
                        {
                            imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                            imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            bitMAP1.Save(targetPath, jpgEncoder, myEncoderParameters);

                            bitMAP1.Dispose();
                            imgGraph.Dispose();
                            originalBMP.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exc = ex.Message;
                throw;

            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
