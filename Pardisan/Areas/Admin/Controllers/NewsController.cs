using Pardisan.Interfaces;
using Pardisan.Models.News;
using Pardisan.ViewModels;
using Pardisan.ViewModels.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetNews(NewsDatatableInput input)
        {
            var response = await _newsRepository.GetNewsDatatable(input);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ViewData["CategoryId"] = await _newsRepository.CategoryListForDropdown();
            if (id != null || id == 0)
            {
                var newsModel = await _newsRepository.GetNewsById(id.Value);
                
            return View(newsModel.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(UpsertNewsVM input)
        {
            var response = new Response<string>();
            if (input.Id == 0)
            {
                response = await _newsRepository.AddNews(input);
            }
            else
            {
                response = await _newsRepository.UpdateNews(input);
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetNewsById(int Id)
        {
            var response = await _newsRepository.GetNewsById(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveNews(int Id)
        {
            var response = await _newsRepository.ActiveNews(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisableNews(int Id)
        {
            var response = await _newsRepository.DisableNews(Id);
            return new JsonResult(response);
        }




        #region News Category
        public IActionResult NewsCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetNewsCategory(NewsCategoryDatatableInput input)
        {
            var response = await _newsRepository.GetNewsCategoryDatatable(input);
            return new JsonResult(response);
        }


        [HttpPost]
        public async Task<IActionResult> UpsertNewsCategory(UpsertNewsCategoryVM input)
        {
            var response = new Response<string>();
            if (input.Id == 0)
            {
                response = await _newsRepository.AddNewsCategory(input);
            }
            else
            {
                response = await _newsRepository.UpdateNewsCategory(input);
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetNewsCategoryById(int Id)
        {
            var response = await _newsRepository.GetNewsCategoryById(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveNewsCategory(int Id)
        {
            var response = await _newsRepository.ActiveNewsCategory(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisableNewsCategory(int Id)
        {
            var response = await _newsRepository.DisableNewsCategory(Id);
            return new JsonResult(response);
        }


        #endregion
    }
}
