using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.PropertyOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropertyOwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyOwnerRepository _propertyOwnerRepository;
        private readonly IUnitRepository _unitRepository;

        public PropertyOwnerController(IOwnerRepository ownerRepository, IPropertyRepository propertyRepository, IPropertyOwnerRepository propertyOwnerRepository, IUnitRepository unitRepository)
        {
            _ownerRepository = ownerRepository;
            _propertyRepository = propertyRepository;
            _propertyOwnerRepository = propertyOwnerRepository;
            _unitRepository = unitRepository;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreatePropertyOwnerVM input)
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

            var ownerResult = await _ownerRepository.DoesItExist(input.OwnerId);
            if (!ownerResult)
            {
                errors.Add("مالک نا معتبر");
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));
            }

            var propertyResult = await _propertyRepository.DoesItExist(input.PropertyId);
            if (!propertyResult)
            {
                errors.Add("ملک نا معتبر");
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));
            }

            if (input.UnitId != null)
            {
                var unitResult = await _unitRepository.DoesItExist(input.UnitId.Value);
                if (!unitResult)
                {
                    errors.Add("واحد نا معتبر");
                    return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));
                }
            }

            var ownerShipResult = await _propertyOwnerRepository.CheckPropertyOrUnitAlreadyHasOwner(input);

            if (ownerShipResult)
            {
                errors.Add("ملک یا واحد انتخاب شده از قبل به مالک اختصاص داده شده");
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", errors, null));
            }

            await _propertyOwnerRepository.Create(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpGet("List")]
        public async Task<IActionResult> List(int propertyId)
        {
            var data = await _propertyOwnerRepository.List();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _propertyOwnerRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditPropertyOwnerVM input)
        {
            try
            {
                await _propertyOwnerRepository.Edit(input);
                return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
            }
            catch (Exception e)
            {
                return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), new { e.Message, e.InnerException, input }));
            }
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _propertyOwnerRepository.Delete(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
    }
}
