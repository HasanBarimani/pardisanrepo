using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.Unit
{
    public class CreateUnitVM
    {
        public int PropertyId { get; set; }
        public int? OwnerId { get; set; }
        [Display(Name = "طبقه")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public int Floor { get; set; }
        [Display(Name = "شماره واحد")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public int Number { get; set; }
        [Display(Name = "متراژ")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public float Meterage { get; set; }
    }
}
