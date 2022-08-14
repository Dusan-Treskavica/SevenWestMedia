using System.Collections.Generic;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.App.Manager.Interface
{
    public interface IUserManager
    {
        User GetById(int id);
        IList<User> GetAll();
    }
}