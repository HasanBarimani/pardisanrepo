using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class Owner : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NeighborhoodOfGrowingUp { get; set; }
        public string PhoneNumber { get; set; }
        public string Job { get; set; }
        public IncomeBase IncomeBase { get; set; }
        public DateTime BirthDate { get; set; }
        public string Instagram { get; set; }
        public string Telegram { get; set; }
        public string Whatsapp { get; set; }
    }
}
