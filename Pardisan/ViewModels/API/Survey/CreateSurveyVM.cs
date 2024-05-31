using Pardisan.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pardisan.ViewModels.API.Survey
{
    public class CreateSurveyVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Title { get; set; }
        public string Describtion { get; set; }
        public int PropertyId { get; set; }
        public List<SurveyOwnerVM> Owners { get; set; }
    }
}
