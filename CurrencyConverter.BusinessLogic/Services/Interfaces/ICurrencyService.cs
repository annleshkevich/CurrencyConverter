using CurrencyConverter.Common.DTOs;

namespace CurrencyConverter.BusinessLogic.Services.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDto> Get();
        CurrencyDto Get(int id);
        public bool Create(CurrencyDto currencyDto);
        bool Update(CurrencyDto currencyDto);
        bool Delete(int id);
        string ConverterFromBYN(double entered, int number);
        bool Save();
        double ConverterInBYN(double entered, int number);
        string Converter(int numberOne, int numberTwo, double entered);
     
        string ConverterBYN(double entered, int v);
    }
}
