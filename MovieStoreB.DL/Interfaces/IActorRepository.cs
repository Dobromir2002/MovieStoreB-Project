using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Interfaces
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor?> GetByIdAsync(string id); 
        Task AddAsync(Actor actor);
        Task DeleteAsync(string id);
        Task<IEnumerable<Actor>> GetActors(List<string> ids);
    }
}


