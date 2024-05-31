using System.Collections.Generic;

namespace Pardisan.ViewModels.DataTable
{
    public class DatatableResponse
    {
        public object AaData { get; set; }
        public int ITotalDisplayRecords { get; set; }
        public int ITotalRecords { get; set; }
        public int SEcho { get; set; }
    }
}
