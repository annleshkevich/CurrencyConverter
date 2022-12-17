using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConverter.Model.DatabaseModels
{
    [Table("ExchangeRates")]
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }
        public DateTime Period { get; set; }
        public double RateAmount { get; set; }
        public double? UnitAmount { get; set; }
        [ForeignKey("CurrencyId")]
        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
    }
}
