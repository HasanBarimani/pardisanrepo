using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Setting : BaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
