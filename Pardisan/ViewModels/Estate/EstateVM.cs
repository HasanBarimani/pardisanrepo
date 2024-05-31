using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.Estate
{

    public class EstateIndexViewModel
    {
        public List<EstateVM> Estates { get; set; }
    }


    public class EstateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Code { get; set; }
      
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProjectStatus Status { get; set; }
      

    }
}
