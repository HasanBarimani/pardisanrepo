using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API
{
    public class SignInVM
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string UserName { get; set; }

        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0} باید حداقل 6 رقم باشد")]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Password { get; set; }
    }
}
