using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
