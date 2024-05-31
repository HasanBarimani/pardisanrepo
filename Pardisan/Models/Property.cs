using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Property : BaseModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string FloorCount { get; set; }
        public string ProjectSupervisor { get; set; }
        public string Description { get; set; }
        public List<Unit> Units { get; set; }
        public ICollection<PropertyFeatures> Features { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
