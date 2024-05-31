using Microsoft.AspNetCore.Mvc;
using Pardisan.ViewModels.Estate;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pardisan.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Pardisan.Data;
using Pardisan.ViewModels.API.CustomerForSale;
using Pardisan.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Pardisan.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CustomerForSaleController : Controller
    {
        private readonly ICustomerForSaleRepository _customerForSaleRepository;
        public CustomerForSaleController(ICustomerForSaleRepository customerForSaleRepository)
        {
            _customerForSaleRepository = customerForSaleRepository;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> Get()
        {
            var data = await _customerForSaleRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
       
        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert(UpsertCustomerForSaleVM input)
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
                response = await _customerForSaleRepository.Add(input);
            }
            else
            {
                response = await _customerForSaleRepository.Update(input);
            }

            return new JsonResult(response);
        }

        [HttpPost("Active")]
        public async Task<IActionResult> Active(int Id)
        {
            var response = await _customerForSaleRepository.Active(Id);
            return new JsonResult(response);
        }

        [HttpPost("Disable")]
        public async Task<IActionResult> Disable(int Id)
        {
            var response = await _customerForSaleRepository.Disable(Id);
            return new JsonResult(response);
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _customerForSaleRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _customerForSaleRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
    }
}
