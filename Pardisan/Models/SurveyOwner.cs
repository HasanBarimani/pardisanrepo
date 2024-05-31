using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class SurveyOwner : BaseModel
    {
        public Owner Owner { get; set; }
        public int OwnerId { get; set; }
        public Survey Survey { get; set; }
        public int SurveyId { get; set; }
        public bool HasAnswered { get; set; }
        public string Code { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
