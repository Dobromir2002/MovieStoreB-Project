using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.BL
{
    public class ActorService
    {
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public Task<IEnumerable<Actor>> GetAllAsync() => _actorRepository.GetAllAsync();
        public Task<Actor?> GetByIdAsync(string id) => _actorRepository.GetByIdAsync(id);
        public Task AddAsync(Actor actor) => _actorRepository.AddAsync(actor);
        public Task DeleteAsync(string id) => _actorRepository.DeleteAsync(id);
    }
}
