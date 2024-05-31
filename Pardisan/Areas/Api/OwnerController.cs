using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.Owner;
using Pardisan.ViewModels.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly INeighborhoodOfGrowingUpRecommendationRepository _neighborhoodOfGrowingUpRecommendationRepository;

        public OwnerController(IOwnerRepository ownerRepository, INeighborhoodOfGrowingUpRecommendationRepository neighborhoodOfGrowingUpRecommendationRepository)
        {
            _ownerRepository = ownerRepository;
            _neighborhoodOfGrowingUpRecommendationRepository = neighborhoodOfGrowingUpRecommendationRepository;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var data = await _ownerRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateOwnerVM input)
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

            var result = await _ownerRepository.IsPhoneNumberAlreadyInSystem(input.PhoneNumber);
            if (result)
            {
                errors.Add("شماره تلفن از قبل در سیستم ثبت شده است");
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));
            }
            var data = await _ownerRepository.Create(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));

        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _ownerRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _ownerRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpGet("GetOwnerProperties")]
        public async Task<IActionResult> GetOwnerProperties(int id)
        {
            var data = await _ownerRepository.GetOwnerProperties(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditOwnerVM input)
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
            var result = await _ownerRepository.DoesItExist(input.Id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _ownerRepository.Edit(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ownerRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _ownerRepository.Delete(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpGet("NeighborhoodOfGrowingUpRecommendations")]
        public async Task<IActionResult> NeighborhoodOfGrowingUpRecommendations()
        {
            var data = await _neighborhoodOfGrowingUpRecommendationRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
    }
}
