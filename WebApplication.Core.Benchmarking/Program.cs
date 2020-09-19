using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication.Core.API.Controllers;
using WebApplication.Core.Data;

namespace WebApplication.Core.Benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }

    public class EmployeeControllerTests
    {
        public string ConnectionString
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("WebApplication_ConnectionString");
                if (string.IsNullOrEmpty(value))
                {
                    value = (ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)).AppSettings.Settings["WebApplication_ConnectionString"].Value;
                }
                return value;
            }
        }

        [Benchmark]
        public async Task GetEmployee() 
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebApplicationContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            var controller = new EmployeesController(new WebApplicationContext(optionsBuilder.Options));

            await controller.GetEmployee(1);
        }
    }
}
