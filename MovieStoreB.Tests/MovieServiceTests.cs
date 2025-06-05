using Moq;
using MovieStoreB.BL;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Tests
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;

        private readonly List<Movie> _movies = new()
        {
            new Movie
            {
                Id = "c3bd1985-792e-4208-af81-4d154bff15c8",
                Title = "Movie 1",
                Year = 2021,
                ActorIds = new() {
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "baac2b19-bbd2-468d-bd3b-5bd18aba98d7"
                }
            },
            new Movie
            {
                Id = "4c304bec-f213-47b5-8ae0-9df4a4eb3b99",
                Title = "Movie 2",
                Year = 2022,
                ActorIds = new() {
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "5c93ba13-e803-49c1-b465-d471607e97b3"
                }
            }
        };

        public MovieServiceTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMovie_WhenExists()
        {
            // Arrange
            var movieId = _movies[0].Id;

            _movieRepositoryMock
                .Setup(x => x.GetMovieById(It.IsAny<string>()))
                .ReturnsAsync((string id) => _movies.FirstOrDefault(x => x.Id == id));

            var movieService = new MovieService(_movieRepositoryMock.Object);

            // Act
            var result = await movieService.GetByIdAsync(movieId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result?.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var movieId = "non-existent-id";

            _movieRepositoryMock
                .Setup(x => x.GetMovieById(It.IsAny<string>()))
                .ReturnsAsync((string id) => _movies.FirstOrDefault(x => x.Id == id));

            var movieService = new MovieService(_movieRepositoryMock.Object);

            // Act
            var result = await movieService.GetByIdAsync(movieId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllMovies()
        {
            // Arrange
            _movieRepositoryMock
                .Setup(x => x.GetMovies())
                .ReturnsAsync(_movies);

            var movieService = new MovieService(_movieRepositoryMock.Object);

            // Act
            var result = await movieService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_movies.Count, result.Count());
        }
    }
}

