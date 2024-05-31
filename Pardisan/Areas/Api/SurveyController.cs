using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.Survey;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IPropertyRepository _propertyRepository;

        public SurveyController(ISurveyRepository surveyRepository, IPropertyRepository propertyRepository)
        {
            _surveyRepository = surveyRepository;
            _propertyRepository = propertyRepository;
        }
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var data = await _surveyRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateSurveyVM input)
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

            var surveyId = await _surveyRepository.Create(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), surveyId));

        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _surveyRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _surveyRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditSurveyVM input)
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
            var result = await _surveyRepository.DoesItExist(input.Id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _surveyRepository.Edit(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _surveyRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _surveyRepository.Delete(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpGet("AnswerByCode")]
        public async Task<IActionResult> AnswerByCode(int id,string code)
        {
            var result = await _surveyRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _surveyRepository.DetailWithAnswers(id, code);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpGet("AnswerInGeneral")]
        public async Task<IActionResult> AnswerInGeneral(int id)
        {
            var result = await _surveyRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _surveyRepository.DetailWithAnswers(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }

    }
}
