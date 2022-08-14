using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenWestMedia.App.DataHandlers.Interfaces.Http
{
    public interface IHttpHandler<T> 
    {
        Task<IEnumerable<T>> GetAsync(string url); 
    }
}