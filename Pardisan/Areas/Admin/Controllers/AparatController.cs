using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Interfaces;
using Pardisan.Migrations;
using Pardisan.ViewModels.Holding;
using Pardisan.ViewModels;
using System.Threading.Tasks;
using Pardisan.ViewModels.Aparat;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AparatController : Controller
    {
        private readonly IAparatRepository _aparatRepository;
        public AparatController(IAparatRepository aparatRepository)
        {
            _aparatRepository= aparatRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetBlog(AparatDatatableInput input)
        {
            var response = await _aparatRepository.GetDatatable(input);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            if (id != null || id == 0)
            {
                var BlogModel = await _aparatRepository.GetById(id.Value);
                return View(BlogModel.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(UpsertAparatVM input)
        {
            var response = new Response<string>();
            if (input.Id == 0)
            {
                response = await _aparatRepository.Add(input);
            }
            else
            {
                response = await _aparatRepository.Update(input);
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetBlogById(int Id)
        {
            var response = await _aparatRepository.GetById(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveBlog(int Id)
        {
            var response = await _aparatRepository.Active(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisableBlog(int Id)
        {
            var response = await _aparatRepository.Disable(Id);
            return new JsonResult(response);
        }
    }
}
