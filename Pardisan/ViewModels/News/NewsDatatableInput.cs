using Pardisan.ViewModels.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.News
{
    public class NewsDatatableInput : DatatableInput
    {
        public int? Id { get; set; }
        public bool Deleted { get; set; }
    }

    public class NewsDatatableResult
    {
        public int? Id { get; set; }
        public bool? IsActive { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Describtion { get; set; }
    }
    
    public class NewsCategoryDatatableInput : DatatableInput
    {
        public int? Id { get; set; }
        public bool Deleted { get; set; }
    }

    public class NewsCategoryDatatableResult
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public bool? IsActive { get; set; }
    }

}
