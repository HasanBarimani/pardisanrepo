using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Globalization;
using System.IO;

namespace Pardisan.Extensions
{
    public static class PublicHelper
    {
        public static string FilterUrl(string Url)
        {
            var m = Url.Replace(" ", "_");
            m = m.Replace("‌", "_");
            m = m.Replace("@", "_");
            m = m.Replace("!", "_");
            m = m.Replace("$", "_");
            m = m.Replace("%", "_");
            m = m.Replace("^", "_");
            m = m.Replace("&", "_");
            m = m.Replace("*", "_");
            m = m.Replace("(", "_");
            m = m.Replace(")", "_");
            m = m.Replace("+", "_");
            m = m.Replace("=", "_");
            m = m.Replace("-", "_");
            m = m.Replace(":", "_");
            m = m.Replace(";", "_");
            m = m.Replace("'", "_");
            m = m.Replace("?", "_");
            m = m.Replace("/", "_");
            m = m.Replace(".", "_");
            m = m.Replace("<", "_");
            m = m.Replace(">", "_");


            return m;
        }
        public const string SecretKey = "D8l707dkWyrW0N9y7j,a8yH0m7jF19jr";
        public class Authorization
        {
            public const string DefaultEmail = "admin@gmail.com";
            public const string DefaultPassword = "Admin@123";
        }
        public class Roles
        {
            public const string Administrators = "Admin";
        }
        public static string TextToUrl(this string text)
        {
            if (!text.StartsWith("//") && !text.StartsWith("http://") && !text.StartsWith("http://") && !text.StartsWith("www."))
            {
                text = "//" + text;
            }

            return text;
        }

        public static string GetMonthName(this DateTime date)
        {
            PersianCalendar jc = new PersianCalendar();
            string pdate = string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(date), jc.GetMonth(date), jc.GetDayOfMonth(date));

            string[] dates = pdate.Split('/');
            int month = Convert.ToInt32(dates[1]);

            switch (month)
            {
                case 1: return "فررودين";
                case 2: return "ارديبهشت";
                case 3: return "خرداد";
                case 4: return "تير‏";
                case 5: return "مرداد";
                case 6: return "شهريور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دي";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "";
            }

        }
        public class FilePath
        {
            public const string NewsImagePath = "wwwroot\\img\\News";
            public const string NewsImagePathForShow = "/img/News/";
            
            public const string BlogImagePath = "wwwroot\\img\\Blog";
            public const string BlogImagePathForShow = "/img/Blog/";

            public const string EstateImagePath = "wwwroot\\img\\Estate";
            public const string EstateImagePathForShow = "/img/Estate/";

        }

        public class FileUploader
        {
            public enum ImageComperssion
            {
                Maximum = 50,
                Product = 60,
                Normal = 75,
                Fast = 80,
                Minimum = 90,
                None = 100,
            }

            public enum ImageWidth
            {
                Small = 350,
                Medium = 550,
                Large = 850,
                Standard = 1024,
                FullHD = 1920,
                Unset = 0
            }
            public enum ImageHeight
            {
                Small = 350,
                Medium = 550,
                Large = 850,
                Standard = 768,
                FullHD = 1080,
                Unset = 0
            }
            //10 mb
            const int _maxImageLength = 10;

            public static bool UpdateImage(IFormFile file, string path)
            {
                if (!IsImageMimeTypeValid(file) || !IsImageExtentionValid(file))
                {
                    return false;
                }
                try
                {
                    var image = Image.Load(file.OpenReadStream());

                    if (!Directory.Exists(Path.GetFullPath(path)))
                        Directory.CreateDirectory(Path.GetFullPath(path));

                    image.SaveAsPng(Path.GetFullPath(Path.Combine(path, "logo.png")));
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static (bool succsseded, string result) UploadImage(IFormFile file, string path, int maxLength = _maxImageLength, int width = (int)ImageWidth.Unset, int height = (int)ImageHeight.Unset, int compression = (int)ImageComperssion.Normal, string format = "default")
            {

                if (!IsImageMimeTypeValid(file) || !IsImageExtentionValid(file))
                {
                    return (false, "فرمت عکس صحیح نیست.");
                }

                if (!IsImageSizeValid(file, maxLength))
                {
                    return (false, $"سایز عکس باید کمتر از {maxLength} باشد");
                }

                try
                {
                    var image = Image.Load(file.OpenReadStream());
                    var resizeOptions = new ResizeOptions()
                    {
                        Size = new Size(width, height),
                        Mode = ResizeMode.Crop
                    };

                    if (width != (int)ImageWidth.Unset && height != (int)ImageHeight.Unset)
                        image.Mutate(x => x.Resize(resizeOptions));

                    var encoder = new JpegEncoder()
                    {
                        Quality = compression
                    };

                    var fileName = GetRandomFileName(file);
                    var savePath = Path.GetFullPath(Path.Combine(path, fileName));

                    if (!Directory.Exists(Path.GetFullPath(path)))
                        Directory.CreateDirectory(Path.GetFullPath(path));


                    if (format == "logo")
                    {
                        image.SaveAsPng(Path.GetFullPath(Path.Combine(path, "logo.png")), new PngEncoder());

                    }
                    else if (file.ContentType.ToLower() == "image/png")
                    {
                        image.SaveAsPng(savePath, new PngEncoder());
                    }
                    else
                    {
                        image.Save(savePath, encoder);

                    }

                    return (true, fileName);
                }
                catch (Exception e)
                {
                    return (false, e.Message);
                }
            }

            public static bool DeleteFile(string filepath)
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                    return true;
                }
                return false;
            }

            private static bool IsImageSizeValid(IFormFile image, int validLength = _maxImageLength)
            {
                if (image.Length > (validLength * 1024 * 1024))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            private static bool IsImageMimeTypeValid(IFormFile image)
            {
                string mimeType = image.ContentType.ToLower();
                if (mimeType != "image/jpg" &&
                     mimeType != "image/jpeg" &&
                     mimeType != "image/pjpeg" &&
                     mimeType != "image/gif" &&
                     mimeType != "image/x-png" &&
                     mimeType != "image/png")
                {
                    return false;
                }
                return true;
            }

            private static string GetRandomFileName(IFormFile file)
            {
                return Guid.NewGuid() + Path.GetExtension(file.FileName).ToLower();
            }

            private static bool IsImageExtentionValid(IFormFile image)
            {
                string extention = Path.GetExtension(image.FileName).ToLower();

                if (extention != ".jpg"
                    && extention != ".png"
                    && extention != ".gif"
                    && extention != ".jpeg")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
