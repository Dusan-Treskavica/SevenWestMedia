using System.Collections.Generic;
using System.Linq;
using System.Text;
using SevenWestMedia.App.Manager.Interface;
using SevenWestMedia.Common.Models;

namespace ConsoleClient.Users
{
    public class UserClient : IUserClient
    {
        private readonly IUserManager _userManager;

        public UserClient(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public string GetUserFullNameById(int id)
        {
            User user = _userManager.GetById(id);
            
            return $"{user.FirstName} {user.LastName}";
        }
        
        public string GetAllUserFirstNamesByAge(int age)
        {
            IList<User> users = _userManager.GetAll().Where(x => x.Age == age).ToList();
            return string.Join(",", users.Select(x => x.FirstName));
        }

        public IList<string> GetUserDataGroupedByAgeAndGender()
        {
            List<string> userData = new List<string>();
            var users = _userManager.GetAll()
                .GroupBy(x => new
                {
                    x.Age
                })
                .OrderBy(grouping => grouping.Key.Age)
                .ToList();

            foreach (var user in users)
            {
                var orderedByGender = user.ToList().GroupBy(x => x.Gender).OrderByDescending(x => x.Key).ToList();
                StringBuilder builder = new StringBuilder(string.Empty);
                builder.Append($"Age: {user.Key.Age}, ");
                builder.Append($"Male: {user.Count(x => x.Gender.Equals("M"))}, ");
                builder.Append($"Female: {user.Count(x => x.Gender.Equals("F"))}");
                userData.Add(builder.ToString());
            }

            return userData;

        }
    }
}