
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Infrastructure.Interfaces
{
    public interface ICacheConsumerService
    {
        Task StartAsync(CancellationToken cancellationToken);
        List<Movie> GetAllCachedMovies();
    }
}
