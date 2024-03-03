using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.Fake
{
    public class FakeConvertConditionRepository : IConvertConditionRepository
    {
        private readonly List<ConvertCondition> conditions = new List<ConvertCondition>();

        public FakeConvertConditionRepository()
        {
            this.conditions.Add(new ConvertCondition());
        }

        public Task AddConvertConditionAsync(ConvertCondition convertCondition)
        {
            convertCondition.Id = 0;
            this.conditions.Clear();
            this.conditions.Add(convertCondition);
            return Task.CompletedTask;
        }

        public Task DeleteConvertConditionAsync(int id)
        {
            var toDelete = this.conditions.FirstOrDefault(f => f.Id == id);
            if (toDelete is not null)
            {
                this.conditions.Remove(toDelete);
            }
            return Task.CompletedTask;
        }

        public Task<ConvertCondition?> GetConvertConditionAsync(int id)
        {
            return Task.FromResult<ConvertCondition?>(this.conditions.FirstOrDefault(f => f.Id == id));
        }

        public Task UpdateConvertConditionAsync(int id, ConvertCondition convertCondition)
        {
            var toUpdate = this.conditions.FirstOrDefault(f => f.Id == id);
            if (toUpdate is not null)
            {
                toUpdate.UpdateFrom(convertCondition);
            }

            return Task.CompletedTask;
        }
    }
}
