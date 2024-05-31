using Microsoft.AspNetCore.Http;
using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.Estate
{
    public class UpsertEstateVM
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public int Code { get; set; }
        public IFormFile Image { get; set; }
        public string ImageShow { get; set; }
        [Display(Name = "محتوا")]

        public string Description { get; set; }
        [Display(Name = "تعداد طبقه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FloorCount { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime StartDate { get; set; }
        public string StartDateForShow { get; set; }
        [Display(Name = "محل پروژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Province { get; set; }
        [Display(Name = "وضعیت پروژه")]
        public ProjectStatus Status { get; set; }
        [Display(Name = "متراژ پروژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EstateMeterage { get; set; }
        [Display(Name = " تعداد واحد در طبقه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UnitInFlorCount { get; set; }
        [Display(Name = "تعداد کل واحد ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TotalUnits { get; set; }
        [Display(Name = "تاریخ اتمام پروژه")]

        public DateTime ProjectCompletionDate { get; set; }
        public string CompletionDateForShow { get; set; }
        [Display(Name = "نوع کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UsageType { get; set; }
        [Display(Name = "لینک آپارات")]

        public string AparatLink { get; set; }
  
        public double Lat { get; set; }
        public double Long { get; set; }
      
       
        public List<IFormFile> Files { get; set; }
        public EstateForShowVM  GalleryIameges { get; set; }
        public bool Fibr { get; set; }
        public bool AbNama { get; set; }
        public bool QRCode { get; set; }
        public bool Camera { get; set; }
        public bool Security { get; set; }
        public bool Shomineh { get; set; }
        public bool Flower { get; set; }
        public bool Alachigh { get; set; }
    }
}
