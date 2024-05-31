using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.API.Question
{
    public class EditQuestionVM
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} معتبر نیست")]
        public string Title { get; set; }
        public string Tip { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
