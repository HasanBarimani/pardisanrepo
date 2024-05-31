using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var data = await _propertyRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreatePropertyVM input)
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
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));
            }
            var result = await _propertyRepository.Create(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), result));

        }
        [HttpPost("SubmitFullProperty")]
        public async Task<IActionResult> SubmitFullProperty([FromBody]FullPropertyVM input)
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
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));
            }

            await _propertyRepository.SaveFullProperty(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), new { }));

        }
        [HttpGet("GetFullProperty")]
        public async Task<IActionResult> GetFullProperty(int id)
        {
            var data = await _propertyRepository.GetFullProperty(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));

        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _propertyRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _propertyRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditPropertyVM input)
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
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));
            }
            var result = await _propertyRepository.DoesItExist(input.Id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _propertyRepository.Edit(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));

        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _propertyRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _propertyRepository.Delete(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpGet("Gallery")]
        public async Task<IActionResult> Gallery(int id)
        {
            var result = await _propertyRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _propertyRepository.Images(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));

        }
        [HttpPost("AddItemToGallery")]
        public async Task<IActionResult> AddItemToGallery(AddToGalleryVM input)
        {
            var result = await _propertyRepository.DoesItExist(input.PropertyId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _propertyRepository.AddItemToGallery(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("AddListToGallery")]
        public async Task<IActionResult> AddListToGallery(AddListToGallery input)
        {
            var result = await _propertyRepository.DoesItExist(input.PropertyId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            if (input.Files == null)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "فایلی دریافت نشده است" }, null));

            foreach (var item in input.Files)
            {
                await _propertyRepository.AddItemToGallery(new AddToGalleryVM { Item = item.Item, PropertyId = input.PropertyId });
            }
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("RemoveImageFromGallery")]
        public IActionResult RemoveImageFromGallery(int id)
        {

            _propertyRepository.RemoveImageFromGallery(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
    }
}
