using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Interfaces;
using Pardisan.ViewModels.Blog;
using Pardisan.ViewModels;
using System.Threading.Tasks;
using Pardisan.ViewModels.Holding;
using Pardisan.Services;
using Pardisan.ViewModels.Estate;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HoldingController : Controller
    {
        private readonly IHoldingRepository _holdingRepository;

        public HoldingController(IHoldingRepository holdingRepository)
        {
            _holdingRepository = holdingRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetBlog(HoldingDatatableInput input)
        {
            var response = await _holdingRepository.GetDatatable(input);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            if (id != null || id == 0)
            {
                var BlogModel = await _holdingRepository.GetById(id.Value);
                return View(BlogModel.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(UpsertHoldingVM input)
        {
            var response = new Response<string>();
            if (input.Id == 0)
            {
                response = await _holdingRepository.Add(input);
            }
            else
            {
                response = await _holdingRepository.Update(input);
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetBlogById(int Id)
        {
            var response = await _holdingRepository.GetById(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveBlog(int Id)
        {
            var response = await _holdingRepository.Active(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisableBlog(int Id)
        {
            var response = await _holdingRepository.Disable(Id);
            return new JsonResult(response);
        }
        public async Task<IActionResult> Gallery(int id)
        {

            var vm = new HoldingImageVm
            {
                HoldingId = id
            };
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> GetGallery(HoldingImagesDatatableInput inpur)
        {

            var data = await _holdingRepository.GetGallery(inpur);
            return new JsonResult(data);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int Id)
        {
            var response = await _holdingRepository.DeleteImage(Id);
            return new JsonResult(response);
        }
        public async Task<IActionResult> InsertImages(HoldingImageVm input)
        {
            var response = await _holdingRepository.InsertImages(input);

            return new JsonResult(response);
        }

    }
}
