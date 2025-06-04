
using System.Threading.Tasks;

namespace MovieStoreB.Infrastructure.Interfaces
{
    public interface ICacheDistributorService
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
