using System.IO;
using ConsoleClient.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SevenWestMedia.App.DI;

namespace ConsoleClient.StartupConfiguration
{
    public abstract class Startup
    {

        public IHost GetHost()
        {
            BuildConfiguration();
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices);
            DecorateHostBuilder(hostBuilder);

            return hostBuilder.Build();
        }

        protected abstract void BuildConfiguration();

        protected abstract void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services);

        protected abstract void DecorateHostBuilder(IHostBuilder hostBuilder);

    }
}