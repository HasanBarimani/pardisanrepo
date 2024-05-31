using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class PropertyOwner : BaseModel
    {
        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public Property Property { get; set; }
        public int PropertyId { get; set; }

        public Unit Unit { get; set; }
        public int? UnitId { get; set; }

        public bool IsUnitOwnership { get; set; }
        public DateTime ContractSigningDate { get; set; }
        public SatisfactionLevel SatisfactionLevel { get; set; }
        public string Description { get; set; }
        public bool HasDesireToIntroduce { get; set; }
        public bool HasIntroductionLeadsToPurchase { get; set; }
        public string DisSatisfactionLevelReason { get; set; }
    }
}
