using Microsoft.AspNetCore.Mvc;
using Pardisan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository BlogRepository)
        {
            _blogRepository = BlogRepository;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _blogRepository.GetAllBlog();
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _blogRepository.GetBlogById(id);
            return View(response.Data);
        }
    }
}
