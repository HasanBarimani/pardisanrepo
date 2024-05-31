using Pardisan.Models;
using System.Collections.Generic;

namespace Pardisan.ViewModels.Estate
{
    public class FloorVM
    {
        public int id { get; set; }
        public int floorNumber { get; set; }
        public string FloorName { get; set; }
        public List<UnitVM> units { get; set; }
    }
}