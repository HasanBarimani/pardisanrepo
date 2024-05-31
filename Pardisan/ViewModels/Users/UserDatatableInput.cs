using Pardisan.ViewModels.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.Users
{
    public class UserDatatableInput : DatatableInput
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
    }

    public class UserDatatableResult
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
