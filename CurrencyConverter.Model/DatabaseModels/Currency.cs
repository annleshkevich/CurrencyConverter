using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConverter.Model.DatabaseModels
{
    [Table("Currencies")]
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public List<ExchangeRate> ExchangeRates { get; set; } = new();
      
    }
}
