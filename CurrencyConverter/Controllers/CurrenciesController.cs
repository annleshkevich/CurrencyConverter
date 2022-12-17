using CurrencyConverter.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    public class CurrenciesController : Controller
    {
        private ICurrencyService _currencyService;

        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public ActionResult Index(double byn, double usd, double eur, double rub, double uah, double pln, double cny, string graph)
        {
            
            if (graph != null) ViewBag.Image = graph;
            double entered = 0;
            int numberOne = 0;
            int numberTwo = 0;
            List<double> currencies = new() { byn, usd, eur, rub, uah, pln, cny };
            for (int i = 0; i < currencies.Count; i++)
            {
                if (currencies[i] != 0)
                {
                    entered = currencies[i];
                    numberOne = i;
                }

            }
            
            ViewBag.Byn = entered;
            ViewBag.Usd = double.Parse(_currencyService.Converter(numberOne, 1, entered));
            ViewBag.Eur = double.Parse(_currencyService.Converter(numberOne, 2, entered));
            ViewBag.Rub = double.Parse(_currencyService.Converter(numberOne, 3, entered));
            ViewBag.Uah = double.Parse(_currencyService.Converter(numberOne, 4, entered));
            ViewBag.Pln = double.Parse(_currencyService.Converter(numberOne, 5, entered));
            ViewBag.Cny = double.Parse(_currencyService.Converter(numberOne, 6, entered));
            



            _currencyService.ConverterFromBYN(entered, numberOne);
            return View("~/Pages/Index.cshtml");
        }

    }
}
