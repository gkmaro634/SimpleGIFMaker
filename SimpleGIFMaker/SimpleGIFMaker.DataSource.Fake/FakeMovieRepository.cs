using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.Fake
{
    public class FakeMovieRepository : IMovieRepository
    {
        private readonly List<Movie> movies = new List<Movie>();

        public FakeMovieRepository()
        {
            this.movies.Add(new Movie());
        }

        public Task<Movie> GetMovieAsync(int _)
        {
            return Task.FromResult(this.movies[0]);
        }
    }
}
