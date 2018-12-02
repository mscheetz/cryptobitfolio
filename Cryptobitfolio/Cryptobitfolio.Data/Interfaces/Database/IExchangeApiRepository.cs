using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Trade;

namespace Cryptobitfolio.Data.Interfaces
{
    public interface IExchangeApiRepository
    {
        Task<List<ExchangeApi>> Get();

        Task<ExchangeApi> Get(int Id);

        Task<ExchangeApi> Get(string exchangeName);

        Task<ExchangeApi> Add(ExchangeApi entity);

        Task<List<ExchangeApi>> AddAll(List<ExchangeApi> entityList);

        Task<ExchangeApi> Update(ExchangeApi entity);

        Task<List<ExchangeApi>> UpdateAll(List<ExchangeApi> entityList);

        Task<bool> Delete(ExchangeApi entity);

        Task<bool> DeleteAll();
    }
}