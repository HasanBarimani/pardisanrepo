using Microsoft.AspNetCore.Http;
using Pardisan.Models;
using Pardisan.ViewModels.Estate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.Holding
{
    public class UpsertHoldingVM
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile Image { get; set; }
        public string ImageForShow { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public string AparatLink { get; set; }
        public string History { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public List<IFormFile> Files { get; set; }
        public HoldingForShowVM GalleryIameges { get; set; }
    }

    public class HoldingIndexViewModel
    {
        public List<HoldingVM> Holding { get; set; }
    }

    public class HoldingVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }


    }
    public class HoldingForShowVM
    {
        public int PropertyId { get; set; }
        public List<HoldingImage> Images { get; set; }
    }
    public class HoldingImageVm
    {

        public int HoldingId { get; set; }
        [Display(Name = "عکس")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public List<IFormFile> Item { get; set; }
    }

}
