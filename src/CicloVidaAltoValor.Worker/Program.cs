using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CicloVidaAltoValor.Application;
using CicloVidaAltoValor.Application.Interfaces.Services;

namespace CicloVidaAltoValor.Worker
{
    class Program
    {
        private static ILogger _logger;

        private static IServiceProvider Initialize()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            //Register services
            var services = new ServiceCollection();

            services.ConfigureServices(configuration);

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(configuration.GetSection("Logging"))
                .AddFile(configuration.GetSection("Logging"))
                .AddDebug();


            _logger = loggerFactory.CreateLogger<Program>();

            const string culture = "pt-Br";
            var cultureBr = new System.Globalization.CultureInfo(culture);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureBr;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureBr;

            _logger.LogInformation("Initialize..");

            return serviceProvider;
        }

        static void Main(string[] args)
        {
            var serviceProvider = Initialize();
            var fileService = serviceProvider.GetService<IFileService>();
            fileService.Process().Wait();
            Thread.Sleep(TimeSpan.FromSeconds(1));            
            
        }
    }
}
