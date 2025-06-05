namespace MovieStoreB.Models.DTO
{
    public class Actor
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int BirthYear { get; set; }
        public string Name { get; set; } = null!;
    }
}
