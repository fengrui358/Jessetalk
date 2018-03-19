using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NetCoreWebDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //禁用热更新
                .ConfigureAppConfiguration(config => { config.AddJsonFile("appsettings.json", false, false); })
                //使用自定义地址
                //.UseUrls("http://localhost:50001", "http://localhost:50002")
                .UseStartup<Startup>()
                .Build();
    }
}
