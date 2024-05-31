using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels;
using Pardisan.ViewModels.Blog;
using Pardisan.ViewModels.Page;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetPages(PagesDatatableInput input)
        {
            var response = await _pageRepository.GetPagesDatatable(input);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
         
            if (id != null || id == 0)
            {
                var pageModel = await _pageRepository.GetPagesById(id.Value);

                return View(pageModel.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(UpsertPageVM input)
        {
            var response = new Response<string>();
            if (input.Id == 0)
            {
                response = await _pageRepository.AddPages(input);
            }
            else
            {
                response = await _pageRepository.UpdatePages(input);
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetPagesById(int Id)
        {
            var response = await _pageRepository.GetPagesById(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActivePages(int Id)
        {
            var response = await _pageRepository.ActivePages(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisablePages(int Id)
        {
            var response = await _pageRepository.DisablePages(Id);
            return new JsonResult(response);
        }

    }
}
