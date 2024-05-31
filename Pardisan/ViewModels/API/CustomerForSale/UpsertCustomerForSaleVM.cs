using Pardisan.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pardisan.ViewModels.API.CustomerForSale
{
    public class UpsertCustomerForSaleVM
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
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
        public string DateForShow { get; set; }
        [Display(Name = "آدرس ملک")]
        public string PropertyAddress { get; set; }
        [Display(Name = "کاربری")]
        public string Usgae { get; set; }
        [Display(Name = "گزارش اول")]
        public string FirstRecord { get; set; }
        [Display(Name = "گزارش دوم")]
        public string SecondRecord { get; set; }
        [Display(Name = "اظهار نظر مدیر فروش")]
        public string SalesManagerOpinion { get; set; }
        [Display(Name = "اظهار نظر نهایی")]
        public string FinalOpinion { get; set; }
    }
}
