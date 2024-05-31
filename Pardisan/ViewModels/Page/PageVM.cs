using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pardisan.ViewModels.Page
{
    public class UpsertPageVM
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }



        [Display(Name = "محتوا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Content { get; set; }

        [Display(Name = "آدرس صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Url { get; set; }
    }

    public class PageIndexViewModel
    {
        public List<PageVM> Pages { get; set; }
    }

    public class PageVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
        public string Url { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
