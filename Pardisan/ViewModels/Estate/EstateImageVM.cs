using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pardisan.ViewModels.Estate
{
    public class EstateImageVM
    {
        public int PropertyId { get; set; }
        [Display(Name = "عکس")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public List<IFormFile> Item { get; set; }
    }

}
