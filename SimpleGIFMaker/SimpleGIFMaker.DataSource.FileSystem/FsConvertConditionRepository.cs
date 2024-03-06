using SimpleGIFMaker.DataSource.FileSystem.Dto;
using SimpleGIFMaker.DataSource.FileSystem.Utils;
using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.FileSystem
{
    public class FsConvertConditionRepository : IConvertConditionRepository
    {
        private string jsonDirPath = string.Empty;

        public FsConvertConditionRepository()
        {
            this.jsonDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");
            if (Directory.Exists(jsonDirPath) == false)
            {
                Directory.CreateDirectory(jsonDirPath);
            }
        }

        public Task AddConvertConditionAsync(IConvertCondition convertCondition)
        {
            convertCondition.Id = 0;
            var dto = ConvertConditionDto.CreateFrom(convertCondition);

            var jsonFileName = $"condition_{convertCondition.Id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            JsonFile.Save(dto, jsonPath);
            return Task.CompletedTask;
        }

        public Task DeleteConvertConditionAsync(int id)
        {
            var jsonFileName = $"condition_{id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            if (File.Exists(jsonPath) == false)
            {
                return Task.CompletedTask;
            }

            File.Delete(jsonPath);
            return Task.CompletedTask;
        }

        public Task<IConvertCondition?> GetConvertConditionAsync(int id)
        {
            var jsonFileName = $"condition_{id}.json";
            var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
            var dto = JsonFile.Load<ConvertConditionDto>(jsonPath);
            if (dto == null)
            {
                return Task.FromResult<IConvertCondition?>(null);
            }

            var condition = dto.Create();
            return Task.FromResult<IConvertCondition?>(condition);
        }

        public Task UpdateConvertConditionAsync(int id, IConvertCondition convertCondition)
        {
            var toUpdate = GetConvertConditionAsync(id).GetAwaiter().GetResult();
            if (toUpdate is not null)
            {
                toUpdate.UpdateFrom(convertCondition);

                var dto = ConvertConditionDto.CreateFrom(convertCondition);

                var jsonFileName = $"condition_{convertCondition.Id}.json";
                var jsonPath = Path.Combine(this.jsonDirPath, jsonFileName);
                JsonFile.Save(dto, jsonPath);
            }
            return Task.CompletedTask;
        }
    }
}
