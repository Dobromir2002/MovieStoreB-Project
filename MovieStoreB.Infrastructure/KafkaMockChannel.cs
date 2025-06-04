
using System.Collections.Concurrent;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Infrastructure
{
    public static class KafkaMockChannel
    {
        public static readonly BlockingCollection<Movie> CacheTopic = new();
    }
}
