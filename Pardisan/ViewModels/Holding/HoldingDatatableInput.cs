using Pardisan.ViewModels.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.Holding
{
    public class HoldingDatatableInput : DatatableInput
    {
        public int? Id { get; set; }
        public bool Deleted { get; set; }
    }
    public class HoldingImagesDatatableInput : DatatableInput
    {
        public int Id { get; set; }
    }
    public class HoldingDatatableResult
    {
        public int? Id { get; set; }
        public bool? IsActive { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
     
    }
    

    public class BlogCategoryDatatableResult
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public bool? IsActive { get; set; }
    }

}
