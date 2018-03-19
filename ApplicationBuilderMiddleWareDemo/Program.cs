using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationBuilderMiddleWareDemo
{
    /// <summary>
    /// IApplicationBuilder的Use创建管道的Demo
    /// </summary>
    class Program
    {
        private static readonly List<Func<RequestDelegate, RequestDelegate>> List = new List<Func<RequestDelegate, RequestDelegate>>();

        static void Main(string[] args)
        {
            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine($"step 1... task{Thread.CurrentThread.ManagedThreadId}");
                    return next.Invoke(context);
                };
            });

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine($"step 2... task{Thread.CurrentThread.ManagedThreadId}");
                    return next.Invoke(context);
                };
            });

            RequestDelegate end = context =>
            {
                Console.WriteLine($"end... task{Thread.CurrentThread.ManagedThreadId}");
                return Task.CompletedTask;
            };

            List.Reverse();
            foreach (var middleWare in List)
            {
                end = middleWare.Invoke(end);
            }

            Console.WriteLine($"传入HttpContext  task{Thread.CurrentThread.ManagedThreadId}");

            end.Invoke(new Context());
            Console.ReadLine();
        }

        private static void Use(Func<RequestDelegate, RequestDelegate> middleWare)
        {
            List.Add(middleWare);
        }
    }
}
