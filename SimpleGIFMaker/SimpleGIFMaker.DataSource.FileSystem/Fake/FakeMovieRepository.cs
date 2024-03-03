using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.Fake
{
    public class FakeMovieRepository : IMovieRepository
    {
        private readonly List<IMovie> movies = new List<IMovie>();

        public FakeMovieRepository()
        {
        }

        public Task AddMovieAsync(IMovie movie)
        {
            movie.Id = 0;
            this.movies.Clear();
            this.movies.Add(movie);
            return Task.CompletedTask;
        }

        public Task<IMovie?> GetMovieAsync(int id)
        {
            return Task.FromResult<IMovie?>(this.movies.FirstOrDefault(m => m.Id == id));
        }
    }
}
