using Pardisan.Models;
using Pardisan.ViewModels.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.Estate
{
    public class EstateDatatableInput : DatatableInput
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
    public class EstateImagesDatatableInput : DatatableInput
    {
        public int Id { get; set; }
    }
    public class EstateDatatableResult
    {
        public int? Id { get; set; }
        public bool? IsActive { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Code { get; set; }
        public string FloorCount { get; set; }
        public string UsageType { get; set; }
        public string Province { get; set; }
        public ProjectStatus Status { get; set; }

    }

}
