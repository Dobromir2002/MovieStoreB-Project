using MovieStoreB.Models.DTO;


namespace MovieStoreB.Models.Responses
{
    public class FullMovieDetails
    {
        public int Year { get; set; }
        

        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public List<Actor> Actors { get; set; } = new();

    }
}
