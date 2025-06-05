namespace MovieStoreB.Models.DTO

{
    public class Movie
    {
        public int Year { get; set; }

        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public List<string> ActorIds { get; set; } = new();

    }
}
