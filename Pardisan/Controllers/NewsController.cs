using Microsoft.AspNetCore.Mvc;
using Pardisan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _newsRepository.GetAllNews();
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _newsRepository.GetNewsById(id);
            return View(response.Data);
        }
    }
}
