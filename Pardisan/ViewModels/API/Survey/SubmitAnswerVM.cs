using Pardisan.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.Survey
{
    public class SubmitAnswerVM
    {
        public int SurveyId { get; set; }
        public string Code { get; set; }
        [MustHaveOneElement(ErrorMessage = "حداقل باید یک گزینه وارد کنید")]
        public List<QuestionAnswerVM> QuestionAnswers { get; set; }
    }
}
