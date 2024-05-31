using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Feature : BaseModel
    {
        public string Text { get; set; }
        public List<FeatureOption> Options { get; set; }
    }
}
