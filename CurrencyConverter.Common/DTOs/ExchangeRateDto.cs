namespace CurrencyConverter.Common.DTOs
{
    public class ExchangeRateDto
    {
        public int Id { get; set; }
        public DateTime Period { get; set; }
        public double RateAmount { get; set; }
        public double UnitAmount { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
    }
}
