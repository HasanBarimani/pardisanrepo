
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pardisan.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Services
{
    public class UploaderService : IUploaderService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public UploaderService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> SimpleUpload(IFormFile file, string key)
        {
            var fileName = DateTime.Now.Ticks.ToString();
            fileName += Path.GetFileName(file.FileName);
            var path = _hostingEnvironment.WebRootPath + key + fileName;
            await file.CopyToAsync(new FileStream(path, FileMode.Create));
            return key + fileName;
        }
    }
}
