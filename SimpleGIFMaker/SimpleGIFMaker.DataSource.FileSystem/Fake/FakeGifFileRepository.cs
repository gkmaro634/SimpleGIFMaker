using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.Fake
{
    public class FakeGifFileRepository : IGifFileRepository
    {
        private readonly List<IGifFile> gifFiles = new List<IGifFile>();

        public FakeGifFileRepository()
        {
        }

        public Task AddGifFileAsync(IGifFile gifFile)
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

        public Task<IGifFile?> GetGifFileAsync(int id)
        {
            var toReturn = this.gifFiles.FirstOrDefault(f => f.Id == id);
            if (toReturn is not null)
            {
                return Task.FromResult<IGifFile?>(toReturn);
            }

            return Task.FromResult<IGifFile?>(null);
        }

        public Task UpdateGifFileAsync(int id, IGifFile gifFile)
        {
            this.DeleteGifFileAsync(id);
            this.AddGifFileAsync(gifFile);

            return Task.CompletedTask;
        }
    }
}
