using Microsoft.AspNetCore.Identity;
using Onnorokom.ShoppingCart.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Seeds
{
    public class AdminDataSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminDataSeed(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedUserAsync()
        {
            var adminUser = new ApplicationUser
            {
                UserName = "xyz@gmail.com",
                Email = "xyz@gmail.com",
                EmailConfirmed = true
            };

            IdentityResult result = null;
            var password = "Hello@Xyz";

            if (await _userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                result = await _userManager.CreateAsync(adminUser, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
