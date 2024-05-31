using Microsoft.AspNetCore.Http;
using Pardisan.Migrations;
using System.Collections.Generic;

namespace Pardisan.ViewModels.Estate
{
    public class EstateImagesVM
    {
        public int PropertyId { get; set; }
        public string Files { get; set; }
    }
    public class EstateImagesListVM {
        public List<EstateImagesVM> List { get; set; }
    }
   
}
