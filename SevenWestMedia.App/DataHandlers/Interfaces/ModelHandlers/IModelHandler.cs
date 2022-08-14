using System.Collections.Generic;
using System.Threading.Tasks;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.App.DataHandlers.Interfaces.ModelHandlers
{
    public interface IModelHandler<T>
    {
        Task<IList<User>> GetDataAsync(); 
    }
}