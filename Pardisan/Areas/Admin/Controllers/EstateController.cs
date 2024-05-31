using DNTPersianUtils.Core.IranCities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Services;
using Pardisan.ViewModels;
using Pardisan.ViewModels.API.Property;
using Pardisan.ViewModels.Blog;
using Pardisan.ViewModels.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EstateController : Controller
    {
        private readonly IEstateRepository _estateRepository;

        public EstateController(IEstateRepository estateRepository)
        {
            _estateRepository = estateRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(EstateDatatableInput input)
        {
            var response = await _estateRepository.GetDatatable(input);
            return new JsonResult(response);
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            if (id != null || id == 0)
            {
                var model = await _estateRepository.GetById(id.Value);
                return View(model.Data);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(UpsertEstateVM input)
        {
            var errors = new List<string>();
            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState.Values)
                {
                    foreach (var err in item.Errors)
                    {
                        errors.Add(err.ErrorMessage);
                    }
                }
                return new JsonResult(new Response<string>(400, "خطا", errors));
            }
            var response = new Response<string>();
            if (input.Id == 0)
            {
                response = await _estateRepository.Add(input);
            }
            else
            {
                response = await _estateRepository.Update(input);
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveEstate(int Id)
        {
            var response = await _estateRepository.Active(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> DisableEstate(int Id)
        {
            var response = await _estateRepository.Disable(Id);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Gallery(int id)
        {

            var vm = new EstateForShowVM
            {
                PropertyId = id
            };
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> GetGallery(EstateImagesDatatableInput inpur)
        {

            var data = await _estateRepository.GetGallery(inpur);
            return new JsonResult(data);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int Id)
        {
            var response = await _estateRepository.DeleteImage(Id);
            return new JsonResult(response);
        }
        public async Task<IActionResult> InsertImages(EstateImageVM input)
        {
            var response = await _estateRepository.InsertImages(input);

            return new JsonResult(response);
        }
    }
}
