using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.Question;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ISurveyRepository _surveyRepository;

        public QuestionController(IQuestionRepository questionRepository, ISurveyRepository surveyRepository)
        {
            _questionRepository = questionRepository;
            _surveyRepository = surveyRepository;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateQuestionVM input)
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
            var result = await _surveyRepository.DoesItExist(input.SurveyId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _questionRepository.Create(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromBody] EditQuestionVM input)
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

            var result = await _questionRepository.DoesItExist(input.Id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _questionRepository.Edit(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _questionRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _questionRepository.Delete(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("DeleteOption")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            await _questionRepository.DeleteOption(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
    }
}
