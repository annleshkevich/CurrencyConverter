using CurrencyConverter.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    public class ExchangeRatesController:Controller
    {
        private readonly IExchangeRateService _exchangeRateService;
        public ExchangeRatesController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

       
        
    }
}
