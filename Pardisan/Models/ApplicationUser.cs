using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(maximumLength: 100)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 100)]
        public string LastName { get; set; }
    }
}
