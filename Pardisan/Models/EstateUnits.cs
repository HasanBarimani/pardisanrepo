using System.Collections.Generic;

namespace Pardisan.Models
{
    public class EstateUnits : BaseModel
    {

        public Floors Floors { get; set; }
        public int FloorId { get; set; }
        public int? MergedUnitId { get; set; }
        public bool IsEmpty{ get; set; }
        public int Meterage { get; set; }
        public string name { get; set; }
        public int ClientIdForUnit { get; set; }
    }
}
