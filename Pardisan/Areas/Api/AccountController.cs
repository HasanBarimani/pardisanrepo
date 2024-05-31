using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Extentions;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IJwtManager _jwtManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IJwtManager jwtManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _jwtManager = jwtManager;
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInVM input)
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
            var user = await _context.Users.FirstOrDefaultAsync(d => d.UserName == input.UserName);
            if (user == null)
            {
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی",
                    new List<string> {
                    "نام کاربری یا رمز عبور اشتباه است"
                }, null));
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin)
            {
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی",
                    new List<string> {
                    "نام کاربری یا رمز عبور اشتباه است"
                }, null));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, true);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new JsonResponse(Pardisan.Data.StatusCode.BadRequest, "خطا در اطلاعات ارسالی",
                    new List<string> {
                    "نام کاربری یا رمز عبور اشتباه است"
                }, null));
            }
            else
            {

                var userInfo = new JwtUser
                {
                    Token = _jwtManager.CreateToken(user),
                    UserName = user.UserName,
                    Role = "Admin"
                };
                return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), new
                {
                    userInfo.Token,
                    userInfo.UserName,
                    userInfo.Role,
                }));
            }
        }
        [HttpGet("TestToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult TestToken()
        {
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), true));
        }
    }
}
