using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.Property
{
    public class AddListToGallery
    {
        public int PropertyId { get; set; }
        public List<Image> Files { get; set; }
    }
    public class Image
    {
        public IFormFile Item { get; set; }
    }
}
