using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.CustomerForm;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Pardisan.Areas.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CustomerFormsController : ControllerBase
    {
        private readonly ICustomerFormRepository _customerFormRepository;
        public CustomerFormsController(ICustomerFormRepository customerFormRepository)
        {
            _customerFormRepository = customerFormRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _customerFormRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert(EditCustomerFormVM input)
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
                response = await _customerFormRepository.Add(input);
            }
            else
            {
                response = await _customerFormRepository.Update(input);
            }

            return new JsonResult(response);
        }

        [HttpPost("Active")]
        public async Task<IActionResult> Active(int Id)
        {
            var response = await _customerFormRepository.Active(Id);
            return new JsonResult(response);
        }

        [HttpPost("Disable")]
        public async Task<IActionResult> Disable(int Id)
        {
            var response = await _customerFormRepository.Disable(Id);
            return new JsonResult(response);
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _customerFormRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _customerFormRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
    }
}
