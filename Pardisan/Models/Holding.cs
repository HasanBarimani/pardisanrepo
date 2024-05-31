using System.Collections.Generic;

namespace Pardisan.Models
{
    public class Holding : BaseModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string AparatLink { get; set; }
        public string History { get; set; }
        public string Target { get; set; }
        public string MapLink { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public ICollection<HoldingImage> Images { get; set; }

    }
}
