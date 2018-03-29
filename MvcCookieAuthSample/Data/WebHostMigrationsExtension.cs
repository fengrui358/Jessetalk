using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MvcCookieAuthSample.Data
{
    public static class WebHostMigrationsExtension
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    context.Database.Migrate();
                    seeder(context, services);

                    logger.LogInformation($"执行DBContext {typeof(TContext).Name} seed方法成功");
                }
                catch (Exception e)
                {
                    logger.LogError(e, $"执行DBContext {typeof(TContext).Name} seed方法失败");
                }
            }

            return host;
        }
    }
}
