using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.Property
{
    public class AddToGalleryVM
    {
        public int PropertyId { get; set; }
        [Display(Name = "عکس")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile Item { get; set; }
    }
}
