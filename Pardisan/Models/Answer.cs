using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Answer : BaseModel
    {
        public Survey Survey { get; set; }
        public int? SurveyId { get; set; }

        public Question Question { get; set; }
        public int QuestionId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }
        
        public SurveyOwner SurveyOwner { get; set; }
        public int? SurveyOwnerId { get; set; }

        public string ChosenOptions { get; set; }
    }
}
