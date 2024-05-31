using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Member : BaseModel
    {
        public Group Group { get; set; }
        public int GroupId { get; set; }
        public Owner Owner { get; set; }
        public int OwnerId { get; set; }
    }
}
