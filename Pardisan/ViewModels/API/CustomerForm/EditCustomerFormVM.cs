using DNTPersianUtils.Core;
using Pardisan.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Pardisan.ViewModels.API.CustomerForm
{
    public class EditCustomerFormVM
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Name { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        [ValidIranianMobileNumber(ErrorMessage = "تلفن همراه معتبر نیست")]
        public string PhoneNumer { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime Date { get; set; }
        public string DateForShow { get; set; }
        [Display(Name = "موضوع تماس")]
        public string CallSubject { get; set; }
        [Display(Name = "نحوه آشنایی")]
        public string HowToKnow { get; set; }
        [Display(Name = "محتوا")]
        public string Content { get; set; }

        [Display(Name = "نوع تراکنش")]
        public TransactionsType Transactions { get; set; }
    }
}
