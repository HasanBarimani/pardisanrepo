using DNTPersianUtils.Core;
using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pardisan.ViewModels.API.CustomerForSale
{
 
    public class CustomerForSaleVM
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Name { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        [ValidIranianMobileNumber(ErrorMessage = "تلفن همراه معتبر نیست")]
        public string PhoneNumber { get; set; }
        [Display(Name = "تقاضا")]
        public string Demand { get; set; }
        [Display(Name = "نوع سند")]
        public string DocumentType { get; set; }
        [Display(Name = "متراژ")]
        public int Meterage { get; set; }
        [Display(Name = "گذر")]
        public int Passage { get; set; }
        [Display(Name = "بر زمین")]
        public int LandBar { get; set; }
        [Display(Name = "نحوه آشنایی")]
        public string HowToKnow { get; set; }
        [Display(Name = "تامین کننده")]
      
        //CustomerForSaleProviders
        public CFSProviders Providers { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime Date { get; set; }
        [Display(Name = "آدرس ملک")]
        public string PropertyAddress { get; set; }
        [Display(Name = "کاربری")]
        public string Usgae { get; set; }
        [Display(Name = "گزارش اولیه")]
        public string FirstRecord { get; set; }
        [Display(Name = "گزارش ثانویه")]
        public string SecondRecord { get; set; }
        [Display(Name = "اظهار نظر مدیر فروش")]
        public string SalesManagerOpinion { get; set; }
        [Display(Name = "اظهار نظر نهایی")]
        public string FinalOpinion { get; set; }
    }


}
