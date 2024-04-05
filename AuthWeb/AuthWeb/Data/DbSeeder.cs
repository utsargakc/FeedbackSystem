using AuthWeb.Constants;
using Microsoft.AspNetCore.Identity;
using System;

namespace AuthWeb.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //seed roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var user = new ApplicationUser
            {
                UserName = "ram@gmail.com",
                Email = "ram@gmail.com",
                SecurityQn1=" ",
                SecurityAns1 = " ",
                SecurityQn2=" ",
                SecurityAns2 = " ",
                SecurityQn3=" ",
                SecurityAns3 = " ",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }

    }
}
