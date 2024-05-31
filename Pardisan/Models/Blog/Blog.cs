using Pardisan.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Models.Blog
{
    public class Blog : BaseModel
    {
        public string Title { get; set; }
        public string Describtion { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Tags { get; set; }
        public int CategoryId { get; set; }
        public BlogCategory Category { get; set; }
    }
}
