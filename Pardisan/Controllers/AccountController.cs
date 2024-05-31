using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Models;
using Pardisan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Login(string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;

            if (_signInManager.IsSignedIn(User))
            {
                return Redirect(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInAdmin(LoginVM input)
        {
            input.ReturnUrl ??= Url.Content("/admin");

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(d => d.UserName == input.UserName);
                if (user == null)
                {
                    return Json(new { Status = 0, Error = "کاربر یافت نشد" });
                }

                var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return new JsonResult(new { Status = 1, ReturnUrl = input.ReturnUrl });
                }
                else
                {
                    return new JsonResult(new { Status = 0, Error = "نام کاربری یا پسورد اشتباه" });
                }

            }
            return new JsonResult(new { Status = 0, Error = "خطایی رخ داده" });
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            returnUrl = returnUrl ?? Url.Content("/");
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Json((Status: 1, Message: "Logged Out"));
        }
    }

}
