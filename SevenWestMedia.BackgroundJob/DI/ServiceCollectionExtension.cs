using Microsoft.Extensions.DependencyInjection;
using SevenWestMedia.BackgroundJob.Services;

namespace SevenWestMedia.BackgroundJob.DI
{
    public static class ServiceCollectionExtension
    {
        public static void AddSevenWestMediaBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<UserBackgroundService>();
        }
    }
}