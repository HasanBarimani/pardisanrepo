using System;

namespace Pardisan.Models
{
    public class HoldingImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? HoldingId { get; set; }
        public Holding Estate { get; set; }
        public DateTime Date { get; set; }
    }
}
