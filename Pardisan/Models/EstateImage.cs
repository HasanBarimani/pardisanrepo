using System;

namespace Pardisan.Models
{
    public class EstateImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? EstateId { get; set; }
        public Estate Estate { get; set; }
        public DateTime Date { get; set; }
    }
}
