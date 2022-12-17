using CurrencyConverter.Common.DTOs;
using CurrencyConverter.Model.DatabaseModels;
using AutoMapper;

namespace CurrencyConverter.Common.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<Currency, CurrencyDto>().ReverseMap();
            CreateMap<ExchangeRate, ExchangeRateDto>().ReverseMap();
        }
    }
}
