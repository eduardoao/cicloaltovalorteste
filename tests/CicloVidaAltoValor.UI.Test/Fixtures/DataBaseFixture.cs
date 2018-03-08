using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CicloVidaAltoValor.Application.Test.Fixtures
{
    public class DataBaseFixture : IDisposable
    {
        public IConfigurationRoot Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        public DataBaseFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var services = new ServiceCollection();

            CicloAltoValorConfiguration.ConfigureServices(services, Configuration);

            services.AddLogging();
            services.AddOptions();

            ServiceProvider = services.BuildServiceProvider();

        }
        public void Dispose()
        {
            Configuration = null;
            ServiceProvider = null;
        }
    }
}
