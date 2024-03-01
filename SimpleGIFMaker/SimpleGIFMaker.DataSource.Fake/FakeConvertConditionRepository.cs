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
            this.conditions.Clear();
            this.conditions.Add(convertCondition);
            return Task.CompletedTask;
        }

        public Task DeleteConvertConditionAsync(int id)
        {
            this.conditions.Clear();
            return Task.CompletedTask;
        }

        public Task<ConvertCondition> GetConvertConditionAsync(int _)
        {
            return Task.FromResult(this.conditions[0]);
        }

        public Task UpdateConvertConditionAsync(int id, ConvertCondition convertCondition)
        {
            // TODO
            //var toUpdate = this.conditions[0];

            return Task.CompletedTask;
        }
    }
}
