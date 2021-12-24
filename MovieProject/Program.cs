using BenchmarkDotNet.Running;
using Business.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MovieProject.Controllers;

namespace MovieProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<MovieUrlPerformanceTest>();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
