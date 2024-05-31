using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Interfaces;
using Pardisan.ViewModels;
using Pardisan.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetBlog(BlogDatatableInput input)
        {
            var response = await _blogRepository.GetBlogDatatable(input);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ViewData["CategoryId"] = await _blogRepository.CategoryListForDropdown();
            if (id != null || id == 0)
            {
                var BlogModel = await _blogRepository.GetBlogById(id.Value);
                return View(BlogModel.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(UpsertBlogVM input)
        {
            var response = new Response<string>();
            if (input.Id == 0)
            {
                response = await _blogRepository.AddBlog(input);
            }
            else
            {
                response = await _blogRepository.UpdateBlog(input);
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetBlogById(int Id)
        {
            var response = await _blogRepository.GetBlogById(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveBlog(int Id)
        {
            var response = await _blogRepository.ActiveBlog(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisableBlog(int Id)
        {
            var response = await _blogRepository.DisableBlog(Id);
            return new JsonResult(response);
        }




        #region Blog Category
        public IActionResult BlogCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetBlogCategory(BlogCategoryDatatableInput input)
        {
            var response = await _blogRepository.GetBlogCategoryDatatable(input);
            return new JsonResult(response);
        }


        [HttpPost]
        public async Task<IActionResult> UpsertBlogCategory(UpsertBlogCategoryVM input)
        {
            var response = new Response<string>();
            if (input.Id == 0)
            {
                response = await _blogRepository.AddBlogCategory(input);
            }
            else
            {
                response = await _blogRepository.UpdateBlogCategory(input);
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetBlogCategoryById(int Id)
        {
            var response = await _blogRepository.GetBlogCategoryById(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveBlogCategory(int Id)
        {
            var response = await _blogRepository.ActiveBlogCategory(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisableBlogCategory(int Id)
        {
            var response = await _blogRepository.DisableBlogCategory(Id);
            return new JsonResult(response);
        }


        #endregion
    }
}
