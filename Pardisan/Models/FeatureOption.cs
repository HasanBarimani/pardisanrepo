using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class FeatureOption : BaseModel
    {
        public string Text { get; set; }
        public Feature Feature { get; set; }
        public int FeatureId { get; set; }
    }
}
