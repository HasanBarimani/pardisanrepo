using Pardisan.ViewModels.DataTable;

namespace Pardisan.ViewModels.Page
{
    public class PagesDatatableInput : DatatableInput
    {
        public int? Id { get; set; }
        public bool Deleted { get; set; }
    }

    public class PagesDatatableResult
    {
        public int? Id { get; set; }
        public bool? IsActive { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
    }


}
