using Microsoft.AspNetCore.Http;
using Pardisan.Models.News;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels
{
    public class UpsertNewsVM
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Describtion { get; set; }

        [Display(Name = "محتوا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Content { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile Image { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }

    public class NewsIndexViewModel
    {
        public List<NewsVM> News { get; set; }
    }

    public class NewsVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Describtion { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Tags { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }

    }

}
