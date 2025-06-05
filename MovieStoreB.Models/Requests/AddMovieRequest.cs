namespace MovieStoreB.Models.Requests
{
    public class AddMovieRequest
    {
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public List<string> ActorIds { get; set; } = new();
    }
}
