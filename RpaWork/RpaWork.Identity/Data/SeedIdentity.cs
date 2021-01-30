using Microsoft.AspNetCore.Identity;
using RpaWork.Identity.Entities;
using System.Threading.Tasks;

namespace RpaWork.Identity.Data
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var username = "Admin";
            var email = "admin@admin";
            var password = "Admin123";
            var role = "admin";

            if (await userManager.FindByNameAsync(username) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));

                var user = new ApplicationUser()
                {
                    UserName = username,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Age = 20,
                    Email = email,
                };
                
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
