using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Question :  BaseModel
    {
        public string Title { get; set; }
        public string Tip { get; set; }
        public QuestionType QuestionType { get; set; }
        public ICollection<Option> Options { get; set; }
        public Survey Survey { get; set; }
        public int SurveyId { get; set; }
    }
}
