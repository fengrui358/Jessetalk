using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ConfiguartionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultArgs = new Dictionary<string, string>
            {
                {"name", "free"},
                {"age", "30"}
            };

            //ReadCommandLine(defaultArgs, args);
            ReadJsonFile(defaultArgs, "class.json");

            Console.ReadLine();
        }

        private static void ReadCommandLine(IEnumerable<KeyValuePair<string, string>> defaultArgs, string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(defaultArgs)
                .AddCommandLine(args);

            var configuartion = builder.Build();

            Console.WriteLine($"name:{configuartion["name"]}");
            Console.WriteLine($"age:{configuartion["age"]}");
        }

        private static void ReadJsonFile(IEnumerable<KeyValuePair<string, string>> defaultArgs, string jsonFileName)
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(defaultArgs)
                .AddJsonFile(jsonFileName, false, true);

            var configuartion = builder.Build();

            Console.WriteLine($"name:{configuartion["name"]}");
            Console.WriteLine($"age:{configuartion["age"]}");
            Console.WriteLine($"ClassNo:{configuartion["ClassNo"]}");
            Console.WriteLine($"ClassDesc:{configuartion["ClassDesc"]}");

            Console.WriteLine("Students");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"name:{configuartion[$"Students:{i}:name"]}----age:{configuartion[$"Students:{i}:age"]}");
            }
        }
    }
}