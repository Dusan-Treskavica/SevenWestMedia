using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SevenWestMedia.App.Cache.Constants;
using SevenWestMedia.App.Cache.Interface;
using SevenWestMedia.App.DataHandlers.Interfaces.ModelHandlers;
using SevenWestMedia.App.Manager;
using SevenWestMedia.App.Manager.Interface;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.Test.Unit.Manager
{
    [TestFixture]
    public class UserManagerTest
    {
        private IModelHandler<User> _userModelHandler;
        private IAppCache _appCache;
        private IUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _userModelHandler = Substitute.For<IModelHandler<User>>();
            _appCache = Substitute.For<IAppCache>();

            _userManager = new UserManager(_userModelHandler, _appCache);
        }

        private static IEnumerable<TestCaseData> GetByIdTestSource
        {
            get
            {
                yield return new TestCaseData(1, new User {Id = 1, Age = 30, FirstName = "Bob", LastName = "Dylan", Gender = "M"});
                yield return new TestCaseData(3, null);
            }
        } 
        
        private static IEnumerable<TestCaseData> GetAllTestSource
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData(GetAllUsers());
            }
        } 
        
        [TestCaseSource(nameof(GetByIdTestSource))]
        public void GetById_GetFromAppCache_Successfully(int userId, User expectedUser)
        {
            _appCache.GetDataForKey(Arg.Is(CacheKeyConstants.UserCacheKey)).Returns(GetAllUsers());

            User actualUser = _userManager.GetById(userId);

            Assert.AreEqual(expectedUser, actualUser);
            _appCache.Received(1).GetDataForKey(Arg.Is(CacheKeyConstants.UserCacheKey));
            _userModelHandler.DidNotReceive().GetDataAsync();
        }
        
        [TestCaseSource(nameof(GetByIdTestSource))]
        public void GetById_GetFromUserHandler_Successfully(int userId, User expectedUser)
        {
            _appCache.GetDataForKey(Arg.Is(CacheKeyConstants.UserCacheKey)).Returns(null);
            _userModelHandler.GetDataAsync().Returns(GetAllUsers());

            User actualUser = _userManager.GetById(userId);

            Assert.AreEqual(expectedUser, actualUser);
            _appCache.Received(1).GetDataForKey(Arg.Is(CacheKeyConstants.UserCacheKey));
            _userModelHandler.Received(1).GetDataAsync();
        }
        
        [Test]
        public void GetAll_GetFromAppCache_Successfully()
        {
            _appCache.GetDataForKey(Arg.Is(CacheKeyConstants.UserCacheKey)).Returns(GetAllUsers());

            IList<User> actualUsers = _userManager.GetAll();

            AssertUsers(GetAllUsers(), actualUsers);
            _appCache.Received(1).GetDataForKey(Arg.Is(CacheKeyConstants.UserCacheKey));
            _userModelHandler.DidNotReceive().GetDataAsync();
        }
        
        [Test]
        public void GetAll_GetFromUserHandler_Successfully()
        {
            _appCache.GetDataForKey(Arg.Is(CacheKeyConstants.UserCacheKey)).Returns(null);
            _userModelHandler.GetDataAsync().Returns(GetAllUsers());
            IList<User> actualUsers = _userManager.GetAll();

            AssertUsers(GetAllUsers(), actualUsers);
            _appCache.Received(1).GetDataForKey(Arg.Is(CacheKeyConstants.UserCacheKey));
            _userModelHandler.Received(1).GetDataAsync();
        }
        
        private void AssertUsers(IList<User> expectedUsers, IList<User> actualUsers)
        {
            Assert.AreEqual(expectedUsers.Count(), actualUsers.Count());
            for (int i = 0; i < expectedUsers.Count(); i++)
            {
                Assert.AreEqual(expectedUsers[i], actualUsers[i]);
            }
        }
        
        private static IList<User> GetAllUsers()
        {
            return new List<User>
            {
                new User {Id = 1, Age = 30, FirstName = "Bob", LastName = "Dylan", Gender = "M"},
                new User {Id = 2, Age = 40, FirstName = "Pat", LastName = "Benatar", Gender = "F"},
                new User {Id = 4, Age = 30, FirstName = "Robert", LastName = "Plant", Gender = "M"},
                new User {Id = 6, Age = 50, FirstName = "Patti", LastName = "Smith", Gender = "F"},
                new User {Id = 8, Age = 40, FirstName = "David", LastName = "Bowie", Gender = "M"},
                new User {Id = 9, Age = 60, FirstName = "Tina", LastName = "Turner", Gender = "F"},
            };
        }
    }
}