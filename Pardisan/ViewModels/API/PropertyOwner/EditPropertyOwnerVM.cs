using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.PropertyOwner
{
    public class EditPropertyOwnerVM
    {
        public int Id { get; set; }
        [Display(Name = "تاریخ ثبت قرارداد")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public DateTime ContractSigningDate { get; set; }
        public SatisfactionLevel SatisfactionLevel { get; set; }
        //[Display(Name = "توضیحات")]
        //[Required(ErrorMessage = "{0} معتبر نیست")]
        public string Description { get; set; }
        public bool HasDesireToIntroduce { get; set; }
        public bool HasIntroductionLeadsToPurchase { get; set; }
        public string DisSatisfactionLevelReason { get; set; }
    }
}
