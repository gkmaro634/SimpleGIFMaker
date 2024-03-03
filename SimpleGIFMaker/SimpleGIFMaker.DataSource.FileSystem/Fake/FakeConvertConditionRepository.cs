using SimpleGIFMaker.Domains;
using SimpleGIFMaker.Domains.Repositories;

namespace SimpleGIFMaker.DataSource.Fake
{
    public class FakeConvertConditionRepository : IConvertConditionRepository
    {
        private readonly List<IConvertCondition> conditions = new List<IConvertCondition>();

        public FakeConvertConditionRepository()
        {
            this.conditions.Add(new ConvertCondition());
        }

        public Task AddConvertConditionAsync(IConvertCondition convertCondition)
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

        public Task<IConvertCondition?> GetConvertConditionAsync(int id)
        {
            return Task.FromResult<IConvertCondition?>(this.conditions.FirstOrDefault(f => f.Id == id));
        }

        public Task UpdateConvertConditionAsync(int id, IConvertCondition convertCondition)
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
