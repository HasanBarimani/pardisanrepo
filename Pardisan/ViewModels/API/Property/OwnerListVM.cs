using System.Collections.Generic;

namespace Pardisan.ViewModels.API.Property
{
    public class OwnerListVM
    {
      
        public int? UnitId { get; set; }
        public List<int> OwnersId { get; set; }
    }
    public class OwnersVM {

        public int Id { get; set; }
    }
}
