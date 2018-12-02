using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;

namespace Cryptobitfolio.Data.Interfaces
{
    public interface ICoinRepository
    {
        Task<List<Coin>> Get();

        Task<Coin> Get(int Id);

        Task<Coin> Add(Coin entity);

        Task<List<Coin>> AddAll(List<Coin> entityList);

        Task<Coin> Update(Coin entity);

        Task<List<Coin>> UpdateAll(List<Coin> entityList);

        Task<bool> Delete(Coin entity);

        Task<bool> DeleteAll();
    }
}