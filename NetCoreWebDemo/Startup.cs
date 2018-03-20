using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreWebDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();

            //向服务容器添加路由组件，为了让IApplicationBuilder接口可以使用扩展方法UseRouter，如果添加了Mvc组件可以不再单独添加路由组件
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //演示Mvc的路由
            app.UseRouter(builder =>
                {
                    builder.MapGet("action", context => context.Response.WriteAsync("this is action~"));
                });

            //使用MVC
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            applicationLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("Started");
            });

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("Stopped");
            });

            applicationLifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("Stopping");
            });

            //映射一个异步的Task路由
            app.Map("/task", taskApp =>
                {
                    taskApp.Run(async context => { await context.Response.WriteAsync("This is a task~~~"); });
                });

            //IApplicationBuilder使用use方法提供一个使用管道的能力
            app.Use(async (context, next)=>{
                await context.Response.WriteAsync($"1.Before start...{Environment.NewLine}");
                await next.Invoke();
            });

            app.Use(requestDelegate =>
            {
                return async context =>
                {
                    await context.Response.WriteAsync($"2.in the middle of start...{Environment.NewLine}");
                    await requestDelegate.Invoke(context);
                };
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync($"3.start...{Environment.NewLine}");
                await context.Response.WriteAsync($"{configuration["Logging:LogLevel:Default"]}{Environment.NewLine}");

                var properties = env.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var propertyInfo in properties)
                {
                    await context.Response.WriteAsync($"{propertyInfo.Name}:[{propertyInfo.GetValue(env)}]{Environment.NewLine}");
                }
            });
        }
    }
}
