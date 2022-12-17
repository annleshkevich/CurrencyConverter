using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CurrencyConverter.Model
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<CurrencyConverterContext>
    {
        public CurrencyConverterContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CurrencyConverterContext>();

            // get config from appsettings.json file
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // get connection string from appsettings.json file
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new CurrencyConverterContext(optionsBuilder.Options);
        }
    }
}
