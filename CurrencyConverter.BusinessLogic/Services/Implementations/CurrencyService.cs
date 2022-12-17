using AutoMapper;
using CurrencyConverter.Common.DTOs;
using CurrencyConverter.Model.DatabaseModels;
using CurrencyConverter.Model;
using System.Data.Entity;
using CurrencyConverter.BusinessLogic.Services.Interfaces;
using AutoMapper.Execution;

namespace CurrencyConverter.BusinessLogic.Services.Implementations
{
    public class CurrencyService:ICurrencyService
    {
        public readonly CurrencyConverterContext _db;
        public readonly IMapper _mapper;
       
        public CurrencyService(CurrencyConverterContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public string ConverterFromBYN(double amount, int number)
        {
            return (amount * double.Parse(_db.ExchangeRates.ToList()[number].UnitAmount.ToString())
            / double.Parse(_db.ExchangeRates.ToList()[number].RateAmount.ToString())).ToString("0.0000");
        }
        public double ConverterInBYN(double amount, int number) 
        {
            return (double)(amount * (_db.ExchangeRates.ToList()[number].RateAmount) / _db.ExchangeRates.ToList()[number].UnitAmount);
        }
        public string Converter(int numberOne, int numberTwo, double amount) 
        {
            if (numberOne == 0 && numberTwo !=0)
            { return ConverterFromBYN(amount, numberTwo); }
            if (numberOne !=0&&numberTwo==0)
            { return ConverterInBYN(amount, numberOne).ToString(); }
            if (numberOne != 0 && numberTwo != 0)
            { return ConverterFromBYN(ConverterInBYN(amount, numberOne), numberTwo); }
            return null;
        }
        public string ConverterBYN(double amount, int number)
        {
            return _db.ExchangeRates.ToList()[number].RateAmount.ToString();
        }
        public IEnumerable<CurrencyDto> Get()
        {
            return _mapper.Map<List<CurrencyDto>>(_db.Currencies.AsNoTracking().ToList());
        }

        public CurrencyDto Get(int id)
        {
            return _mapper.Map<CurrencyDto>(_db.Currencies.FirstOrDefault(c => c.Id == id)!);
        }

        public bool Create(CurrencyDto currencyDto)
        {
            _db.Currencies.Add(_mapper.Map<Currency>(currencyDto));
            return Save();
        }
        public bool Update(CurrencyDto currencyDto)
        {
            _db.Update(currencyDto);
            return Save();
        }
        public bool Delete(int id)
        {
            var currency = _db.Currencies.FirstOrDefault(c => c.Id == id);
            if (currency == null)
                return false;
            _db.Currencies.Remove(currency);
            return Save();
        }
        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

      
    }
}
