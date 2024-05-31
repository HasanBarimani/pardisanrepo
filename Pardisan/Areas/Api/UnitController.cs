using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UnitController : ControllerBase
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPropertyOwnerRepository _propertyOwnerRepository;

        public UnitController(IUnitRepository unitRepository, IPropertyRepository propertyRepository, IOwnerRepository ownerRepository, IPropertyOwnerRepository propertyOwnerRepository)
        {
            _unitRepository = unitRepository;
            _propertyRepository = propertyRepository;
            _ownerRepository = ownerRepository;
            _propertyOwnerRepository = propertyOwnerRepository;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(int propertyId)
        {
            var data = await _unitRepository.GetByProperty(propertyId);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUnitVM input)
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

            var result = await _propertyRepository.DoesItExist(input.PropertyId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var unitId = await _unitRepository.Create(input);

            if (input.OwnerId != null)
            {
                var ownerResult = await _ownerRepository.DoesItExist(input.OwnerId.Value);
                if (!ownerResult)
                    return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

                await _propertyOwnerRepository.Create(new ViewModels.API.PropertyOwner.CreatePropertyOwnerVM
                {
                    ContractSigningDate = DateTime.Now,
                    Description = "",
                    DisSatisfactionLevelReason = "",
                    HasDesireToIntroduce = false,
                    HasIntroductionLeadsToPurchase = false,
                    OwnerId = input.OwnerId.Value,
                    PropertyId = input.PropertyId,
                    SatisfactionLevel = Models.SatisfactionLevel.Medium,
                    UnitId = unitId
                });
            }
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));

        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _unitRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _unitRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditUnitVM input)
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

            var result = await _propertyRepository.DoesItExist(input.PropertyId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));


            await _unitRepository.Edit(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));

        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _unitRepository.Delete(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
    }
}
