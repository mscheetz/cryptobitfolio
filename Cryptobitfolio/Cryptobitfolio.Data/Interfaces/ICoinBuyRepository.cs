using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;

namespace Cryptobitfolio.Data.Interfaces
{
    public interface ICoinBuyRepository
    {
        Task<List<CoinBuy>> Get();

        Task<CoinBuy> Get(int Id);

        Task<CoinBuy> Add(CoinBuy entity);

        Task<List<CoinBuy>> AddAll(List<CoinBuy> entityList);

        Task<CoinBuy> Update(CoinBuy entity);

        Task<List<CoinBuy>> UpdateAll(List<CoinBuy> entityList);

        Task Delete(Coin entity);
    }
}