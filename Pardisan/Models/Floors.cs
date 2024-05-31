using System.Collections.Generic;

namespace Pardisan.Models
{
    public class Floors : BaseModel
    {
        public Estate Estate { get; set; }
        public int EstateId { get; set; }
        public int FloorNumber { get; set; }
        public string FloorName { get; set; }
        public List<EstateUnits> EstateUnits { get; set; }
        public int ClientId { get; set; }
    }
}
