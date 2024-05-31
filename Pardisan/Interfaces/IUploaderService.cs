using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interface
{
    public interface IUploaderService
    {
        Task<string> SimpleUpload(IFormFile file,string key);

    }
}
