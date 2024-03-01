using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.Fake
{
    public class FakeGifFileRepository : IGifFileRepository
    {
        private readonly List<GifFile> gifFiles = new List<GifFile>();

        public FakeGifFileRepository()
        {
        }

        public Task AddGifFileAsync(GifFile gifFile)
        {
            this.gifFiles.Add(gifFile);
            return Task.CompletedTask;
        }

        public Task DeleteGifFileAsync(int _)
        {
            var toDelete = this.gifFiles.FirstOrDefault();
            if (toDelete is not null)
            {
                this.gifFiles.Remove(toDelete);
            }

            return Task.CompletedTask;
        }

        public Task<GifFile?> GetGifFileAsync(int _)
        {
            var toReturn = this.gifFiles.FirstOrDefault();
            if (toReturn is not null)
            {
                return Task.FromResult<GifFile?>(toReturn);
            }

            return Task.FromResult<GifFile?>(null);
        }

        public Task UpdateGifFileAsync(int _, GifFile gifFile)
        {
            var toUpdate = this.gifFiles.FirstOrDefault();
            if (toUpdate is not null)
            {
                // TODO
            }

            return Task.CompletedTask;
        }
    }
}
