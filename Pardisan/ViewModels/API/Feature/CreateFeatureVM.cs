
using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModel.Feature
{
    public class CreateFeatureVM
    {
        [Display(Name = "متن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Text { get; set; }
        public List<FeatureOption> Options { get; set; }
    }
}
