using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pardisan.Data;
using Pardisan.Interface;
using Pardisan.ViewModels.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploaderService _uploaderService;

        public SettingsController(ApplicationDbContext context, IUploaderService uploaderService)
        {
            _context = context;
            _uploaderService = uploaderService;
        }

        public IActionResult Insert()
        {
            _context.Settings.Add(new Models.Setting
            {
                Key = "titleForHolding",
                Value = "هلدینگ "
            });

            _context.SaveChanges();

            return new JsonResult(_context.Settings);
        }

        public IActionResult Index()
        {
            var settings = _context.Settings.ToList();
            var vm = new SettingVM
            {
                Address = settings.FirstOrDefault(x => x.Key == "Address").Value,
                Cellphone = settings.FirstOrDefault(x => x.Key == "Cellphone").Value,
                Email = settings.FirstOrDefault(x => x.Key == "Email").Value,
                Instagram = settings.FirstOrDefault(x => x.Key == "Instagram").Value,
                Phone = settings.FirstOrDefault(x => x.Key == "Phone").Value,
                Telegram = settings.FirstOrDefault(x => x.Key == "Telegram").Value,
                Twitter = settings.FirstOrDefault(x => x.Key == "Twitter").Value,
                Whatsapp = settings.FirstOrDefault(x => x.Key == "Whatsapp").Value,
                WorkTime = settings.FirstOrDefault(x => x.Key == "WorkTime").Value,
                OurServices = settings.FirstOrDefault(x => x.Key == "OurServices").Value,
                QualityMaterials = settings.FirstOrDefault(x => x.Key == "QualityMaterials").Value,
                modernView = settings.FirstOrDefault(x => x.Key == "modernView").Value,
                HighStrength = settings.FirstOrDefault(x => x.Key == "HighStrength").Value,
                LifetimeSupport = settings.FirstOrDefault(x => x.Key == "LifetimeSupport").Value,
                UnderSliderText = settings.FirstOrDefault(x => x.Key == "UnderSliderText").Value,
                CounselingTitle = settings.FirstOrDefault(x => x.Key == "CounselingTitle").Value,
                CounselingContent = settings.FirstOrDefault(x => x.Key == "CounselingContent").Value,
                VideosTitle = settings.FirstOrDefault(x => x.Key == "VideosTitle").Value,
                StaticContent = settings.FirstOrDefault(x => x.Key == "StaticContent").Value,
                SailedUnits = settings.FirstOrDefault(x => x.Key == "SaiedUnits").Value,
                InterduceProject = settings.FirstOrDefault(x => x.Key == "InterduceProject").Value,
                FirstLinkName = settings.FirstOrDefault(x => x.Key == "FirstLinkName").Value,
                FirstLink = settings.FirstOrDefault(x => x.Key == "FirstLink").Value,
                SecondLinkName = settings.FirstOrDefault(x => x.Key == "SecondLinkName").Value,
                SecondLink = settings.FirstOrDefault(x => x.Key == "SecondLink").Value,
                ThirdLinkName = settings.FirstOrDefault(x => x.Key == "ThirdLinkName").Value,
                ThirdLink = settings.FirstOrDefault(x => x.Key == "ThirdLink").Value,
                ImageShow = settings.FirstOrDefault(x => x.Key == "ProjectPic").Value,
                ImageDocsShow = settings.FirstOrDefault(x => x.Key == "ImageDocs").Value,
                titleForVideos = settings.FirstOrDefault(x => x.Key == "titleForVideos").Value,
                titleForProject = settings.FirstOrDefault(x => x.Key == "titleForProject").Value,
                titleForBlogs = settings.FirstOrDefault(x => x.Key == "titleForHolding").Value,


            };

            return View(vm);
        }
        public async Task<IActionResult> Submit(SettingVM input)
        {
            var settings = _context.Settings.ToList();
            settings.FirstOrDefault(x => x.Key == "Address").Value = input.Address;
            settings.FirstOrDefault(x => x.Key == "Cellphone").Value = input.Cellphone;
            settings.FirstOrDefault(x => x.Key == "Email").Value = input.Email;
            settings.FirstOrDefault(x => x.Key == "Instagram").Value = input.Instagram;
            settings.FirstOrDefault(x => x.Key == "Phone").Value = input.Phone;
            settings.FirstOrDefault(x => x.Key == "Telegram").Value = input.Telegram;
            settings.FirstOrDefault(x => x.Key == "Twitter").Value = input.Twitter;
            settings.FirstOrDefault(x => x.Key == "Whatsapp").Value = input.Whatsapp;
            settings.FirstOrDefault(x => x.Key == "WorkTime").Value = input.WorkTime;
            settings.FirstOrDefault(x => x.Key == "OurServices").Value = input.OurServices;
            settings.FirstOrDefault(x => x.Key == "QualityMaterials").Value = input.QualityMaterials;
            settings.FirstOrDefault(x => x.Key == "modernView").Value = input.modernView;
            settings.FirstOrDefault(x => x.Key == "HighStrength").Value = input.HighStrength;
            settings.FirstOrDefault(x => x.Key == "LifetimeSupport").Value = input.LifetimeSupport;
            settings.FirstOrDefault(x => x.Key == "UnderSliderText").Value = input.UnderSliderText;
            settings.FirstOrDefault(x => x.Key == "CounselingTitle").Value = input.CounselingTitle;
            settings.FirstOrDefault(x => x.Key == "CounselingContent").Value = input.CounselingContent;
            settings.FirstOrDefault(x => x.Key == "VideosTitle").Value = input.CounselingContent;
            settings.FirstOrDefault(x => x.Key == "StaticContent").Value = input.StaticContent;
            settings.FirstOrDefault(x => x.Key == "SaiedUnits").Value = input.SailedUnits;
            settings.FirstOrDefault(x => x.Key == "InterduceProject").Value = input.InterduceProject;
            settings.FirstOrDefault(x => x.Key == "FirstLinkName").Value = input.FirstLinkName;
            settings.FirstOrDefault(x => x.Key == "FirstLink").Value = input.FirstLink;
            settings.FirstOrDefault(x => x.Key == "SecondLinkName").Value = input.SecondLinkName;
            settings.FirstOrDefault(x => x.Key == "SecondLink").Value = input.SecondLink;
            settings.FirstOrDefault(x => x.Key == "ThirdLinkName").Value = input.ThirdLinkName;
            settings.FirstOrDefault(x => x.Key == "ThirdLink").Value = input.ThirdLink;
            settings.FirstOrDefault(x => x.Key == "titleForVideos").Value = input.titleForVideos;
            settings.FirstOrDefault(x => x.Key == "titleForProject").Value = input.titleForProject;
            settings.FirstOrDefault(x => x.Key == "titleForHolding").Value = input.titleForBlogs;
            if (input.Image != null && input.Image.Length > 0)
            {
                settings.FirstOrDefault(x => x.Key == "ProjectPic").Value = await _uploaderService.SimpleUpload(input.Image, "/Img/Estate/");
            }
            if (input.ImageDocs != null && input.ImageDocs.Length > 0)
            {
                settings.FirstOrDefault(x => x.Key == "ImageDocs").Value = await _uploaderService.SimpleUpload(input.ImageDocs, "/Img/Estate/");

            }
            await _context.SaveChangesAsync();
            return Json(new { status = '1', message = "با موفقیت انجام شد" });
        }
    }
}
