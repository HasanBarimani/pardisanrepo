using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.News
{

    public class UpsertNewsCategoryVM
    {
        public int Id { get; set; }
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class NewsCategoryIndexViewModel
    {
        public List<NewsCategoryVM> NewsCategory { get; set; }
    }

    public class NewsCategoryVM
    {
        public int Id { get; set; }
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
