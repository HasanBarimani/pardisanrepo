using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.Survey
{
    public class QuestionAnswerVM
    {
        public int QuestionId { get; set; }
        public string ChosenOptions { get; set; }
    }
}
