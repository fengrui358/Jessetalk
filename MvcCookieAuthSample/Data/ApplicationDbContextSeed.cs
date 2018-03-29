using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MvcCookieAuthSample.Models;

namespace MvcCookieAuthSample.Data
{
    public class ApplicationDbContextSeed
    {
        private UserManager<ApplicationUser> _userManager;

        public async Task SeedAsync(ApplicationDbContext context, IServiceProvider services)
        {
            if (!context.Users.Any())
            {
                _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var defaultUser = new ApplicationUser
                {
                    UserName = "free",
                    Email = "free358@163.com",
                    NormalizedUserName = "admin"
                };

                var resul = await _userManager.CreateAsync(defaultUser, "Free@12345678");
                if (!resul.Succeeded)
                {
                    throw new Exception("初始化默认用户失败");
                }
            }
        }
    }
}
