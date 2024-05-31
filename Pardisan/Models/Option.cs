using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Option : BaseModel
    {
        public string Title { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
