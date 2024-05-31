using System.Collections.Generic;

namespace Pardisan.ViewModels.DataTable
{
    public class DatatableInput
    {
        public int Draw { get; set; }
        public int Length { get; set; }
        public int Start { get; set; }
        public DatatableSearchInput Search { get; set; }
    }
    public class DatatableSearchInput
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }
}
