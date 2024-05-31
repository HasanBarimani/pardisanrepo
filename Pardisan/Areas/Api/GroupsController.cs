using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IOwnerRepository _ownerRepository;
        public GroupsController(IGroupRepository groupRepository, IOwnerRepository ownerRepository)
        {
            _groupRepository = groupRepository;
            _ownerRepository = ownerRepository;
        }
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var data = await _groupRepository.GetAll();
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateGroupVM input)
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

            var data = await _groupRepository.Create(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _groupRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            var data = await _groupRepository.Detail(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), data));
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditGroupVM input)
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
            var result = await _groupRepository.DoesItExist(input.Id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _groupRepository.Edit(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _groupRepository.DoesItExist(id);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));

            await _groupRepository.Delete(id);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember(AddMemberVM input)
        {
            var result = await _groupRepository.DoesItExist(input.GroupId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));
            
            var ownerResult = await _ownerRepository.DoesItExist(input.OwnerId);
            if (!ownerResult)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));


            var membershipResult = await _groupRepository.IsOwnerAMemberOfTheGroup(input);
            if (membershipResult)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "این مالک از قبل عضو گروه بوده است" }, null));


            await _groupRepository.AddMember(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        } 
        [HttpPost("RemoveMember")]
        public async Task<IActionResult> RemoveMember(AddMemberVM input)
        {
            var result = await _groupRepository.DoesItExist(input.GroupId);
            if (!result)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));
            
            var ownerResult = await _ownerRepository.DoesItExist(input.OwnerId);
            if (!ownerResult)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "اطلاعات مورد نظر پیدا نشد" }, null));


            var membershipResult = await _groupRepository.IsOwnerAMemberOfTheGroup(input);
            if (!membershipResult)
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی", new List<string> { "این مالک از قبل عضو گروه نبوده است" }, null));


            await _groupRepository.RemoveMember(input);
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK));
        }
    }
}
