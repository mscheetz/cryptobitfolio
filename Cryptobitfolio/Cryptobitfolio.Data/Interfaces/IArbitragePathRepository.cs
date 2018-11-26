using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;

namespace Cryptobitfolio.Data.Interfaces
{
    public interface IArbitragePathRepository
    {
        Task<List<ArbitragePath>> Get();

        Task<ArbitragePath> Get(int Id);

        Task<ArbitragePath> Add(ArbitragePath entity);

        Task<List<ArbitragePath>> AddAll(List<ArbitragePath> entityList);

        Task<ArbitragePath> Update(ArbitragePath entity);

        Task<List<ArbitragePath>> UpdateAll(List<ArbitragePath> entityList);

        Task Delete(ArbitragePath entity);
    }
}