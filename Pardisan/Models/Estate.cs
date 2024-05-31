using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Estate : BaseModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int  Code{ get; set; }
        public string FloorCount { get; set; }
        public float LandArea { get; set; }
        public float TotalInfrastructure { get; set; }
        public float UsefulInfrastructure { get; set; }
        public DateTime StartDate { get; set; }
        public string ProjectSupervisor { get; set; }
        public string FinancialValue { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public ProjectStatus Status { get; set; }
        public string EstateMeterage { get; set; }
        public string UnitInFlorCount { get; set; }
        public string TotalUnits { get; set; }
        public DateTime ProjectCompletionDate { get; set; }
        public string UsageType { get; set; }
        public string AparatLink { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public ICollection<EstateImage> Images { get; set; }
        public bool Fibr { get; set; }
        public bool AbNama { get; set; }
        public bool QRCode { get; set; }
        public bool Camera { get; set; }
        public bool Security { get; set; }
        public bool Shomineh { get; set; }
        public bool Flower { get; set; }
        public bool Alachigh { get; set; }


    }
}
