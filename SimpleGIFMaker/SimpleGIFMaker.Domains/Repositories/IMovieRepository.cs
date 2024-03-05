namespace SimpleGIFMaker.Domains.Repositories
{
    public interface IMovieRepository
    {
        Task<IMovie?> GetMovieAsync(int id);

        //Task UpdateMovieAsync(int id, Movie movie);

        //Task DeleteMovieAsync(int id);

        Task AddMovieAsync(IMovie movie);
    }
}
