
using System.Threading.Tasks;

namespace MovieStoreB.BL.Interfaces
{
    public interface IExternalMovieApiService
    {
        Task FetchAndSaveExternalMoviesAsync();
    }
}
