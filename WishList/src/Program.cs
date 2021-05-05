using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WishList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private class Tuples
        {
            public void UnnamedTuple()
            {
                var unnamed = ("some", "value", 322);
                Console.WriteLine(unnamed.Item1, unnamed.Item2, unnamed.Item3);
            }
            public void Named()
            {
                var named = (name: "Foo", age: 23);
                Console.WriteLine("Foo", named.name);
                Console.WriteLine(23.ToString(), named.age);
                var (name, age) = named;
                Console.WriteLine(name, age);
            }
        }
    }
}
