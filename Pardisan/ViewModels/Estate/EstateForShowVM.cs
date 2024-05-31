using Microsoft.AspNetCore.Http;
using Pardisan.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pardisan.ViewModels.Estate
{
    public class EstateForShowVM
    {
        public int PropertyId { get; set; }
        public List<EstateImage> Images { get; set; }
    }
}
