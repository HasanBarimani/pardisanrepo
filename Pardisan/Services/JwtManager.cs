using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Pardisan.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Pardisan.Extensions;
using Pardisan.Data;

namespace Pardisan.Interfaces
{
    public class JwtManager : IJwtManager
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public JwtManager(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PublicHelper.SecretKey));
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim("Role", "Admin"),

            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ApplicationUser GetUser()
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value;

            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            return user;
        }
    }
}
