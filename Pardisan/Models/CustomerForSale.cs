using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;
using System;
using System.Security.Permissions;

namespace Pardisan.Models
{
    public class CustomerForSale : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Demand { get; set; }
        public string DocumentType { get; set; }
        public int Meterage { get; set; }
        public int Passage { get; set; }
        public int LandBar { get; set; }
        public string HowToKnow { get; set; }
        //CustomerForSaleProviders
        public CFSProviders Providers { get; set; }
        public DateTime Date { get; set; }
        public string PropertyAddress { get; set; }
        public string Usgae { get; set; }
        public string FirstRecord { get; set; }
        public string SecondRecord { get; set; }
        public string SalesManagerOpinion { get; set; }
        public string FinalOpinion { get; set; }

    }
}
