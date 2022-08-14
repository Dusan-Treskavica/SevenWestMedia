using System.Collections.Generic;
using System.Linq;
using SevenWestMedia.Common.Models;

namespace ConsoleClient.Users
{
    public interface IUserClient
    {
        string GetUserFullNameById(int id);
        string GetAllUserFirstNamesByAge(int age);
        IList<string> GetUserDataGroupedByAgeAndGender();
    }
}