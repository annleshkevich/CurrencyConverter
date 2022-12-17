using AutoMapper;
using CurrencyConverter.BusinessLogic.Services.Interfaces;
using CurrencyConverter.Common.DTOs;
using CurrencyConverter.Model;
using CurrencyConverter.Model.DatabaseModels;
using System.Data.Entity;

namespace CurrencyConverter.BusinessLogic.Services.Implementations
{
    public class ExchangeRateService: IExchangeRateService
    {
        private readonly CurrencyConverterContext _db;
        private readonly IMapper _mapper;

        public ExchangeRateService(CurrencyConverterContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IEnumerable<ExchangeRateDto> Get()
        {
            return _mapper.Map<List<ExchangeRateDto>>(_db.ExchangeRates.AsNoTracking().ToList());
        }

        public ExchangeRateDto Get(int id)
        {
            return _mapper.Map<ExchangeRateDto>(_db.ExchangeRates.FirstOrDefault(er => er.Id == id)!);
        }

        public bool Create(ExchangeRateDto exchangeRateDto)
        {
            _db.ExchangeRates.Add(_mapper.Map<ExchangeRate>(exchangeRateDto));
            return Save();
        }
        public bool Update(ExchangeRateDto exchangeRateDto)
        {
            _db.Update(exchangeRateDto);
            return Save();
        }
        public bool Delete(int id)
        {
            var exchangeRate = _db.ExchangeRates.FirstOrDefault(er => er.Id == id);
            if (exchangeRate == null)
                return false;
            _db.ExchangeRates.Remove(exchangeRate);
            return Save();
        }
        public bool Save() 
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
