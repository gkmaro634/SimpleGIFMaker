using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGIFMaker.Domains.Repositories
{
    public interface IGifFileRepository
    {
        Task<IGifFile?> GetGifFileAsync(int id);

        Task UpdateGifFileAsync(int id, IGifFile gifFile);

        Task DeleteGifFileAsync(int id);

        Task AddGifFileAsync(IGifFile gifFile);
    }
}
