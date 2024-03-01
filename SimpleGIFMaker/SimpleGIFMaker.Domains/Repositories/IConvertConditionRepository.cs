using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.Domains.Repositories
{
    public interface IConvertConditionRepository
    {
        Task<ConvertCondition> GetConvertConditionAsync(int id);

        Task UpdateConvertConditionAsync(int id, ConvertCondition convertCondition);

        Task DeleteConvertConditionAsync(int id);

        Task AddConvertConditionAsync(ConvertCondition gifFile);
    }
}
