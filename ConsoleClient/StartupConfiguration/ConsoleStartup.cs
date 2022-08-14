using System.IO;
using ConsoleClient.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SevenWestMedia.App.DI;
using SevenWestMedia.BackgroundJob.DI;

namespace ConsoleClient.StartupConfiguration
{
    public class ConsoleStartup : Startup
    {
        public IHostBuilder GetHostBuilder()
        {
            BuildConfiguration();
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices);
            DecorateHostBuilder(hostBuilder);

            return hostBuilder;
        }
        
        protected override void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSevenWestMediaAppServices();
            services.AddSevenWestMediaBackgroundServices();

            services.AddLogging(builder => builder.AddConsole());
            services.AddScoped<IUserClient, UserClient>();
        }

        protected override void BuildConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }

        protected override void DecorateHostBuilder(IHostBuilder hostBuilder)
        {
            hostBuilder
                .UseConsoleLifetime();
        }
    }
}