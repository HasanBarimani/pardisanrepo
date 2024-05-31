using System;

namespace Pardisan.Models
{
    public class CustomerForBuy : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string HowToKnow { get; set; }
        public double Budget { get; set; }
        public string FirstRecord { get; set; }
        public string SecondRecord { get; set; }
        public string SaleManagerOpinion { get; set; }
        public string FinalOpinion { get; set; }
    }
}
