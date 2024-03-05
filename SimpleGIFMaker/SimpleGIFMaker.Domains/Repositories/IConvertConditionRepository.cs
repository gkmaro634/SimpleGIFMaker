namespace SimpleGIFMaker.Domains.Repositories
{
    public interface IConvertConditionRepository
    {
        Task<IConvertCondition?> GetConvertConditionAsync(int id);

        Task UpdateConvertConditionAsync(int id, IConvertCondition convertCondition);

        Task DeleteConvertConditionAsync(int id);

        Task AddConvertConditionAsync(IConvertCondition convertCondition);
    }
}
