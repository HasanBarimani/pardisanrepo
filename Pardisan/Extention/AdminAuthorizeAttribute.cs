using Pardisan.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Pardisan.Extentions
{
    public class AdminAuthorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }
        public void OnActionExecuting(ActionExecutingContext context)
        {

            var errors = new List<string>();
            errors.Add("ادمین نامعتبر");



            var _context = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

            var userName = context.HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value;

            userName = userName ?? "notvalid:)";

            var user = _context.Users
           .FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                context.Result = new UnauthorizedObjectResult(new JsonResponse(StatusCode.UnAuthorized, "خطا", errors, null));

        }

    }
}
