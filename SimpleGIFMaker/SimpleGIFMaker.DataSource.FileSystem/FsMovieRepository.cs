using SimpleGIFMaker.DataSource.FileSystem.Dto;
using SimpleGIFMaker.DataSource.FileSystem.Utils;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.FileSystem
{
    public class FsMovieRepository : IMovieRepository
    {
        private string jsonDirPath = string.Empty;

        public FsMovieRepository()
        {
            this.jsonDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");
            if (Directory.Exists(jsonDirPath) == false)
            {
                Directory.CreateDirectory(jsonDirPath);
            }
        }

        public Task AddMovieAsync(IMovie movie)
        {
            movie.Id = 0;
            var dto = MovieDto.CreateFrom(movie);

            var jsonFileName = $"movie_{movie.Id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            JsonFile.Save(dto, jsonPath);
            return Task.CompletedTask;
        }

        public Task<IMovie?> GetMovieAsync(int id)
        {
            var jsonFileName = $"movie_{id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            var dto = JsonFile.Load<MovieDto>(jsonPath);
            if (dto == null)
            {
                return Task.FromResult<IMovie?>(null);
            }

            var movie = dto.Create();
            return Task.FromResult<IMovie?>(movie);
        }
    }
}
