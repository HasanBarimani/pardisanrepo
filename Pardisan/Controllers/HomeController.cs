using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _blogRepository;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository,ApplicationDbContext context)
        {
            _logger = logger;
            _blogRepository = blogRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            //var s = _context.PropertyOwners.ToList();
            //foreach (var item in s)
            //{
            //    item.IsActive = false;
            //}
            //_context.SaveChanges();
            var blogs = await _blogRepository.GetAllBlog();

            var vm = new HomeVM
            {
                Blogs = blogs.Data,
            };
            return View(vm);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Achievement()
        {
            return View();
        }

        public IActionResult Certificate()
        {
            return View();
        }

        public IActionResult ContactUS()
        {
            return View();
        }
        
        public IActionResult OurTeam()
        {
            return View();
        }
        
        public IActionResult Services()
        {
            return View();
        }
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
