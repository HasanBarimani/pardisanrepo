using Pardisan.Data;
using Pardisan.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan
{
    public class Seed
    {
        public static async Task SeedData(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager,ApplicationDbContext Context)
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            var adminUserName = "Admin";
            var adminUser = new ApplicationUser
            {
                AccessFailedCount = 0,
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FirstName = "admin",
                LastName = "admin",
                NormalizedUserName = "admin".Normalize(),
                UserName = "admin",
            };

            await _userManager.CreateAsync(adminUser, "Admin@123");
            adminUser = await _userManager.FindByNameAsync(adminUserName);
            await _userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
