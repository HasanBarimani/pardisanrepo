using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Group : BaseModel
    {
        public string Title { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
