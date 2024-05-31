using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnswerSurveyController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;

        public AnswerSurveyController(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        [HttpGet("CheckSurveyValidity")]
        public async Task<IActionResult> CheckSurveyValidity(string code)
        {
            var result = await _surveyRepository.CheckSurveyValidity(code);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), result));
        }

        [HttpGet("SurveyDetial")]
        public async Task<IActionResult> SurveyDetail(int surveyId, string code)
        {
            var result = await _surveyRepository.DetailWithAnswers(surveyId,code);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), result));
        }
        [HttpPost("SubmitAnswer")]
        public async Task<IActionResult> SubmitAnswer(SubmitAnswerVM input)
        {
            var result = await _surveyRepository.DoesItExist(input.SurveyId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var finalResult = await _surveyRepository.Answer(input);
            if(!finalResult)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), result));
        }
    }
}
