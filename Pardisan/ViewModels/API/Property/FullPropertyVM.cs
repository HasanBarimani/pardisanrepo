using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.Property
{
    public class FullPropertyVM
    {
        public int Id { get; set; }
        public int FloorsCount { get; set; }
        public List<FloorVM> Floors { get; set; }
    }
    public class FloorVM
    {
        public int FloorNumber { get; set; }
        public List<UnitVM> Units { get; set; }

    }
    public class UnitVM
    {
        public int Id { get; set; }
        public List<int> OwnerId { get; set; }
        public List<PropertyOwnersList> OwnersForShow { get; set; }
        public int FloorNumber { get; set; }
        public int? MergedUnitId { get; set; }
        public int Number { get; set; }
        public float Meterage { get; set; }
    }
}
