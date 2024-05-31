using Microsoft.AspNetCore.Http;
using Pardisan.Extention;
using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.Property
{
    public class EditPropertyVM
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Title { get; set; }
        [Display(Name = "عکس")]
        public IFormFile Image { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Address { get; set; }
        [Display(Name = "تعداد طبقه")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string FloorCount { get; set; }
        [Display(Name = "سرپرست پروژه")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string ProjectSupervisor { get; set; }
        [Display(Name = "اطلاعات پروژه")]
        public string Description { get; set; }
        //public ICollection<PropertyFeatures> Features { get; set; }
    }
}
