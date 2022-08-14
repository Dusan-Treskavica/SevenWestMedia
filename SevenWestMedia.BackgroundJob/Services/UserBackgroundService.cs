using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SevenWestMedia.App.Cache.Constants;
using SevenWestMedia.App.Cache.Interface;
using SevenWestMedia.App.DataHandlers.Interfaces.ModelHandlers;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.BackgroundJob.Services
{
    public class UserBackgroundService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IModelHandler<User> _userModelHandler;
        private IAppCache _appCache;
        
        public UserBackgroundService(IConfiguration configuration, ILogger<UserBackgroundService> logger, IModelHandler<User> userModelHandler, IAppCache appCache)
        {
            _configuration = configuration;
            _logger = logger;
            _userModelHandler = userModelHandler;
            _appCache = appCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Started UserBackground Service ...");
            while (!stoppingToken.IsCancellationRequested)
            {
                await SyncUserData();
                int interval = Int32.Parse(_configuration["UserData:fetchingInterval"]);
                await Task.Delay(TimeSpan.FromSeconds(interval), stoppingToken);
            }
        }

        private async Task SyncUserData()
        {
            IList<User> users = await _userModelHandler.GetDataAsync();
            _appCache.StoreData(CacheKeyConstants.UserCacheKey, users);
        }
    }
}