using DNTPersianUtils.Core;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Pardisan.ViewModels.API.CustomerForBuy
{
    public class EditCustomerForBuyVM
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Name { get; set; }
        [Display(Name = "تلقن همراه")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string PhoneNumber { get; set; }
        [Display(Name = "تاریخ ")]
        public string DateForShow { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "نحوه آشنایی")]
        public string HowToKnow { get; set; }
        [Display(Name = "بودجه")]
        public double Budget { get; set; }
        [Display(Name = "گزارش اولیه")]
        public string FirstRecord { get; set; }
        [Display(Name = "گزارش ثانویه")]
        public string SecondRecord { get; set; }
        [Display(Name = "اظهار نظر مدیر فروش")]
        public string SaleManagerOpinion { get; set; }
        [Display(Name = "اظهار نظر نهایی")]
        public string FinalOpinion { get; set; }
    }
}
