using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities;
using Cryptobitfolio.Business.Entities.Trade;

namespace Cryptobitfolio.Data.Interfaces
{
    public interface IExchangeUpdateRepository
    {
        Task<IEnumerable<ExchangeUpdate>> Get();

        Task<IEnumerable<ExchangeUpdate>> Get(Exchange exchange);

        Task<ExchangeUpdate> Add(ExchangeUpdate entity);

        Task<List<ExchangeUpdate>> AddAll(List<ExchangeUpdate> entityList);

        Task<ExchangeUpdate> Update(ExchangeUpdate entity);

        Task<List<ExchangeUpdate>> UpdateAll(List<ExchangeUpdate> entityList);

        Task<int> Delete(ExchangeUpdate entity);

        Task<int> DeleteAll();
    }
}