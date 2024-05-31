using Pardisan.Models;
using System.Collections.Generic;

namespace Pardisan.ViewModels.DynamicContent
{
    public class GlobalDynomicContentVM
    {
        public string WorkTime { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Telegram { get; set; }
        public string Address { get; set; }
        public string flinkName { get; set; }
        public string flink { get; set; }
        public string slinkName { get; set; }
        public string slink { get; set; }
        public string tlinkName { get; set; }
        public string tlink { get; set; }
        public int dayCountVisit { get; set; }
        public int weekCountVisit { get; set; }
        public int monthCountVisit { get; set; }
        public string ProjectPic { get; set; }
        public string titleForVideos { get; set; }
        public string titleForHolding { get; set; }
        public List<PageVm> page { get; set; }
        
    }
    public class PageVm
    {

        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }

    }
}
