using Eventures.Data;
using Eventures.Data.Models;
using Eventures.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Middleware
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHashService hashService;

        public SeedRolesMiddleware(RequestDelegate next, IHashService hashService)
        {
            this.next = next;
            this.hashService = hashService;
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

            if (!dbContext.Users.Any(x => x.UserName == "Admin"))
            {
                var user = new User
                {
                    UserName = "Admin",
                    Email = "Admin@Admin.Admin",
                    PasswordHash = hashService.GetHash("Admin"),
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
