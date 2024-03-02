using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.Fake
{
    public class FakeMovieRepository : IMovieRepository
    {
        private readonly List<Movie> movies = new List<Movie>();

        public FakeMovieRepository()
        {
        }

        public Task AddMovieAsync(Movie movie)
        {
            movie.Id = 0;
            this.movies.Clear();
            this.movies.Add(movie);
            return Task.CompletedTask;
        }

        public Task<Movie?> GetMovieAsync(int id)
        {
            return Task.FromResult<Movie?>(this.movies.FirstOrDefault(m => m.Id == id));
        }
    }
}
