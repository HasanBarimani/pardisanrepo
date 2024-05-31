using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models.News
{
    public class NewsCategory : BaseModel
    {
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public ICollection<News> NewsList { get; set; }
    }

}
