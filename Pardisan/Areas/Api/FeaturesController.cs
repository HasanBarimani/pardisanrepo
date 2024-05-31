using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModel.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Admin.Controllers
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IFeatureOptionRepository _optionRepository;

        public FeaturesController(IFeatureRepository featureRepository, IFeatureOptionRepository optionRepository)
        {
            _featureRepository = featureRepository;
            _optionRepository = optionRepository;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateFeatureVM input)
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
            if (input.Options == null || input.Options.Count == 0)
            {
                errors.Add("لطفا پاسخ را وارد کنید");
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));

            }

            await _featureRepository.Create(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));

        }
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var list = await _featureRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), list));
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int featureId)
        {

            var result = await _featureRepository.DoesItExist(featureId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _featureRepository.Detail(featureId);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditFeatureVM input)
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
            var result = await _featureRepository.DoesItExist(input.Id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _featureRepository.Edit(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int featureId)
        {
            var result = await _featureRepository.DoesItExist(featureId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _featureRepository.Delete(featureId);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
    }
}
