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
            gifFile.Id = 0;
            this.gifFiles.Clear();
            this.gifFiles.Add(gifFile);
            return Task.CompletedTask;
        }

        public Task DeleteGifFileAsync(int id)
        {
            var toDelete = this.gifFiles.FirstOrDefault(f => f.Id == id);
            if (toDelete is not null)
            {
                this.gifFiles.Remove(toDelete);
            }

            return Task.CompletedTask;
        }

        public Task<GifFile?> GetGifFileAsync(int id)
        {
            var toReturn = this.gifFiles.FirstOrDefault(f => f.Id == id);
            if (toReturn is not null)
            {
                return Task.FromResult<GifFile?>(toReturn);
            }

            return Task.FromResult<GifFile?>(null);
        }

        public Task UpdateGifFileAsync(int id, GifFile gifFile)
        {
            this.DeleteGifFileAsync(id);
            this.AddGifFileAsync(gifFile);

            return Task.CompletedTask;
        }
    }
}
