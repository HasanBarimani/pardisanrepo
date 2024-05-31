using System;

namespace Pardisan.Models
{
    public class CustomerForm : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumer { get; set; }
        public DateTime Date { get; set; }
        public string CallSubject { get; set; }
        public string HowToKnow { get; set; }
        public string Content { get; set; }
        public TransactionsType Transactions { get; set; }

    }
}
