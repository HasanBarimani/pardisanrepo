using Pardisan.Extention;
using Pardisan.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pardisan.ViewModels.API.Question
{
    public class CreateQuestionVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Title { get; set; }
        public string Tip { get; set; }
        public QuestionType QuestionType { get; set; }
        [MustHaveOneElement(ErrorMessage = "حداقل باید یک گزینه وارد کنید")]
        public ICollection<Option> Options { get; set; }
        public int SurveyId { get; set; }
    }
}
