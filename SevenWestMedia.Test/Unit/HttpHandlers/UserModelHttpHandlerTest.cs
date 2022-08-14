using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using SevenWestMedia.App.DataHandlers.Http;
using SevenWestMedia.App.DataHandlers.Interfaces.Http;
using SevenWestMedia.App.Validation;
using SevenWestMedia.Common.DTOs;
using SevenWestMedia.Common.Models;
using ILogger = Castle.Core.Logging.ILogger;

namespace SevenWestMedia.Test.Unit.HttpHandlers
{
    [TestFixture]
    public class UserModelHttpHandlerTest
    {
        private IHttpHandler<UserDTO> _httpHandler;
        private IValidationService<User> _userValidationService;
        private IConfiguration _configuration;
        private IMapper _mapper;
        private ILogger<UserModelHttpHandler> _logger;
        private UserModelHttpHandler _userModelHttpHandler;

        [SetUp]
        public void Setup()
        {
            _httpHandler = Substitute.For<IHttpHandler<UserDTO>>();
            _userValidationService = new ValidationService<User>();
            _configuration = Substitute.For<IConfiguration>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<UserModelHttpHandler>>();

            _userModelHttpHandler =
                new UserModelHttpHandler(_httpHandler, _userValidationService, _configuration, _mapper, _logger);
        }

        [Test]
        public async Task GetDataAsync_Successfully()
        {
            string url = "testUrl";
            _configuration["UserData:apiUrl"].Returns(url);
            IEnumerable<UserDTO> userDtos = GetAllUsersDTO();
            _httpHandler.GetAsync(Arg.Is(url)).Returns(Task.FromResult(userDtos));
            _mapper.Map<IEnumerable<User>>(Arg.Is(userDtos)).Returns(GetAllUsers());
            
            IList<User> users = await _userModelHttpHandler.GetDataAsync();
            
            AssertUsers(GetAllExpectedUsers(), users);
        }

        private void AssertUsers(IList<User> expectedUsers, IList<User> actualUsers)
        {
            Assert.AreEqual(expectedUsers.Count(), actualUsers.Count());
            for (int i = 0; i < expectedUsers.Count(); i++)
            {
                Assert.AreEqual(expectedUsers[i], actualUsers[i]);
            }
        }
        
        private IEnumerable<UserDTO> GetAllUsersDTO()
        {
            return new List<UserDTO>
            {
                new UserDTO {Id = 1, Age = 30, First = "Bob", Last = "Dylan", Gender = "M"},
                new UserDTO {Id = 2, Age = 40, First = "Pat", Last = "Benatar", Gender = "F"},
                new UserDTO {Id = 3, Age = 50, First = "Elvis", Last = "Presley", Gender = "X"},
                new UserDTO {Id = 4, Age = 30, First = "Robert", Last = "Plant", Gender = "M"},
                new UserDTO {Id = 5, Age = 200, First = "Janis", Last = "Joplin", Gender = "F"},
                new UserDTO {Id = 6, Age = 50, First = "Patti", Last = "Smith", Gender = "F"},
                new UserDTO {Id = 7, Age = 30, First = "Freddie", Last = "Mercury", Gender = "Z"},
                new UserDTO {Id = 8, Age = 40, First = "David", Last = "Bowie", Gender = "M"},
                new UserDTO {Id = 9, Age = 60, First = "Tina", Last = "Turner", Gender = "F"}
            };
        }
        
        private IEnumerable<User> GetAllUsers()
        {
            return new List<User>
            {
                new User {Id = 1, Age = 30, FirstName = "Bob", LastName = "Dylan", Gender = "M"},
                new User {Id = 2, Age = 40, FirstName = "Pat", LastName = "Benatar", Gender = "F"},
                new User {Id = 3, Age = 50, FirstName = "Elvis", LastName = "Presley", Gender = "X"},
                new User {Id = 4, Age = 30, FirstName = "Robert", LastName = "Plant", Gender = "M"},
                new User {Id = 5, Age = 200, FirstName = "Janis", LastName = "Joplin", Gender = "F"},
                new User {Id = 6, Age = 50, FirstName = "Patti", LastName = "Smith", Gender = "F"},
                new User {Id = 7, Age = 30, FirstName = "Freddie", LastName = "Mercury", Gender = "Z"},
                new User {Id = 8, Age = 40, FirstName = "David", LastName = "Bowie", Gender = "M"},
                new User {Id = 9, Age = 60, FirstName = "Tina", LastName = "Turner", Gender = "F"}
            };
        }
        
        private IList<User> GetAllExpectedUsers()
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