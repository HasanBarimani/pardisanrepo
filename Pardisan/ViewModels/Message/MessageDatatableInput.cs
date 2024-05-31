using Pardisan.ViewModels.DataTable;

namespace Pardisan.ViewModels.Message
{
    public class MessageDatatableInput : DatatableInput
    {
        public int? Id { get; set; }
        public bool Deleted { get; set; }
    }

    public class MessageDatatableResult
    {
        public int? Id { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }


}

