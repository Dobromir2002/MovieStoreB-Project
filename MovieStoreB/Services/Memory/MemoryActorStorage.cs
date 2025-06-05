using System.Collections.Concurrent;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Services.Memory
{
    public static class MemoryActorStorage
    {
        private static readonly ConcurrentDictionary<string, Actor> _cache = new();

        public static void AddOrUpdate(Actor actor)
        {
            if (!string.IsNullOrEmpty(actor.Id))
                _cache[actor.Id] = actor;
        }

        public static IEnumerable<Actor> GetAll() => _cache.Values;
    }
}
