using Pardisan.ViewModels.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.Blog
{
    public class BlogDatatableInput : DatatableInput
    {
        public int? Id { get; set; }
        public bool Deleted { get; set; }
    }

    public class BlogDatatableResult
    {
        public int? Id { get; set; }
        public bool? IsActive { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Describtion { get; set; }
    }
    
    public class BlogCategoryDatatableInput : DatatableInput
    {
        public int? Id { get; set; }
        public bool Deleted { get; set; }
    }

    public class BlogCategoryDatatableResult
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public bool? IsActive { get; set; }
    }

}
