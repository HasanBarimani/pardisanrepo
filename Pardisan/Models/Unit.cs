using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Unit : BaseModel
    {
        public Property Property { get; set; }
        public int PropertyId { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public float Meterage { get; set; }
        public int? OwnerId { get; set; }
        public int? MergedUnitId { get; set; }
        public List<PropertyOwnersList>Owners { get; set; }
        public int OwnersId { get; set; }
    }
}
