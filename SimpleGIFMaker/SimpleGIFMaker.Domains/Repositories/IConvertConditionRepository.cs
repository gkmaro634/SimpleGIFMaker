using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.Domains.Repositories
{
    public interface IConvertConditionRepository
    {
        Task<IConvertCondition?> GetConvertConditionAsync(int id);

        Task UpdateConvertConditionAsync(int id, IConvertCondition convertCondition);

        Task DeleteConvertConditionAsync(int id);

        Task AddConvertConditionAsync(IConvertCondition gifFile);
    }
}
