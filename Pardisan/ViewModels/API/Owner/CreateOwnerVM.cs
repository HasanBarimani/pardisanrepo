using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;

namespace Pardisan.ViewModels.Owner
{
    public class CreateOwnerVM
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string LastName { get; set; }
        [Display(Name = "اهلیت")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string NeighborhoodOfGrowingUp { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        [ValidIranianMobileNumber(ErrorMessage = "تلفن همراه معتبر نیست")]
        public string PhoneNumber { get; set; }
        [Display(Name = "شغل")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Job { get; set; }
        [Display(Name = "پایه درآمدی")]
        public IncomeBase IncomeBase { get; set; }
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "اینستاگرام")]
        public string Instagram { get; set; }
        [Display(Name = "تلگرام")]
        public string Telegram { get; set; }
        [Display(Name = "واتساپ")]
        public string Whatsapp { get; set; }
    }
}
