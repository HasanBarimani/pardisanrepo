using Pardisan.ViewModels.DataTable;

namespace Pardisan.ViewModels.Aparat
{
    public class AparatDatatableInput : DatatableInput
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
    public class AparatDatatableResult
    {
        public int? Id { get; set; }
        public bool? IsActive { get; set; }
        public string AparatLink { get; set; }
        public string Title { get; set; }
        public int Code { get; set; }

    }
}
