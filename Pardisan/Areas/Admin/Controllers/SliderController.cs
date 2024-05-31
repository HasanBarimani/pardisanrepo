using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pardisan.Data;
using Pardisan.Interface;
using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Pardisan.Extensions.PublicHelper;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SliderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploaderService _uploaderService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderController(ApplicationDbContext context, IUploaderService uploaderService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _uploaderService = uploaderService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var data = _context.Sliders.Where(x => x.IsActive.Value).ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> CreateSlider(IFormFile image)
        {

            var host = _webHostEnvironment.WebRootPath;
            var path = FilePath.SliderImagePath;
            var pathWaterMark = FilePath.WaterMarkImagePath;
            var pathShow = FilePath.SliderImagePathForShow;
            var slider = new Slider
            {
                Url = pathShow + FileUploader.UploadImage(image, Path.Combine(host, path), Path.Combine(host, pathWaterMark), false, compression: 30).result


            };
            _context.Sliders.Add(slider);
            await _context.SaveChangesAsync();
            return Json(new { status = '1', message = "با موفقیت انجام شد" });
        }
        public IActionResult DisableSlider(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            slider.IsActive = false;
            _context.SaveChanges();
            return Json(new { status = '1', message = "با موفقیت انجام شد" });

        }
    }
}
