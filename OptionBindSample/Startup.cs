using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OptionBindSample
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Class>(Configuration);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    var myClass = new Class();
            //    Configuration.Bind(myClass);

            //    await context.Response.WriteAsync("Hello World!");

            //    await context.Response.WriteAsync($"ClassNo:{myClass.ClassNo}");
            //    await context.Response.WriteAsync($"ClassDesc:{myClass.ClassDesc}");

            //    await context.Response.WriteAsync("Students");
            //    for (int i = 0; i < myClass.Students.Count; i++)
            //    {
            //        await context.Response.WriteAsync($"name:{myClass.Students[i].Name}----age:{myClass.Students[i].Age}");
            //    }
            //});
        }
    }
}
