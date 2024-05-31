using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pardisan.ViewModels.API.CustomerForBuy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Pardisan.Areas.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
   
    public class CustomerForBuyController : ControllerBase
    {
        private readonly ICustomerForBuyRepository _customerForBuyRepository;
        public CustomerForBuyController(ICustomerForBuyRepository customerForBuyRepository)
        {
            _customerForBuyRepository = customerForBuyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _customerForBuyRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
      
        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert(EditCustomerForBuyVM input)
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
                response = await _customerForBuyRepository.Add(input);
            }
            else
            {
                response = await _customerForBuyRepository.Update(input);
            }

            return new JsonResult(response);
        }

        [HttpPost("Active")]
        public async Task<IActionResult> Active(int Id)
        {
            var response = await _customerForBuyRepository.Active(Id);
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Disable(int Id)
        {
            var response = await _customerForBuyRepository.Disable(Id);
            return new JsonResult(response);
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _customerForBuyRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _customerForBuyRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
    }
}
