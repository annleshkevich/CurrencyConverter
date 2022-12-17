using CurrencyConverter.Common.DTOs;

namespace CurrencyConverter.BusinessLogic.Services.Interfaces
{
    public interface IExchangeRateService
    {
        IEnumerable<ExchangeRateDto> Get();
        ExchangeRateDto Get(int id);
        bool Create(ExchangeRateDto currencyDto);
        bool Update(ExchangeRateDto currencyDto);
        bool Delete(int id);
    }
}
