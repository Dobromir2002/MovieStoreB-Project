using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.Configurations;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories.MongoRepositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly IMongoCollection<Actor> _actorCollection;

        public ActorRepository(IOptionsMonitor<MongoDbConfiguration> config)
        {
            var client = new MongoClient(config.CurrentValue.ConnectionString);
            var db = client.GetDatabase(config.CurrentValue.DatabaseName);
            _actorCollection = db.GetCollection<Actor>("Actors");
        }

        public async Task<IEnumerable<Actor>> GetActors(List<string> ids)
        {
            var result = await _actorCollection.FindAsync(actor => ids.Contains(actor.Id));
            return await result.ToListAsync();
        }

        public async Task<Actor?> GetByIdAsync(string id)
        {
            var result = await _actorCollection.FindAsync(a => a.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task AddAsync(Actor actor)
        {
            actor.Id = Guid.NewGuid().ToString();
            await _actorCollection.InsertOneAsync(actor);
        }

        public async Task DeleteAsync(string id)
        {
            await _actorCollection.DeleteOneAsync(a => a.Id == id);
        }

        
        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var result = await _actorCollection.FindAsync(_ => true);
            return await result.ToListAsync();
        }
    }
}
