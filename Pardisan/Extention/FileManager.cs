using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Png;

namespace Pardisan.Extensions
{
    public static class FileManager
    {
        public static class Images
        {
            public static async Task<string> Upload(string path, IFormFile file, int? width = null, int? height = null, string customExtension = null)
            {
                //if (CheckIfImageFile(file))
                //{
                //}
                    return await WriteFile(path, file, width, height, customExtension);

                //return null;
            }
            public static bool Delete(string path, string filename)
            {
                if (DeleteFile(path, filename))
                {
                    return true;
                }

                return false;
            }

            public static async Task<string> Update(string oldFileName, string path, IFormFile file, int? width = null, int? height = null, string customExtension = null)
            {
                if (CheckIfImageFile(file))
                {
                    DeleteFile(path, oldFileName);
                    return await WriteFile(path, file, width, height, customExtension);
                }
                return null;
            }

            public static bool CheckIfImageFile(IFormFile file)
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                return GetImageFormat(fileBytes) != ImageFormat.unknown;
            }

            private static async Task<string> WriteFile(string url, IFormFile file, int? width = null, int? height = null, string customExtension = null)
            {
                string fileName;
                try
                {
                    if (string.IsNullOrEmpty(url))
                        url = "wwwroot";

                    string directory = Path.Combine(Directory.GetCurrentDirectory(), url);
                    if (Directory.Exists(directory) == false)
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    
                    using Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(file.OpenReadStream());

                    if (width != null && width == null)
                    {
                        var difference = width.Value - image.Width;
                        var resultHeight = Math.Abs(image.Height + difference);

                        image.Mutate(x => x.Resize(width.Value, resultHeight));
                    }
                    else if (width != null && height != null)
                    {
                        image.Mutate(x => x.Resize(width.Value, height.Value));
                    }
                    else
                    {
                        image.Mutate(x => x.Resize(image.Width, image.Height));
                    }

                    fileName = Guid.NewGuid().ToString();
                    var path = Path.Combine(Directory.GetCurrentDirectory(), url);
                    if (customExtension != null)
                    {
                        if (customExtension.ToLower().Equals(".jpg") || customExtension.ToLower().Equals(".jpeg"))
                        {
                            await image.SaveAsJpegAsync(Path.Combine(path, fileName + ".jpg"));
                        }
                        if (customExtension.ToLower().Equals(".png"))
                        {
                            await image.SaveAsPngAsync(Path.Combine(path, fileName + ".png"));
                        }
                        if (customExtension.ToLower().Equals(".bmp"))
                        {
                            await image.SaveAsBmpAsync(Path.Combine(path, fileName + ".bmp"));
                        }
                        return fileName + customExtension;
                    }
                    else
                    {
                        if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg"))
                        {
                            await image.SaveAsJpegAsync(Path.Combine(path, fileName + ".jpg"));
                        }
                        if (extension.ToLower().Equals(".png"))
                        {
                            await image.SaveAsPngAsync(Path.Combine(path, fileName + ".png"));
                        }
                        if (extension.ToLower().Equals(".bmp"))
                        {
                            await image.SaveAsBmpAsync(Path.Combine(path, fileName + ".bmp"));
                        }
                        return fileName + extension;
                    }
                }
                catch (Exception)
                {

                    return null;
                }
            }

            private static bool DeleteFile(string url, string fileName)
            {
                try
                {
                    if (string.IsNullOrEmpty(url))
                        url = "wwwroot";

                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), url, fileName);

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }


            private enum ImageFormat
            {
                bmp,
                jpeg,
                png,
                unknown
            }


            private static ImageFormat GetImageFormat(byte[] bytes)
            {
                var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
                var png = new byte[] { 137, 80, 78, 71 };    // PNG
                var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg

                if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                    return ImageFormat.bmp;

                if (png.SequenceEqual(bytes.Take(png.Length)))
                    return ImageFormat.png;

                if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                    return ImageFormat.jpeg;

                return ImageFormat.unknown;
            }

        }
        public static class Files
        {
            public static async Task<string> Upload(string path, IFormFile file, bool zipit = false)
            {
                return await WriteFile(path, file, zipit);

            }


            public static bool Delete(string path, string filename)
            {
                if (DeleteFile(path, filename))
                {
                    return true;
                }

                return false;
            }

            private static async Task<string> WriteFile(string url, IFormFile file, bool zipit)
            {
                string fileName;
                try
                {
                    if (string.IsNullOrEmpty(url))
                        url = "wwwroot";

                    string directory = Path.Combine(Directory.GetCurrentDirectory(), url);
                    if (Directory.Exists(directory) == false)
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    var zipextension = ".zip";
                    fileName = Guid.NewGuid().ToString() + extension; //Create a new Name for the file due to security reasons.
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), url, fileName);

                    using (var bits = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(bits);
                    }

                    if (zipit)
                    {
                        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), url, fileName + zipextension);
                        // string extractPath = @".\extract";

                        //  ZipFile.CreateFromDirectory(path, zipPath);

                        // ZipFile.ExtractToDirectory(zipPath, extractPath);


                        // string extractPath = @"c:\users\exampleuser\extract";
                        //string newFile = @"c:\users\exampleuser\NewFile.txt";

                        using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
                        {
                            archive.CreateEntryFromFile(filepath, fileName + extension);
                            //  archive.ExtractToDirectory(extractPath);
                        }
                        DeleteFile(Path.Combine(Directory.GetCurrentDirectory(), url), fileName + extension);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return fileName;
            }

            private static bool DeleteFile(string url, string fileName)
            {
                try
                {
                    if (string.IsNullOrEmpty(url))
                        url = "wwwroot";

                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), url, fileName);

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }
    }
}