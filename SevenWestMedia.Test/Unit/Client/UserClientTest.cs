using System.Collections.Generic;
using System.Linq;
using ConsoleClient.Users;
using NSubstitute;
using NUnit.Framework;
using SevenWestMedia.App.Manager.Interface;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.Test.Unit.Client
{
    [TestFixture]
    public class UserClientTest
    {
        private IUserManager _userManager;
        private IUserClient _userClient;

        [SetUp]
        public void Setup()
        {
            _userManager = Substitute.For<IUserManager>();
            _userClient = new UserClient(_userManager);
        }

        [Test]
        public void GetUserFullNameById_Successfully()
        {
            int userId = 1;
            User user = new User {Id = userId, Age = 30, FirstName = "Bob", LastName = "Dylan", Gender = "M"};
            _userManager.GetById(1).Returns(user);

            string userFullName = _userClient.GetUserFullNameById(userId);
            
            Assert.AreEqual($"{user.FirstName} {user.LastName}", userFullName);
        }
        
        [Test]
        public void GetAllUserFirstNamesByAge_Successfully()
        {
            int age = 30;
            IList<User> allUsers = GetAllUsers();
            _userManager.GetAll().Returns(allUsers);

            string userFirstNamesByAge = _userClient.GetAllUserFirstNamesByAge(age);

            string expectedUserNamesByAge = string.Join(",", allUsers.Where(x => x.Age == age).Select(x => x.FirstName));
            Assert.AreEqual(expectedUserNamesByAge, userFirstNamesByAge);
        }
        
        [Test]
        public void GetUserDataGroupedByAgeAndGender_Successfully()
        {
            
            IList<User> allUsers = GetAllUsers();
            _userManager.GetAll().Returns(allUsers);
            List<string> expectedGroupedUserData = new List<string>
            {
                "Age: 30, Male: 3, Female: 0",
                "Age: 40, Male: 1, Female: 2",
                "Age: 50, Male: 1, Female: 1",
                "Age: 60, Male: 0, Female: 1",
            };
            IList<string> groupedUserData = _userClient.GetUserDataGroupedByAgeAndGender();
            
            Assert.AreEqual(expectedGroupedUserData.Count(), groupedUserData.Count());
            for (int i = 0; i < groupedUserData.Count(); i++)
            {
                Assert.AreEqual(expectedGroupedUserData[i], groupedUserData[i]);
            }
        }

        private IList<User> GetAllUsers()
        {
            return new List<User>
            {
                new User {Id = 1, Age = 30, FirstName = "Bob", LastName = "Dylan", Gender = "M"},
                new User {Id = 2, Age = 40, FirstName = "Pat", LastName = "Benatar", Gender = "F"},
                new User {Id = 3, Age = 50, FirstName = "Elvis", LastName = "Presley", Gender = "M"},
                new User {Id = 4, Age = 30, FirstName = "Robert", LastName = "Plant", Gender = "M"},
                new User {Id = 5, Age = 40, FirstName = "Janis", LastName = "Joplin", Gender = "F"},
                new User {Id = 6, Age = 50, FirstName = "Patti", LastName = "Smith", Gender = "F"},
                new User {Id = 7, Age = 30, FirstName = "Freddie", LastName = "Mercury", Gender = "M"},
                new User {Id = 8, Age = 40, FirstName = "David", LastName = "Bowie", Gender = "M"},
                new User {Id = 9, Age = 60, FirstName = "Tina", LastName = "Turner", Gender = "F"}
            };
        }
    }
}