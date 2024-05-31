using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.ViewModels.Setting
{
    public class SettingVM
    {
        public string WorkTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Whatsapp { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Telegram { get; set; }
        public string Address { get; set; }
        public string Cellphone { get; set; }
        public string OurServices { get; set; }
        public string QualityMaterials { get; set; }
        public string modernView { get; set; }
        public string HighStrength { get; set; }
        public string LifetimeSupport { get; set; }
     
        public string UnderSliderText { get; set; }
        public string CounselingTitle { get; set; }
        public string CounselingContent { get; set; }
        public string VideosTitle { get; set; }
        public string  StaticContent{ get; set; }
        public string SailedUnits { get; set; }
        public string InterduceProject { get; set; }
        public string FirstLinkName { get; set; }
        public string FirstLink { get; set; }
        public string SecondLinkName { get; set; }
        public string SecondLink { get; set; }
        public string ThirdLinkName { get; set; }
        public string ThirdLink { get; set; }
        public IFormFile Image { get; set; }
        public string ImageShow { get; set; }
        public IFormFile ImageDocs { get; set; }
        public string ImageDocsShow { get; set; }
        public string titleForVideos { get; set; }
        public string titleForProject { get; set; }
        public string titleForBlogs { get; set; }

    }
}
