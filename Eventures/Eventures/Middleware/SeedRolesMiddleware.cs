using Eventures.Data;
using Eventures.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Eventures.Middleware
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager
            )
        {
            if (await roleManager.RoleExistsAsync("User") == false)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            if (await roleManager.RoleExistsAsync("Admin") == false)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            User user = await userManager.FindByEmailAsync("Admin@Admin.Admin");
            if (user == null)
            {
                user = new User
                {
                    UserName = "Admin@Admin.Admin",
                    Email = "Admin@Admin.Admin",
                    FirstName = "Secret",
                    LastName = "Secret",
                };
                var result = await userManager.CreateAsync(user, "Admin");
                await userManager.AddToRoleAsync(user, "Admin");
            }
            await next(context);
        }
    }
}
