using Microsoft.Extensions.DependencyInjection;
using SevenWestMedia.App.Cache;
using SevenWestMedia.App.Cache.Interface;
using SevenWestMedia.App.DataHandlers.Http;
using SevenWestMedia.App.DataHandlers.Interfaces.Http;
using SevenWestMedia.App.DataHandlers.Interfaces.ModelHandlers;
using SevenWestMedia.App.Manager;
using SevenWestMedia.App.Manager.Interface;
using SevenWestMedia.App.Profiles;
using SevenWestMedia.App.Validation;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.App.DI
{
    public static class ServiceCollectionExtension
    {
        public static void AddSevenWestMediaAppServices(this IServiceCollection services)
        {
            //data handler registration
            services.AddScoped(typeof(IHttpHandler<>), typeof(HttpHandler<>));
            services.AddScoped<IModelHandler<User>, UserModelHttpHandler>();
            services.AddHttpClient();
            
            //cache registration
            services.AddSingleton<IAppCache, AppCache>();
            
            //validation service registration
            services.AddScoped(typeof(IValidationService<>), typeof(ValidationService<>));
            
            //managers registration
            services.AddScoped<IUserManager, UserManager>();
            
            //automapper profiles registration
            services.AddAutoMapper(typeof(UserDTOProfile));
        }
    }
}