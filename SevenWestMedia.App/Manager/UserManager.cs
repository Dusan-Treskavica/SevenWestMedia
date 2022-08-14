using System;
using System.Collections.Generic;
using System.Linq;
using SevenWestMedia.App.Cache.Constants;
using SevenWestMedia.App.Cache.Interface;
using SevenWestMedia.App.DataHandlers.Interfaces.ModelHandlers;
using SevenWestMedia.App.Manager.Interface;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.App.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IModelHandler<User> _userModelHandler;
        private readonly IAppCache _appCache;
        
        public UserManager(IModelHandler<User> userModelHandler, IAppCache appCache)
        {
            _userModelHandler = userModelHandler;
            _appCache = appCache;
        }

        public User GetById(int id)
        {
            IList<User> users =  _appCache.GetDataForKey(CacheKeyConstants.UserCacheKey) as IList<User> ??
                                 _userModelHandler.GetDataAsync().Result;
            return users.FirstOrDefault(x => x.Id == id);
        }

        public IList<User> GetAll()
        {
            return  _appCache.GetDataForKey(CacheKeyConstants.UserCacheKey) as IList<User> ??
                    _userModelHandler.GetDataAsync().Result;
        }
    }
}