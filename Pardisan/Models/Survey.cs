using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Survey : BaseModel
    {
        public string Title { get; set; }
        public string Describtion { get; set; }
        public int AnswerCount { get; set; }
        public Property Property { get; set; }
        public int PropertyId { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<SurveyOwner> Owners { get; set; }
    }
}
