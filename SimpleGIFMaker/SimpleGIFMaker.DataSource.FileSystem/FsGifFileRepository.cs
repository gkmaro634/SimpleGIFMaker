using SimpleGIFMaker.DataSource.FileSystem.Dto;
using SimpleGIFMaker.DataSource.FileSystem.Utils;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.FileSystem
{
    public class FsGifFileRepository : IGifFileRepository
    {
        private string jsonDirPath = string.Empty;

        public FsGifFileRepository()
        {
            this.jsonDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");
            if (Directory.Exists(jsonDirPath) == false)
            {
                Directory.CreateDirectory(jsonDirPath);
            }
        }

        public Task AddGifFileAsync(IGifFile gifFile)
        {
            gifFile.Id = 0;
            var dto = GifFileDto.CreateFrom(gifFile);

            // GIFファイルをタイムスタンプのフォルダに移動する
            var now = DateTime.Now;
            var timestamp = now.ToString("yyyyMMddHHmmss");
            var gifDirPath = Path.Combine(this.jsonDirPath, timestamp);
            if (Directory.Exists (gifDirPath) == false)
            {
                Directory.CreateDirectory(gifDirPath);
            }

            var fileName = Path.GetFileName(gifFile.Path);
            var toCopyFilePath = Path.Combine(gifDirPath, fileName);
            File.Copy(gifFile.Path, toCopyFilePath, true);
            if (File.Exists(toCopyFilePath))
            {
                File.Delete(gifFile.Path);
                dto.Path = toCopyFilePath;
            }

            var jsonFileName = $"gif_{gifFile.Id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            JsonFile.Save(dto, jsonPath);
            return Task.CompletedTask;
        }

        public async Task DeleteGifFileAsync(int id)
        {
            var jsonFileName = $"gif_{id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            if (File.Exists(jsonPath) == false)
            {
                await Task.CompletedTask;
            }

            File.Delete(jsonPath);
        }

        public Task<IGifFile?> GetGifFileAsync(int id)
        {
            var jsonFileName = $"gif_{id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            var dto = JsonFile.Load<GifFileDto>(jsonPath);
            if (dto == null)
            {
                return Task.FromResult<IGifFile?>(null);
            }

            var gif = dto.Create();
            return Task.FromResult<IGifFile?>(gif);
        }

        public Task UpdateGifFileAsync(int id, IGifFile gifFile)
        {
            this.DeleteGifFileAsync(id).GetAwaiter().GetResult();
            var dto = GifFileDto.CreateFrom(gifFile);

            var jsonFileName = $"gif_{id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            JsonFile.Save(dto, jsonPath);
            return Task.CompletedTask;
        }
    }
}
